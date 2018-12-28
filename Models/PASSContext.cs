using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infobase.Models.PASS;

namespace Infobase.Models
{
    public class PASSContext : DbContext
    {
        public PASSContext (DbContextOptions<PASSContext> options)
            : base(options)
        {
        }

        public DbSet<Activity> Activity { get; set; }


        public DbSet<Measure> Measure { get; set; }
        public DbSet<IndicatorGroup> IndicatorGroup { get; set; }
        public DbSet<LifeCourse> LifeCourse { get; set; }
        public DbSet<Indicator> Indicator { get; set; }
        public DbSet<Strata> Strata { get; set; }
        public DbSet<Point> Point { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AutoIncrement the indexes
            modelBuilder.ForNpgsqlUseIdentityColumns();

            /* Make the Indexes unique. Conflcits must FAIL. */
            modelBuilder.Entity<Activity>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<IndicatorGroup>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<Indicator>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<LifeCourse>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<Measure>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<Strata>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<Point>()
                .HasAlternateKey(entity => entity.Index);

            
            /* Bunch of boilerplate to enable many-to-many bindings */
            modelBuilder.Entity<ActivityNameTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.ActivityId });

            modelBuilder.Entity<ActivityNameTranslation>()
                .HasOne(pc => pc.Activity)
                .WithMany(p => p.ActivityNameTranslations)
                .HasForeignKey(pc => pc.ActivityId);

