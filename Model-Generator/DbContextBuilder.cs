using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using CSharpLoader;
using Microsoft.EntityFrameworkCore;

namespace Model_Generator
{
    public static class DbContextBuilder
    {
        public static DbContext GetDBContext(Assembly dbContextASM, string dbContextFullName, Action<DbContextOptionsBuilder> configureOptionBuilder)
        {
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

        public static Assembly BuildDbContextAssembly(string datasetName)
        {
            var dbContextIMC = new InMemoryCompiler();
            
            dbContextIMC.AddFile($"../Models/Contexts/{datasetName}/Context.cs");
            dbContextIMC.AddFile($"../Models/Contexts/{datasetName}/Master.cs");
            dbContextIMC.AddFile($"../Models/Contexts/{datasetName}/Models.cs");

            return dbContextIMC.CompileAssembly();
        }

    }
}