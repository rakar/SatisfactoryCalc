using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace SatisfactoryCalc
{
    public class RecipeBook : Dictionary<Item, Recipe>
    {
        public List<Item> Items;

        public RecipeBook()
        {
            Items = new List<Item>();
        }

        //public void QuickAdd(params object[] args)
        //{
        //    Recipe r = new Recipe();
        //    r.Ingredients = new List<Ingredient>();
        //    int i;
        //    if (args[0].GetType().Name == "Item")
        //    {
        //        r.Item = (Item)args[0];
        //        r.Production = 1;
        //        i = 1;
        //    }
        //    else
        //    {
        //        r.Production = Convert.ToDouble(args[0]);
        //        r.Item = (Item)args[1];
        //        i = 2;
        //    }
        //    for (; i < args.Length; i += 2)
        //    {
        //        Item item = ((Item)args[i + 1]);
        //        double amount = Convert.ToDouble(args[i]);
        //        IngredientType type = IngredientType.RECURRING;
        //        if (i + 2 < args.Length && args[i + 2].GetType() == typeof(IngredientType))
        //        {
        //            type = (IngredientType)args[i + 2];
        //            i++;
        //        }
        //        r.Ingredients.Add(new Ingredient() { Item = item, Amount = amount, Type = type });
        //    };
        //    this.Add(r.Item, r);
        //}

        public void EmptyBook()
        {
            this.Clear();
            Items = new List<Item>();
        }

        public void CleanRecipeBook(ExcelWorkbook ew)
        {
            EmptyBook();

            // clean items
            //ExcelWorkbook ew = ef.Workbook;
            ExcelWorksheet itemWorksheet = ew.Worksheets["Items"];
            int itemCount = itemWorksheet.Dimension.End.Row;
            //foreach (var row in itemRow.r .Rows)
            for(int i=1;i<=itemCount;i++)
            {
                //ExcelRow row = itemRow.Row(i);
                string cA = (string)itemWorksheet.Cells[i, 1].Value;
                string cB = (string)itemWorksheet.Cells[i, 2].Value;

                if (!string.IsNullOrWhiteSpace(cA) && cA.Substring(0, 1) != "#")
                {
                    cA = PrettyName(cA);
                    itemWorksheet.Cells[i, 1].Value = cA;
                    Items.Add(new Item(cA, cB));
                }

            }

            // Recipes
            ExcelWorksheet recipeWorksheet = ew.Worksheets["Recipes"];

            int[] itemCols = { 2, 6, 8, 10, 12, 14 };

            int recipeCount = recipeWorksheet.Dimension.End.Row;
            //foreach (var row in recipeWorksheet.Rows)
            for(int i=1;i<=recipeCount;i++)
            {
                if (recipeWorksheet.Cells[i, 1].Value != null && recipeWorksheet.Cells[i, 1].Value.GetType().Name != "String")
                {
                    foreach (int x in itemCols)
                    {
                        string cX = (string)recipeWorksheet.Cells[i, x].Value;
                        if (!string.IsNullOrWhiteSpace(cX) && cX.Substring(0, 1) != "#")
                        {
                            cX = PrettyName(cX);
                            recipeWorksheet.Cells[i, x].Value = cX;
                            Item itemCheck = FindItem(cX);
                            if (itemCheck == null)
                            {
                                recipeWorksheet.Cells[i, x].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                recipeWorksheet.Cells[i, x].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Red);
                            }
                            else
                            {
                                recipeWorksheet.Cells[i, x].Style.Fill.PatternType = ExcelFillStyle.None;
                            }
                        }
                    }
                }
            }
        }

        public void LoadRecipes(ExcelWorkbook ew)
        {
            EmptyBook();

            //ExcelFile ef = new ExcelFile();
            //ef.LoadXlsx(filename, XlsxOptions.None);

            // clean items
            ExcelWorksheet itemWorksheet = ew.Worksheets["Items"];
            int itemCount = itemWorksheet.Dimension.End.Row;
            //foreach (var row in itemWorksheet.Rows)
            for(int i=1;i<=itemCount;i++)
            {
                string cA = (string)itemWorksheet.Cells[i,1].Value;
                string cB = (string)itemWorksheet.Cells[i,2].Value;

                if (!string.IsNullOrWhiteSpace(cA) && cA.Substring(0, 1) != "#")
                {
                    cA = PrettyName(cA);
                    itemWorksheet.Cells[i, 1].Value = cA;
                    Items.Add(new Item(cA, cB));
                }
            }

            // Recipes
            ExcelWorksheet recipeWorksheet = ew.Worksheets["Recipes"];

            int[] itemCols = { 6, 8, 10, 12, 14 };

            int recipeCount = recipeWorksheet.Dimension.End.Row;
            for (int i = 1; i <= recipeCount; i++)
            {
                if (recipeWorksheet.Cells[i, 1].Value != null && recipeWorksheet.Cells[i, 1].Value.GetType().Name != "String")
                {
                    double cA = Convert.ToDouble(recipeWorksheet.Cells[i, 1].Value ?? 1);
                    string cB = (string)recipeWorksheet.Cells[i, 2].Value;
                    string cC = (string)recipeWorksheet.Cells[i, 3].Value;
                    double cD = Convert.ToDouble(recipeWorksheet.Cells[i, 4].Value ?? 0);

                    if (!string.IsNullOrWhiteSpace(cB) && cB.Substring(0, 1) != "#")
                    {
                        Item product = FindItem(cB);
                        Item building = FindItem(cC);
                        if (product != null)
                        {
                            Recipe r = new Recipe() { Item = product, Production = cA, Building = building, Power = cD };
                            foreach (int x in itemCols)
                            {
                                string cX = (string)recipeWorksheet.Cells[i, x].Value;
                                if (!string.IsNullOrWhiteSpace(cX) && cX.Substring(0, 1) != "#")
                                {
                                    cX = PrettyName(cX);
                                    Item item = FindItem(cX);
                                    double amount = Convert.ToDouble(recipeWorksheet.Cells[i, x-1].Value ?? 0);
                                    IngredientType type = IngredientType.RECURRING; // not used at the momment
                                    r.Ingredients.Add(new Ingredient() { Item = item, Amount = amount, Type = type });
                                }
                            }
                            Add(r.Item, r);
                        }
                    }
                }
            }

            // second pass to update power...
            foreach (var r in this)
            {
                Item building = FindItem(r.Value.Building?.Name);
                if (building != null && ContainsKey(building))
                {
                    Recipe buildRecipe = this[building];
                    r.Value.Power = buildRecipe.Power;
                }
            }
        }

        public static string PrettyName(string txt)
        {
            char[] ic = txt.ToCharArray();
            List<char> oc = new List<char>();
            foreach (char c in ic)
            {
                if (char.IsUpper(c))
                {
                    oc.Add(' ');
                }
                oc.Add(c);
            }
            string wordstring = new string(oc.ToArray());
            string[] words = wordstring.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string pretty = "";
            foreach (string word in words)
            {
                pretty += word.Substring(0, 1).ToUpper() + word.Substring(1) + " ";
            }
            return pretty.Trim();
        }

        public Item FindItem(string itemName)
        {
            if (itemName == null)
            {
                return null;
            }

            itemName = itemName.Trim().Replace(" ", "").ToLower();
            return Items.SingleOrDefault(i => i.Name.Replace(" ", "").ToLower() == itemName);
        }
    }
}
