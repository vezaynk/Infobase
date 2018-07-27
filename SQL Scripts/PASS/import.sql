
/* Prepare translation table */

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'EN', [Activity] FROM Master A
		WHERE  A.[Activity] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'EN', [Indicator Group] FROM Master A
		WHERE  A.[Indicator Group] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'EN', [Life Course] FROM Master A
		WHERE  A.[Life Course] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'EN', [Indicator] FROM Master A
		WHERE  A.[Indicator] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)
		
INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Specific Measure 1], [Type]='Datatool' FROM Master A
		WHERE  A.[Specific Measure 1] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Datatool')	

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Specific Measure 2], [Type]='Index' FROM Master A
		WHERE  A.[Specific Measure 2] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Index')	

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'EN', [Data Breakdowns] FROM Master A
		WHERE  A.[Data Breakdowns] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)			

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Population 1], [Type] = 'Datatool' FROM Master A
		WHERE  A.[Population 1] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)				

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Population 2], [Type] = 'Index' FROM Master A
		WHERE  A.[Population 2] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)				

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Data Source 1], [Type] = 'Datatool' FROM Master A
		WHERE  A.[Data Source 1] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)					

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Data Source 2], [Type] = 'Index' FROM Master A
		WHERE  A.[Data Source 2] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)					

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Data Source 3], [Type] = 'Measure' FROM Master A
		WHERE  A.[Data Source 3] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)		

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'EN', [Disaggregation] FROM Master A
		WHERE  A.[Disaggregation] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)
		
INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Unit Label 1], [Type] = 'Datatool' FROM Master A
		WHERE  A.[Unit Label 1] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Datatool')

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Unit Label 2], [Type] = 'Index' FROM Master A
		WHERE  A.[Unit Label 2] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Index')
		

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'EN', [Notes] FROM Master A
		WHERE  A.[Notes] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)

INSERT INTO Translation (LanguageCode, [Text])
	SELECT DISTINCT 'EN', [Additional Remarks] FROM Master A
		WHERE  A.[Notes] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null)
		
INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Population 2], [Type] = 'Index' FROM Master A
		WHERE  A.[Population 2] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Index')

INSERT INTO Translation (LanguageCode, [Text], [Type])
	SELECT DISTINCT 'EN', [Population 1], [Type] = 'Datatool' FROM Master A
		WHERE  A.[Population 1] NOT IN (SELECT [Text] FROM Translation WHERE [Text] is NOT null AND [Type] = 'Datatool')




/* Copy data to tmp table*/

SELECT [Activity]
      ,[Indicator Group]
      ,[Life Course]
      ,[Indicator]
      ,[Specific Measure 1]
      ,[Data Breakdowns]
      ,[Disaggregation]
      ,[nObs]
      ,[CV_1]
      ,[StError]
      ,[Data]
      ,[CI_low_95]
      ,[CI_Upper_95]
      ,[CV_Interpretation]
      ,[CV Range 1]
      ,[CV Range 2]
      ,[Population 1]
      ,[Unit Label 1]
      ,[Data Source 1]
      ,[Notes]
      ,[Unit Label 2]
      ,[Data Source 2]
      ,[Specific Measure 2]
      ,[Defintion]
      ,[Data Source 3]
      ,[Data Available]
      ,[Population 2]
      ,[Estimate Calculation]
      ,[Additional Remarks]
      ,[Include_DT]
	  ,[Display Data]
  INTO Tmp
  FROM [Master]
  order by Include_DT
 ;

/* Work will continue only on the temporary table from now on */


/* Insert distinct data into Activity*/

ALTER table Activity
add ActivityText varchar(max);
GO

INSERT INTO Activity(ActivityText)
SELECT Distinct [Activity] from Tmp;

/* Add column to track ActivityID*/
ALTER table Tmp
add ActivityId int;
GO

/* Innert generated ActivityIDs */
UPDATE Tmp
SET Tmp.ActivityId = Activity.ActivityID
FROM Activity
WHERE Activity.ActivityText = Tmp.[Activity]

/* Bind with translations */
INSERT INTO ActivityNameTranslation (ActivityId, TranslationId)
	SELECT ActivityId, B.TranslationId from Activity A
		JOIN Translation B 
			ON A.ActivityText = B.[Text];

/* Lets do Indicator Groups*/
ALTER table IndicatorGroup
add IndicatorGroupText varchar(max);

ALTER table Tmp
add IndicatorGroupId int;

GO

