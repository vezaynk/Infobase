/**
PASS-DB Normalization Script v4
 - Denormalized languages
	- Removed most tables
	- Data and text are not together

PASS-DB Normalization Script v3
 - First-class PostgreSQL support
 - Added safe-guards against fake nulls (Nulls wrongly imported as empty strings) using coalesce and case

PASS-DB Normalization Script v2
 - Improved to allow after-the-fact-insertions (still need some adjustments)
 - No longer expanding schema for joins
 - No longer using temporary tables
 - Always defaults to the correct option
**/

insert into "Activity" ("Index", "ActivityNameEn")
	select min(id::Integer), Activity from master group by Activity;


insert into "IndicatorGroup"("ActivityId", "Index", "IndicatorGroupNameEn")
	select
		"ActivityId",
		min(masterA.id::Integer),
		MasterA."indicator_group"
	from
		master masterA, 
		master masterB,
		"Activity"
	where 
		masterB.id::Integer = "Activity"."Index" and 
		masterB.Activity = mastera.Activity
	group by 
		"ActivityId",
		MasterA."indicator_group";


insert into "LifeCourse"("IndicatorGroupId", "Index", "LifeCourseNameEn")
	select
		"IndicatorGroupId",
		min(masterA.id::Integer),
		masterA."life_course"
	from
		master masterA, 
		master masterB,
		"IndicatorGroup"
	where 
		masterB.id::Integer = "IndicatorGroup"."Index" and 
		masterB."indicator_group" = mastera."indicator_group" and
		masterb.Activity = mastera.Activity
	group by 
		"IndicatorGroupId",
		masterA."life_course";

insert into "Indicator"("LifeCourseId", "Index", "IndicatorNameEn")
	select
		"LifeCourseId",
		min(masterA.id::Integer),
		masterA."indicator"
	from
		master masterA, 
		master masterB,
		"LifeCourse"
	where 
		masterB.id::Integer= "LifeCourse"."Index" and 
		masterB."life_course" = mastera."life_course" and
		masterB."indicator_group" = mastera."indicator_group" and
		masterb.Activity = mastera.Activity
	group by 
		"LifeCourseId",
		masterA."indicator";

insert into "Measure"("IndicatorId", "Index", "CVWarnAt", "CVSuppressAt", "Included", "Aggregator", "MeasureNameIndexEn", "MeasureNameDataToolEn", "MeasureAdditionalRemarksEn", "MeasureDataAvailableEn", "MeasureMethodEn", "MeasureDefinitionEn", "MeasurePopulationGroupEn", "MeasureSourceShortEn", "MeasureSourceLongEn", "MeasureUnitLongEn", "MeasureUnitShortEn")
	select
		"IndicatorId",
		min(masterA.id::Integer),
		case coalesce(mastera."cv_range_1", '') when '' then null else mastera."cv_range_1"::double precision end,
		case coalesce(masterA."cv_range_2", '') when '' then null else mastera."cv_range_2"::double precision end,
		case mastera.include_dt when 'Y' then true else false end,
		masterA."other_dt_display" is not null,
		trim(masterA."specific_measure_1"),
		trim(masterA."specific_measure_2"),
		trim(masterA."additional_remarks"),
		trim(masterA."data_available"),
		trim(masterA."defintion"),
		trim(masterA."estimate_calculation"),
		trim(masterA."population_2"),
		trim(masterA."data_source_2"),
		trim(masterA."data_source_3"),
		trim(masterA."unit_label_1"),
		trim(masterA."unit_label_2")
	from
		master masterA, 
		master masterB,
		"Indicator"
	where 
		masterB.id::Integer = "Indicator"."Index" and 
		masterB.Indicator = mastera.Indicator and
		masterB."life_course" = mastera."life_course" and
		masterB."indicator_group" = mastera."indicator_group" and
		masterb.Activity = mastera.Activity
	group by 
		"IndicatorId",
		mastera."cv_range_1",
		mastera."cv_range_2",
		mastera.include_dt,
		masterA."other_dt_display",
		trim(masterA."specific_measure_1"),
		trim(masterA."specific_measure_2"),
		trim(masterA."additional_remarks"),
		trim(masterA."data_available"),
		trim(masterA."defintion"),
		trim(masterA."estimate_calculation"),
		trim(masterA."population_2"),
		trim(masterA."data_source_2"),
		trim(masterA."data_source_3"),
		trim(masterA."unit_label_1"),
		trim(masterA."unit_label_2");


