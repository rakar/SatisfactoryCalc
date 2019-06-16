using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryCalc
{
    public enum ItemType { CONSUMABLE,STATIC};
    public class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }

        public Item(string name, ItemType type=ItemType.CONSUMABLE)
        {
            Name = name;
            Type = type;
        }

        public Item(string name, string typeName)
        {
            Name = name;
            if (typeName == null) typeName = "";
            typeName = typeName.Trim().ToUpper();
            ItemType type = ItemType.CONSUMABLE;
            if(Enum.TryParse(typeName, out type))
            {
                Type = type;
            }
        }
    }
}