            modelBuilder.Entity<ActivityNameTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.ActivityNameTranslations)
                .HasForeignKey(pc => pc.TranslationId);

            modelBuilder.Entity<ActivityDescriptionTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.ActivityId });

            modelBuilder.Entity<ActivityDescriptionTranslation>()
                .HasOne(pc => pc.Activity)
                .WithMany(p => p.ActivityDescriptionTranslations)
                .HasForeignKey(pc => pc.ActivityId);

            modelBuilder.Entity<ActivityDescriptionTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.ActivityDescriptionTranslations)
                .HasForeignKey(pc => pc.TranslationId);




            modelBuilder.Entity<IndicatorGroupNameTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.IndicatorGroupId });

            modelBuilder.Entity<IndicatorGroupNameTranslation>()
                .HasOne(pc => pc.IndicatorGroup)
                .WithMany(p => p.IndicatorGroupNameTranslations)
                .HasForeignKey(pc => pc.IndicatorGroupId);

            modelBuilder.Entity<IndicatorGroupNameTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.IndicatorGroupNameTranslations)
                .HasForeignKey(pc => pc.TranslationId);


            modelBuilder.Entity<IndicatorNameTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.IndicatorId });

            modelBuilder.Entity<IndicatorNameTranslation>()
                .HasOne(pc => pc.Indicator)
                .WithMany(p => p.IndicatorNameTranslations)
                .HasForeignKey(pc => pc.IndicatorId);

            modelBuilder.Entity<IndicatorNameTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.IndicatorNameTranslations)
                .HasForeignKey(pc => pc.TranslationId);



            modelBuilder.Entity<StrataNotesTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.StrataId });

            modelBuilder.Entity<StrataNotesTranslation>()
                .HasOne(pc => pc.Strata)
                .WithMany(p => p.StrataNotesTranslations)
                .HasForeignKey(pc => pc.StrataId);

            modelBuilder.Entity<StrataNotesTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.StrataNotesTranslations)
                .HasForeignKey(pc => pc.TranslationId);



            modelBuilder.Entity<MeasureUnitTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.MeasureId });

            modelBuilder.Entity<MeasureUnitTranslation>()
                .HasOne(pc => pc.Measure)
                .WithMany(p => p.MeasureUnitTranslations)
                .HasForeignKey(pc => pc.MeasureId);

            modelBuilder.Entity<MeasureUnitTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.MeasureUnitTranslations)
                .HasForeignKey(pc => pc.TranslationId);

            modelBuilder.Entity<MeasureDefinitionTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.MeasureId });

            modelBuilder.Entity<MeasureDefinitionTranslation>()
                .HasOne(pc => pc.Measure)
                .WithMany(p => p.MeasureDefinitionTranslations)
                .HasForeignKey(pc => pc.MeasureId);

            modelBuilder.Entity<MeasureDefinitionTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.MeasureDefinitionTranslations)
                .HasForeignKey(pc => pc.TranslationId);

            modelBuilder.Entity<MeasureSourceTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.MeasureId });

            modelBuilder.Entity<MeasureSourceTranslation>()
                .HasOne(pc => pc.Measure)
                .WithMany(p => p.MeasureSourceTranslations)
                .HasForeignKey(pc => pc.MeasureId);

            modelBuilder.Entity<MeasureSourceTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.MeasureSourceTranslations)
                .HasForeignKey(pc => pc.TranslationId);

            modelBuilder.Entity<MeasurePopulationTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.MeasureId });

            modelBuilder.Entity<MeasurePopulationTranslation>()
                .HasOne(pc => pc.Measure)
                .WithMany(p => p.MeasurePopulationTranslations)
                .HasForeignKey(pc => pc.MeasureId);

            modelBuilder.Entity<MeasurePopulationTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.MeasurePopulationTranslations)
                .HasForeignKey(pc => pc.TranslationId);

            modelBuilder.Entity<MeasureDataAvailableTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.MeasureId });

            modelBuilder.Entity<MeasureDataAvailableTranslation>()
                .HasOne(pc => pc.Measure)
                .WithMany(p => p.MeasureDataAvailableTranslations)
                .HasForeignKey(pc => pc.MeasureId);

            modelBuilder.Entity<MeasureDataAvailableTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.MeasureDataAvailableTranslations)
                .HasForeignKey(pc => pc.TranslationId);

            modelBuilder.Entity<MeasureAdditionalRemarksTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.MeasureId });

            modelBuilder.Entity<MeasureAdditionalRemarksTranslation>()
                .HasOne(pc => pc.Measure)
                .WithMany(p => p.MeasureAdditionalRemarksTranslations)
                .HasForeignKey(pc => pc.MeasureId);

            modelBuilder.Entity<MeasureAdditionalRemarksTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.MeasureAdditionalRemarksTranslations)
                .HasForeignKey(pc => pc.TranslationId);

            modelBuilder.Entity<MeasureMethodTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.MeasureId });

            modelBuilder.Entity<MeasureMethodTranslation>()
                .HasOne(pc => pc.Measure)
                .WithMany(p => p.MeasureMethodTranslations)
                .HasForeignKey(pc => pc.MeasureId);

            modelBuilder.Entity<MeasureMethodTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.MeasureMethodTranslations)
                .HasForeignKey(pc => pc.TranslationId);



            modelBuilder.Entity<MeasureNameTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.MeasureId });
            modelBuilder.Entity<MeasureNameTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.MeasureNameTranslations)
                .HasForeignKey(pc => pc.TranslationId);



            modelBuilder.Entity<LifeCourseNameTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.LifeCourseId });

            modelBuilder.Entity<LifeCourseNameTranslation>()
                .HasOne(pc => pc.LifeCourse)
                .WithMany(p => p.LifeCourseNameTranslations)
                .HasForeignKey(pc => pc.LifeCourseId);

            modelBuilder.Entity<LifeCourseNameTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.LifeCourseNameTranslations)
                .HasForeignKey(pc => pc.TranslationId);


            modelBuilder.Entity<StrataNameTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.StrataId });

            modelBuilder.Entity<StrataNameTranslation>()
                .HasOne(pc => pc.Strata)
                .WithMany(p => p.StrataNameTranslations)
                .HasForeignKey(pc => pc.StrataId);

            modelBuilder.Entity<StrataNameTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.StrataNameTranslations)
                .HasForeignKey(pc => pc.TranslationId);

            modelBuilder.Entity<StrataSourceTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.StrataId });

            modelBuilder.Entity<StrataSourceTranslation>()
                .HasOne(pc => pc.Strata)
                .WithMany(p => p.StrataSourceTranslations)
                .HasForeignKey(pc => pc.StrataId);

            modelBuilder.Entity<StrataSourceTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.StrataSourceTranslations)
                .HasForeignKey(pc => pc.TranslationId);

            modelBuilder.Entity<StrataPopulationTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.StrataId });

            modelBuilder.Entity<StrataPopulationTranslation>()
                .HasOne(pc => pc.Strata)
                .WithMany(p => p.StrataPopulationTranslations)
                .HasForeignKey(pc => pc.StrataId);

            modelBuilder.Entity<StrataPopulationTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.StrataPopulationTranslations)
                .HasForeignKey(pc => pc.TranslationId);


            modelBuilder.Entity<PointLabelTranslation>()
                .HasKey(pc => new { pc.TranslationId, pc.PointId });

            modelBuilder.Entity<PointLabelTranslation>()
                .HasOne(pc => pc.Point)
                .WithMany(p => p.PointLabelTranslations)
                .HasForeignKey(pc => pc.PointId);

            modelBuilder.Entity<PointLabelTranslation>()
                .HasOne(pc => pc.Translation)
                .WithMany(c => c.PointLabelTranslations)
                .HasForeignKey(pc => pc.TranslationId);


        }
        

    
    }
}
