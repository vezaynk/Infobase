using System.Collections.Generic;

namespace model_generator
{
    public class MasterEntityModel
    {
        public string DatasetName { get; set; }
        public IEnumerable<string> Properties { get; set; }
    }
}