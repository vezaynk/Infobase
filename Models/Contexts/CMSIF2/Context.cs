using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.Metadata;

namespace Models.Contexts.CMSIF2
{
    [Database("CMSIF2Context")]
    public class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<ColDataBreakdowns> ColDataBreakdowns { get; set; }
        public DbSet<ColDisaggregation> ColDisaggregation { get; set; }
        public DbSet<ColDomain> ColDomain { get; set; }
        public DbSet<ColIndicator> ColIndicator { get; set; }
        public DbSet<ColMeasures> ColMeasures { get; set; }
        public DbSet<Master> Master { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AutoIncrement the indexes
            modelBuilder.ForNpgsqlUseIdentityColumns();

            /* Make the Indexes unique. Conflcits must FAIL. */
            modelBuilder.Entity<ColDataBreakdowns>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<ColDisaggregation>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<ColDomain>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<ColIndicator>()
                .HasAlternateKey(entity => entity.Index);

            modelBuilder.Entity<ColMeasures>()
                .HasAlternateKey(entity => entity.Index);
        }

    }

}