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

namespace model_generator2
{
    public class DummyTypeMapper : IRelationalTypeMapper
    {
        public IByteArrayRelationalTypeMapper ByteArrayMapper { get; }
        public IStringRelationalTypeMapper StringMapper { get; }
        public RelationalTypeMapping FindMapping(Microsoft.EntityFrameworkCore.Metadata.IProperty property)
        {
            throw new NotImplementedException("This is a dummy");
        }
        public RelationalTypeMapping FindMapping(string storeType)
        {
            throw new NotImplementedException("This is a dummy");
        }
        public RelationalTypeMapping FindMapping(Type clrType)
        {
            throw new NotImplementedException("This is a dummy");
        }
        public void ValidateTypeName(string storeType)
        {
            throw new NotImplementedException("This is a dummy");
        }
        public bool IsTypeMapped(Type clrType)
        {
            throw new NotImplementedException("This is a dummy");
        }
    }
    public class MigrationGenerator<T> : IDisposable where T : DbContext
    {
        public IDbContextOptions Options { get; set; }
        public DbContext DbContext { get; set; }
        public MigrationGenerator(IDbContextOptions options)
        {
            Options = options;
            DbContext = (DbContext)Activator.CreateInstance(typeof(T), options);
        }
        public void Dispose()
        {
            DbContext.Dispose();
        }

