using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryCalc
{
    public class ConfiguredItem
    {
        public string Name { get; set; }
        public Item Item { get; set; }
        public double Amount { get; set; }
        public Recipe Recipe { get; set; }
    }
}
