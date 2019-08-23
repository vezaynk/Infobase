
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.Metadata;

namespace Models.Contexts.CMSIF
{
    [Database("CMSIFContext")]
    public class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {
        }

            public DbSet<ColDomain> ColDomain { get; set; }
            public DbSet<ColIndicator> ColIndicator { get; set; }
            public DbSet<ColMeasures> ColMeasures { get; set; }
            public DbSet<ColDataBreakdowns> ColDataBreakdowns { get; set; }
            public DbSet<ColDisaggregation> ColDisaggregation { get; set; }
        
        public DbSet<Master> Master { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AutoIncrement the indexes
            modelBuilder.ForNpgsqlUseIdentityColumns();

                modelBuilder.Entity<ColDomain>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColIndicator>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColMeasures>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColDataBreakdowns>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColDisaggregation>().HasAlternateKey(entity => entity.Index);
        }

    }

}