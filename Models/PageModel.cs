using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactDotNetDemo.Models
{
    public class PageModel
    {
        public bool IsFrench { get; set; }
        public PageModel(bool IsFrench)
        {
            this.IsFrench = IsFrench;
        }
    }
}
