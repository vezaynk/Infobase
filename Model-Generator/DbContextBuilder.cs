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
            // Recompiling the context is not necessary. We can use the existing as long as we use the the recompiled migrations assembly later on
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

        public static Assembly BuildMigrationsAssembly(string datasetName, IEnumerable<ScaffoldedMigration> migrations)
        {
            var dbContextIMC = new InMemoryCompiler();
            
            dbContextIMC.AddFile($"../Models/Contexts/{datasetName}/Context.cs");
            dbContextIMC.AddFile($"../Models/Contexts/{datasetName}/Master.cs");
            dbContextIMC.AddFile($"../Models/Contexts/{datasetName}/Models.cs");
            foreach (var migration in migrations)
            {
                // Not sure what it does, but it comes with the others
                dbContextIMC.AddCodeBody(migration.MetadataCode);
                // Necessary to apply migration
                dbContextIMC.AddCodeBody(migration.MigrationCode);
                // Necessary to generate new migrations
                dbContextIMC.AddCodeBody(migration.SnapshotCode);
            }

            return dbContextIMC.CompileAssembly();
        }

    }
}