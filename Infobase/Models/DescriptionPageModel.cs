using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Metadata;

namespace Infobase.Models
{
    public class DescriptionPageModel : PageModel
    {
        public Dictionary<string, string> Attributes { get; set; }
        public string DatasetName { get; set; }
        public string Title { get; set; }
        public bool Included { get; set; }

        public int Index { get; set; }

        public DescriptionPageModel(string datasetName, string languageCode, object dataObject): base(languageCode)
        {
            Title = (string)Metadata.FindTextPropertiesOnNode(dataObject, languageCode, TextAppearance.Filter).First().Value;
            Attributes = Metadata
                            .FindTextPropertiesOnTree(dataObject, languageCode, TextAppearance.MeasureDescription)
                            .ToDictionary(p => p.Name, p => (string)p.Value);

            Included = Metadata.GetIncludedState(dataObject);
            DatasetName = datasetName;

            Index = (dataObject as dynamic).Index;
        }
    }
}
