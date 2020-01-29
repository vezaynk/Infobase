using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infobase.Models;
using Models.Metadata;
using System.Reflection;

namespace Infobase.Controllers
{
    public class OpenController : Controller
    {
        private readonly Dictionary<string, SortedDictionary<Type, ICollection<dynamic>>> contexts;
        private I18nTransformer _i18nTransformer;
        private string Language => _i18nTransformer.Culture;

        public OpenController(Dictionary<string, SortedDictionary<Type, ICollection<dynamic>>> contextLookup, I18nTransformer i18nTransformer)
        {
            contexts = contextLookup;
            _i18nTransformer = i18nTransformer;
        }
        [NonAction]
        public string LookupInvariant(string datatool) => _i18nTransformer.LookupInvariant(datatool);
        [NonAction]
        private SortedDictionary<Type, ICollection<dynamic>> GetContext(string datatool)
        {
            return contexts[LookupInvariant(datatool).ToUpper() + "Context"];
        }
        public IActionResult Index(string page, string datatool, int index = 1, bool api = false, int? id = null)
        {
            switch (LookupInvariant(page).ToLower()) {
                case "index": return IndexPage(datatool);
                case "data-tool": return Datatool(datatool, index, api);
                case "indicator-details": return Details(datatool, id??0);
                case "publications": return Publications(datatool);
            }

            return List();
            
        }

        public IActionResult IndexPage(string datatool) {
            var context = GetContext(datatool);
            var topLevelType = context.Keys.First();
            // var topLevelEntities = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new[] { topLevelType }).Invoke(typeof(Enumerable), new[] {  });
            // Load top-level
            return View($"{LookupInvariant(datatool)}/index", context[topLevelType]);
        }

        public IActionResult List() {
            foreach (var item in contexts.Keys.Select(key => key.Substring(0, key.Length - "Context".Length)))
            {
                Console.WriteLine(item);
            }
            
            return View("List", contexts.Keys.Select(key => key.Substring(0, key.Length - "Context".Length)));
        }

