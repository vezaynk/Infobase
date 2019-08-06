using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infobase.Models;
using Models.Contexts.PASS;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Infobase.Controllers
{

    public class PASSController : Controller
    {
        private readonly SortedDictionary<Type, IEnumerable> _context;

        public PASSController(Dictionary<string, SortedDictionary<Type, IEnumerable>> contextLookup)
        {
            _context = contextLookup["PASSContext"];
        }

        public async Task<IActionResult> Index(string language)
        {
            var topLevelType = _context.Keys.First();
            var topLevelEntities = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new []{ topLevelType }).Invoke(typeof(Enumerable), new[]{ _context[topLevelType] });
            // Load top-level
            return View(topLevelEntities);
        }

        // [ActionName("data-tool")]
        // public async Task<IActionResult> Datatool(string language, int index = 1, bool api = false)
        // {
        //     var strata = await _context.Strata
        //         .Where(s => s.Index >= index && s.Measure.Included)
        //         .OrderBy(s => s.Index)
        //         .FirstOrDefaultAsync();


        //     if (strata == null)
        //     {
        //         return NotFound();
        //     }

        //     ChartData chart = new ChartData
        //     {
        //         XAxis = strata.StrataName(language),
        //         YAxis = strata.Measure.MeasureUnitLong(language),
        //         Unit = strata.Measure.MeasureUnitShort(language),
        //         Source = strata.StrataSource(language),
        //         Population = strata.Measure.MeasurePopulationGroup(language),
        //         Notes = strata.StrataNotes(language),
        //         Remarks = strata.Measure.MeasureAdditionalRemarks(language),
        //         Definition = strata.Measure.MeasureDefinition(language),
        //         Method = strata.Measure.MeasureMethod(language),
        //         DataAvailable = strata.Measure.MeasureDataAvailable(language),
        //         Points = strata.Points.OrderBy(p => p.Index).Select(p => new ChartData.Point
        //         {
        //             CVInterpretation = p.CVInterpretation,
        //             CVValue = p.CVValue,
        //             Value = p.ValueAverage,
        //             ValueUpper = p.ValueUpper,
        //             ValueLower = p.ValueLower,
        //             Label = p.PointLabel(language),
        //             Text = p.PointText(language),
        //             Type = p.Type
        //         }),
        //         WarningCV = strata.Measure.CVWarnAt,
        //         SuppressCV = strata.Measure.CVSuppressAt,
        //         MeasureName = strata.Measure.MeasureNameDataTool(language),
        //         Title = strata.Measure.MeasureNameIndex(language) + (strata.StrataPopulationTitleFragment(language) != null ? ", " + strata.StrataPopulationTitleFragment(language) : "")
        //     };

        //     var cpm = new ChartPageModel(language, chart);

        //     // top level requires a new query
        //     var activities = _context.Activity
        //                              .Where(ac => ac.DefaultIndicatorGroupId != null)
        //                              .OrderBy(x => x.Index)
        //                              .Select(ac => new DropdownItem
        //                              {
        //                                  Value = ac.DefaultIndicatorGroup.DefaultLifeCourse.DefaultIndicator.DefaultMeasure.DefaultStrata.Index,
        //                                  Text = ac.ActivityName(language)
        //                              });

        //     cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Activité" : "Activity", activities, strata.Index));

        //     var indicatorGroups = _context.IndicatorGroup
        //                              .Where(ig => ig.DefaultLifeCourseId != null && ig.ActivityId == strata.Measure.Indicator.LifeCourse.IndicatorGroup.ActivityId)
        //                              .OrderBy(x => x.Index)
        //                              .Select(ig => new DropdownItem
        //                              {
        //                                  Value = ig.DefaultLifeCourse.DefaultIndicator.DefaultMeasure.DefaultStrata.Index,
        //                                  Text = ig.IndicatorGroupName(language)
        //                              });

        //     cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Groupe d'indicateur" : "Indicator Group", indicatorGroups, strata.Index));

        //     var lifeCourses = _context.LifeCourse
        //                              .Where(lc => lc.DefaultIndicator != null && lc.IndicatorGroupId == strata.Measure.Indicator.LifeCourse.IndicatorGroupId)
        //                              .OrderBy(x => x.Index)
        //                              .Select(lc => new DropdownItem
        //                              {
        //                                  Value = lc.DefaultIndicator.DefaultMeasure.DefaultStrata.Index,
        //                                  Text = lc.LifeCourseName(language)
        //                              });

        //     cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Cours de la vie" : "Life Course", lifeCourses, strata.Index));

        //     var indicators = _context.Indicator
        //                              .Where(i => i.DefaultMeasureId != null && i.LifeCourseId == strata.Measure.Indicator.LifeCourseId)
        //                              .OrderBy(x => x.Index)
        //                              .Select(i => new DropdownItem
        //                              {
        //                                  Value = i.DefaultMeasure.DefaultStrata.Index,
        //                                  Text = i.IndicatorName(language)
        //                              });

        //     cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Indicateurs" : "Indicators", indicators, strata.Index));

        //     var measures = _context.Measure
        //                              .Where(i => i.DefaultStrataId != null && i.IndicatorId == strata.Measure.IndicatorId)
        //                              .OrderBy(x => x.DefaultStrata.Index)
        //                              .Select(m => new DropdownItem
        //                              {
        //                                  Value = m.Index,
        //                                  Text = m.MeasureNameIndex(language)
        //                              });

        //     cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Mesures" : "Measures", measures, strata.Index));

        //     var stratas = _context.Strata
        //                              .Where(i => i.MeasureId == strata.MeasureId)
        //                              .OrderBy(x => x.Index)
        //                              .Select(s => new DropdownItem
        //                              {
        //                                  Value = s.Index,
        //                                  Text = s.StrataName(language)
        //                              });

        //     cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Répartition des données" : "Data Breakdowns", stratas, strata.Index));


        //     if (Request.Method == "GET" && !api)
        //         return View(cpm);
        //     else
        //         return Json(cpm);

        // }

        [ActionName("indicator-details")]
        public async Task<IActionResult> Details(string language, int id)
        {
            var measure = Enumerable.Cast<Measure>(_context[typeof(Measure)]).FirstOrDefault(m => m.Index == id);
            if (measure == null)
            {
                return NotFound();
            }

            return View(measure);
        }

        [ActionName("publications")]
        public IActionResult Publications(string language, int id)
        {
            return View();
        }
    }
}
