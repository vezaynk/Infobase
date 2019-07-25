select "ActivityNameEn" missing, 'ActivityName' "Column", concat('Activity Index: ', "Index") "origin" from "Activity"
where "ActivityNameFr" is null and "ActivityNameEn" is not null
union

select "IndicatorNameEn" missing, 'IndicatorName' "Column", concat('Indicator Index ', "Index") "origin" from "Indicator"
where "IndicatorNameFr" is null and "IndicatorNameEn" is not null
union

select "IndicatorGroupNameEn" missing, 'IndicatorGroupName' "Column", concat('IndicatorGroup Index ', "Index") "origin" from "IndicatorGroup"
where "IndicatorGroupNameFr" is null and "IndicatorGroupNameEn" is not null
union

select "LifeCourseNameEn" missing, 'LifeCourseName' "Column", concat('LifeCourse Index ', "Index") "origin" from "LifeCourse"
where "LifeCourseNameFr" is null and "LifeCourseNameEn" is not null
union

select "MeasureAdditionalRemarksEn" missing, 'MeasureAdditionalRemarks' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasureAdditionalRemarksFr" is null and "MeasureAdditionalRemarksEn" is not null
union

select "MeasureDataAvailableEn" missing, 'MeasureDataAvailable' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasureDataAvailableFr" is null and "MeasureDataAvailableEn" is not null
union

select "MeasureDefinitionEn" missing, 'MeasureDefinition' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasureDefinitionFr" is null and "MeasureDefinitionEn" is not null
union

select "MeasureMethodEn" missing, 'MeasureMethod' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasureMethodFr" is null and "MeasureMethodEn" is not null
union

select "MeasureSourceLongEn" missing, 'MeasureSourceLong' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasureSourceLongFr" is null and "MeasureSourceLongEn" is not null
union

select "MeasureSourceShortEn" missing, 'MeasureSourceShort' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasureSourceShortFr" is null and "MeasureSourceShortEn" is not null
union

select "MeasureUnitLongEn" missing, 'MeasureUnitLong' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasureUnitLongFr" is null and "MeasureUnitLongEn" is not null
union

select "MeasureUnitShortEn" missing, 'MeasureUnitShort' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasureUnitShortFr" is null and "MeasureUnitShortEn" is not null
union

select "MeasureNameDataToolEn" missing, 'MeasureNameDataTool' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasureNameDataToolFr" is null and "MeasureNameDataToolEn" is not null
union

select "MeasureNameIndexEn" missing, 'MeasureNameIndex' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasureNameIndexFr" is null and "MeasureNameIndexEn" is not null
union

select "MeasurePopulationGroupEn" missing, 'MeasurePopulationGroup' "Column", concat('Measure Index ', "Index") "origin" from "Measure"
where "MeasurePopulationGroupFr" is null and "MeasurePopulationGroupEn" is not null
union

select "PointLabelEn" missing, 'PointLabel' "Column", concat('Point Index ', "Index") "origin" from "Point"
where "PointLabelFr" is null and "PointLabelEn" is not null
union

select "PointTextEn" missing, 'PointText' "Column", concat('Point Index ', "Index") "origin" from "Point"
where "PointTextFr" is null and "PointTextEn" is not null
union

select "StrataNameEn" missing, 'StrataName' "Column", concat('Strata Index ', "Index") "origin" from "Strata"
where "StrataNameFr" is null and "StrataNameEn" is not null
union

select "StrataNotesEn" missing, 'StrataNotes' "Column", concat('Strata Index ', "Index") "origin" from "Strata"
where "StrataNotesFr" is null and "StrataNotesEn" is not null
union

select "StrataPopulationTitleFragmentEn" missing, 'StrataPopulationTitleFragment' "Column", concat('Strata Index ', "Index") "origin" from "Strata"
where "StrataPopulationTitleFragmentFr" is null and "StrataPopulationTitleFragmentEn" is not null

union

select "StrataSourceEn" missing, 'StrataSource' "Column", concat('Strata Index ', "Index") "origin" from "Strata"
where "StrataSourceFr" is null and "StrataSourceEn" is not null;