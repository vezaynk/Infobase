using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Design.Internal;

namespace Model_Generator
{
    public class MigrationGenerator
    {
        public DbContext DbContext { get; set; }
        public string MigrationName { get; set; }
        public MigrationGenerator(DbContext dbContext, string migrationName = null)
        {
            DbContext = dbContext;
            // Use a random filename if none is specified
            MigrationName = migrationName ?? Path.GetRandomFileName();
        }
        public ScaffoldedMigration CreateMigration()
        {
            // Extract necessary objects to perform migration
            var designTimeServiceCollection = new ServiceCollection()
                .AddEntityFrameworkDesignTimeServices()
                .AddDbContextDesignTimeServices(DbContext);

            new NpgsqlDesignTimeServices().ConfigureDesignTimeServices(designTimeServiceCollection);

            var designTimeServiceProvider = designTimeServiceCollection.BuildServiceProvider();

            var migrationsScaffolder = designTimeServiceProvider.GetService<IMigrationsScaffolder>();

            // This builds the *incrementental migration* to achieve parity with schema
            var migration = migrationsScaffolder.ScaffoldMigration(
                MigrationName,
                "Models");

            return migration;
        }
    }
}