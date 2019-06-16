using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryCalc
{
    public enum IngredientType { ONETIME,RECURRING}
    public class Ingredient
    {
        public Item Item { get; set; }
        public IngredientType Type { get; set; }
        public double Amount { get; set; }

    }
}
