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
                                                        .ThenInclude(m => m.MeasureNameTranslations)
                                                            .ThenInclude(t => t.Translation)


                                        .Include(a => a.IndicatorGroups)
                                                .ThenInclude(ig => ig.LifeCourses)
                                                .ThenInclude(lc => lc.Indicators)
                                                    .ThenInclude(i => i.Measures)
                                                        .ThenInclude(m => m.MeasureUnitTranslations)
                                                            .ThenInclude(t => t.Translation)

                                        // Include latest data by including points. Will filter later.
                                        .Include(a => a.IndicatorGroups)
                                                .ThenInclude(ig => ig.LifeCourses)
                                                .ThenInclude(lc => lc.Indicators)
                                                .ThenInclude(i => i.Measures)
                                                    .ThenInclude(m => m.DefaultStrata.Points)

                                        // Include source name
                                        .Include(a => a.IndicatorGroups)
                                                .ThenInclude(ig => ig.LifeCourses)
                                                .ThenInclude(lc => lc.Indicators)
                                                .ThenInclude(i => i.Measures)
                                                    .ThenInclude(m => m.MeasureSourceTranslations)
                                                        .ThenInclude(t => t.Translation)

                                        // Include indicator names
                                        .Include(a => a.IndicatorGroups)
                                                .ThenInclude(ig => ig.LifeCourses)
                                                .ThenInclude(lc => lc.Indicators)
                                                    .ThenInclude(m => m.IndicatorNameTranslations)
                                                        .ThenInclude(t => t.Translation)

                                        // Include Life Course names

                                        .Include(a => a.IndicatorGroups)
                                                .ThenInclude(ig => ig.LifeCourses)
                                                    .ThenInclude(lc => lc.LifeCourseNameTranslations)
                                                        .ThenInclude(t => t.Translation)

                                        // Include indicator group names
                                        .Include(a => a.IndicatorGroups)
                                          .ThenInclude(a => a.IndicatorGroupNameTranslations)
                                                        .ThenInclude(t => t.Translation)

                                          // Include Activity names
                                          .Include(a => a.ActivityNameTranslations)
                                                        .ThenInclude(t => t.Translation);
                                                        

            
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
                // Translations
                // Strata
                .Include(s => s.StrataNameTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.StrataNotesTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.StrataSourceTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.StrataPopulationTranslations)
                    .ThenInclude(t => t.Translation)
            
            
                // Measure
                .Include(s => s.Measure.MeasureUnitTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.Measure.MeasureNameTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.Measure.MeasureDefinitionTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.Measure.MeasurePopulationTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.Measure.MeasureSourceTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.Measure.MeasureAdditionalRemarksTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.Measure.MeasureMethodTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.Measure.MeasureDataAvailableTranslations)
                    .ThenInclude(t => t.Translation)

                // Points
                .Include(s => s.Points)
                    .ThenInclude(p => p.PointLabelTranslations)
                        .ThenInclude(t => t.Translation)

                // Indicator
                .Include(s => s.Measure.Indicator)
                    .ThenInclude(p => p.IndicatorNameTranslations)
                        .ThenInclude(t => t.Translation)

                // Life Course
                .Include(s => s.Measure.Indicator.LifeCourse)
                    .ThenInclude(p => p.LifeCourseNameTranslations)
                        .ThenInclude(t => t.Translation)

                // Indicator Group
                .Include(s => s.Measure.Indicator.LifeCourse.IndicatorGroup)
                    .ThenInclude(p => p.IndicatorGroupNameTranslations)
                        .ThenInclude(t => t.Translation)

                // Activity
                .Include(s => s.Measure.Indicator.LifeCourse.IndicatorGroup.Activity)
                    .ThenInclude(p => p.ActivityNameTranslations)
                        .ThenInclude(t => t.Translation)

                // Include associated stratas
                .Include(s => s.Measure)
                    .ThenInclude(p => p.Stratas)
                        .ThenInclude(p => p.StrataNameTranslations)
                            .ThenInclude(t => t.Translation)

                // Include associated measures
                .Include(s => s.Measure.Indicator)
                    .ThenInclude(p => p.Measures)
                        .ThenInclude(p => p.MeasureNameTranslations)
                            .ThenInclude(t => t.Translation)


                // Include associated indicators
                .Include(s => s.Measure.Indicator.LifeCourse)
                    .ThenInclude(p => p.Indicators)
                        .ThenInclude(p => p.IndicatorNameTranslations)
                            .ThenInclude(t => t.Translation)

                // Include associated life courses
                .Include(s => s.Measure.Indicator.LifeCourse.IndicatorGroup)
                    .ThenInclude(p => p.LifeCourses)
                        .ThenInclude(p => p.LifeCourseNameTranslations)
                            .ThenInclude(t => t.Translation)


                // Include associated indicator groups
                .Include(s => s.Measure.Indicator.LifeCourse.IndicatorGroup.Activity)
                    .ThenInclude(p => p.IndicatorGroups)
                        .ThenInclude(p => p.IndicatorGroupNameTranslations)
                            .ThenInclude(t => t.Translation)
                .OrderBy(m => m.StrataId == strataId ? 0 : 1)
                .ThenBy(m => m.Index)
                .FirstOrDefaultAsync();


            if (strata == null)
            {
                return NotFound();
            }

            var chart = new ChartData {
                XAxis = strata.StrataName,
                YAxis = strata.Measure.MeasureUnit,
                Source = new Translatable(strata.Measure.MeasureSource.Union(strata.StrataSource).ToDictionary(p => p.Key, p => p.Value)),
                // Both stratas AND measure contain populations. They must be merged.
                Population = new Translatable(strata.Measure.MeasurePopulation.Union(strata.StrataPopulation).ToDictionary(p => p.Key, p => p.Value)),
                Notes = strata.StrataNotes,
                Remarks = strata.Measure.MeasureAdditionalRemarks,
                Definition = strata.Measure.MeasureDefinition,
                Method = strata.Measure.MeasureMethod,
                DataAvailable = strata.Measure.MeasureDataAvailable,
                Points = strata.Points.OrderBy(p => p.Index).Select(p => new ChartData.Point {
                    CVInterpretation = p.CVInterpretation,
                    CVValue = p.CVValue,
                    Value = p.ValueAverage,
                    ValueUpper = p.ValueUpper,
                    ValueLower = p.ValueLower,
                    Label = p.PointLabel,
                    Type = p.Type
                }),
                WarningCV = strata.Measure.CVWarnAt,
                SuppressCV = strata.Measure.CVSuppressAt,
                MeasureName = strata.Measure.MeasureName
            };


            var cpm = new ChartPageModel(language, chart);

            // top level requires a new query
            var activities = _context.Activity
                                     .Where(ac => ac.DefaultIndicatorGroupId != null)
                                     .Include(ac => ac.ActivityNameTranslations)
                                     .ThenInclude(at => at.Translation)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(ac => new DropdownItem
                                            {
                                                Value = ac.ActivityId,
                                                Text = ac.ActivityName.Get((language, null))
                                            });
            
            cpm.filters.Add(new DropdownMenuModel("Activity", "activityId", activities, strata.Measure.Indicator.LifeCourse.IndicatorGroup.ActivityId));

            var indicatorGroups = strata.Measure.Indicator.LifeCourse.IndicatorGroup.Activity.IndicatorGroups
                                     .Where(ig => ig.DefaultLifeCourseId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(ig => new DropdownItem
                                            {
                                                Value = ig.IndicatorGroupId,
                                                Text = ig.IndicatorGroupName.Get((language, null))
                                            });

            cpm.filters.Add(new DropdownMenuModel("Indicator Group", "indicatorGroupId", indicatorGroups, strata.Measure.Indicator.LifeCourse.IndicatorGroupId));

            var lifeCourses = strata.Measure.Indicator.LifeCourse.IndicatorGroup.LifeCourses
                                     .Where(lc => lc.DefaultIndicatorId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(lc => new DropdownItem
            {
                Value = lc.LifeCourseId,
                Text = lc.LifeCourseName.Get((language, null))
            });

            cpm.filters.Add(new DropdownMenuModel("Life Course", "lifeCourseId", lifeCourses, strata.Measure.Indicator.LifeCourseId));

            var indicators = strata.Measure.Indicator.LifeCourse.Indicators
                                     .Where(i => i.DefaultMeasureId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(i => new DropdownItem
            {
                Value = i.IndicatorId,
                Text = i.IndicatorName.Get((language, null))
            });

            cpm.filters.Add(new DropdownMenuModel("Indicators", "indicatorId", indicators, strata.Measure.IndicatorId));

            var measures = strata.Measure.Indicator.Measures
                                     .Where(m => m.DefaultStrataId != null)
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(m => new DropdownItem
            {
                Value = m.MeasureId,
                Text = m.MeasureName.Get((language, null))
            });

            cpm.filters.Add(new DropdownMenuModel("Measures", "measureId", measures, strata.MeasureId));

            var stratas = strata.Measure.Stratas
                                     .AsEnumerable()
                                     .OrderBy(x => x.Index)
                                     .Select(s => new DropdownItem
            {
                Value = s.StrataId,
                Text = s.StrataName.Get((language, null))
            });

            cpm.filters.Add(new DropdownMenuModel("Data Breakdowns", "strataId", stratas, strataId));


            if (Request.Method == "GET" && !api)
                return View(cpm);
            else
                return Json(cpm);

        }

        // GET: Strata/Create
        
        // GET: Strata/Edit/5
        public async Task<IActionResult> Details(string language, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measure = await _context.Measure
                                    .Include(m => m.MeasureNameTranslations)
                                        .ThenInclude(t => t.Translation)
                                    .Include(m => m.MeasureDefinitionTranslations)
                                        .ThenInclude(t => t.Translation)
                                    .Include(m => m.MeasureSourceTranslations)
                                        .ThenInclude(t => t.Translation)
                                    .Include(m => m.MeasurePopulationTranslations)
                                        .ThenInclude(t => t.Translation)
                                    .Include(m => m.MeasureMethodTranslations)
                                        .ThenInclude(t => t.Translation)
                                    .Include(m => m.MeasureAdditionalRemarksTranslations)
                                        .ThenInclude(t => t.Translation)
                                    .Include(m => m.MeasureDataAvailableTranslations)
                                        .ThenInclude(t => t.Translation)
                                    
                            .FirstOrDefaultAsync(m => m.MeasureId == id);
            if (measure == null)
            {
                return NotFound();
            }
            
            return View(measure);
        }
    }
}
