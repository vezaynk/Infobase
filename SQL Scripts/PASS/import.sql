/**
PASS-DB Normalization Script v4
 - Denormalized languages
 - Removed most tables
 - Data and text are not together
 - Switched to ANSI joins

PASS-DB Normalization Script v3
 - First-class PostgreSQL support
 - Added safe-guards against fake nulls (Nulls wrongly imported as empty strings) using coalesce and case

PASS-DB Normalization Script v2
 - Improved to allow after-the-fact-insertions (still need some adjustments)
 - No longer expanding schema for joins
 - No longer using temporary tables
 - Always defaults to the correct option
 **/
/**
Notes:
    The script is not 100% safe-guarded against malformatted input and ought to be added as needed.
    The common pitfalls include:
         - Trailing whitespace
         - Typos
         - Similar looking characters (especially whitespace)
         - Casing
         - Bad type-casting on import

    Most of these have already been safe-guarded against at least once in the script below.
 **/
INSERT INTO "Activity" ("Index", "ActivityNameEn")
SELECT
    min(id::Integer),
    Activity
FROM
    master
GROUP BY
    Activity;

INSERT INTO "IndicatorGroup" ("ActivityId", "Index", "IndicatorGroupNameEn")
SELECT
    "ActivityId",
    min(masterA.id::Integer),
    MasterA. "indicator_group"
FROM
    "Activity"
    INNER JOIN master masterB ON masterB.id::Integer = "Activity"."Index"
    INNER JOIN master masterA ON masterB.Activity = mastera.Activity
GROUP BY
    "ActivityId",
    MasterA. "indicator_group";

INSERT INTO "LifeCourse" ("IndicatorGroupId", "Index", "LifeCourseNameEn")
SELECT
    "IndicatorGroupId",
    min(masterA.id::Integer),
    masterA. "life_course"
FROM
    "IndicatorGroup"
    INNER JOIN master masterB ON masterB.id::Integer = "IndicatorGroup"."Index"
    INNER JOIN master masterA ON masterB.Activity = mastera.Activity
        AND masterB. "indicator_group" = mastera. "indicator_group"
    GROUP BY
        "IndicatorGroupId",
        masterA. "life_course";

INSERT INTO "Indicator" ("LifeCourseId", "Index", "IndicatorNameEn")
SELECT
    "LifeCourseId",
    min(masterA.id::Integer),
    masterA. "indicator"
FROM
    "LifeCourse"
    INNER JOIN master masterB ON masterB.id::Integer = "LifeCourse"."Index"
    INNER JOIN master masterA ON masterB.Activity = mastera.Activity
        AND masterB. "indicator_group" = mastera. "indicator_group"
        AND masterB. "life_course" = mastera. "life_course"
    GROUP BY
        "LifeCourseId",
        masterA. "indicator";

INSERT INTO "Measure" ("IndicatorId", "Index", "CVWarnAt", "CVSuppressAt", "Included", "Aggregator", "MeasureNameIndexEn", "MeasureNameDataToolEn", "MeasureAdditionalRemarksEn", "MeasureDataAvailableEn", "MeasureMethodEn", "MeasureDefinitionEn", "MeasurePopulationGroupEn", "MeasureSourceShortEn", "MeasureSourceLongEn", "MeasureUnitLongEn", "MeasureUnitShortEn")
SELECT
    "IndicatorId",
    min(masterA.id::Integer),
    CASE coalesce(mastera. "cv_range_1", '')
    WHEN '' THEN
        NULL
    ELSE
        mastera. "cv_range_1"::double precision
    END,
    CASE coalesce(masterA. "cv_range_2", '')
    WHEN '' THEN
        NULL
    ELSE
        mastera. "cv_range_2"::double precision
    END,
    CASE mastera.include_dt
    WHEN 'Y' THEN
        TRUE
    ELSE
        FALSE
    END,
    masterA. "other_dt_display" IS NOT NULL,
    trim(masterA. "specific_measure_1"),
    trim(masterA. "specific_measure_2"),
    trim(masterA. "additional_remarks"),
    trim(masterA. "data_available"),
    trim(masterA. "defintion"),
    trim(masterA. "estimate_calculation"),
    trim(masterA. "population_2"),
    trim(masterA. "data_source_2"),
    trim(masterA. "data_source_3"),
    trim(masterA. "unit_label_1"),
    trim(masterA. "unit_label_2")
