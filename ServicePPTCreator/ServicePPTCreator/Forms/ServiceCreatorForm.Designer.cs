namespace ServicePPTCreator
{
    partial class PPTCreator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PPTCreator));
            this.createMorningPPT = new System.Windows.Forms.Button();
            this.lblURLPDF = new System.Windows.Forms.Label();
            this.morningServiceLinkTxtBox = new System.Windows.Forms.TextBox();
            this.createScripturePPT = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.selectMorningPDF = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.createEveningPPT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.eveningServiceLinkTxtBox = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.createPP6HymnPPT = new System.Windows.Forms.Button();
            this.createHTMLHymnsPPT = new System.Windows.Forms.Button();
            this.createPP6HymnsPPT = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // createMorningPPT
            // 
            this.createMorningPPT.BackColor = System.Drawing.Color.PapayaWhip;
            this.createMorningPPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createMorningPPT.Location = new System.Drawing.Point(559, 113);
            this.createMorningPPT.Name = "createMorningPPT";
            this.createMorningPPT.Size = new System.Drawing.Size(254, 57);
            this.createMorningPPT.TabIndex = 0;
            this.createMorningPPT.Text = "Create PPT from the PDF Bulletin";
            this.createMorningPPT.UseVisualStyleBackColor = false;
            this.createMorningPPT.Click += new System.EventHandler(this.createSlideshow_Click);
            // 
            // lblURLPDF
            // 
            this.lblURLPDF.AutoSize = true;
            this.lblURLPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblURLPDF.Location = new System.Drawing.Point(20, 49);
            this.lblURLPDF.Name = "lblURLPDF";
            this.lblURLPDF.Size = new System.Drawing.Size(152, 24);
            this.lblURLPDF.TabIndex = 1;
            this.lblURLPDF.Text = "Bulletin PDF Link";
            // 
            // morningServiceLinkTxtBox
            // 
            this.morningServiceLinkTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.morningServiceLinkTxtBox.Location = new System.Drawing.Point(24, 76);
            this.morningServiceLinkTxtBox.MaxLength = 700;
            this.morningServiceLinkTxtBox.Name = "morningServiceLinkTxtBox";
            this.morningServiceLinkTxtBox.Size = new System.Drawing.Size(789, 31);
            this.morningServiceLinkTxtBox.TabIndex = 2;
            // 
            // createScripturePPT
            // 
            this.createScripturePPT.BackColor = System.Drawing.Color.WhiteSmoke;
            this.createScripturePPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createScripturePPT.Location = new System.Drawing.Point(559, 40);
            this.createScripturePPT.Name = "createScripturePPT";
            this.createScripturePPT.Size = new System.Drawing.Size(254, 57);
            this.createScripturePPT.TabIndex = 3;
            this.createScripturePPT.Text = "Create PPT from Scripture Reference Selection";
            this.createScripturePPT.UseVisualStyleBackColor = false;
            this.createScripturePPT.Click += new System.EventHandler(this.scriptureRefToPPT_Click);
            // 
            // selectMorningPDF
            // 
            this.selectMorningPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectMorningPDF.Location = new System.Drawing.Point(24, 113);
            this.selectMorningPDF.Name = "selectMorningPDF";
            this.selectMorningPDF.Size = new System.Drawing.Size(150, 37);
            this.selectMorningPDF.TabIndex = 4;
            this.selectMorningPDF.Text = "Select local PDF";
            this.selectMorningPDF.UseVisualStyleBackColor = true;
            this.selectMorningPDF.Click += new System.EventHandler(this.selectLocalPDF_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectMorningPDF);
            this.groupBox1.Controls.Add(this.createMorningPPT);
            this.groupBox1.Controls.Add(this.lblURLPDF);
            this.groupBox1.Controls.Add(this.morningServiceLinkTxtBox);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(829, 186);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Morning Service";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.createEveningPPT);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.eveningServiceLinkTxtBox);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 217);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(829, 186);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Evening Service";
            // 
            // createEveningPPT
            // 
            this.createEveningPPT.BackColor = System.Drawing.Color.DarkSlateGray;
            this.createEveningPPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createEveningPPT.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.createEveningPPT.Location = new System.Drawing.Point(559, 113);
            this.createEveningPPT.Name = "createEveningPPT";
            this.createEveningPPT.Size = new System.Drawing.Size(254, 57);
            this.createEveningPPT.TabIndex = 0;
            this.createEveningPPT.Text = "Create PPT from Planning Center Online (PCO)";
            this.createEveningPPT.UseVisualStyleBackColor = false;
            this.createEveningPPT.Click += new System.EventHandler(this.createEveningPPT_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "PCO Plan Link";
            // 
            // eveningServiceLinkTxtBox
            // 
            this.eveningServiceLinkTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eveningServiceLinkTxtBox.Location = new System.Drawing.Point(24, 76);
            this.eveningServiceLinkTxtBox.MaxLength = 700;
            this.eveningServiceLinkTxtBox.Name = "eveningServiceLinkTxtBox";
            this.eveningServiceLinkTxtBox.Size = new System.Drawing.Size(789, 31);
            this.eveningServiceLinkTxtBox.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.createScripturePPT);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(12, 421);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(829, 116);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "NIV1984 Scripture";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.createPP6HymnPPT);
            this.groupBox4.Controls.Add(this.createHTMLHymnsPPT);
            this.groupBox4.Controls.Add(this.createPP6HymnsPPT);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(13, 556);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(829, 116);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Convert Hymns";
            // 
            // createPP6HymnPPT
            // 
            this.createPP6HymnPPT.BackColor = System.Drawing.SystemColors.Control;
            this.createPP6HymnPPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createPP6HymnPPT.Location = new System.Drawing.Point(558, 40);
            this.createPP6HymnPPT.Name = "createPP6HymnPPT";
            this.createPP6HymnPPT.Size = new System.Drawing.Size(254, 57);
            this.createPP6HymnPPT.TabIndex = 5;
            this.createPP6HymnPPT.Text = "Create PPT from Single ProPresenter File";
            this.createPP6HymnPPT.UseVisualStyleBackColor = false;
            this.createPP6HymnPPT.Click += new System.EventHandler(this.createPP6HymnPPT_Click);
            // 
            // createHTMLHymnsPPT
            // 
            this.createHTMLHymnsPPT.BackColor = System.Drawing.SystemColors.Control;
            this.createHTMLHymnsPPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createHTMLHymnsPPT.Location = new System.Drawing.Point(23, 40);
            this.createHTMLHymnsPPT.Name = "createHTMLHymnsPPT";
            this.createHTMLHymnsPPT.Size = new System.Drawing.Size(254, 57);
            this.createHTMLHymnsPPT.TabIndex = 4;
            this.createHTMLHymnsPPT.Text = "Create all PPTs from Hymnary HTML";
            this.createHTMLHymnsPPT.UseVisualStyleBackColor = false;
            this.createHTMLHymnsPPT.Click += new System.EventHandler(this.createHTMLHymnsPPT_Click);
            // 
            // createPP6HymnsPPT
            // 
            this.createPP6HymnsPPT.BackColor = System.Drawing.SystemColors.Control;
            this.createPP6HymnsPPT.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createPP6HymnsPPT.Location = new System.Drawing.Point(291, 40);
            this.createPP6HymnsPPT.Name = "createPP6HymnsPPT";
            this.createPP6HymnsPPT.Size = new System.Drawing.Size(254, 57);
            this.createPP6HymnsPPT.TabIndex = 3;
            this.createPP6HymnsPPT.Text = "Create all PPTs from ProPresenter Library";
            this.createPP6HymnsPPT.UseVisualStyleBackColor = false;
            this.createPP6HymnsPPT.Click += new System.EventHandler(this.createPP6HymnsPPT_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(854, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configurationToolStripMenuItem
            // 
            this.configurationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultsToolStripMenuItem});
            this.configurationToolStripMenuItem.Name = "configurationToolStripMenuItem";
            this.configurationToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.configurationToolStripMenuItem.Text = "Configuration";
            // 
            // defaultsToolStripMenuItem
            // 
            this.defaultsToolStripMenuItem.Name = "defaultsToolStripMenuItem";
            this.defaultsToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.defaultsToolStripMenuItem.Text = "Defaults";
            this.defaultsToolStripMenuItem.Click += new System.EventHandler(this.defaultsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howToToolStripMenuItem
            // 
            this.howToToolStripMenuItem.Name = "howToToolStripMenuItem";
            this.howToToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.howToToolStripMenuItem.Text = "How To....";
            this.howToToolStripMenuItem.Click += new System.EventHandler(this.howToToolStripMenuItem_Click);
            // 
            // PPTCreator
            // 
            this.ClientSize = new System.Drawing.Size(854, 681);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "PPTCreator";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Service PowerPoint Creator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button createMorningPPT;
        private System.Windows.Forms.Label lblURLPDF;
        private System.Windows.Forms.TextBox morningServiceLinkTxtBox;
        private System.Windows.Forms.Button createScripturePPT;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button selectMorningPDF;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button createEveningPPT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox eveningServiceLinkTxtBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button createPP6HymnPPT;
        private System.Windows.Forms.Button createHTMLHymnsPPT;
        private System.Windows.Forms.Button createPP6HymnsPPT;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howToToolStripMenuItem;
    }
}

