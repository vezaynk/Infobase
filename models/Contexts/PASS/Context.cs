using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.Metadata;

namespace Models.Contexts.PASS
{
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

        public DbSet<Activity> Activity { get; set; }


        public DbSet<Measure> Measure { get; set; }
        public DbSet<IndicatorGroup> IndicatorGroup { get; set; }
        public DbSet<LifeCourse> LifeCourse { get; set; }
        public DbSet<Indicator> Indicator { get; set; }
        public DbSet<Strata> Strata { get; set; }
        public DbSet<Point> Point { get; set; }
        public DbSet<Master> Master { get; set; }


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
        }

    }

}