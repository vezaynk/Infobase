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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Infobase.Controllers
{

    public class PASS2Controller : Controller
    {
        private readonly SortedDictionary<Type, IEnumerable> _context;

        public PASS2Controller(Dictionary<string, SortedDictionary<Type, IEnumerable>> contextLookup)
        {
            _context = contextLookup["PASS2Context"];
        }
        public async Task<IActionResult> Index(string language)
        {
            var topLevelType = _context.Keys.First();
            var topLevelEntities = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(new []{ topLevelType }).Invoke(typeof(Enumerable), new[]{ _context[topLevelType] });
            foreach (var tle in (IEnumerable)topLevelEntities) {
                Console.WriteLine(tle);
            }
            // Load top-level
            return View(topLevelEntities);
        }

        [ActionName("data-tool")]
        public async Task<IActionResult> Datatool(string language, int index = 1, bool api = false)
        {
            var strata = Enumerable.Cast<ColDataBreakdowns>(_context[typeof(ColDataBreakdowns)])
                .Where(s => s.Index >= index && s.ColSpecificMeasure.Include)
                .OrderBy(s => s.Index)
                .First();


            if (strata == null)
            {
                return NotFound();
            }

            ChartData chart = chart = new ChartData
            {
                XAxis = "",
                YAxis = "",
                Unit = "",
                Points = strata.ColStrata.Select(s => new ChartData.Point {
                    Label = s.ColStrataNameEn,
                    Text = "",
                    CVInterpretation = s.CVInterpretation,
                    CVValue = s.CVValue,
                    Value = s.ValueAverage,
                    ValueLower = s.ValueLower,
                    ValueUpper = s.ValueUpper
                }),
                WarningCV = 0,
                SuppressCV = 0,
                Title = ""
            };

            var cpm = new ChartPageModel(language, chart);

            // top level requires a new query
            var activities = Enumerable.Cast<ColActivity>(_context[typeof(ColActivity)])
                                     .Where(ac => ac.DefaultColIndicatorGroupId != null)
                                     .OrderBy(x => x.Index)
                                     .Select(ac => new DropdownItem
                                     {
                                         Value = ac.DefaultColIndicatorGroup.DefaultColLifeCourse.DefaultColIndicator.DefaultColSpecificMeasure.DefaultColDataBreakdowns.Index,
                                         Text = ac.ColActivityNameEn
                                     });

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