insert into "Strata"("MeasureId", "Index", "StrataNameEn")
select
		"MeasureId",		
		min(masterA.id::Integer),
		masterA.data_breakdowns
	from
		master masterA, 
		master masterB,
		"Measure"
	where 
		masterB.id::Integer = "Measure"."Index" and 
		masterB."specific_measure_1" = mastera."specific_measure_1" and
		masterB.Indicator = mastera.Indicator and
		masterB."life_course" = mastera."life_course" and
		masterB."indicator_group" = mastera."indicator_group" and
		masterb.Activity = mastera.Activity
	group by 
		"MeasureId",
		masterA.data_breakdowns;


insert into "Point"("StrataId", "CVInterpretation", "CVValue", "ValueAverage", "ValueUpper", "ValueLower", "Type", "Index", "PointLabelEn")
	select
		"StrataId",
		case coalesce(masterA.CV_Interpretation, '') when '' then null else masterA.CV_Interpretation::Integer end,
		case coalesce(masterA.CV_1, '') when '' then null else masterA.CV_1::float8 end,
		case coalesce(masterA.data, '') when '' then null else round(masterA.data::numeric, 1) end,
		case coalesce(masterA.CI_Upper_95, '') when '' then null else round(masterA.CI_Upper_95::numeric, 1) end,
		case coalesce(masterA.CI_low_95, '') when '' then null else round(masterA.CI_low_95::numeric, 1) end,
		case coalesce(masterA."display_data", '') when '' then 0 else masterA."display_data"::Integer end,
		masterA.id::Integer,
		masterA.disaggregation
	from
		master masterA,
		master masterB,
		"Strata"
	where 
		cast(masterB.id  as Integer)= "Strata"."Index" and 
		masterB."data_breakdowns" = mastera."data_breakdowns" and
		masterB."specific_measure_1" = mastera."specific_measure_1" and
		masterB.Indicator = mastera.Indicator and
		masterB."life_course" = mastera."life_course" and
		masterB."indicator_group" = mastera."indicator_group" and
		masterb.Activity = mastera.Activity and
		masterb.Include_DT = 'Y';
		
update
	"Measure" m set
		"DefaultStrataId" = "StrataId"
	from
		(
		select
			s."MeasureId",
			min(s."Index") as "Index"
		from
			"Measure" m
		inner join "Strata" s on
			s."MeasureId" = m."MeasureId"
		where s."StrataId" in (
			select
				s."StrataId"
			from
				"Point")
		group by
			s."MeasureId") a
	inner join "Strata" s on
		s."Index" = a."Index"
	where
		s."MeasureId" = m."MeasureId" and m."Included";
			   
update
	"Indicator" m set
		"DefaultMeasureId" = "MeasureId"
	from
		(
		select
			s."IndicatorId",
			min(s."Index") as "Index"
		from
			"Indicator" m
		inner join "Measure" s on
			s."IndicatorId" = m."IndicatorId"
		where s."DefaultStrataId" is not null
		group by
			s."IndicatorId") a
	inner join "Measure" s on
		s."Index" = a."Index"
	where
		s."IndicatorId" = m."IndicatorId";

update
	"LifeCourse" m set
		"DefaultIndicatorId" = "IndicatorId"
	from
		(
		select
			s."LifeCourseId",
			min(s."Index") as "Index"
		from
			"LifeCourse" m
		inner join "Indicator" s on
			s."LifeCourseId" = m."LifeCourseId"
		where s."DefaultMeasureId" is not null
		group by
			s."LifeCourseId") a
	inner join "Indicator" s on
		s."Index" = a."Index"
	where
		s."LifeCourseId" = m."LifeCourseId";


update
	"IndicatorGroup" m set
		"DefaultLifeCourseId" = "LifeCourseId"
	from
		(
		select
			s."IndicatorGroupId",
			min(s."Index") as "Index"
		from
			"IndicatorGroup" m
		inner join "LifeCourse" s on
			s."IndicatorGroupId" = m."IndicatorGroupId"
		where s."DefaultIndicatorId" is not null
		group by
			s."IndicatorGroupId") a
	inner join "LifeCourse" s on
		s."Index" = a."Index"
	where
		s."IndicatorGroupId" = m."IndicatorGroupId";

