using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infobase.Models
{
    public class ChartPageModel : PageModel
    {
        public ChartData ChartData { get; set; }
        public List<DropdownMenuModel> Filters { get; set; }
        public string DatasetName { get; set; }

        public ChartPageModel(string datasetName, string languageCode, ChartData chartData): base(languageCode)
        {
            this.Filters = new List<DropdownMenuModel>();
            this.ChartData = chartData;
            this.DatasetName = datasetName;
        }
    }

    public class DropdownMenuModel
    {
        public string Name { get; set; }
        public List<DropdownItem> Items { get; set; } = new List<DropdownItem>();
        public int Selected { get; set; }
        public DropdownMenuModel(string name, IEnumerable<DropdownItem> items, int selected)
        {
            this.Name = name;
            this.Items.AddRange(items);
            this.Selected = selected;
        }
    }

    public class DropdownItem
    {
        public int Value { get; set; }
        public string Text { get; set; }

    }

    public class TabItem
    {
        public string Text { get; set; }
        public string Name { get; set; }

        public TabItem(string name, string text)
        {
            this.Text = text;
            this.Name = name;
        }
    }
}
