// This file was written by a tool
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Infobase.Models.PASS;

namespace Infobase.Models
{
    public class PASSContext : DbContext
    {
        public PASSContext(DbContextOptions<PASSContext> options): base(options) { }
        public DbSet<Master> Master { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<IndicatorGroup> IndicatorGroup { get; set; }
        public DbSet<LifeCourse> LifeCourse { get; set; }
        public DbSet<Indicator> Indicator { get; set; }
        public DbSet<Measure> Measure { get; set; }
        public DbSet<Strata> Strata { get; set; }
        public DbSet<Point> Point { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AutoIncrement the indexes
            modelBuilder.ForNpgsqlUseIdentityColumns();
            /* Make the Indexes unique. Conflcits must FAIL. */
            modelBuilder.Entity<Activity>().HasAlternateKey(entity => entity.Index);
            modelBuilder.Entity<IndicatorGroup>().HasAlternateKey(entity => entity.Index);
            modelBuilder.Entity<LifeCourse>().HasAlternateKey(entity => entity.Index);
            modelBuilder.Entity<Indicator>().HasAlternateKey(entity => entity.Index);
            modelBuilder.Entity<Measure>().HasAlternateKey(entity => entity.Index);
            modelBuilder.Entity<Strata>().HasAlternateKey(entity => entity.Index);
            modelBuilder.Entity<Point>().HasAlternateKey(entity => entity.Index);
            modelBuilder.Entity<Master>().HasKey(m => m.Index);
        }
    }

    public class Master {
        
        public int Index { get; set; }
        public bool Aggregator { get; set; }
        public bool Included { get; set; }
        public double? CVWarnAt { get; set; }
        public double? CVSuppressAt { get; set; }
        public double? ValueAverage { get; set; }
        public double? ValueUpper { get; set; }
        public double? ValueLower { get; set; }
        public int CVInterpretation { get; set; }
        public int? CVValue { get; set; }
        public int Type { get; set; }
        public string ActivityNameEn { get; set; }
        public string IndicatorGroupNameEn { get; set; }
        public string LifeCourseNameEn { get; set; }
        public string IndicatorNameEn { get; set; }
        public string MeasureNameIndexEn { get; set; }
        public string MeasureNameDataToolEn { get; set; }
        public string MeasureAdditionalRemarksEn { get; set; }
        public string MeasureDataAvailableEn { get; set; }
        public string MeasureDefinitionEn { get; set; }
        public string MeasureMethodEn { get; set; }
        public string MeasurePopulationGroupEn { get; set; }
        public string MeasureSourceLongEn { get; set; }
        public string MeasureSourceShortEn { get; set; }
        public string MeasureUnitLongEn { get; set; }
        public string MeasureUnitShortEn { get; set; }
        public string StrataNameEn { get; set; }
        public string StrataNotesEn { get; set; }
        public string StrataPopulationTitleFragmentEn { get; set; }
        public string StrataSourceEn { get; set; }
        public string PointLabelEn { get; set; }
        public string PointTextEn { get; set; }
    }
}