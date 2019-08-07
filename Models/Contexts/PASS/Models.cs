using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Models.Metadata;

namespace Models.Contexts.PASS
{
    [Filter(0)]
    public class Activity
    {
        public int ActivityId { get; set; }
        public int Index { get; set; }
        [InverseProperty("Activity")]
        public virtual ICollection<IndicatorGroup> IndicatorGroups { get; set; }
        /* Text getters */
        public string ActivityName(string culture)
        {
            switch (culture)
            {
                case "en-ca": return ActivityNameEn;
                case "fr-ca": return ActivityNameFr;
            }
            return "No culture";
        }
        [CSVColumn("Activity")]
        [ShowOnAttribute(TextAppearance.Filter)]
        // [Text("Activity", "en-ca")]
        public string ActivityNameEn { get; set; }
        [ShowOnAttribute(TextAppearance.Filter)]
        // [Text("Activité", "fr-ca")]
        public string ActivityNameFr { get; set; }

        public int? DefaultIndicatorGroupId { get; set; }
        [ForeignKey("DefaultIndicatorGroupId")]
        public virtual IndicatorGroup DefaultIndicatorGroup { get; set; }
    }

    [Filter(3)]
    public class Indicator
    {
        public int IndicatorId { get; set; }
        public int LifeCourseId { get; set; }
        public int Index { get; set; }
        public virtual LifeCourse LifeCourse { get; set; }
        [InverseProperty("Indicator")]
        public virtual ICollection<Measure> Measures { get; set; }

        /* Text getters */
        public string IndicatorName(string culture)
        {
            switch (culture)
            {
                case "en-ca": return IndicatorNameEn;
                case "fr-ca": return IndicatorNameFr;
            }
            return "No culture";
        }
        [CSVColumn("Indicator")]
        // [Text("Indicator", "en-ca")]
        public string IndicatorNameEn { get; set; }
        // [Text("Indicateur", "fr-ca")]
        public string IndicatorNameFr { get; set; }

        public int? DefaultMeasureId { get; set; }
        [ForeignKey("DefaultMeasureId")]
        public virtual Measure DefaultMeasure { get; set; }
    }

    // [Text("Data Breakdowns", "en-ca")]
    [Filter(1)]
    public class IndicatorGroup
    {
        public int IndicatorGroupId { get; set; }
        public int ActivityId { get; set; }
        public int Index { get; set; }
        public virtual Activity Activity { get; set; }

        [InverseProperty("IndicatorGroup")]
        public virtual ICollection<LifeCourse> LifeCourses { get; set; }
        public string IndicatorGroupName(string culture)
        {
            switch (culture)
            {
                case "en-ca": return IndicatorGroupNameEn;
                case "fr-ca": return IndicatorGroupNameFr;
            }
            return "No culture";
        }
        [CSVColumn("Indicator Group")]
        public string IndicatorGroupNameEn { get; set; }
        public string IndicatorGroupNameFr { get; set; }
        public int? DefaultLifeCourseId { get; set; }
        [ForeignKey("DefaultLifeCourseId")]
        public virtual LifeCourse DefaultLifeCourse { get; set; }
    }
    // [Text("Life Course", "en-ca")]
    [Filter(2)]
    public class LifeCourse
    {
        public int LifeCourseId { get; set; }
        public int IndicatorGroupId { get; set; }
        public int Index { get; set; }
        public virtual IndicatorGroup IndicatorGroup { get; set; }
        [InverseProperty("LifeCourse")]
        public virtual ICollection<Indicator> Indicators { get; set; }

        /* Text getters */
        public string LifeCourseName(string culture)
        {
            switch (culture)
            {
                case "en-ca": return LifeCourseNameEn;
                case "fr-ca": return LifeCourseNameFr;
            }
            return "No culture";
        }
        [CSVColumn("Life Course")]
        public string LifeCourseNameEn { get; set; }
        public string LifeCourseNameFr { get; set; }

        public int? DefaultIndicatorId { get; set; }
        [ForeignKey("DefaultIndicatorId")]
        public virtual Indicator DefaultIndicator { get; set; }
    }

    // [Text("Measure", "en-ca")]
    // [Text("Measure", "fr-ca")]
    [Filter(4)]
    public class Measure
    {
        public int MeasureId { get; set; }
        public int IndicatorId { get; set; }
        public int Index { get; set; }
        public virtual Indicator Indicator { get; set; }
        public bool Included { get; set; }
        public bool Aggregator { get; set; }
        public double? CVWarnAt { get; set; }
        public double? CVSuppressAt { get; set; }

        [InverseProperty("Measure")]
        public virtual ICollection<Strata> Stratas { get; set; }

