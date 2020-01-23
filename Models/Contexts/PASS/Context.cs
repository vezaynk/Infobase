
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.Metadata;

namespace Models.Contexts.PASS
{
    [Database("PASSContext")]
    public class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {
        }

            public DbSet<ColActivity> ColActivity { get; set; }
            public DbSet<ColIndicatorGroup> ColIndicatorGroup { get; set; }
            public DbSet<ColLifeCourse> ColLifeCourse { get; set; }
            public DbSet<ColIndicator> ColIndicator { get; set; }
            public DbSet<ColSpecificMeasure1> ColSpecificMeasure1 { get; set; }
            public DbSet<ColDataBreakdowns> ColDataBreakdowns { get; set; }
            public DbSet<ColDisaggregation> ColDisaggregation { get; set; }
        
        public DbSet<Master> Master { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AutoIncrement the indexes
            modelBuilder.ForNpgsqlUseIdentityColumns();

                modelBuilder.Entity<ColActivity>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColIndicatorGroup>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColLifeCourse>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColIndicator>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColSpecificMeasure1>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColDataBreakdowns>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColDisaggregation>().HasAlternateKey(entity => entity.Index);
        }

    }

}