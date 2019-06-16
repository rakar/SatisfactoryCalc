using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryCalc
{
    public class Recipe
    {
        public Item Item { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public double Production { get; set; }
        public double Power { get; set; }
        public Item Building { get; set; }

        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }
    }
}