FROM
    "Indicator"
    INNER JOIN master masterB ON masterB.id::Integer = "Indicator"."Index"
    INNER JOIN master masterA ON masterB.Indicator = mastera.Indicator
        AND masterB. "life_course" = mastera. "life_course"
        AND masterB. "indicator_group" = mastera. "indicator_group"
        AND masterb.Activity = mastera.Activity
    GROUP BY
        "IndicatorId",
        mastera. "cv_range_1",
        mastera. "cv_range_2",
        mastera.include_dt,
        masterA. "other_dt_display",
        trim(masterA. "specific_measure_1"),
        trim(masterA. "specific_measure_2"),
        trim(masterA. "additional_remarks"),
        trim(masterA. "data_available"),
        trim(masterA. "defintion"),
        trim(masterA. "estimate_calculation"),
        trim(masterA. "population_2"),
        trim(masterA. "data_source_2"),
        trim(masterA. "data_source_3"),
        trim(masterA. "unit_label_1"),
        trim(masterA. "unit_label_2");

INSERT INTO "Strata" ("MeasureId", "Index", "StrataNameEn", "StrataSourceEn", "StrataPopulationTitleFragmentEn", "StrataNotesEn")
SELECT
    "MeasureId",
    min(masterA.id::Integer),
    masterA.data_breakdowns,
    masterA.data_source_1,
    masterA.population_1,
    masterA.notes
FROM
    "Measure"
    INNER JOIN master masterB ON masterB.id::Integer = "Measure"."Index"
    INNER JOIN master masterA ON masterB. "specific_measure_1" = mastera. "specific_measure_1"
        AND masterB.Indicator = mastera.Indicator
        AND masterB. "life_course" = mastera. "life_course"
        AND masterB. "indicator_group" = mastera. "indicator_group"
        AND masterb.Activity = mastera.Activity
    GROUP BY
        "MeasureId",
        masterA.data_breakdowns,
        masterA.data_source_1,
        masterA.population_1,
        masterA.notes;

INSERT INTO "Point" ("StrataId", "CVInterpretation", "CVValue", "ValueAverage", "ValueUpper", "ValueLower", "Type", "Index", "PointLabelEn", "PointTextEn")
SELECT
    "StrataId",
    CASE coalesce(masterA.CV_Interpretation, '')
    WHEN '' THEN
        NULL
    ELSE
        masterA.CV_Interpretation::Integer
    END,
    CASE coalesce(masterA.CV_1, '')
    WHEN '' THEN
        NULL
    ELSE
        masterA.CV_1::float8
    END,
    CASE coalesce(masterA.data, '')
    WHEN '' THEN
        NULL
    ELSE
        round(masterA.data::numeric, 1)
    END,
    CASE coalesce(masterA.CI_Upper_95, '')
    WHEN '' THEN
        NULL
    ELSE
        round(masterA.CI_Upper_95::numeric, 1)
    END,
    CASE coalesce(masterA.CI_low_95, '')
    WHEN '' THEN
        NULL
    ELSE
        round(masterA.CI_low_95::numeric, 1)
    END,
    CASE coalesce(masterA. "display_data", '')
    WHEN '' THEN
        0
    ELSE
        masterA. "display_data"::Integer
    END,
    masterA.id::Integer,
    masterA.disaggregation,
    CASE coalesce(masterA.pt_table_label, '')
    WHEN '' THEN
        masterA.disaggregation
    ELSE
        masterA.pt_table_label
    END
    
