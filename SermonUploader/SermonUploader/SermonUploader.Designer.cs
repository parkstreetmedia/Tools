namespace SermonUploader
{
    partial class SermonUploader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SermonUploader));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileBrowseDialog = new System.Windows.Forms.OpenFileDialog();
            this.wavFileDrop = new System.Windows.Forms.GroupBox();
            this.fileSelectBox = new System.Windows.Forms.TextBox();
            this.fileBrowseBtn = new System.Windows.Forms.Button();
            this.grpTagDetails = new System.Windows.Forms.GroupBox();
            this.isPMCheckbox = new System.Windows.Forms.CheckBox();
            this.isAMCheckbox = new System.Windows.Forms.CheckBox();
            this.lblAmorPM = new System.Windows.Forms.Label();
            this.tagDate = new System.Windows.Forms.DateTimePicker();
            this.tagSpeakerTitleTxt = new System.Windows.Forms.TextBox();
            this.lblTagSpeakerTitleMorning = new System.Windows.Forms.Label();
            this.tagSpeakerTxt = new System.Windows.Forms.TextBox();
            this.tagScriptureTxt = new System.Windows.Forms.TextBox();
            this.tagTitleTxt = new System.Windows.Forms.TextBox();
            this.lblTagScriptureMorning = new System.Windows.Forms.Label();
            this.lblTagTitleMorning = new System.Windows.Forms.Label();
            this.lblTagSpeakerMorning = new System.Windows.Forms.Label();
            this.lblTagDateMorning = new System.Windows.Forms.Label();
            this.grpProcess = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FileProgress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.YoutubeProgress = new System.Windows.Forms.ProgressBar();
            this.FilePercentDoneLbl = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.closeWhenDoneChk = new System.Windows.Forms.CheckBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.mainGroupBox = new System.Windows.Forms.GroupBox();
            this.shouldSkipS3UploadChk = new System.Windows.Forms.CheckBox();
            this.shouldSkipVideoUploadChk = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.wavFileDrop.SuspendLayout();
            this.grpTagDetails.SuspendLayout();
            this.grpProcess.SuspendLayout();
            this.mainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.menuStrip1.Size = new System.Drawing.Size(1716, 55);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(148, 45);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(246, 54);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // fileBrowseDialog
            // 
            this.fileBrowseDialog.Filter = "Sermons | .mov;*.avi;*.mp4;*.wav;";
            this.fileBrowseDialog.RestoreDirectory = true;
            // 
            // wavFileDrop
            // 
            this.wavFileDrop.Controls.Add(this.fileSelectBox);
            this.wavFileDrop.Controls.Add(this.fileBrowseBtn);
            this.wavFileDrop.Location = new System.Drawing.Point(21, 21);
            this.wavFileDrop.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.wavFileDrop.Name = "wavFileDrop";
            this.wavFileDrop.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.wavFileDrop.Size = new System.Drawing.Size(1659, 176);
            this.wavFileDrop.TabIndex = 1;
            this.wavFileDrop.TabStop = false;
            this.wavFileDrop.Text = "File to Upload";
            // 
            // fileSelectBox
            // 
            this.fileSelectBox.Location = new System.Drawing.Point(16, 45);
            this.fileSelectBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fileSelectBox.Name = "fileSelectBox";
            this.fileSelectBox.Size = new System.Drawing.Size(1627, 38);
            this.fileSelectBox.TabIndex = 1;
            // 
            // fileBrowseBtn
            // 
            this.fileBrowseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileBrowseBtn.Location = new System.Drawing.Point(16, 97);
            this.fileBrowseBtn.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.fileBrowseBtn.Name = "fileBrowseBtn";
            this.fileBrowseBtn.Size = new System.Drawing.Size(1627, 65);
            this.fileBrowseBtn.TabIndex = 0;
            this.fileBrowseBtn.TabStop = false;
            this.fileBrowseBtn.Text = "Browse";
            this.fileBrowseBtn.UseVisualStyleBackColor = true;
            this.fileBrowseBtn.Click += new System.EventHandler(this.fileBrowseBtn_Click);
            // 
            // grpTagDetails
            // 
            this.grpTagDetails.Controls.Add(this.isPMCheckbox);
            this.grpTagDetails.Controls.Add(this.isAMCheckbox);
            this.grpTagDetails.Controls.Add(this.lblAmorPM);
            this.grpTagDetails.Controls.Add(this.tagDate);
            this.grpTagDetails.Controls.Add(this.tagSpeakerTitleTxt);
            this.grpTagDetails.Controls.Add(this.lblTagSpeakerTitleMorning);
            this.grpTagDetails.Controls.Add(this.tagSpeakerTxt);
            this.grpTagDetails.Controls.Add(this.tagScriptureTxt);
            this.grpTagDetails.Controls.Add(this.tagTitleTxt);
            this.grpTagDetails.Controls.Add(this.lblTagScriptureMorning);
            this.grpTagDetails.Controls.Add(this.lblTagTitleMorning);
            this.grpTagDetails.Controls.Add(this.lblTagSpeakerMorning);
            this.grpTagDetails.Controls.Add(this.lblTagDateMorning);
            this.grpTagDetails.Location = new System.Drawing.Point(21, 211);
            this.grpTagDetails.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.grpTagDetails.Name = "grpTagDetails";
            this.grpTagDetails.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.grpTagDetails.Size = new System.Drawing.Size(1659, 703);
            this.grpTagDetails.TabIndex = 0;
            this.grpTagDetails.TabStop = false;
            this.grpTagDetails.Text = "Sermon Info";
            // 
            // isPMCheckbox
            // 
            this.isPMCheckbox.AutoSize = true;
            this.isPMCheckbox.Location = new System.Drawing.Point(48, 234);
            this.isPMCheckbox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.isPMCheckbox.Name = "isPMCheckbox";
            this.isPMCheckbox.Size = new System.Drawing.Size(157, 36);
            this.isPMCheckbox.TabIndex = 19;
            this.isPMCheckbox.Text = "Evening";
            this.isPMCheckbox.UseVisualStyleBackColor = true;
            this.isPMCheckbox.CheckedChanged += new System.EventHandler(this.isPMCheckbox_CheckedChanged);
            // 
            // isAMCheckbox
            // 
            this.isAMCheckbox.AutoSize = true;
            this.isAMCheckbox.Location = new System.Drawing.Point(48, 184);
            this.isAMCheckbox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.isAMCheckbox.Name = "isAMCheckbox";
            this.isAMCheckbox.Size = new System.Drawing.Size(156, 36);
            this.isAMCheckbox.TabIndex = 18;
            this.isAMCheckbox.Text = "Morning";
            this.isAMCheckbox.UseVisualStyleBackColor = true;
            this.isAMCheckbox.CheckedChanged += new System.EventHandler(this.isAMCheckbox_CheckedChanged);
            // 
            // lblAmorPM
            // 
            this.lblAmorPM.AutoSize = true;
            this.lblAmorPM.Location = new System.Drawing.Point(21, 138);
            this.lblAmorPM.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblAmorPM.Name = "lblAmorPM";
            this.lblAmorPM.Size = new System.Drawing.Size(154, 32);
            this.lblAmorPM.TabIndex = 17;
            this.lblAmorPM.Text = "AM or PM?";
            // 
            // tagDate
            // 
            this.tagDate.Location = new System.Drawing.Point(27, 79);
            this.tagDate.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.tagDate.Name = "tagDate";
            this.tagDate.Size = new System.Drawing.Size(527, 38);
            this.tagDate.TabIndex = 16;
            // 
            // tagSpeakerTitleTxt
            // 
            this.tagSpeakerTitleTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tagSpeakerTitleTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tagSpeakerTitleTxt.Location = new System.Drawing.Point(27, 432);
            this.tagSpeakerTitleTxt.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.tagSpeakerTitleTxt.MaxLength = 250;
            this.tagSpeakerTitleTxt.Name = "tagSpeakerTitleTxt";
            this.tagSpeakerTitleTxt.Size = new System.Drawing.Size(631, 38);
            this.tagSpeakerTitleTxt.TabIndex = 4;
            this.tagSpeakerTitleTxt.WordWrap = false;
            // 
            // lblTagSpeakerTitleMorning
            // 
            this.lblTagSpeakerTitleMorning.AutoSize = true;
            this.lblTagSpeakerTitleMorning.Location = new System.Drawing.Point(16, 391);
            this.lblTagSpeakerTitleMorning.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblTagSpeakerTitleMorning.Name = "lblTagSpeakerTitleMorning";
            this.lblTagSpeakerTitleMorning.Size = new System.Drawing.Size(183, 32);
            this.lblTagSpeakerTitleMorning.TabIndex = 0;
            this.lblTagSpeakerTitleMorning.Text = "Speaker Title";
            // 
            // tagSpeakerTxt
            // 
            this.tagSpeakerTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tagSpeakerTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tagSpeakerTxt.Location = new System.Drawing.Point(27, 329);
            this.tagSpeakerTxt.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.tagSpeakerTxt.MaxLength = 250;
            this.tagSpeakerTxt.Name = "tagSpeakerTxt";
            this.tagSpeakerTxt.Size = new System.Drawing.Size(633, 38);
            this.tagSpeakerTxt.TabIndex = 3;
            this.tagSpeakerTxt.WordWrap = false;
            this.tagSpeakerTxt.TextChanged += new System.EventHandler(this.tagSpeakerTxt_TextChanged);
            // 
            // tagScriptureTxt
            // 
            this.tagScriptureTxt.AutoCompleteCustomSource.AddRange(new string[] {
            "Genesis",
            "Exodus",
            "Leviticus",
            "Numbers",
            "Deuteronomy",
            "Joshua",
            "Judges",
            "Ruth",
            "1 Samuel",
            "2 Samuel",
            "1 Kings",
            "2 Kings",
            "1 Chronicles",
            "2 Chronicles",
            "Ezra",
            "Nehemiah",
            "Esther",
            "Job",
            "Psalm",
            "Proverbs",
            "Ecclesiastes",
            "Song of Solomon",
            "Isaiah",
            "Jeremiah",
            "Lamentations",
            "Ezekiel",
            "Daniel",
            "Hosea",
            "Joel",
            "Amos",
            "Obadiah",
            "Jonah",
            "Micah",
            "Nahum",
            "Habakkuk",
            "Zephaniah",
            "Haggai",
            "Zechariah",
            "Malachi",
            "Matthew",
            "Mark",
            "Luke",
            "John",
            "Acts",
            "Romans",
            "1 Corinthians",
            "2 Corinthians",
            "Galatians",
            "Ephesians",
            "Philippians",
            "Colossians",
            "1 Thessalonians",
            "2 Thessalonians",
            "1 Timothy",
            "2 Timothy",
            "Titus",
            "Philemon",
            "Hebrews",
            "James",
            "1 Peter",
            "2 Peter",
            "1 John",
            "2 John",
            "3 John",
            "Jude",
            "Revelation"});
            this.tagScriptureTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.tagScriptureTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tagScriptureTxt.Location = new System.Drawing.Point(27, 534);
            this.tagScriptureTxt.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.tagScriptureTxt.MaxLength = 250;
            this.tagScriptureTxt.Name = "tagScriptureTxt";
            this.tagScriptureTxt.Size = new System.Drawing.Size(631, 38);
            this.tagScriptureTxt.TabIndex = 5;
            this.tagScriptureTxt.WordWrap = false;
            // 
            // tagTitleTxt
            // 
            this.tagTitleTxt.Location = new System.Drawing.Point(27, 634);
            this.tagTitleTxt.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.tagTitleTxt.MaxLength = 250;
            this.tagTitleTxt.Name = "tagTitleTxt";
            this.tagTitleTxt.Size = new System.Drawing.Size(1053, 38);
            this.tagTitleTxt.TabIndex = 6;
            this.tagTitleTxt.WordWrap = false;
            // 
            // lblTagScriptureMorning
            // 
            this.lblTagScriptureMorning.AutoSize = true;
            this.lblTagScriptureMorning.Location = new System.Drawing.Point(16, 494);
            this.lblTagScriptureMorning.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblTagScriptureMorning.Name = "lblTagScriptureMorning";
            this.lblTagScriptureMorning.Size = new System.Drawing.Size(129, 32);
            this.lblTagScriptureMorning.TabIndex = 0;
            this.lblTagScriptureMorning.Text = "Scripture";
            // 
            // lblTagTitleMorning
            // 
            this.lblTagTitleMorning.AutoSize = true;
            this.lblTagTitleMorning.Location = new System.Drawing.Point(16, 596);
            this.lblTagTitleMorning.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblTagTitleMorning.Name = "lblTagTitleMorning";
            this.lblTagTitleMorning.Size = new System.Drawing.Size(176, 32);
            this.lblTagTitleMorning.TabIndex = 0;
            this.lblTagTitleMorning.Text = "Sermon Title";
            // 
            // lblTagSpeakerMorning
            // 
            this.lblTagSpeakerMorning.AutoSize = true;
            this.lblTagSpeakerMorning.Location = new System.Drawing.Point(19, 291);
            this.lblTagSpeakerMorning.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblTagSpeakerMorning.Name = "lblTagSpeakerMorning";
            this.lblTagSpeakerMorning.Size = new System.Drawing.Size(121, 32);
            this.lblTagSpeakerMorning.TabIndex = 0;
            this.lblTagSpeakerMorning.Text = "Speaker";
            // 
            // lblTagDateMorning
            // 
            this.lblTagDateMorning.AutoSize = true;
            this.lblTagDateMorning.Location = new System.Drawing.Point(16, 38);
            this.lblTagDateMorning.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblTagDateMorning.Name = "lblTagDateMorning";
            this.lblTagDateMorning.Size = new System.Drawing.Size(75, 32);
            this.lblTagDateMorning.TabIndex = 0;
            this.lblTagDateMorning.Text = "Date";
            // 
            // grpProcess
            // 
            this.grpProcess.Controls.Add(this.label2);
            this.grpProcess.Controls.Add(this.FileProgress);
            this.grpProcess.Controls.Add(this.label1);
            this.grpProcess.Controls.Add(this.YoutubeProgress);
            this.grpProcess.Controls.Add(this.FilePercentDoneLbl);
            this.grpProcess.Controls.Add(this.progress);
            this.grpProcess.Controls.Add(this.lblProgress);
            this.grpProcess.Location = new System.Drawing.Point(3, 1273);
            this.grpProcess.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.grpProcess.Name = "grpProcess";
            this.grpProcess.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.grpProcess.Size = new System.Drawing.Size(1696, 257);
            this.grpProcess.TabIndex = 0;
            this.grpProcess.TabStop = false;
            this.grpProcess.Text = "Progress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(842, 46);
            this.label2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(333, 32);
            this.label2.TabIndex = 16;
            this.label2.Text = "MP3 Conversion Process";
            // 
            // FileProgress
            // 
            this.FileProgress.Location = new System.Drawing.Point(852, 86);
            this.FileProgress.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.FileProgress.Name = "FileProgress";
            this.FileProgress.Size = new System.Drawing.Size(828, 45);
            this.FileProgress.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 32);
            this.label1.TabIndex = 14;
            this.label1.Text = "Youtube Upload Progress";
            // 
            // YoutubeProgress
            // 
            this.YoutubeProgress.Location = new System.Drawing.Point(29, 89);
            this.YoutubeProgress.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.YoutubeProgress.Name = "YoutubeProgress";
            this.YoutubeProgress.Size = new System.Drawing.Size(810, 42);
            this.YoutubeProgress.TabIndex = 15;
            // 
            // FilePercentDoneLbl
            // 
            this.FilePercentDoneLbl.AutoSize = true;
            this.FilePercentDoneLbl.Location = new System.Drawing.Point(1117, 238);
            this.FilePercentDoneLbl.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.FilePercentDoneLbl.Name = "FilePercentDoneLbl";
            this.FilePercentDoneLbl.Size = new System.Drawing.Size(0, 32);
            this.FilePercentDoneLbl.TabIndex = 13;
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(29, 188);
            this.progress.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(1651, 57);
            this.progress.TabIndex = 0;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(19, 148);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(199, 32);
            this.lblProgress.TabIndex = 0;
            this.lblProgress.Text = "Total Progress";
            // 
            // btnCancel
            // 
            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(1407, 1109);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(273, 55);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // closeWhenDoneChk
            // 
            this.closeWhenDoneChk.AutoSize = true;
            this.closeWhenDoneChk.Checked = true;
            this.closeWhenDoneChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.closeWhenDoneChk.Location = new System.Drawing.Point(21, 935);
            this.closeWhenDoneChk.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.closeWhenDoneChk.Name = "closeWhenDoneChk";
            this.closeWhenDoneChk.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.closeWhenDoneChk.Size = new System.Drawing.Size(478, 36);
            this.closeWhenDoneChk.TabIndex = 14;
            this.closeWhenDoneChk.Text = "?Close on Successful Completion";
            this.closeWhenDoneChk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.closeWhenDoneChk.UseVisualStyleBackColor = true;
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(21, 1010);
            this.btnUpload.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(1659, 79);
            this.btnUpload.TabIndex = 12;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // mainGroupBox
            // 
            this.mainGroupBox.Controls.Add(this.shouldSkipS3UploadChk);
            this.mainGroupBox.Controls.Add(this.shouldSkipVideoUploadChk);
            this.mainGroupBox.Controls.Add(this.grpTagDetails);
            this.mainGroupBox.Controls.Add(this.wavFileDrop);
            this.mainGroupBox.Controls.Add(this.btnUpload);
            this.mainGroupBox.Controls.Add(this.closeWhenDoneChk);
            this.mainGroupBox.Controls.Add(this.btnCancel);
            this.mainGroupBox.Location = new System.Drawing.Point(3, 64);
            this.mainGroupBox.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.mainGroupBox.Name = "mainGroupBox";
            this.mainGroupBox.Padding = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.mainGroupBox.Size = new System.Drawing.Size(1696, 1189);
            this.mainGroupBox.TabIndex = 0;
            this.mainGroupBox.TabStop = false;
            // 
            // shouldSkipS3UploadChk
            // 
            this.shouldSkipS3UploadChk.AutoSize = true;
            this.shouldSkipS3UploadChk.ForeColor = System.Drawing.SystemColors.ControlText;
            this.shouldSkipS3UploadChk.Location = new System.Drawing.Point(1097, 935);
            this.shouldSkipS3UploadChk.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.shouldSkipS3UploadChk.Name = "shouldSkipS3UploadChk";
            this.shouldSkipS3UploadChk.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.shouldSkipS3UploadChk.Size = new System.Drawing.Size(346, 36);
            this.shouldSkipS3UploadChk.TabIndex = 16;
            this.shouldSkipS3UploadChk.Text = "?Skip S3 Audio Upload";
            this.shouldSkipS3UploadChk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.shouldSkipS3UploadChk.UseVisualStyleBackColor = true;
            // 
            // shouldSkipVideoUploadChk
            // 
            this.shouldSkipVideoUploadChk.AutoSize = true;
            this.shouldSkipVideoUploadChk.ForeColor = System.Drawing.SystemColors.ControlText;
            this.shouldSkipVideoUploadChk.Location = new System.Drawing.Point(573, 935);
            this.shouldSkipVideoUploadChk.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.shouldSkipVideoUploadChk.Name = "shouldSkipVideoUploadChk";
            this.shouldSkipVideoUploadChk.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.shouldSkipVideoUploadChk.Size = new System.Drawing.Size(418, 36);
            this.shouldSkipVideoUploadChk.TabIndex = 15;
            this.shouldSkipVideoUploadChk.Text = "?Skip Youtube Video Upload";
            this.shouldSkipVideoUploadChk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.shouldSkipVideoUploadChk.UseVisualStyleBackColor = true;
            // 
            // SermonUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1716, 1541);
            this.Controls.Add(this.grpProcess);
            this.Controls.Add(this.mainGroupBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.MaximizeBox = false;
            this.Name = "SermonUploader";
            this.Text = "Sermon Uploader";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.wavFileDrop.ResumeLayout(false);
            this.wavFileDrop.PerformLayout();
            this.grpTagDetails.ResumeLayout(false);
            this.grpTagDetails.PerformLayout();
            this.grpProcess.ResumeLayout(false);
            this.grpProcess.PerformLayout();
            this.mainGroupBox.ResumeLayout(false);
            this.mainGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog fileBrowseDialog;
        private System.Windows.Forms.GroupBox wavFileDrop;
        private System.Windows.Forms.TextBox fileSelectBox;
        private System.Windows.Forms.Button fileBrowseBtn;
        private System.Windows.Forms.GroupBox grpTagDetails;
        private System.Windows.Forms.TextBox tagSpeakerTitleTxt;
        private System.Windows.Forms.Label lblTagSpeakerTitleMorning;
        private System.Windows.Forms.TextBox tagSpeakerTxt;
        private System.Windows.Forms.TextBox tagScriptureTxt;
		private System.Windows.Forms.TextBox tagTitleTxt;
        private System.Windows.Forms.Label lblTagScriptureMorning;
        private System.Windows.Forms.Label lblTagTitleMorning;
		private System.Windows.Forms.Label lblTagSpeakerMorning;
        private System.Windows.Forms.Label lblTagDateMorning;
        private System.Windows.Forms.GroupBox grpProcess;
        private System.Windows.Forms.Label FilePercentDoneLbl;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox closeWhenDoneChk;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.GroupBox mainGroupBox;
        private System.Windows.Forms.DateTimePicker tagDate;
        private System.Windows.Forms.Label lblAmorPM;
        private System.Windows.Forms.CheckBox isPMCheckbox;
        private System.Windows.Forms.CheckBox isAMCheckbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar YoutubeProgress;
        private System.Windows.Forms.CheckBox shouldSkipVideoUploadChk;
        private System.Windows.Forms.CheckBox shouldSkipS3UploadChk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar FileProgress;
    }
}

