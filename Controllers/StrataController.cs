using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReactDotNetDemo.Models;
using ReactDotNetDemo.Models.PASS;
using Newtonsoft.Json;

namespace ReactDotNetDemo.Controllers
{

    public class StrataController : Controller
    {
        private readonly PASSContext _context;

        public StrataController(PASSContext context)
        {
            _context = context;
        }

        // GET: Strata
        public async Task<IActionResult> Index()
        {
            var pASSContext = _context.Strata.Include(s => s.Measure);
            return View(await pASSContext.ToListAsync());
        }

        // GET: Strata/Details/
        public async Task<IActionResult> Details(int? measureId, int? indicatorId, int? lifeCourseId, int? indicatorGroupId, int? activityId, int strataId = 1)
        {
            /* Figure out a strataId to use. Not terribly efficient but it works fam and wastes only a single query. */
            if (activityId != null)
                indicatorGroupId = _context.IndicatorGroup.FirstOrDefault(m => m.ActivityId == activityId).IndicatorGroupId;

            if (indicatorGroupId != null)
                lifeCourseId = _context.LifeCourse.FirstOrDefault(m => m.IndicatorGroupId == indicatorGroupId).LifeCourseId;

            if (lifeCourseId != null)
                indicatorId = _context.Indicator.FirstOrDefault(m => m.LifeCourseId == lifeCourseId).IndicatorId;

            if (indicatorId != null)
                measureId = _context.Measure.FirstOrDefault(m => m.IndicatorId == indicatorId).MeasureId;

            if (measureId != null)
                strataId = _context.Strata.FirstOrDefault(m => m.MeasureId == measureId).StrataId;
            

            var strata = await _context.Strata
                // Translations
                // Strata
                .Include(s => s.StrataNameTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.StrataPopulationTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.StrataNotesTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.StrataSourceTranslations)
                    .ThenInclude(t => t.Translation)
                // Measure
                .Include(s => s.Measure.MeasureUnitTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.Measure.MeasureNameTranslations)
                    .ThenInclude(t => t.Translation)
                .Include(s => s.Measure.MeasureDefinitionTranslations)
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

                .FirstOrDefaultAsync(m => m.StrataId == strataId);


            if (strata == null)
            {
                return NotFound();
            }

            ChartPageModel cpm = new ChartPageModel(false)
            {
                ChartData = new ChartData
                {
                    axis = new ChartData.Labels
                    {
                        x = strata.GetStrataName("EN"),
                        y = strata.Measure.GetMeasureUnit("EN")
                    },
                    title = strata.Measure.GetMeasureName("EN") + ", " + strata.GetStrataPopulation("EN"),
                    population = strata.GetStrataPopulation("EN"),
                    notes = strata.GetStrataNotes("EN"),
                    source = strata.GetStrataSource("EN"),
                    values = new List<ChartData.Values>(),
                    measure = new ChartData.MeasureDescription
                    {
                        definition = strata.Measure.GetMeasureDefinition("EN"),
                        dataAvailable = "",
                        method = "",
                        additionalNotes = ""
                    }
                }
            };

            var ChartDataPointsQuery = strata.Points.Select(p => new
            {
                label = p.GetPointLabel("EN"),
                value = p.ValueAverage ?? 0,
                    upper = p.ValueUpper ?? 0,
                    lower = p.ValueLower ?? 0,
                    cv = 0,
                    CV_interpretation = p.CVInterpretation
            });

            foreach (var item in ChartDataPointsQuery)
            {

                // Build point from retrieved data
                var point = new ChartData.Point
                {
                    label = item.label,
                    value = item.value,
                    confidence = new ChartData.Point.Interval
                    {
                        upper = item.upper,
                        lower = item.lower
                    },
                    cv = new ChartData.Point.CV
                    {
                        value = item.cv,
                        interpretation = item.CV_interpretation
                    }
                };

                // Assume the type to be a bar
                /*
                 * 0 = bar
                 * 1 = trend
                 * 2 = line
                 */

                int type = 0;
                if (strata.GetStrataName("EN").Contains("Trend"))
                    type = 1;


                // If the label is "total" or "canada", it's a line across the chart
                string label = item.label;
                if (label.ToLower().Contains("total") || label.ToLower().Contains("canada"))
                    type = 2;

                // if value type is not yet included in the dataset, create it
                int targetIndex = cpm.ChartData.values.FindIndex(value => value.type == type);
                if (targetIndex == -1)
                {
                    // Chart type not present yet
                    targetIndex = cpm.ChartData.values.Count();
                    cpm.ChartData.values.Add(new ChartData.Values());
                    cpm.ChartData.values.ElementAt(targetIndex).points = new List<ChartData.Point>();
                }

                // place point into values
                cpm.ChartData.values.ElementAt(targetIndex).points.Add(point);
                cpm.ChartData.values.ElementAt(targetIndex).type = type;
            }

            var activities = _context.Activity
                                     .Include(ac => ac.ActivityNameTranslations)
                                     .ThenInclude(at => at.Translation)
                                     .Select(ac => new DropdownItem
                                            {
                                                Value = ac.ActivityId,
                                                Text = ac.GetActivityName("EN", true)
                                            });
            
            cpm.filters.Add(new DropdownMenuModel("Activity", "activityId", activities, strata.Measure.Indicator.LifeCourse.IndicatorGroup.ActivityId));

            var indicatorGroups = strata.Measure.Indicator.LifeCourse.IndicatorGroup.Activity.IndicatorGroups.Select(ig => new DropdownItem
            {
                Value = ig.IndicatorGroupId,
                Text = ig.GetIndicatorGroupName("EN")
            });

            cpm.filters.Add(new DropdownMenuModel("Indicator Group", "indicatorGroupId", indicatorGroups, strata.Measure.Indicator.LifeCourse.IndicatorGroupId));

            var lifeCourses = strata.Measure.Indicator.LifeCourse.IndicatorGroup.LifeCourses.Select(lc => new DropdownItem
            {
                Value = lc.LifeCourseId,
                Text = lc.GetLifeCourseName("EN")
            });

            cpm.filters.Add(new DropdownMenuModel("Life Course", "lifeCourseId", lifeCourses, strata.Measure.Indicator.LifeCourseId));

            var indicators = strata.Measure.Indicator.LifeCourse.Indicators.Select(i => new DropdownItem
            {
                Value = i.IndicatorId,
                Text = i.GetIndicatorName("EN")
            });

            cpm.filters.Add(new DropdownMenuModel("Indicators", "indicatorId", indicators, strata.Measure.IndicatorId));

            var measures = strata.Measure.Indicator.Measures.Select(m => new DropdownItem
            {
                Value = m.MeasureId,
                Text = m.GetMeasureName("EN")
            });

            cpm.filters.Add(new DropdownMenuModel("Measures", "measureId", measures, strata.MeasureId));

            var stratas = strata.Measure.Stratas.Select(s => new DropdownItem
            {
                Value = s.StrataId,
                Text = s.GetStrataName("EN")
            });

            cpm.filters.Add(new DropdownMenuModel("Data Breakdowns", "strataId", stratas, strataId));


            if (Request.Method == "GET")
                return View(cpm);
            else
                return Json(cpm);

        }

