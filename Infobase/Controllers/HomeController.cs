using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infobase.Models;

namespace Infobase.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
        //     var mg = new ModelGenerator();
        //     mg.DatasetName = "PASS";
            // mg.Models = new Collection<Model> {
            //     new Model {
            //         ModelName = "Activity",
            //         ModelProperties = new Collection<ModelProperty> {
            //             new ModelProperty {
            //                 PropertyName = "Name",
            //                 PropertyType = ModelModifier.Text
            //             }
            //         }
            //     },
                
            //     new Model {
            //         ModelName = "Indicator Group",
            //         ModelProperties = new Collection<ModelProperty> {
            //             new ModelProperty {
            //                 PropertyName = "Name",
            //                 PropertyType = ModelModifier.Text
            //             }
            //         }
            //     },
                
            //     new Model {
            //         ModelName = "Life Course",
            //         ModelProperties = new Collection<ModelProperty> {
            //             new ModelProperty {
            //                 PropertyName = "Name",
            //                 PropertyType = ModelModifier.Text
            //             }
            //         }
            //     },
                
            //     new Model {
            //         ModelName = "Indicator",
            //         ModelProperties = new Collection<ModelProperty> {
            //             new ModelProperty {
            //                 PropertyName = "Name",
            //                 PropertyType = ModelModifier.Text
            //             }
            //         }
            //     },
                
            //     new Model {
            //         ModelName = "Measure",
            //         ModelProperties = new Collection<ModelProperty> {
            //             new ModelProperty {
            //                 PropertyName = "Name",
            //                 PropertyType = ModelModifier.Text
            //             }
            //         }
            //     },
                
            //     new Model {
            //         ModelName = "Strata",
            //         ModelProperties = new Collection<ModelProperty> {
            //             new ModelProperty {
            //                 PropertyName = "Name",
            //                 PropertyType = ModelModifier.Text
            //             }
            //         }
            //     }
            // };

            //Console.WriteLine(mg.Generate());
            return Json(new {});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
