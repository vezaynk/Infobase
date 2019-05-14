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

namespace Infobase.Controllers
{

    public class StrataController : Controller
    {
        private readonly PASSContext _context;

        public StrataController(PASSContext context)
        {
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _context = context;
        }

        // GET: Strata
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

        // GET: Strata/Details/
        public async Task<IActionResult> Datatool(string language, int? measureId, int? indicatorId, int? lifeCourseId, int? indicatorGroupId, int? activityId, int strataId = -1, bool api = false)
        {
            /* Figure out a strataId to use. Not terribly efficient. A better solution is needed. */

            if (activityId != null)
                indicatorGroupId = _context.Activity.FirstOrDefault(m => m.ActivityId == activityId && m.DefaultIndicatorGroupId != null).DefaultIndicatorGroupId;

            if (indicatorGroupId != null)
                lifeCourseId = _context.IndicatorGroup.FirstOrDefault(m => m.IndicatorGroupId == indicatorGroupId && m.DefaultLifeCourseId != null).DefaultLifeCourseId;

            if (lifeCourseId != null)
                indicatorId = _context.LifeCourse.FirstOrDefault(m => m.LifeCourseId == lifeCourseId && m.DefaultIndicatorId != null).DefaultIndicatorId;

            if (indicatorId != null)
                measureId = _context.Indicator.FirstOrDefault(m => m.IndicatorId == indicatorId && m.DefaultMeasureId != null).DefaultMeasureId;

            if (measureId != null)
                strataId = _context.Measure.FirstOrDefault(m => m.MeasureId == measureId && m.DefaultStrataId != null).DefaultStrataId ?? -1;
            

            var strata = await _context.Strata
            
            
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
                .OrderBy(m => m.StrataId == strataId ? 0 : 1)
                .ThenBy(m => m.Index)
                .FirstOrDefaultAsync();


            if (strata == null)
            {
                return NotFound();
            }

            var chart = new ChartData {
                XAxis = strata.StrataNameEn,
                YAxis = strata.Measure.MeasureUnitLongEn,
                Unit = strata.Measure.MeasureUnitShortEn,
                Source = strata.StrataSourceEn,
                // Both stratas AND measure contain populations. They must be merged.
                // Population = new Translatable(strata.Measure.MeasurePopulation.Union(strata.StrataPopulation).ToDictionary(p => p.Key, p => p.Value)),
                Notes = strata.StrataNotesEn,
                Remarks = strata.Measure.MeasureAdditionalRemarksEn,
                Definition = strata.Measure.MeasureDefinitionEn,
                Method = strata.Measure.MeasureMethodEn,
                DataAvailable = strata.Measure.MeasureDataAvailableEn,
                Points = strata.Points.OrderBy(p => p.Index).Select(p => new ChartData.Point {
                    CVInterpretation = p.CVInterpretation,
                    CVValue = p.CVValue,
                    Value = p.ValueAverage,
                    ValueUpper = p.ValueUpper,
                    ValueLower = p.ValueLower,
                    Label = p.PointLabelEn,
                    Type = p.Type
                }),
                WarningCV = strata.Measure.CVWarnAt,
                SuppressCV = strata.Measure.CVSuppressAt,
                MeasureName = strata.Measure.MeasureNameDataToolEn
            };


            var cpm = new ChartPageModel(language, chart);

            // top level requires a new query
            var activities = _context.Activity
                                     .Where(ac => ac.DefaultIndicatorGroupId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(ac => new DropdownItem
                                            {
                                                Value = ac.ActivityId,
                                                Text = ac.ActivityNameEn
                                            });
            
			Console.Write("String",language);
			
            cpm.filters.Add(new DropdownMenuModel(language=="fr-ca"?"Activité":"Activity", "activityId", activities, strata.Measure.Indicator.LifeCourse.IndicatorGroup.ActivityId));

            var indicatorGroups = strata.Measure.Indicator.LifeCourse.IndicatorGroup.Activity.IndicatorGroups
                                     .Where(ig => ig.DefaultLifeCourseId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(ig => new DropdownItem
                                            {
                                                Value = ig.IndicatorGroupId,
                                                Text = ig.IndicatorGroupNameEn
                                            });

            cpm.filters.Add(new DropdownMenuModel(language=="fr-ca"?"Groupe d'indicateur":"Indicator Group", "indicatorGroupId", indicatorGroups, strata.Measure.Indicator.LifeCourse.IndicatorGroupId));

            var lifeCourses = strata.Measure.Indicator.LifeCourse.IndicatorGroup.LifeCourses
                                     .Where(lc => lc.DefaultIndicatorId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(lc => new DropdownItem
            {
                Value = lc.LifeCourseId,
                Text = lc.LifeCourseNameEn
            });

            cpm.filters.Add(new DropdownMenuModel(language=="fr-ca"?"Cours de la vie":"Life Course", "lifeCourseId", lifeCourses, strata.Measure.Indicator.LifeCourseId));

            var indicators = strata.Measure.Indicator.LifeCourse.Indicators
                                     .Where(i => i.DefaultMeasureId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(i => new DropdownItem
            {
                Value = i.IndicatorId,
                Text = i.IndicatorNameEn
            });

            cpm.filters.Add(new DropdownMenuModel(language=="fr-ca"?"Indicateurs":"Indicators", "indicatorId", indicators, strata.Measure.IndicatorId));

            var measures = strata.Measure.Indicator.Measures
                                     .Where(m => m.DefaultStrataId != null && m.Included)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(m => new DropdownItem
            {
                Value = m.MeasureId,
                Text = m.MeasureNameIndexEn
            });

            cpm.filters.Add(new DropdownMenuModel(language=="fr-ca"?"Mesures":"Measures", "measureId", measures, strata.MeasureId));

            var stratas = strata.Measure.Stratas
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(s => new DropdownItem
            {
                Value = s.StrataId,
                Text = s.StrataNameEn
            });

            cpm.filters.Add(new DropdownMenuModel(language=="fr-ca"?"Répartition des données":"Data Breakdowns", "strataId", stratas, strataId));


            if (Request.Method == "GET" && !api)
                return View(cpm);
            else
                return Json(cpm);

        }

        public async Task<IActionResult> Details(string language, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measure = await _context.Measure.FirstOrDefaultAsync(m => m.MeasureId == id);
            if (measure == null)
            {
                return NotFound();
            }
            
            return View(measure);
        }
    }
}