update
	"Activity" m set
		"DefaultIndicatorGroupId" = "IndicatorGroupId"
	from
		(
		select
			s."ActivityId",
			min(s."Index") as "Index"
		from
			"Activity" m
		inner join "IndicatorGroup" s on
			s."ActivityId" = m."ActivityId"
		where s."DefaultLifeCourseId" is not null
		group by
			s."ActivityId") a
	inner join "IndicatorGroup" s on
		s."Index" = a."Index"
	where
		s."ActivityId" = m."ActivityId";

/* Prepare "Translation" table */
/*
INSERT INTO "Translation" ("LanguageCode", "Text")
	SELECT DISTINCT 'en-ca', "activity" FROM Master A
		WHERE  A."activity" != '' AND A."activity" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);

INSERT INTO "Translation" ("LanguageCode", "Text")
	SELECT DISTINCT 'en-ca', "indicator_group" FROM Master A
		WHERE  A."indicator_group" != '' AND A."indicator_group" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);

INSERT INTO "Translation" ("LanguageCode", "Text")
	SELECT DISTINCT 'en-ca', "life_course" FROM Master A
		WHERE  A."life_course" != '' AND A."life_course" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);

INSERT INTO "Translation" ("LanguageCode", "Text")
	SELECT DISTINCT 'en-ca', "indicator" FROM Master A
		WHERE  A."indicator" != '' AND A."indicator" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);
		
INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "specific_measure_1", 'Datatool' FROM Master A
		WHERE  A."specific_measure_1" != '' AND A."specific_measure_1" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null AND "Type" = 'Datatool');

INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "specific_measure_2", 'Index' FROM Master A
		WHERE  A."specific_measure_2" != '' AND A."specific_measure_2" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null AND "Type" = 'Index');	

INSERT INTO "Translation" ("LanguageCode", "Text")
	SELECT DISTINCT 'en-ca', "data_breakdowns" FROM Master A
		WHERE  A."data_breakdowns" != '' AND A."data_breakdowns" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);			

INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "population_1", 'Datatool' FROM Master A
		WHERE  A."population_1" != '' AND A."population_1" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);				

INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "population_2", 'Index' FROM Master A
		WHERE  A."population_2" != '' AND A."population_2" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);				

INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "data_source_1", 'Datatool' FROM Master A
		WHERE  A."data_source_1" != '' AND A."data_source_1" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);					

INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "data_source_2", 'Index' FROM Master A
		WHERE  A."data_source_2" != '' AND A."data_source_2" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);					

INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "data_source_3", 'Measure' FROM Master A
		WHERE  A."data_source_3" != '' AND A."data_source_3" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);		

INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "disaggregation", 'Datatool' FROM Master A
		WHERE  A."disaggregation" != '' AND A."disaggregation" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);
		
INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "unit_label_1", 'Datatool' FROM Master A
		WHERE  A."unit_label_1" != '' AND A."unit_label_1" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null AND "Type" = 'Datatool');
		
INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "unit_label_2", 'Index' FROM Master A
		WHERE  A."unit_label_2" != '' AND A."unit_label_2" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null AND "Type" = 'Index');
		
INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "estimate_calculation", 'Index' FROM Master A
		WHERE  A."estimate_calculation" != '' AND A."estimate_calculation" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null AND "Type" = 'Measure');

INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "additional_remarks", 'Index' FROM Master A
		WHERE  A."additional_remarks" != '' AND A."additional_remarks" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null AND "Type" = 'Measure');

INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "data_available", 'Index' FROM Master A
		WHERE  A."data_available" != '' AND A."data_available" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null AND "Type" = 'Measure');
		

INSERT INTO "Translation" ("LanguageCode", "Text")
	SELECT DISTINCT 'en-ca', "notes" FROM Master A
		WHERE  A."notes" != '' AND A."notes" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);

INSERT INTO "Translation" ("LanguageCode", "Text")
	SELECT DISTINCT 'en-ca', "additional_remarks" FROM Master A
		WHERE  A."notes" != '' AND A."notes" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null);
		
INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "defintion", 'Measure' FROM Master A
		WHERE  A."defintion" != '' AND A."defintion" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null AND "Type" = 'Measure');

INSERT INTO "Translation" ("LanguageCode", "Text", "Type")
	SELECT DISTINCT 'en-ca', "population_1", 'Datatool' FROM Master A
		WHERE  A."population_1" != '' AND A."population_1" NOT IN (SELECT "Text" FROM "Translation" WHERE "Text" is NOT null AND "Type" = 'Datatool');

		
insert into "ActivityNameTranslation"("ActivityId", "TranslationId")
select "ActivityId", "TranslationId" from "master"
inner join "Activity" on "Index" = "master".id::Integer
inner join "Translation" on "activity" = "Text";

insert into "IndicatorGroupNameTranslation"("IndicatorGroupId", "TranslationId")
select "IndicatorGroupId", "TranslationId" from "master"
inner join "IndicatorGroup" on "Index" = "master".id::Integer
inner join "Translation" on "indicator_group" = "Text";

insert into "LifeCourseNameTranslation"("LifeCourseId", "TranslationId")
select "LifeCourseId", "TranslationId" from "master"
inner join "LifeCourse" on "Index" = "master".id::Integer
inner join "Translation" on "life_course" = "Text";

insert into "MeasureAdditionalRemarksTranslation"("MeasureId", "TranslationId")
select "MeasureId", "TranslationId" from "master"
inner join "Measure" on "Index" = "master".id::Integer
inner join "Translation" on "additional_remarks" = "Text";

insert into "MeasureDataAvailableTranslation"("MeasureId", "TranslationId")
select "MeasureId", "TranslationId" from "master"
inner join "Measure" on "Index" = "master".id::Integer
inner join "Translation" on "data_available" = "Text";

insert into "MeasureDefinitionTranslation"("MeasureId", "TranslationId")
select "MeasureId", "TranslationId" from "master"
inner join "Measure" on "Index" = "master".id::Integer
inner join "Translation" on "defintion" = "Text";

insert into "MeasureMethodTranslation"("MeasureId", "TranslationId")
select "MeasureId", "TranslationId" from "master"
inner join "Measure" on "Index" = "master".id::Integer
inner join "Translation" on "estimate_calculation" = "Text";

insert into "MeasureNameTranslation"("MeasureId", "TranslationId")
select "MeasureId", "TranslationId" from "master"
inner join "Measure" on "Index" = "master".id::Integer
inner join "Translation" on "specific_measure_1" = "Text" or "specific_measure_2" = "Text";

insert into "MeasurePopulationTranslation"("MeasureId", "TranslationId")
select "MeasureId", "TranslationId" from "master"
inner join "Measure" on "Index" = "master".id::Integer
inner join "Translation" on "population_2" = "Text";

insert into "MeasureSourceTranslation"("MeasureId", "TranslationId")
select "MeasureId", "TranslationId" from "master"
inner join "Measure" on "Index" = "master".id::Integer
inner join "Translation" on "data_source_2" = "Text" or "data_source_3" = "Text";

insert into "MeasureUnitTranslation"("MeasureId", "TranslationId")
select "MeasureId", "TranslationId" from "master"
inner join "Measure" on "Index" = "master".id::Integer
inner join "Translation" on "unit_label_1" = "Text" or "unit_label_2" = "Text";

insert into "IndicatorNameTranslation"("IndicatorId", "TranslationId")
select "IndicatorId", "TranslationId" from "master"
inner join "Indicator" on "Index" = "master".id::Integer
inner join "Translation" on "indicator" = "Text";


insert into "StrataNameTranslation"("StrataId", "TranslationId")
select "StrataId", "TranslationId" from "master"
inner join "Strata" on "Index" = "master".id::Integer
inner join "Translation" on "data_breakdowns" = "Text";

insert into "StrataNotesTranslation"("StrataId", "TranslationId")
select "StrataId", "TranslationId" from "master"
inner join "Strata" on "Index" = "master".id::Integer
inner join "Translation" on notes = "Text";

insert into "StrataPopulationTranslation"("StrataId", "TranslationId")
select "StrataId", "TranslationId" from "master"
inner join "Strata" on "Index" = "master".id::Integer
inner join "Translation" on "population_1" = "Text";

insert into "StrataSourceTranslation"("StrataId", "TranslationId")
select "StrataId", "TranslationId" from "master"
inner join "Strata" on "Index" = "master".id::Integer
inner join "Translation" on "data_source_1" = "Text";

insert into "PointLabelTranslation"("PointId", "TranslationId")
select "PointId", "TranslationId" from "master"
inner join "Point" on "Index" = "master".id::Integer
inner join "Translation" on "disaggregation" = "Text";
*/