FROM
    "Strata"
    INNER JOIN master masterB ON CAST(masterB.id AS Integer) = "Strata"."Index"
    INNER JOIN master masterA ON masterB. "data_breakdowns" = mastera. "data_breakdowns"
        AND masterB. "specific_measure_1" = mastera. "specific_measure_1"
        AND masterB.Indicator = mastera.Indicator
        AND masterB. "life_course" = mastera. "life_course"
        AND masterB. "indicator_group" = mastera. "indicator_group"
        AND masterb.Activity = mastera.Activity
WHERE
    masterB.Include_DT = 'Y';

UPDATE
    "Measure" m
SET
    "DefaultStrataId" = "StrataId"
FROM (
    SELECT
        s. "MeasureId",
        min(s. "Index") AS "Index"
    FROM
        "Measure" m
        INNER JOIN "Strata" s ON s. "MeasureId" = m. "MeasureId"
    WHERE
        s. "StrataId" IN (
            SELECT
                s. "StrataId"
            FROM
                "Point")
        GROUP BY
            s. "MeasureId") a
    INNER JOIN "Strata" s ON s. "Index" = a. "Index"
WHERE
    s. "MeasureId" = m. "MeasureId"
    AND m. "Included";

UPDATE
    "Indicator" m
SET
    "DefaultMeasureId" = "MeasureId"
FROM (
    SELECT
        s. "IndicatorId",
        min(s. "Index") AS "Index"
    FROM
        "Indicator" m
        INNER JOIN "Measure" s ON s. "IndicatorId" = m. "IndicatorId"
    WHERE
        s. "DefaultStrataId" IS NOT NULL
    GROUP BY
        s. "IndicatorId") a
    INNER JOIN "Measure" s ON s. "Index" = a. "Index"
WHERE
    s. "IndicatorId" = m. "IndicatorId";

UPDATE
    "LifeCourse" m
SET
    "DefaultIndicatorId" = "IndicatorId"
FROM (
    SELECT
        s. "LifeCourseId",
        min(s. "Index") AS "Index"
    FROM
        "LifeCourse" m
        INNER JOIN "Indicator" s ON s. "LifeCourseId" = m. "LifeCourseId"
    WHERE
        s. "DefaultMeasureId" IS NOT NULL
    GROUP BY
        s. "LifeCourseId") a
    INNER JOIN "Indicator" s ON s. "Index" = a. "Index"
WHERE
    s. "LifeCourseId" = m. "LifeCourseId";

UPDATE
    "IndicatorGroup" m
SET
    "DefaultLifeCourseId" = "LifeCourseId"
FROM (
    SELECT
        s. "IndicatorGroupId",
        min(s. "Index") AS "Index"
    FROM
        "IndicatorGroup" m
        INNER JOIN "LifeCourse" s ON s. "IndicatorGroupId" = m. "IndicatorGroupId"
    WHERE
        s. "DefaultIndicatorId" IS NOT NULL
    GROUP BY
        s. "IndicatorGroupId") a
    INNER JOIN "LifeCourse" s ON s. "Index" = a. "Index"
WHERE
    s. "IndicatorGroupId" = m. "IndicatorGroupId";

UPDATE
    "Activity" m
SET
    "DefaultIndicatorGroupId" = "IndicatorGroupId"
FROM (
    SELECT
        s. "ActivityId",
        min(s. "Index") AS "Index"
    FROM
        "Activity" m
        INNER JOIN "IndicatorGroup" s ON s. "ActivityId" = m. "ActivityId"
    WHERE
        s. "DefaultLifeCourseId" IS NOT NULL
    GROUP BY
        s. "ActivityId") a
    INNER JOIN "IndicatorGroup" s ON s. "Index" = a. "Index"
WHERE
    s. "ActivityId" = m. "ActivityId";

