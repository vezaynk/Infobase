using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Infobase.Models;
using Infobase.Models.PASS;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;



namespace Infobase.Controllers
{

    public class PASSController : Controller
    {
        private readonly PASSContext _context;

        public PASSController(PASSContext context)
        {
            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context = context;
        }

        public async Task<IActionResult> Index(string language)
        {
            var activities = _context.Activity
                                        // Include Measure names
                                        .Include(a => a.DefaultIndicatorGroup)
                                        .Include(a => a.IndicatorGroups)
                                                .ThenInclude(ig => ig.LifeCourses)
                                                .ThenInclude(lc => lc.Indicators)
                                                    .ThenInclude(i => i.Measures)

                                        // Include latest data by including points. Will filter later.
                                        .Include(a => a.IndicatorGroups)
                                                .ThenInclude(ig => ig.LifeCourses)
                                                .ThenInclude(lc => lc.Indicators)
                                                .ThenInclude(i => i.Measures)
                                                    .ThenInclude(m => m.DefaultStrata.Points);


            // Razor handles the rest
            return View(await activities.ToListAsync());
        }

        [ActionName("data-tool")] 
        public async Task<IActionResult> Datatool(string language, int index=1, bool api = false)
        {
            var strata = await _context.Strata

/* idc anymore just lazyload it
                // Measure
                .Include(s => s.Measure)

                // Points
                .Include(s => s.Points)

                // Indicator
                .Include(s => s.Measure.Indicator)

                // Life Course
                .Include(s => s.Measure.Indicator.LifeCourse)

                // Indicator Group
                .Include(s => s.Measure.Indicator.LifeCourse.IndicatorGroup)

                // Activity
                .Include(s => s.Measure.Indicator.LifeCourse.IndicatorGroup.Activity)

                // Include associated stratas
                .Include(s => s.Measure)
                    .ThenInclude(p => p.Stratas)

                // Include associated measures
                .Include(s => s.Measure.Indicator)
                    .ThenInclude(p => p.Measures)


                // Include associated indicators
                .Include(s => s.Measure.Indicator.LifeCourse)
                    .ThenInclude(p => p.Indicators)

                // Include associated life courses
                .Include(s => s.Measure.Indicator.LifeCourse.IndicatorGroup)
                    .ThenInclude(p => p.LifeCourses)


                // Include associated indicator groups
                .Include(s => s.Measure.Indicator.LifeCourse.IndicatorGroup.Activity)
                    .ThenInclude(p => p.IndicatorGroups)
                .Include(s => s.Measure.Indicator.LifeCourse.IndicatorGroup.Activity.IndicatorGroups)
                .Include(s => s.Measure.Indicator.LifeCourse.IndicatorGroup.LifeCourses)
                .Include(s => s.Measure.Indicator.LifeCourse.Indicators)
                .Include(s => s.Measure.Indicator.Measures) */
                .FirstOrDefaultAsync(m => m.Index == index);


            if (strata == null)
            {
                return NotFound();
            }

            ChartData chart = chart = new ChartData
                {
                    XAxis = strata.StrataName(language),
                    YAxis = strata.Measure.MeasureUnitLong(language),
                    Unit = strata.Measure.MeasureUnitShort(language),
                    Source = strata.StrataSource(language),
                    // Both stratas AND measure contain populations. They must be merged.
                    // Population = new Translatable(strata.Measure.MeasurePopulation.Union(strata.StrataPopulation).ToDictionary(p => p.Key, p => p.Value)),
                    Notes = strata.StrataNotes(language),
                    Remarks = strata.Measure.MeasureAdditionalRemarks(language),
                    Definition = strata.Measure.MeasureDefinition(language),
                    Method = strata.Measure.MeasureMethod(language),
                    DataAvailable = strata.Measure.MeasureDataAvailable(language),
                    Points = strata.Points.OrderBy(p => p.Index).Select(p => new ChartData.Point
                    {
                        CVInterpretation = p.CVInterpretation,
                        CVValue = p.CVValue,
                        Value = p.ValueAverage,
                        ValueUpper = p.ValueUpper,
                        ValueLower = p.ValueLower,
                        Label = p.PointLabel(language),
                        Text = p.PointText(language),
                        Type = p.Type
                    }),
                    WarningCV = strata.Measure.CVWarnAt,
                    SuppressCV = strata.Measure.CVSuppressAt,
                    MeasureName = strata.Measure.MeasureNameDataTool(language),
                    Title = strata.Measure.MeasureNameIndex(language) + ", " + strata.StrataPopulationTitleFragment(language)
                };

            var cpm = new ChartPageModel(language, chart);

            // top level requires a new query
            var activities = _context.Activity
                                     .Where(ac => ac.DefaultIndicatorGroupId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(ac => new DropdownItem
                                     {
                                         Value = ac.DefaultIndicatorGroup.DefaultLifeCourse.DefaultIndicator.DefaultMeasure.DefaultStrata.Index,
                                         Text = ac.ActivityName(language)
                                     });

            cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Activité" : "Activity", activities, strata.Index));

            var indicatorGroups = strata.Measure.Indicator.LifeCourse.IndicatorGroup.Activity.IndicatorGroups
                                     .Where(ig => ig.DefaultLifeCourseId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(ig => new DropdownItem
                                     {
                                         Value = ig.DefaultLifeCourse.DefaultIndicator.DefaultMeasure.DefaultStrata.Index,
                                         Text = ig.IndicatorGroupName(language)
                                     });

            cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Groupe d'indicateur" : "Indicator Group", indicatorGroups, strata.Index));

            var lifeCourses = strata.Measure.Indicator.LifeCourse.IndicatorGroup.LifeCourses
                                     .Where(lc => lc.DefaultIndicatorId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(lc => new DropdownItem
                                     {
                                         Value = lc.DefaultIndicator.DefaultMeasure.DefaultStrata.Index,
                                         Text = lc.LifeCourseName(language)
                                     });

            cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Cours de la vie" : "Life Course", lifeCourses, strata.Index));

            var indicators = strata.Measure.Indicator.LifeCourse.Indicators
                                     .Where(i => i.DefaultMeasureId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(i => new DropdownItem
                                     {
                                         Value = i.DefaultMeasure.DefaultStrata.Index,
                                         Text = i.IndicatorName(language)
                                     });

            cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Indicateurs" : "Indicators", indicators, strata.Index));

            var measures = strata.Measure.Indicator.Measures
                                     .Where(m => m.DefaultStrataId != null && m.Included)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(m => new DropdownItem
                                     {
                                         Value = m.DefaultStrata.Index,
                                         Text = m.MeasureNameIndex(language)
                                     });

            cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Mesures" : "Measures", measures, strata.Index));

            var stratas = strata.Measure.Stratas
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(s => new DropdownItem
                                     {
                                         Value = s.Index,
                                         Text = s.StrataName(language)
                                     });

            cpm.filters.Add(new DropdownMenuModel(language == "fr-ca" ? "Répartition des données" : "Data Breakdowns", stratas, strata.Index));


            if (Request.Method == "GET" && !api)
                return View(cpm);
            else
                return Json(cpm);

        }

        [ActionName("indicator-details")] 
        public async Task<IActionResult> Details(string language, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measure = await _context.Measure.FirstOrDefaultAsync(m => m.Index == id);
            if (measure == null)
            {
                return NotFound();
            }

            return View(measure);
        }
    }
}
