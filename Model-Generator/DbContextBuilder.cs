using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using CSharpLoader;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Design;

namespace Model_Generator
{
    public static class DbContextBuilder
    {
        public static DbContext GetDBContext(Assembly dbContextASM, string dbContextFullName, Action<DbContextOptionsBuilder> configureOptionBuilder)
        {
            // Get type of the dbContext. This is needed to use any generic methods involving a dbContexts
            Type dbContextType = dbContextASM.GetType(dbContextFullName);
            
            // Generic OptionBuilder type to work with the loaded DbContext 
            Type optionBuilderType = typeof(DbContextOptionsBuilder<>).MakeGenericType(new Type[] { dbContextType });
            // Instance of optionBuilder
            DbContextOptionsBuilder optionsBuilder = (DbContextOptionsBuilder)Activator.CreateInstance(optionBuilderType);

            // Let caller configure it
            configureOptionBuilder(optionsBuilder);

            // Create dbContext using configured optionBuilder
            var dbContext = (DbContext)Activator.CreateInstance(dbContextType, new object[] {
                    optionsBuilder.Options
                });

            return dbContext;
        }

        public static DbContext GetDBContextFromSource(string name, string path, Action<DbContextOptionsBuilder, Assembly> configureOptionBuilder, IEnumerable<ScaffoldedMigration> migrations = null, string migrationsDirectory = null)
        {

            var asm = BuildAssembly(name, path, migrations, migrationsDirectory);
            // Extract DbContext from the resulting assembly
            return GetDBContext(asm, $"Models.Contexts.{name}.Context", ob => configureOptionBuilder(ob, asm));
        }

        public static Assembly BuildAssembly(string name, string path, IEnumerable<ScaffoldedMigration> migrations = null, string migrationsDirectory = null)
        {
            var dbContextIMC = new InMemoryCompiler();

            // The Context file is the main file that is analyzed in order to determine what needs to be migrated.
            // In fact, the actual migration files are not necessary to generate migrations.
            // They are, however, necessary to apply them to the database.
            dbContextIMC.AddFile(Path.Join(path, name, "Context.cs"));

            // The actual entities are stored in Models.cs
            dbContextIMC.AddFile(Path.Join(path, name, "Models.cs"));

            // The actual entities are stored in Models.cs
            dbContextIMC.AddFile(Path.Join(path, name, "Master.cs"));

            // Sometimes, its convenient to load in a migration without saving it to disk
            if (migrations != null)
            {
                foreach (var migration in migrations)
                {
                    // Not sure what it does, but it comes with the others
                    dbContextIMC.AddCodeBody(migration.MetadataCode);
                    // Necessary to apply migration
                    dbContextIMC.AddCodeBody(migration.MigrationCode);
                    // Necessary to generate new migrations
                    dbContextIMC.AddCodeBody(migration.SnapshotCode);
                }
            }

            // If there is a known migration directory, we should load all of them
            // if (migrationsDirectory != null)
            //     foreach (string filename in Directory.GetFiles(migrationsDirectory, "*.cs"))
            //     {
            //         dbContextIMC.AddFile(filename);
            //     }

            return dbContextIMC.CompileAssembly();
        }

    }
}