using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infobase.Models;
using System.Reflection;
using RazorLight;
using System.IO;

namespace Infobase.Automation
{

    [Flags]
    public enum ModelModifier
    {
        CVBoundries,
        Include,
        Aggregator,
        Data
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class Modifier : Attribute
    {
        public ModelModifier Modifiers { get; set; }
        public Modifier(ModelModifier modifiers)
        {
            Modifiers = modifiers;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ParentOf : Attribute
    {
        public ParentOf(Type child)
        {
            Child = child;
        }
        public ParentOf(Type child, bool includeDefault) : this(child)
        {
            IncludeDefault = includeDefault;
        }
        public bool IncludeDefault { get; set; } = true;
        public Type Child { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ChildOf : Attribute
    {
        public ChildOf(Type parent)
        {
            Parent = parent;
        }
        public Type Parent { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TextData : Attribute
    {
        public TextData(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
    public class ModelParser
    {

        public static async void GetModelsByDataset(string dataset)
        {
            var models = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == $"Infobase.Models.{dataset}").OrderByDescending(m =>
            {
                int numberOfChildren = 0;
                Type currentModel = m;
                while (currentModel != null)
                {
                    currentModel = currentModel.GetCustomAttribute<ParentOf>()?.Child;
                    numberOfChildren++;
                }
                return numberOfChildren;
            }).AsEnumerable();



            var engine = new RazorLightEngineBuilder()
                        .UseFilesystemProject($"{Directory.GetCurrentDirectory()}/Automation/Templates")
                        .UseMemoryCachingProvider()
                        .Build();

            Console.WriteLine(await engine.CompileRenderAsync("Context.cshtml", (dataset, models)));

            foreach (var a in models)
            {

                string result = await engine.CompileRenderAsync("Entity.cshtml", a);
                Console.WriteLine(a);
                // var childAttribute = a.GetCustomAttribute<ChildOf>();
                // if (childAttribute == null)
                // {
                //     Console.WriteLine("Null");
                // }
                // else
                // {
                //     Console.WriteLine($"Parent Name: {childAttribute.Parent.Name}");
                // }
                // var parentAttribute = a.GetCustomAttribute<ParentOf>();
                // if (parentAttribute == null)
                // {
                //     Console.WriteLine("Null");
                // }
                // else
                // {
                //     Console.WriteLine($"Child Name: {parentAttribute.Child.Name}");
                // }

                // var textDataAttributes = a.GetCustomAttributes<TextData>();
                // foreach (var textDataAttribute in textDataAttributes)
                // {
                //     Console.WriteLine($"Text Name: {textDataAttribute.Name}");
                // }


                // var modifierAttribute = a.GetCustomAttribute<Modifier>();
                // if (modifierAttribute == null)
                // {
                //     Console.WriteLine("Null");
                // }
                // else
                // {
                //     Console.WriteLine($"Modifiers: {modifierAttribute}");
                // }

                // modifierAttribute.Modifiers.HasFlag(ModelModifier.Aggregator)

            }
            var x = typeof(Models.PASS.Activity);
        }
    }
    // public class ModelProperty
    // {
    //     public string PropertyName { get; set; }
    //     public ModelModifier PropertyType { get; set; }
    // }

    // public class Model
    // {
    //     public string ModelName { get; set; }
    //     public ICollection<ModelProperty> ModelProperties { get; set; }
    // }

    // public class ModelGenerator
    // {
    //     public string DatasetName { get; set; }
    //     public ICollection<Model> Models { get; set; }

    //     public string Generate()
    //     {
    //         var zipped = this.Models.Zip(this.Models.Skip(1).Append(null), (parent, child) =>
    //         {
    //             return $"{parent.ModelName} => {child?.ModelName ?? "Point"}";
    //         });
    //         return zipped.Aggregate((a, b) => $"{a}\n{b}");
    //     }
    // }
}
