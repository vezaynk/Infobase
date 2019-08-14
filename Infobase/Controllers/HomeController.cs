using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infobase.Models;
using Models.Contexts.PASS2;
using Models.Metadata;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Infobase.Controllers
{

    public class PASS2Controller : Controller
    {
        private readonly SortedDictionary<Type, IEnumerable<dynamic>> _context;

        public PASS2Controller(Dictionary<string, SortedDictionary<Type, IEnumerable<dynamic>>> contextLookup)
        {
            _context = contextLookup["CMSIFContext"];
        }
        public async Task<IActionResult> Index(string language)
        {
            var topLevelType = _context.Keys.First();
            var topLevelEntities = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new[] { topLevelType }).Invoke(typeof(Enumerable), new[] { _context[topLevelType] });

            // Load top-level
            return View(topLevelEntities);
        }

        [ActionName("data-tool")]
        public async Task<IActionResult> Datatool(string language, int index = 1, bool api = false)
        {
            var dataBreakdownLevelType = _context.Keys.SkipLast(1).Last();
            
            var selectedBreakdown = _context[dataBreakdownLevelType]
                .Where(s => (int)s.Index >= index)
                .First();

            ChartData chart = chart = new ChartData
            {
                XAxis = "",
                YAxis = "",
                Unit = "",
                Points = ChildrenAttribute.GetChildrenOf((object)selectedBreakdown).Select((child) => new Point {
                    Label = DataLabelChartAttribute.GetDataLabelChart(child, "en-ca"),
                    Text = DataLabelTableAttribute.GetDataLabelTable(child, "en-ca"),
                    CVInterpretation = CVInterpretationAttribute.GetCVInterpretation(child),
                    CVValue = CVValueAttribute.GetCVValue(child),
                    Value = PointAverageAttribute.GetPointAverage(child),
                    ValueLower = PointLowerAttribute.GetPointLower(child),
                    ValueUpper = PointUpperAttribute.GetPointUpper(child),
                    Type = 0
                }),
                WarningCV = 0,
                SuppressCV = 0,
                DescriptionTable = new Dictionary<string, string> {
                    {"Test 1 Header", "Test 1 Body"}
                },
                Notes = new Dictionary<string, string> {
                    {"Test 1 Header", "Test 1 Body"}
                }
            };

            var cpm = new ChartPageModel(language, chart);

            var dropdowns = _context.SkipLast(1).Select(pair =>
            {
                Type type = pair.Key;
                var textProperty = TextAttribute.GetTextProperty(type, "en-ca", TextAppearance.Filter);

                IEnumerable<dynamic> entities = pair.Value;

                // Walk up the tree until the type is the same
                var parentOfCurrentType = selectedBreakdown;
                while (parentOfCurrentType.GetType() != type)
                {
                    parentOfCurrentType = ParentAttribute.GetParentOf(parentOfCurrentType);
                }

                var dropdownItems = entities
                    .Where(entity => DefaultChildAttribute.GetDefaultChildOf(entity) != null)
                    .Where(entity =>
                    {
                        var parentOfEntity = ParentAttribute.GetParentOf(entity);
                        // Top-level selectors should always be displayed
                        if (parentOfEntity == null) return true;

                        // Check for common ancestor
                        return ParentAttribute.GetParentOf(parentOfCurrentType) == parentOfEntity;
                    })
                    .Select(entity =>
                    {
                        var currentLevel = entity;

                        var entityText = (string)textProperty.GetValue(entity);

                        while (currentLevel.GetType() != dataBreakdownLevelType)
                        {
                            currentLevel = DefaultChildAttribute.GetDefaultChildOf(currentLevel);
                            if (currentLevel == null) throw new Exception("Default tree structure is broken");
                        }
                        var entityIndex = currentLevel.Index;

                        return new { Text = entityText, Value = entityIndex, Entity = entity };

                    });

                return new DropdownMenuModel(
                        textProperty.GetCustomAttribute<TextAttribute>().Name,
                        dropdownItems.Select(di => new DropdownItem { Text = di.Text, Value = di.Value }),
                        dropdownItems.First(di => di.Entity == parentOfCurrentType).Value
                    );
            });

            foreach (var dropdown in dropdowns)
            {
                cpm.filters.Add(dropdown);
            };

            if (Request.Method == "GET" && !api)
                return View(cpm);
            else
                return Json(cpm);

        }

        // [ActionName("indicator-details")]
        // public async Task<IActionResult> Details(string language, int id)
        // {
        //     var measure = Enumerable.Cast<Measure>(_context[typeof(Measure)]).FirstOrDefault(m => m.Index == id);
        //     if (measure == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(measure);
        // }

        [ActionName("publications")]
        public IActionResult Publications(string language, int id)
        {
            return View();
        }
    }
}
