﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infobase.Models.PASS
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

        public Translatable MeasureName => Translation.GetTranslation(MeasureNameTranslations);
        public Translatable MeasureDefinition => Translation.GetTranslation(MeasureDefinitionTranslations);
        public Translatable MeasureUnit => Translation.GetTranslation(MeasureUnitTranslations);
        public Translatable MeasureSource => Translation.GetTranslation(MeasureSourceTranslations);
        public Translatable MeasurePopulation => Translation.GetTranslation(MeasurePopulationTranslations);
        public Point MeasurePoint { get => DefaultStrata.Points.FirstOrDefault(p => p.Type == 1); }

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

public interface ITranslation
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