INSERT Into IndicatorGroup(ActivityId, IndicatorGroupText)
SELECT Distinct ActivityID, [Indicator Group] from Tmp;

UPDATE Tmp
SET Tmp.IndicatorGroupId = IndicatorGroup.IndicatorGroupId
FROM IndicatorGroup
WHERE IndicatorGroup.IndicatorGroupText = Tmp.[Indicator Group] AND Tmp.ActivityID = IndicatorGroup.ActivityId;

/* Bind with translations */
INSERT INTO IndicatorGroupNameTranslation (IndicatorGroupId, TranslationId)
	SELECT IndicatorGroupId, B.TranslationId from IndicatorGroup A
		JOIN Translation B 
			ON A.IndicatorGroupText = B.[Text];

/* Lets do Life Courses*/
ALTER table LifeCourse
add LifeCourseText varchar(max);

ALTER table Tmp
add LifeCourseId int;

GO

INSERT Into LifeCourse(IndicatorGroupId, LifeCourseText)
SELECT Distinct IndicatorGroupId, [Life Course] from Tmp;

UPDATE Tmp
SET Tmp.LifeCourseId = LifeCourse.LifeCourseId
FROM LifeCourse
WHERE LifeCourse.LifeCourseText = Tmp.[Life Course] AND Tmp.IndicatorGroupID = LifeCourse.IndicatorGroupId;

/* Bind with translations */
INSERT INTO LifeCourseNameTranslation(LifecourseId, TranslationId)
	SELECT LifeCourseId, B.TranslationId from LifeCourse A
		JOIN Translation B 
			ON A.LifeCourseText = B.[Text];


/* Lets do Indicators */
ALTER table Indicator
add IndicatorText varchar(max);

ALTER table Tmp
add IndicatorId int;

GO

INSERT Into Indicator(LifeCourseId, IndicatorText)
SELECT Distinct LifeCourseId, [Indicator] from Tmp;

UPDATE Tmp
SET Tmp.IndicatorId = Indicator.IndicatorId
FROM Indicator
WHERE Indicator.IndicatorText = Tmp.[Indicator] AND Tmp.LifeCourseId = Indicator.LifeCourseId;

/* Bind with translations */
INSERT INTO IndicatorNameTranslation(IndicatorId, TranslationId)
	SELECT IndicatorId, B.TranslationId from Indicator A
		JOIN Translation B 
			ON A.IndicatorText = B.[Text];


/* Lets do Measures */
ALTER table Measure
add MeasureText1 varchar(max), MeasureText2 varchar(max), MeasureUnit1 varchar(max), MeasureUnit2 varchar(max), MeasurePopulation2 varchar(max), DataSource2 varchar(max), DataSource3 varchar(max);

ALTER table Tmp
add MeasureId int;

GO

INSERT Into Measure(IndicatorId, MeasureText1, MeasureText2, MeasureUnit1, MeasureUnit2, MeasurePopulation2, Datasource2, Datasource3, [Included], CVWarnAt, CVSuppressAt)
SELECT Distinct IndicatorId, [Specific Measure 1], [Specific Measure 2], [Unit Label 1], [Unit Label 2], [Population 2],  [Data Source 2], [Data Source 3], IIF(include_dt = 'Y', 1, 0), [CV Range 1], [CV Range 2] from Tmp;

UPDATE Tmp
SET Tmp.MeasureId = Measure.MeasureId
FROM Measure
WHERE Measure.MeasureText1 = Tmp.[Specific Measure 1] AND Tmp.IndicatorId = Measure.IndicatorId;

/* Bind with translations */
INSERT INTO MeasureNameTranslation(MeasureId, TranslationId)
	SELECT MeasureId, B.TranslationId from Measure A
		JOIN Translation B 
			ON A.MeasureText1 = B.[Text] OR A.MeasureText2 = B.[Text];


INSERT INTO MeasureUnitTranslation(MeasureId, TranslationId)
	SELECT MeasureId, B.TranslationId from Measure A
		JOIN Translation B 
			ON A.MeasureUnit1 = B.[Text] OR A.MeasureUnit2 = B.[Text];

INSERT INTO MeasurePopulationTranslation(MeasureId, TranslationId)
	SELECT MeasureId, B.TranslationId from Measure A
		JOIN Translation B 
			ON A.MeasurePopulation2 = B.[Text];

INSERT INTO MeasureSourceTranslation(MeasureId, TranslationId)
	SELECT MeasureId, B.TranslationId from Measure A
		JOIN Translation B 
			ON A.Datasource2 = B.[Text] OR A.Datasource3 = B.[Text];

