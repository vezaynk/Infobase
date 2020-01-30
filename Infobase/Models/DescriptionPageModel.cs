using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;
using System.Text.Json.Serialization;

namespace Infobase.Models
{
    public class MeasureAttribute {
        public string Name { get; set; }
        public string Body { get; set; }
    }
    public class DescriptionPageModel : PageModel
    {
        public ICollection<MeasureAttribute> MeasureAttributes { get; set; }
        public string DatasetName { get; set; }
        public string Title { get; set; }
        public bool Included { get; set; }

        public int Index { get; set; }

        public DescriptionPageModel(string datasetName, string languageCode, object dataObject): base(languageCode)
        {
            Title = (string)Metadata.FindTextPropertiesOnNode(dataObject, languageCode, TextAppearance.Filter).First().Value;
            MeasureAttributes = Metadata
                            .FindTextPropertiesOnTree(dataObject, languageCode, TextAppearance.MeasureDescription)
                            .Select(p => new MeasureAttribute { Name = p.Name, Body = (string)p.Value })
                            .ToList();

            Included = Metadata.GetIncludedState(dataObject);
            DatasetName = datasetName;

            Index = (dataObject as dynamic).Index;
        }
    }
}
