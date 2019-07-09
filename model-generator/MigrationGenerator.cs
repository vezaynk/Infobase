using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Design.Internal;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Design;

namespace model_generator
{
    public class MigrationGenerator
    {
        public DbContext DbContext { get; set; }
        public MigrationGenerator(DbContext dbContext)
        {
            DbContext = dbContext;
        }
        
        public ScaffoldedMigration CreateMigration()
        {
            var designTimeServiceCollection = new ServiceCollection()
                .AddEntityFrameworkDesignTimeServices()
                .AddDbContextDesignTimeServices(DbContext);
            new NpgsqlDesignTimeServices().ConfigureDesignTimeServices(designTimeServiceCollection);

            var designTimeServiceProvider = designTimeServiceCollection.BuildServiceProvider();

            var migrationsScaffolder = designTimeServiceProvider.GetService<IMigrationsScaffolder>();

            var migration = migrationsScaffolder.ScaffoldMigration(
                Path.GetRandomFileName(),
                "Infobase");

            return migration;
        }
    }
}