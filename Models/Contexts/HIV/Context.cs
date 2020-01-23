
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Models.Metadata;

namespace Models.Contexts.HIV
{
    [Database("HIVContext")]
    public class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {
        }

            public DbSet<ColIndicatorParent> ColIndicatorParent { get; set; }
            public DbSet<ColSecondaryBreakdownName> ColSecondaryBreakdownName { get; set; }
            public DbSet<ColLocation> ColLocation { get; set; }
            public DbSet<ColValue> ColValue { get; set; }
        
        public DbSet<Master> Master { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AutoIncrement the indexes
            modelBuilder.ForNpgsqlUseIdentityColumns();

                modelBuilder.Entity<ColIndicatorParent>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColSecondaryBreakdownName>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColLocation>().HasAlternateKey(entity => entity.Index);
                modelBuilder.Entity<ColValue>().HasAlternateKey(entity => entity.Index);
        }

    }

}