/* Lets do Stratas */
ALTER table Strata
add StrataText varchar(max), StrataNotes varchar(max), DataSource1 varchar(max), StrataPopulation1 varchar(max);

ALTER table Tmp
add StrataId int;

GO

INSERT Into Strata(MeasureId, StrataText, StrataNotes, DataSource1, StrataPopulation1)
SELECT Distinct MeasureId, [Data Breakdowns], Notes, [Data Source 1], [Population 1] from Tmp WHERE Include_DT = 'Y';

UPDATE Tmp
SET Tmp.StrataId = Strata.StrataId
FROM Strata
WHERE Strata.StrataText = Tmp.[Data Breakdowns] AND Tmp.MeasureId = Strata.MeasureId;

/* Bind with translations */
INSERT INTO StrataNameTranslation(StrataId, TranslationId)
	SELECT StrataId, B.TranslationId from Strata A
		JOIN Translation B 
			ON A.StrataText = B.[Text];
			

INSERT INTO StrataNotesTranslation(StrataId, TranslationId)
	SELECT StrataId, B.TranslationId from Strata A
		JOIN Translation B 
			ON A.StrataNotes = B.[Text];
			
INSERT INTO StrataPopulationTranslation(StrataId, TranslationId)
	SELECT StrataId, B.TranslationId from Strata A
		JOIN Translation B 
			ON A.StrataPopulation1 = B.[Text];

INSERT INTO StrataSourceTranslation(StrataId, TranslationId)
	SELECT StrataId, B.TranslationId from Strata A
		JOIN Translation B 
			ON A.DataSource1 = B.[Text];


/* Lets do Points */
ALTER table Point
add PointText varchar(max);

ALTER table Tmp
add PointId int;

GO

INSERT Into Point(StrataId, PointText, CVInterpretation, CVValue, ValueAverage, ValueUpper, ValueLower, [Type])
SELECT Distinct StrataId, [Disaggregation], CV_Interpretation, CV_1, [Data], CI_Upper_95, CI_low_95, isnull([Display Data], 0) from Tmp WHERE StrataId is not null;

UPDATE Tmp
SET Tmp.PointId = Point.PointId
FROM Point
WHERE Point.PointText = Tmp.[Disaggregation] AND Tmp.StrataId = Point.StrataId;

/* Bind with translations */
INSERT INTO PointLabelTranslation(PointId, TranslationId)
	SELECT PointId, B.TranslationId from Point A
		JOIN Translation B 
			ON A.PointText = B.[Text];

/* Clean up */

GO
ALTER table Point
drop column PointText;

GO
ALTER table Strata
drop column StrataText, StrataNotes, StrataPopulation1, DataSource1;

GO
ALTER table Measure
drop column MeasureText1, MeasureUnit1, MeasureText2, MeasureUnit2, DataSource2, DataSource3, MeasurePopulation2;

GO
ALTER table Indicator
drop column IndicatorText;

GO
ALTER table LifeCourse
drop column LifeCourseText;

GO
ALTER table IndicatorGroup
drop column IndicatorGroupText;

GO
ALTER table Activity
drop column ActivityText;
GO


SELECT * FROM Tmp;


/* Retrieve Defaults */

UPDATE Measure
SET Measure.DefaultStrataId = Strata.StrataId
FROM Strata
WHERE Measure.MeasureId = Strata.MeasureId;

UPDATE Indicator
SET Indicator.DefaultMeasureId = Measure.MeasureId
FROM Measure
WHERE Indicator.IndicatorId = Measure.IndicatorId AND Measure.DefaultStrataId is not null;

UPDATE LifeCourse
SET LifeCourse.DefaultIndicatorId = Indicator.IndicatorId
FROM Indicator
WHERE LifeCourse.LifeCourseId = Indicator.LifeCourseId AND Indicator.DefaultMeasureId is not null;

UPDATE IndicatorGroup
SET IndicatorGroup.DefaultLifeCourseId = LifeCourse.LifeCourseId
FROM LifeCourse
WHERE IndicatorGroup.IndicatorGroupId = LifeCourse.IndicatorGroupId AND LifeCourse.DefaultIndicatorId is not null;

UPDATE Activity
SET Activity.DefaultIndicatorGroupId = IndicatorGroup.IndicatorGroupId
FROM IndicatorGroup
WHERE Activity.ActivityId = IndicatorGroup.ActivityId AND IndicatorGroup.DefaultLifeCourseId is not null;