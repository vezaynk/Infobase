using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infobase.Models;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Diagnostics;
using Npgsql.EntityFrameworkCore.PostgreSQL.Update.Internal;
using Microsoft.EntityFrameworkCore.Update;
using Npgsql.EntityFrameworkCore.PostgreSQL.Design.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding;

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