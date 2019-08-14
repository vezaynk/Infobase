// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models.Contexts.CMSIF;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Models.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20190814180928_CMSIFideidxxo.hr1")]
    partial class CMSIFideidxxohr1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Models.Contexts.CMSIF.ColDataBreakdowns", b =>
                {
                    b.Property<int>("ColDataBreakdownsId")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("CVRangeLower");

                    b.Property<double?>("CVRangeUpper");

                    b.Property<string>("ColDataBreakdownsNameEn");

                    b.Property<string>("ColDataBreakdownsNameFr");

                    b.Property<int>("ColMeasuresId");

                    b.Property<int?>("DefaultColDisaggregationId");

                    b.Property<int>("Index");

                    b.Property<string>("UnitLong");

                    b.Property<string>("UnitShort");

                    b.HasKey("ColDataBreakdownsId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ColMeasuresId");

                    b.HasIndex("DefaultColDisaggregationId");

                    b.ToTable("ColDataBreakdowns");
                });

            modelBuilder.Entity("Models.Contexts.CMSIF.ColDisaggregation", b =>
                {
                    b.Property<int>("ColDisaggregationId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CVInterpretation");

                    b.Property<double?>("CVValue");

                    b.Property<int>("ColDataBreakdownsId");

                    b.Property<string>("ColDisaggregationNameEn");

                    b.Property<string>("ColDisaggregationNameFr");

                    b.Property<int>("Index");

                    b.Property<double?>("ValueAverage");

                    b.Property<double?>("ValueLower");

                    b.Property<double?>("ValueUpper");

                    b.HasKey("ColDisaggregationId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ColDataBreakdownsId");

                    b.ToTable("ColDisaggregation");
                });

            modelBuilder.Entity("Models.Contexts.CMSIF.ColDomain", b =>
                {
                    b.Property<int>("ColDomainId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColDomainNameEn");

                    b.Property<string>("ColDomainNameFr");

                    b.Property<int?>("DefaultColIndicatorId");

                    b.Property<int>("Index");

                    b.HasKey("ColDomainId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("DefaultColIndicatorId");

                    b.ToTable("ColDomain");
                });

            modelBuilder.Entity("Models.Contexts.CMSIF.ColIndicator", b =>
                {
                    b.Property<int>("ColIndicatorId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ColDomainId");

                    b.Property<string>("ColIndicatorNameEn");

                    b.Property<string>("ColIndicatorNameFr");

                    b.Property<int?>("DefaultColMeasuresId");

                    b.Property<int>("Index");

                    b.HasKey("ColIndicatorId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ColDomainId");

                    b.HasIndex("DefaultColMeasuresId");

                    b.ToTable("ColIndicator");
                });

            modelBuilder.Entity("Models.Contexts.CMSIF.ColMeasures", b =>
                {
                    b.Property<int>("ColMeasuresId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ColIndicatorId");

                    b.Property<string>("ColMeasuresNameEn");

                    b.Property<string>("ColMeasuresNameFr");

                    b.Property<int?>("DefaultColDataBreakdownsId");

                    b.Property<bool>("Include");

                    b.Property<int>("Index");

                    b.Property<bool>("IsAggregator");

                    b.HasKey("ColMeasuresId");

                    b.HasAlternateKey("Index");

                    b.HasIndex("ColIndicatorId");

                    b.HasIndex("DefaultColDataBreakdownsId");

                    b.ToTable("ColMeasures");
                });

            modelBuilder.Entity("Models.Contexts.CMSIF.Master", b =>
                {
                    b.Property<int>("Index")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColAdditionalRemarks");

                    b.Property<string>("ColCILow95");

                    b.Property<string>("ColCIUpper95");

                    b.Property<string>("ColCV");

                    b.Property<string>("ColCVInterpretation");

                    b.Property<string>("ColDataAvailable");

                    b.Property<string>("ColDataBreakdowns");

                    b.Property<string>("ColDataSource");

                    b.Property<string>("ColDefinition");

                    b.Property<string>("ColDisaggregation");

                    b.Property<string>("ColDomain");

                    b.Property<string>("ColEstimateCalculation");

                    b.Property<string>("ColIndicator");

                    b.Property<string>("ColLabel");

                    b.Property<string>("ColMeasure");

                    b.Property<string>("ColMeasures");

                    b.Property<string>("ColNObs");

                    b.Property<string>("ColNW");

                    b.Property<string>("ColNotes");

                    b.Property<string>("ColObs");

                    b.Property<string>("ColPercent");

                    b.Property<string>("ColPopulation");

                    b.Property<string>("ColSource");

                    b.HasKey("Index");

                    b.ToTable("Master");
                });

            modelBuilder.Entity("Models.Contexts.CMSIF.ColDataBreakdowns", b =>
                {
                    b.HasOne("Models.Contexts.CMSIF.ColMeasures", "ColMeasures")
                        .WithMany("ColDataBreakdowns")
                        .HasForeignKey("ColMeasuresId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Contexts.CMSIF.ColDisaggregation", "DefaultColDisaggregation")
                        .WithMany()
                        .HasForeignKey("DefaultColDisaggregationId");
                });

            modelBuilder.Entity("Models.Contexts.CMSIF.ColDisaggregation", b =>
                {
                    b.HasOne("Models.Contexts.CMSIF.ColDataBreakdowns", "ColDataBreakdowns")
                        .WithMany("ColDisaggregations")
                        .HasForeignKey("ColDataBreakdownsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Contexts.CMSIF.ColDomain", b =>
                {
                    b.HasOne("Models.Contexts.CMSIF.ColIndicator", "DefaultColIndicator")
                        .WithMany()
                        .HasForeignKey("DefaultColIndicatorId");
                });

            modelBuilder.Entity("Models.Contexts.CMSIF.ColIndicator", b =>
                {
                    b.HasOne("Models.Contexts.CMSIF.ColDomain", "ColDomain")
                        .WithMany("ColIndicators")
                        .HasForeignKey("ColDomainId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Contexts.CMSIF.ColMeasures", "DefaultColMeasures")
                        .WithMany()
                        .HasForeignKey("DefaultColMeasuresId");
                });

            modelBuilder.Entity("Models.Contexts.CMSIF.ColMeasures", b =>
                {
                    b.HasOne("Models.Contexts.CMSIF.ColIndicator", "ColIndicator")
                        .WithMany("ColMeasures")
                        .HasForeignKey("ColIndicatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Models.Contexts.CMSIF.ColDataBreakdowns", "DefaultColDataBreakdowns")
                        .WithMany()
                        .HasForeignKey("DefaultColDataBreakdownsId");
                });
#pragma warning restore 612, 618
        }
    }
}