        [ActionName("data-tool")]
        public IActionResult Datatool(string datatool, int index = 1, bool api = false)
        {
            var context = GetContext(datatool);
            var dataBreakdownLevelType = context.Keys.SkipLast(1).Last();
            var disaggregatorLevelType = context.Keys.Last();
            var selectedBreakdown = context[dataBreakdownLevelType]
                .Where(s => (int)s.Index >= index)
                .First();

            var children = Enumerable.Cast<dynamic>((IEnumerable)Metadata
                                .FindPropertyOnType<ChildrenAttribute>(dataBreakdownLevelType)
                                .GetValue((object)selectedBreakdown)).ToList();

            var measureDescription = Metadata
                            .FindTextPropertiesOnTree((object)selectedBreakdown, Language, TextAppearance.MeasureDescription)
                            .Where(mp => !string.IsNullOrEmpty(mp.Name) && !string.IsNullOrEmpty((string)mp.Value))
                            .ToList();

            var notes = Metadata
                            .FindTextPropertiesOnTree((object)selectedBreakdown, Language, TextAppearance.Notes)
                            .Where(mp => !string.IsNullOrEmpty(mp.Name) && !string.IsNullOrEmpty((string)mp.Value))
                            .ToList();

            PropertyInfo GetProperty<T>() where T : Attribute
            {
                return Metadata.FindPropertyOnType<T>(disaggregatorLevelType);
            }
            var averageValueProperty = GetProperty<PointAverageAttribute>();
            var lowerValueProperty = GetProperty<PointLowerAttribute>();
            var upperValueProperty = GetProperty<PointUpperAttribute>();
            var cVValueProperty = GetProperty<CVValueAttribute>();
            var cVInterpretationProperty = GetProperty<CVInterpretationAttribute>();
            var typeProperty = GetProperty<TypeAttribute>();
            var xAxis = (string)Metadata.FindTextPropertiesOnNode<ShowOnAttribute>((object)selectedBreakdown, Language, TextAppearance.Filter).FirstOrDefault()?.Value;
            var yAxis = (string)Metadata.FindTextPropertiesOnTree<UnitLongAttribute>((object)selectedBreakdown, Language).FirstOrDefault()?.Value;
            var unit = (string)Metadata.FindTextPropertiesOnTree<UnitShortAttribute>((object)selectedBreakdown, Language).FirstOrDefault()?.Value;
            var title = (string)Metadata.FindTextPropertiesOnTree<TitleAttribute>((object)selectedBreakdown, Language).FirstOrDefault()?.Value;

            ChartData chart = chart = new ChartData
            {
                XAxis = xAxis,
                YAxis = yAxis,
                Unit = unit,
                Title = title,
                Points = children.Select((child) => new Point
                {
                    Label = (string)Metadata.FindTextPropertiesOnTree<DataLabelChartAttribute>((object)child, Language).FirstOrDefault()?.Value,
                    Text = (string)Metadata.FindTextPropertiesOnTree<DataLabelTableAttribute>((object)child, Language).FirstOrDefault()?.Value,
                    CVInterpretation = (int)cVInterpretationProperty.GetValue(child),
                    CVValue = (double?)cVValueProperty.GetValue(child),
                    Value = (double?)averageValueProperty.GetValue(child),
                    ValueLower = (double?)lowerValueProperty.GetValue(child),
                    ValueUpper = (double?)upperValueProperty.GetValue(child),
                    Type = 0//(int)typeProperty.GetValue(child)
                }).ToList(),
                WarningCV = null,
                SuppressCV = null,
                DescriptionTable = measureDescription.ToDictionary(mp => mp.Name, mp => (string)mp.Value),
                Notes = notes.ToDictionary(mp => mp.Name, mp => (string)mp.Value)
            };

            var cpm = new ChartPageModel(datatool, Language, chart);

            var dropdowns = context.SkipLast(1).Select(pair =>
            {
                Type type = pair.Key;

                ICollection<dynamic> entities = pair.Value;

                var parentOfCurrentType = Metadata.GetAllParentNodes((object)selectedBreakdown).First(p => p.GetType() == type);

                var dropdownItems = entities
                    .Where(entity => Metadata.FindPropertyOnType<DefaultChildAttribute>(type).GetValue(entity) != null)
                    .Where(entity =>
                    {
                        var parentOfEntity = Metadata.GetParentOf(entity);
                        // Top-level selectors should always be displayed
                        if (parentOfEntity == null) return true;

                        // Check for common ancestor
                        return Metadata.GetParentOf(parentOfCurrentType) == parentOfEntity;
                    })
                    .Select(entity =>
                    {
                        var currentLevel = entity;

                        var entityText = (string)Metadata.FindTextPropertiesOnNode<ShowOnAttribute>((object)entity, Language, TextAppearance.Filter).First().Value;

                        while (currentLevel.GetType() != dataBreakdownLevelType)
                        {
                            currentLevel = Metadata.FindPropertyOnType<DefaultChildAttribute>(((object)currentLevel).GetType()).GetValue(currentLevel);
                            if (currentLevel == null) throw new Exception("Default tree structure is broken");
                        }
                        var entityIndex = currentLevel.Index;

                        return new { Text = entityText, Value = entityIndex, Entity = entity };

                    }).ToList();

                var filterName = Metadata.FindTextPropertiesOnNode<ShowOnAttribute>((object)dropdownItems.First().Entity, Language, TextAppearance.Filter).First().Name;
                return new DropdownMenuModel(
                        filterName,
                        dropdownItems.Select(di => new DropdownItem { Text = di.Text, Value = di.Value }),
                        dropdownItems.First(di => di.Entity == parentOfCurrentType).Value
                    );
            });

            foreach (var dropdown in dropdowns)
            {
                cpm.Filters.Add(dropdown);
            };

            if (Request.Method == "GET" && !api)
                return View("data-tool", cpm);
            else
                return Json(cpm);

        }

        [ActionName("indicator-details")]
        public IActionResult Details(string datatool, int id)
        {
            var context = GetContext(datatool);
            var measure = context[context.Keys.SkipLast(2).Last()].FirstOrDefault(m => m.Index == id);
            if (measure == null)
            {
                return NotFound();
            }

            return View("indicator-details", new DescriptionPageModel(datatool, Language, measure));
        }

        [ActionName("publications")]
        public IActionResult Publications(string datatool)
        {
            return View($"{LookupInvariant(datatool)}/publications");
        }
    }
}