        // GET: Strata/Create
        public IActionResult Create()
        {
            ViewData["MeasureId"] = new SelectList(_context.Measure, "MeasureId", "MeasureId");
            return View();
        }

        // POST: Strata/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StrataId,MeasureId")] Strata strata)
        {
            if (ModelState.IsValid)
            {
                _context.Add(strata);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeasureId"] = new SelectList(_context.Measure, "MeasureId", "MeasureId", strata.MeasureId);
            return View(strata);
        }

        // GET: Strata/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strata = await _context.Strata.FindAsync(id);
            if (strata == null)
            {
                return NotFound();
            }
            ViewData["MeasureId"] = new SelectList(_context.Measure, "MeasureId", "MeasureId", strata.MeasureId);
            return View(strata);
        }

        // POST: Strata/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StrataId,MeasureId")] Strata strata)
        {
            if (id != strata.StrataId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(strata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrataExists(strata.StrataId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeasureId"] = new SelectList(_context.Measure, "MeasureId", "MeasureId", strata.MeasureId);
            return View(strata);
        }

        // GET: Strata/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strata = await _context.Strata
                .Include(s => s.Measure)
                .FirstOrDefaultAsync(m => m.StrataId == id);
            if (strata == null)
            {
                return NotFound();
            }

            return View(strata);
        }

        // POST: Strata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var strata = await _context.Strata.FindAsync(id);
            _context.Strata.Remove(strata);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StrataExists(int id)
        {
            return _context.Strata.Any(e => e.StrataId == id);
        }
    }
}
