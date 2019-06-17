using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactoryCalc
{
    public class CalcSession
    {
        Dictionary<string, ConfiguredItem> requestedMaterial;
        Dictionary<string, ConfiguredItem> intermediateMaterial;
        Dictionary<string, ConfiguredItem> structuralMaterial;
        Dictionary<string, ConfiguredItem> buildMaterial;

        SatisfactoryInfo info;

        public CalcSession(SatisfactoryInfo si)
        {
            info = si;
            requestedMaterial = new Dictionary<string, ConfiguredItem>();
            intermediateMaterial = new Dictionary<string, ConfiguredItem>();
            structuralMaterial = new Dictionary<string, ConfiguredItem>();
            buildMaterial = new Dictionary<string, ConfiguredItem>();
        }

        public StringBuilder doCalc(string[] requirements)
        {
            StringBuilder o = new StringBuilder();

            int line = 0;
            foreach (string req in requirements)
            {
                explodeInitialRequirements(o, ++line, req);
            }


            o.AppendLine("Output:");
            foreach (var m in requestedMaterial)
            {
                o.AppendLine($"{Math.Ceiling(m.Value.Amount)}({m.Value.Amount}) {m.Key}");
            }
            o.AppendLine();
            o.AppendLine("Intermediate Production:");
            foreach (var m in intermediateMaterial)
            {
                o.AppendLine($"{Math.Ceiling(m.Value.Amount)}({m.Value.Amount}) {m.Key}");
            }
            o.AppendLine();
            o.AppendLine("Structural Requirements:");
            foreach (var m in structuralMaterial)
            {
                o.AppendLine($"{Math.Ceiling(m.Value.Amount)}({m.Value.Amount}) {m.Key}");
                explodeBuildRecipe(m.Value.Item, Math.Ceiling(m.Value.Amount));
            }
            o.AppendLine();
            o.AppendLine("Build Requirements:");
            foreach (var m in buildMaterial)
            {
                o.AppendLine($"{Math.Ceiling(m.Value.Amount)}({m.Value.Amount}) {m.Key}");
            }

            return o;
        }

        private void explodeInitialRequirements(StringBuilder o, int line, string reqTxt)
        {
            reqTxt = reqTxt.Trim();
            char[] chars = reqTxt.ToArray();
            int p = -1;
            for(int i=0;i<chars.Length;i++)
            {
                if(char.IsWhiteSpace(chars[i]) || chars[i]==',')
                {
                    p = i;
                    break;
                }
            }

            if (p>0 && p+1<reqTxt.Length)
            {
                string qtyText = reqTxt.Substring(0, p).Split('(')[0];
                string itemName = reqTxt.Substring(p+1);

                Item item = info.Book.FindItem(itemName);
                if (item == null)
                {
                    o.AppendLine($"Error bad item: {itemName} on line: {line}");
                }
                else
                {
                    double amount = 0;
                    if (!double.TryParse(qtyText, out amount))
                    {
                        o.AppendLine($"Error bad amount: {qtyText} on line: {line}");
                    }
                    else
                    {
                        updateMaterial(requestedMaterial, item, amount);
                        if (info.Book.ContainsKey(item))
                        {
                            explodeRecipe(item, amount);
                        }
                    }
                }
            }
        }

        private void explodeBuildRecipe(Item item, double amount, bool initial = false)
        {
            if (!info.Book.ContainsKey(item))
            {
                updateMaterial(buildMaterial, item, amount);
            }
            else
            {
                Recipe r = info.Book[item];

                foreach (var i in r.Ingredients)
                {
                    double extAmount = amount / r.Production * i.Amount;
                    if (info.Book.ContainsKey(i.Item))
                    {
                        if (!initial)
                        {
                            if (i.Item.Type == ItemType.STATIC)
                            {
                                //updateMaterial(intermediateMaterial, i.Item, extAmount);
                                explodeBuildRecipe(i.Item, extAmount);
                            }
                            else
                            {
                                updateMaterial(buildMaterial, i.Item, extAmount);
                            }
                        }
                        else
                        {
                            //explodeBuildRecipe(i.Item, extAmount);
                        }
                    }
                    else
                    {
                        if (i.Item.Type == ItemType.STATIC)
                        {
                            //updateMaterial(structuralMaterial, i.Item, extAmount);
                        }
                        else
                        {
                            updateMaterial(buildMaterial, i.Item, extAmount);
                        }
                    }
                }
            }
        }

        private void explodeRecipe(Item item, double amount, bool initial = false)
        {

            Recipe r = info.Book[item];

            if (r.Building != null)
            {
                updateMaterial(structuralMaterial, r.Building, amount / r.Production, $"{item.Name} {r.Building.Name}");
                if (info.Book.ContainsKey(r.Building))
                {
                    double power = info.Book[r.Building].Power;
                    if (power > 0)
                    {
                        Item mw = info.Book.FindItem("Mega Watts");
                        updateMaterial(structuralMaterial, mw, amount / r.Production * power);
                    }
                }
            }

            foreach (var i in r.Ingredients)
            {
                double extAmount = amount / r.Production * i.Amount;
                if (info.Book.ContainsKey(i.Item))
                {
                    if (!initial)
                    {
                        if (i.Item.Type == ItemType.CONSUMABLE)
                        {
                            updateMaterial(intermediateMaterial, i.Item, extAmount);
                            explodeRecipe(i.Item, extAmount);
                        }
                        else
                        {
                            updateMaterial(structuralMaterial, i.Item, extAmount);
                        }
                    }
                    else
                    {
                        explodeRecipe(i.Item, extAmount);
                    }
                }
                else
                {
                    if (i.Item.Type == ItemType.STATIC)
                    {
                        updateMaterial(structuralMaterial, i.Item, extAmount);
                    }
                    else
                    {
                        updateMaterial(intermediateMaterial, i.Item, extAmount);
                    }
                }
            }
        }

        private void updateMaterial(Dictionary<string, ConfiguredItem> mat, Item item, double amount, string configuredName = "")
        {
            if (configuredName == "") configuredName = item.Name;
            if (mat.ContainsKey(configuredName))
            {
                mat[configuredName].Amount += amount;
            }
            else
            {
                mat.Add(configuredName, new ConfiguredItem() { Name = configuredName, Item = item, Amount = amount });
            }
        }
    }
}
