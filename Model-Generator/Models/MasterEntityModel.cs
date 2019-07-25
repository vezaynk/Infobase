using System.Collections.Generic;

namespace Model_Generator
{
    public class MasterEntityModel
    {
        public string DatasetName { get; set; }
        public IEnumerable<string> Properties { get; set; }
    }
}