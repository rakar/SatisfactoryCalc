using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using GemBox.Spreadsheet;
using OfficeOpenXml;
using static SatisfactoryCalc.SatisfactoryInfo;


namespace SatisfactoryCalc
{
    public partial class Main : Form
    {
        SatisfactoryInfo info;

        public Main()
        {
            InitializeComponent();
            info = new SatisfactoryInfo();
        }

        private void cleanRecipieBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(ofd.FileName)))
                {
                    info.Book.CleanRecipeBook(package.Workbook);

                    SaveFileDialog sfd = new SaveFileDialog();
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        package.SaveAs(new FileInfo(sfd.FileName));
                    }
                }
            }
        }

        private void openRecipeBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRecipeBook();
        }

        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(info.Book.Count==0)
            {
                OpenRecipeBook();
            }
            CalcSession session = new CalcSession(info);
            Out.Text = session.doCalc(In.Lines).ToString();
        }

        private void OpenRecipeBook()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                using (ExcelPackage package = new ExcelPackage(new FileInfo(ofd.FileName)))
                {
                    info.Book.LoadRecipes(package.Workbook);
                }
            }
        }
    }
}
