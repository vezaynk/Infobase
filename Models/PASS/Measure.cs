using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models.PASS
{
    public class Measure
    {
        public int MeasureId { get; set; }
        public int IndicatorId { get; set; }
        public virtual Indicator Indicator { get; set; }
        public bool Included { get; set; }
        public double? CVWarnAt { get; set; }
        public double? CVSuppressAt { get; set; }
        public virtual ICollection<MeasureNameTranslation> MeasureNameTranslations { get; set; }
        public virtual ICollection<MeasureUnitTranslation> MeasureUnitTranslations { get; set; }
        public virtual ICollection<MeasureSourceTranslation> MeasureSourceTranslations { get; set; }
        public virtual ICollection<MeasurePopulationTranslation> MeasurePopulationTranslations { get; set; }
        public virtual ICollection<MeasureDefinitionTranslation> MeasureDefinitionTranslations { get; set; }
        [InverseProperty("Measure")]
        public virtual ICollection<Strata> Stratas { get; set; }

        public string GetMeasureName(string lc, string type) => Translation.GetTranslation(MeasureNameTranslations, lc, null);
        public string GetMeasureDefinition(string lc, string type) => Translation.GetTranslation(MeasureDefinitionTranslations, lc, null);
        public string GetMeasureUnit(string lc, string type) => Translation.GetTranslation(MeasureUnitTranslations, lc, null);
        public string GetMeasureSource(string lc, string type) => Translation.GetTranslation(MeasureSourceTranslations, lc, null);
        public string GetMeasurePopulation(string lc, string type) => Translation.GetTranslation(MeasurePopulationTranslations, lc, null);
        public Point MeasurePoint { get => DefaultStrata.Points.FirstOrDefault(p => p.Type == 2); }

        public int? DefaultStrataId { get; set; }
        [ForeignKey("DefaultStrataId")]
        public virtual Strata DefaultStrata { get; set; }
    }

    public class MeasureNameTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }
    }
    public class MeasureUnitTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }
    }

    public class MeasureDefinitionTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }
    }



    public class MeasurePopulationTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }
    }

    public class MeasureSourceTranslation : ITranslation
    {
        public int TranslationId { get; set; }
        public virtual Translation Translation { get; set; }
        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }
    }
}

interface ITranslation
{
    string Text { get; set; }
}

class NameTranslation : ITranslation
{
    public string Text { get; set; }
}

class DescriptionTranslation : ITranslation
{
    public string Text { get; set; }
}

class Test
{
    ICollection<DescriptionTranslation> dt;

    public void TryIt()
    {
        ICollection<ITranslation> myList = new List<ITranslation>();
        DoStuff(myList);
    }
    public void DoStuff(ICollection<ITranslation> Param) { }
}