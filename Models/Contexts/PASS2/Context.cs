using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.Metadata;

namespace Models.Contexts.PASS2
{
    [Database("PASS2Context")]
    public class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<ColActivity> Activity { get; set; }


        public DbSet<ColSpecificMeasure> Measure { get; set; }
        public DbSet<ColIndicatorGroup> IndicatorGroup { get; set; }
        public DbSet<ColLifeCourse> LifeCourse { get; set; }
        public DbSet<ColIndicator> Indicator { get; set; }
        public DbSet<ColStrata> Strata { get; set; }
        public DbSet<ColDataBreakdowns> Point { get; set; }
        public DbSet<Master> Master { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AutoIncrement the indexes
            modelBuilder.ForNpgsqlUseIdentityColumns();

            /* Make the Indexes unique. Conflcits must FAIL. */
            modelBuilder.Entity<ColActivity>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<ColIndicatorGroup>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<ColIndicator>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<ColLifeCourse>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<ColSpecificMeasure>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<ColStrata>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<ColDataBreakdowns>()
                .HasAlternateKey(entity => entity.Index);
        }

    }

}