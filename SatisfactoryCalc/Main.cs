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

        private void Main_Load(object sender, EventArgs e) { }

        private void calc_b_Click(object sender, EventArgs e)
        {
            CalcSession session = new CalcSession(info);

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                StringBuilder o = session.doCalc(ofd.FileName);

                SaveFileDialog sfd = new SaveFileDialog();
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, o.ToString());
                }
            }
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