        public ScaffoldedMigration CreateMigration()
        {
            var reporter = new OperationReporter(
            new OperationReportHandler(
                m => Console.WriteLine("  error: " + m),
                m => Console.WriteLine("   warn: " + m),
                m => Console.WriteLine("   info: " + m),
                m => Console.WriteLine("verbose: " + m)));

            var designTimeServiceCollection = new ServiceCollection()
                .AddEntityFrameworkDesignTimeServices()
                .AddDbContextDesignTimeServices(DbContext);
            new NpgsqlDesignTimeServices().ConfigureDesignTimeServices(designTimeServiceCollection);

            var designTimeServiceProvider = designTimeServiceCollection.BuildServiceProvider();

            var migrationsScaffolder = designTimeServiceProvider.GetService<IMigrationsScaffolder>();
            // migrationsScaffolder.ScaffoldMigration(...)
            // migrationsScaffolder.RemoveMigration(...)

            var reverseEngineerScaffolder = designTimeServiceProvider.GetService<IReverseEngineerScaffolder>();
            // reverseEngineerScaffolder.ScaffoldModel(...)

            // var designTimeServices = new ServiceCollection()
            //     .AddSingleton(DbContext.GetService<IHistoryRepository>())
            //     .AddSingleton(DbContext.GetService<IMigrationsIdGenerator>())
            //     .AddSingleton(DbContext.GetService<IMigrationsModelDiffer>())
            //     .AddSingleton(DbContext.GetService<IMigrationsAssembly>())
            //     .AddSingleton(DbContext.Model)
            //     .AddSingleton(DbContext.GetService<ICurrentDbContext>())
            //     .AddSingleton(DbContext.GetService<IDatabaseProvider>())
            //     .AddSingleton<ValueConverterSelectorDependencies>()
            //     .AddSingleton<IValueConverterSelector, ValueConverterSelector>()
            //     .AddSingleton<MigrationsCodeGeneratorDependencies>()
            //     .AddSingleton<TypeMappingSourceDependencies>()
            //     .AddSingleton<RelationalTypeMappingSourceDependencies>()
            //     .AddSingleton<RelationalSqlGenerationHelperDependencies>()
            //     .AddSingleton<ISqlGenerationHelper, NpgsqlSqlGenerationHelper>()
            //     .AddSingleton<IRelationalTypeMappingSource, NpgsqlTypeMappingSource>()
            //     .AddSingleton<ICSharpHelper, CSharpHelper>()
            //     .AddSingleton<CSharpMigrationOperationGeneratorDependencies>()
            //     .AddSingleton<ICSharpMigrationOperationGenerator, CSharpMigrationOperationGenerator>()
            //     .AddSingleton<IMigrationsCodeGeneratorSelector, MigrationsCodeGeneratorSelector>()
            //     .AddSingleton<CSharpSnapshotGeneratorDependencies>()
            //     .AddSingleton<ICSharpSnapshotGenerator, CSharpSnapshotGenerator>()
            //     .AddSingleton<CSharpMigrationsGeneratorDependencies>()
            //     .AddSingleton<IMigrationsCodeGenerator, CSharpMigrationsGenerator>()
            //     .AddSingleton<IOperationReporter>(reporter)
            //     .AddSingleton<ISnapshotModelProcessor, SnapshotModelProcessor>()
            //     .AddSingleton<IDbContextOptions>(Options)
            //     .AddSingleton<ILoggingOptions>(new LoggingOptions())
            //     .AddSingleton<ILoggerFactory, LoggerFactory>()
            //     .AddSingleton<DiagnosticSource>(new DiagnosticListener("my diagnostics listener"))
            //     .AddSingleton<IDiagnosticsLogger<DbLoggerCategory.Database.Transaction>, DiagnosticsLogger<DbLoggerCategory.Database.Transaction>>()
            //     .AddSingleton<IDiagnosticsLogger<DbLoggerCategory.Database.Connection>, DiagnosticsLogger<DbLoggerCategory.Database.Connection>>()
            //     .AddSingleton<IDiagnosticsLogger<DbLoggerCategory.Database.Command>, DiagnosticsLogger<DbLoggerCategory.Database.Command>>()
            //     .AddSingleton<IDiagnosticsLogger<DbLoggerCategory.Infrastructure>, DiagnosticsLogger<DbLoggerCategory.Infrastructure>>()
            //     .AddSingleton<IDiagnosticsLogger<DbLoggerCategory.Migrations>, DiagnosticsLogger<DbLoggerCategory.Migrations>>()
            //     .AddSingleton<INamedConnectionStringResolver, NamedConnectionStringResolver>()
            //     .AddSingleton<RelationalTransactionFactoryDependencies>()
            //     .AddSingleton<IRelationalTransactionFactory, RelationalTransactionFactory>()
            //     .AddSingleton<RelationalConnectionDependencies>()
            //     .AddSingleton<IRelationalConnection, NpgsqlRelationalConnection>()
            //     .AddSingleton<IRelationalCommandBuilderFactory, RelationalCommandBuilderFactory>()
            //     .AddSingleton<IRelationalTypeMapper, DummyTypeMapper>()
            //     .AddSingleton<MigrationsSqlGeneratorDependencies>()
            //     .AddSingleton<UpdateSqlGeneratorDependencies>()
            //     .AddSingleton<ISingletonUpdateSqlGenerator, NpgsqlUpdateSqlGenerator>()
            //     .AddSingleton<IMigrationsSqlGenerator, MigrationsSqlGenerator>()
            //     .AddSingleton<IMigrationCommandExecutor, MigrationCommandExecutor>()
            //     .AddSingleton<ExecutionStrategyDependencies>()
            //     .AddSingleton<IExecutionStrategyFactory, ExecutionStrategyFactory>()
            //     .AddSingleton<RelationalDatabaseCreatorDependencies>()
            //     .AddSingleton<INpgsqlRelationalConnection, NpgsqlRelationalConnection>()
            //     .AddSingleton<ParameterNameGeneratorDependencies>()
            //     .AddSingleton<IParameterNameGeneratorFactory, ParameterNameGeneratorFactory>()
            //     .AddSingleton<IRawSqlCommandBuilder, RawSqlCommandBuilder>()
            //     .AddSingleton<IDatabaseCreator, NpgsqlDatabaseCreator>()
            //     .AddSingleton<IMigrator, Migrator>()
            //     .AddSingleton<MigrationsScaffolderDependencies>()
            //     .AddSingleton<MigrationsScaffolder>()
            //     .BuildServiceProvider();


            //var scaffolder = designTimeServices.GetRequiredService<MigrationsScaffolder>();

            var migration = migrationsScaffolder.ScaffoldMigration(
                Path.GetRandomFileName(),
                "Infobase");

            return migration;
        }
    }
}