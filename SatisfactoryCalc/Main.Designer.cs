namespace SatisfactoryCalc
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.calc_b = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.utilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanRecipieBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openRecipeBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // calc_b
            // 
            this.calc_b.Location = new System.Drawing.Point(180, 111);
            this.calc_b.Name = "calc_b";
            this.calc_b.Size = new System.Drawing.Size(280, 100);
            this.calc_b.TabIndex = 0;
            this.calc_b.Text = "calc";
            this.calc_b.UseVisualStyleBackColor = true;
            this.calc_b.Click += new System.EventHandler(this.calc_b_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.utilityToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 42);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openRecipeBookToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 38);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // utilityToolStripMenuItem
            // 
            this.utilityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cleanRecipieBookToolStripMenuItem});
            this.utilityToolStripMenuItem.Name = "utilityToolStripMenuItem";
            this.utilityToolStripMenuItem.Size = new System.Drawing.Size(89, 38);
            this.utilityToolStripMenuItem.Text = "Utility";
            // 
            // cleanRecipieBookToolStripMenuItem
            // 
            this.cleanRecipieBookToolStripMenuItem.Name = "cleanRecipieBookToolStripMenuItem";
            this.cleanRecipieBookToolStripMenuItem.Size = new System.Drawing.Size(324, 38);
            this.cleanRecipieBookToolStripMenuItem.Text = "Clean Recipie Book";
            this.cleanRecipieBookToolStripMenuItem.Click += new System.EventHandler(this.cleanRecipieBookToolStripMenuItem_Click);
            // 
            // openRecipeBookToolStripMenuItem
            // 
            this.openRecipeBookToolStripMenuItem.Name = "openRecipeBookToolStripMenuItem";
            this.openRecipeBookToolStripMenuItem.Size = new System.Drawing.Size(324, 38);
            this.openRecipeBookToolStripMenuItem.Text = "Open Recipe Book";
            this.openRecipeBookToolStripMenuItem.Click += new System.EventHandler(this.openRecipeBookToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.calc_b);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Satisfactory Calculator";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button calc_b;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem utilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanRecipieBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openRecipeBookToolStripMenuItem;
    }
}