        public string MeasureNameIndex(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasureNameIndexEn;
                case "fr-ca": return MeasureNameIndexFr;
            }
            return "No culture";
        }
        public string MeasureNameDataTool(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasureNameDataToolEn;
                case "fr-ca": return MeasureNameDataToolFr;
            }
            return "No culture";
        }
        public string MeasureDefinition(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasureDefinitionEn;
                case "fr-ca": return MeasureDefinitionFr;
            }
            return "No culture";
        }
        public string MeasureMethod(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasureMethodEn;
                case "fr-ca": return MeasureMethodFr;
            }
            return "No culture";
        }
        public string MeasureAdditionalRemarks(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasureAdditionalRemarksEn;
                case "fr-ca": return MeasureAdditionalRemarksFr;
            }
            return "No culture";
        }
        public string MeasureDataAvailable(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasureDataAvailableEn;
                case "fr-ca": return MeasureDataAvailableFr;
            }
            return "No culture";
        }
        public string MeasurePopulationGroup(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasurePopulationGroupEn;
                case "fr-ca": return MeasurePopulationGroupFr;
            }
            return "No culture";
        }
        public string MeasureSourceLong(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasureSourceLongEn;
                case "fr-ca": return MeasureSourceLongFr;
            }
            return "No culture";
        }
        public string MeasureSourceShort(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasureSourceShortEn;
                case "fr-ca": return MeasureSourceShortFr;
            }
            return "No culture";
        }
        public string MeasureUnitLong(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasureUnitLongEn;
                case "fr-ca": return MeasureUnitLongFr;
            }
            return "No culture";
        }
        public string MeasureUnitShort(string culture)
        {
            switch (culture)
            {
                case "en-ca": return MeasureUnitShortEn;
                case "fr-ca": return MeasureUnitShortFr;
            }
            return "No culture";
        }
        [CSVColumn("Specific Measure")]
        public string MeasureNameIndexEn { get; set; }
        [CSVColumn("Specific Measure 2")]

        public string MeasureNameDataToolEn { get; set; }
        public string MeasureDefinitionEn { get; set; }
        public string MeasureMethodEn { get; set; }
        public string MeasureAdditionalRemarksEn { get; set; }
        public string MeasureDataAvailableEn { get; set; }
        public string MeasurePopulationGroupEn { get; set; }
        public string MeasureSourceShortEn { get; set; }
        public string MeasureSourceLongEn { get; set; }
        public string MeasureUnitShortEn { get; set; }
        public string MeasureUnitLongEn { get; set; }


        public string MeasureNameIndexFr { get; set; }
        public string MeasureNameDataToolFr { get; set; }
        public string MeasureDefinitionFr { get; set; }
        public string MeasureMethodFr { get; set; }
        public string MeasureAdditionalRemarksFr { get; set; }
        public string MeasureDataAvailableFr { get; set; }
        public string MeasurePopulationGroupFr { get; set; }
        public string MeasureSourceShortFr { get; set; }
        public string MeasureSourceLongFr { get; set; }
        public string MeasureUnitShortFr { get; set; }
        public string MeasureUnitLongFr { get; set; }

        public int? DefaultStrataId { get; set; }
        [ForeignKey("DefaultStrataId")]
        public virtual Strata DefaultStrata { get; set; }
    }
    [Filter(6)]
    public class Point
    {
        public int PointId { get; set; }
        public int StrataId { get; set; }
        public int Index { get; set; }
        public double? ValueAverage { get; set; }
        public double? ValueUpper { get; set; }
        public double? ValueLower { get; set; }
        public int CVInterpretation { get; set; }
        public int? CVValue { get; set; }
        public virtual Strata Strata { get; set; }

        /* Text getters */

        public string PointLabel(string culture)
        {
            switch (culture)
            {
                case "en-ca": return PointLabelEn;
                case "fr-ca": return PointLabelFr;
            }
            return "No culture";
        }
        [CSVColumn("Strata")]
        public string PointLabelEn { get; set; }
        public string PointLabelFr { get; set; }


        public string PointText(string culture)
        {
            switch (culture)
            {
                case "en-ca": return PointTextEn;
                case "fr-ca": return PointTextFr;
            }
            return "No culture";
        }
        public string PointTextEn { get; set; }
        public string PointTextFr { get; set; }


        public int Type { get; set; }
    }

    // [Text("Data Breakdown", "en-ca")]
    // [Text("", "fr-ca")]
    [Filter(5)]
    public class Strata
    {
        public int StrataId { get; set; }
        public int MeasureId { get; set; }
        public int Index { get; set; }
        public virtual Measure Measure { get; set; }
        [InverseProperty("Strata")]
        public virtual ICollection<Point> Points { get; set; }
        /* Text getters */

        public string StrataNotes(string culture)
        {
            switch (culture)
            {
                case "en-ca": return StrataNotesEn;
                case "fr-ca": return StrataNotesFr;
            }
            return "No culture";
        }

        public string StrataName(string culture)
        {
            switch (culture)
            {
                case "en-ca": return StrataNameEn;
                case "fr-ca": return StrataNameFr;
            }
            return "No culture";
        }

        public string StrataPopulationTitleFragment(string culture)
        {
            switch (culture)
            {
                case "en-ca": return StrataPopulationTitleFragmentEn;
                case "fr-ca": return StrataPopulationTitleFragmentFr;
            }
            return "No culture";
        }

        public string StrataSource(string culture)
        {
            switch (culture)
            {
                case "en-ca": return StrataSourceEn;
                case "fr-ca": return StrataSourceFr;
            }
            return "No culture";
        }
        public int? DefaultPointId { get; set; }
        [ForeignKey("DefaultPointId")]
        public Point DefaultPoint { get; set; }
        public string StrataNotesEn { get; set; }
        [CSVColumn("Data Breakdowns")]
        public string StrataNameEn { get; set; }
        public string StrataSourceEn { get; set; }
        public string StrataPopulationTitleFragmentEn { get; set; }

        public string StrataNotesFr { get; set; }
        public string StrataNameFr { get; set; }
        public string StrataSourceFr { get; set; }
        public string StrataPopulationTitleFragmentFr { get; set; }
    }
}
