using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models
{
    public class ChartPageModel : PageModel
    {
        public ChartData ChartData { get; set; }
        public List<DropdownMenuModel> filters;

        public ChartPageModel(bool IsFrench): base(IsFrench)
        {
            filters = new List<DropdownMenuModel>();
        }
    }

    public class DropdownMenuModel
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<DropdownItem> Items { get; set; } = new List<DropdownItem>();
        public DropdownMenuModel(string name, string id, IEnumerable<DropdownItem> items)
        {
            this.Name = name;
            this.Id = id;
            this.Items.AddRange(items);
        }
    }

    public class DropdownItem
    {
        public int Value { get; set; }
        public bool Selected { get; set; }
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
