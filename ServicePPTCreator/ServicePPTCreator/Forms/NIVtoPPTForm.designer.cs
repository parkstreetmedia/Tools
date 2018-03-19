namespace ServicePPTCreator
{
    partial class NIVtoPPT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NIVtoPPT));
            this.buttonCreateSlides = new System.Windows.Forms.Button();
            this.selectBookFrom = new System.Windows.Forms.ComboBox();
            this.selectChapterFrom = new System.Windows.Forms.ComboBox();
            this.selectVerseFrom = new System.Windows.Forms.ComboBox();
            this.selectVerseTo = new System.Windows.Forms.ComboBox();
            this.selectChapterTo = new System.Windows.Forms.ComboBox();
            this.selectBookTo = new System.Windows.Forms.ComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCreateSlides
            // 
            this.buttonCreateSlides.Font = new System.Drawing.Font("Open Sans", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateSlides.Location = new System.Drawing.Point(74, 142);
            this.buttonCreateSlides.Name = "buttonCreateSlides";
            this.buttonCreateSlides.Size = new System.Drawing.Size(224, 49);
            this.buttonCreateSlides.TabIndex = 0;
            this.buttonCreateSlides.Text = "Create Slides";
            this.buttonCreateSlides.UseVisualStyleBackColor = true;
            this.buttonCreateSlides.Click += new System.EventHandler(this.buttonCreateSlides_Click);
            // 
            // selectBookFrom
            // 
            this.selectBookFrom.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectBookFrom.FormattingEnabled = true;
            this.selectBookFrom.Location = new System.Drawing.Point(13, 34);
            this.selectBookFrom.Name = "selectBookFrom";
            this.selectBookFrom.Size = new System.Drawing.Size(164, 28);
            this.selectBookFrom.TabIndex = 1;
            this.selectBookFrom.SelectedIndexChanged += new System.EventHandler(this.SelectBookFrom_SelectedIndexChanged);
            // 
            // selectChapterFrom
            // 
            this.selectChapterFrom.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectChapterFrom.FormattingEnabled = true;
            this.selectChapterFrom.Location = new System.Drawing.Point(206, 34);
            this.selectChapterFrom.Name = "selectChapterFrom";
            this.selectChapterFrom.Size = new System.Drawing.Size(66, 28);
            this.selectChapterFrom.TabIndex = 2;
            this.selectChapterFrom.SelectedIndexChanged += new System.EventHandler(this.SelectChapterFrom_SelectedIndexChanged);
            // 
            // selectVerseFrom
            // 
            this.selectVerseFrom.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectVerseFrom.FormattingEnabled = true;
            this.selectVerseFrom.Location = new System.Drawing.Point(291, 34);
            this.selectVerseFrom.Name = "selectVerseFrom";
            this.selectVerseFrom.Size = new System.Drawing.Size(61, 28);
            this.selectVerseFrom.TabIndex = 3;
            this.selectVerseFrom.SelectedIndexChanged += new System.EventHandler(this.selectVerseFrom_SelectedIndexChanged);
            // 
            // selectVerseTo
            // 
            this.selectVerseTo.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectVerseTo.FormattingEnabled = true;
            this.selectVerseTo.Location = new System.Drawing.Point(291, 94);
            this.selectVerseTo.Name = "selectVerseTo";
            this.selectVerseTo.Size = new System.Drawing.Size(61, 28);
            this.selectVerseTo.TabIndex = 6;
            // 
            // selectChapterTo
            // 
            this.selectChapterTo.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectChapterTo.FormattingEnabled = true;
            this.selectChapterTo.Location = new System.Drawing.Point(206, 94);
            this.selectChapterTo.Name = "selectChapterTo";
            this.selectChapterTo.Size = new System.Drawing.Size(66, 28);
            this.selectChapterTo.TabIndex = 5;
            this.selectChapterTo.SelectedIndexChanged += new System.EventHandler(this.SelectChapterTo_SelectedIndexChanged);
            // 
            // selectBookTo
            // 
            this.selectBookTo.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectBookTo.FormattingEnabled = true;
            this.selectBookTo.Location = new System.Drawing.Point(13, 94);
            this.selectBookTo.Name = "selectBookTo";
            this.selectBookTo.Size = new System.Drawing.Size(164, 28);
            this.selectBookTo.TabIndex = 4;
            this.selectBookTo.SelectedIndexChanged += new System.EventHandler(this.SelectBookTo_SelectedIndexChanged);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(11, 12);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(220, 20);
            this.lblFrom.TabIndex = 7;
            this.lblFrom.Text = "Start at: Book - Chapter - Verse";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Open Sans", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(11, 72);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(203, 20);
            this.lblTo.TabIndex = 8;
            this.lblTo.Text = "Thru: Book - Chapter - Verse";
            // 
            // NIVtoPPT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 206);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.selectVerseTo);
            this.Controls.Add(this.selectChapterTo);
            this.Controls.Add(this.selectBookTo);
            this.Controls.Add(this.selectVerseFrom);
            this.Controls.Add(this.selectChapterFrom);
            this.Controls.Add(this.selectBookFrom);
            this.Controls.Add(this.buttonCreateSlides);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NIVtoPPT";
            this.Text = "NIV PPT Slide Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button buttonCreateSlides;
        private System.Windows.Forms.ComboBox selectBookFrom;
        private System.Windows.Forms.ComboBox selectChapterFrom;
        private System.Windows.Forms.ComboBox selectVerseFrom;
        private System.Windows.Forms.ComboBox selectVerseTo;
        private System.Windows.Forms.ComboBox selectChapterTo;
        private System.Windows.Forms.ComboBox selectBookTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
    }
}

