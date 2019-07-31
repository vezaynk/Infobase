// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Contexts.PASS2;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Models.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Models.Contexts.PASS2.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivityNameEn");

                    b.Property<string>("ActivityNameFr");

                    b.Property<int?>("DefaultIndicatorGroupId");

                    b.Property<int>("Index");

                    b.HasKey("ActivityId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("DefaultIndicatorGroupId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.DataBreakdowns", b =>
                {
                    b.Property<int>("DataBreakdownsId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CVRangeLower");

                    b.Property<double>("CVRangeUpper");

                    b.Property<string>("DataBreakdownsNameEn");

                    b.Property<string>("DataBreakdownsNameFr");

                    b.Property<int?>("DefaultStrataId");

                    b.Property<int>("Index");

                    b.Property<int>("SpecificMeasureId");

                    b.Property<string>("UnitLong");

                    b.Property<string>("UnitShort");

                    b.HasKey("DataBreakdownsId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("DefaultStrataId");

                    b.HasIndex("SpecificMeasureId");

                    b.ToTable("Point");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.Indicator", b =>
                {
                    b.Property<int>("IndicatorId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DefaultSpecificMeasureId");

                    b.Property<int>("Index");

                    b.Property<string>("IndicatorNameEn");

                    b.Property<string>("IndicatorNameFr");

                    b.Property<int>("LifeCourseId");

                    b.HasKey("IndicatorId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("DefaultSpecificMeasureId");

                    b.HasIndex("LifeCourseId");

                    b.ToTable("Indicator");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.IndicatorGroup", b =>
                {
                    b.Property<int>("IndicatorGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityId");

                    b.Property<int?>("DefaultLifeCourseId");

                    b.Property<int>("Index");

                    b.Property<string>("IndicatorGroupNameEn");

                    b.Property<string>("IndicatorGroupNameFr");

                    b.HasKey("IndicatorGroupId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ActivityId");

                    b.HasIndex("DefaultLifeCourseId");

                    b.ToTable("IndicatorGroup");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.LifeCourse", b =>
                {
                    b.Property<int>("LifeCourseId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DefaultIndicatorId");

                    b.Property<int>("Index");

                    b.Property<int>("IndicatorGroupId");

                    b.Property<string>("LifeCourseNameEn");

                    b.Property<string>("LifeCourseNameFr");

                    b.HasKey("LifeCourseId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("DefaultIndicatorId");

                    b.HasIndex("IndicatorGroupId");

                    b.ToTable("LifeCourse");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.Master", b =>
                {
                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Activity");

                    b.Property<string>("AdditionalRemarks");

                    b.Property<string>("CILow95");

                    b.Property<string>("CIUpper95");

                    b.Property<string>("CV");

                    b.Property<string>("CVInterpretation");

                    b.Property<string>("CVRangeLower");

                    b.Property<string>("CVRangeUpper");

                    b.Property<string>("Data");

                    b.Property<string>("DataAvailable");

                    b.Property<string>("DataBreakdowns");

                    b.Property<string>("DataSource1");

                    b.Property<string>("DataSource2");

                    b.Property<string>("DataSource3");

                    b.Property<string>("Defintion");

                    b.Property<string>("EstimateCalculation");

                    b.Property<string>("FeatureData");

                    b.Property<string>("Indicator");

                    b.Property<string>("IndicatorGroup");

                    b.Property<string>("IsAggregator");

                    b.Property<string>("IsIncluded");

                    b.Property<string>("LifeCourse");

                    b.Property<string>("Notes");

                    b.Property<string>("PTTableLabel");

                    b.Property<string>("Population1");

                    b.Property<string>("Population2");

                    b.Property<string>("SpecificMeasure");

                    b.Property<string>("SpecificMeasure2");

                    b.Property<string>("Strata");

                    b.Property<string>("UnitLabel2");

                    b.Property<string>("UnitLabelLong");

                    b.HasKey("Index");

                    b.ToTable("Master");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.SpecificMeasure", b =>
                {
                    b.Property<int>("SpecificMeasureId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DefaultDataBreakdownsId");

                    b.Property<bool>("Included");

                    b.Property<int>("Index");

                    b.Property<int>("IndicatorId");

                    b.Property<string>("SpecificMeasureNameEn");

                    b.Property<string>("SpecificMeasureNameFr");

                    b.HasKey("SpecificMeasureId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("DefaultDataBreakdownsId");

                    b.HasIndex("IndicatorId");

                    b.ToTable("Measure");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.Strata", b =>
                {
                    b.Property<int>("StrataId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CVInterpretation");

                    b.Property<int?>("CVValue");

                    b.Property<int>("DataBreakdownsId");

                    b.Property<int>("Index");

                    b.Property<string>("StrataNameEn");

                    b.Property<string>("StrataNameFr");

                    b.Property<double?>("ValueAverage");

                    b.Property<double?>("ValueLower");

                    b.Property<double?>("ValueUpper");

                    b.HasKey("StrataId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("DataBreakdownsId");

                    b.ToTable("Strata");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.Activity", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.IndicatorGroup", "DefaultIndicatorGroup")
                        .WithMany()
                        .HasForeignKey("DefaultIndicatorGroupId");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.DataBreakdowns", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.Strata", "DefaultStrata")
                        .WithMany()
                        .HasForeignKey("DefaultStrataId");

                    b.HasOne("Models.Contexts.PASS2.SpecificMeasure", "SpecificMeasure")
                        .WithMany("DataBreakdowns")
                        .HasForeignKey("SpecificMeasureId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Contexts.PASS2.Indicator", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.SpecificMeasure", "DefaultSpecificMeasure")
                        .WithMany()
                        .HasForeignKey("DefaultSpecificMeasureId");

                    b.HasOne("Models.Contexts.PASS2.LifeCourse", "LifeCourse")
                        .WithMany("Indicators")
                        .HasForeignKey("LifeCourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Contexts.PASS2.IndicatorGroup", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.Activity", "Activity")
                        .WithMany("IndicatorGroups")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Contexts.PASS2.LifeCourse", "DefaultLifeCourse")
                        .WithMany()
                        .HasForeignKey("DefaultLifeCourseId");
                });

            modelBuilder.Entity("Models.Contexts.PASS2.LifeCourse", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.Indicator", "DefaultIndicator")
                        .WithMany()
                        .HasForeignKey("DefaultIndicatorId");

                    b.HasOne("Models.Contexts.PASS2.IndicatorGroup", "IndicatorGroup")
                        .WithMany("LifeCourses")
                        .HasForeignKey("IndicatorGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Contexts.PASS2.SpecificMeasure", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.DataBreakdowns", "DefaultDataBreakdowns")
                        .WithMany()
                        .HasForeignKey("DefaultDataBreakdownsId");

                    b.HasOne("Models.Contexts.PASS2.Indicator", "Indicator")
                        .WithMany("SpecificMeasures")
                        .HasForeignKey("IndicatorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Contexts.PASS2.Strata", b =>
                {
                    b.HasOne("Models.Contexts.PASS2.DataBreakdowns", "DataBreakdowns")
                        .WithMany("Strata")
                        .HasForeignKey("DataBreakdownsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
