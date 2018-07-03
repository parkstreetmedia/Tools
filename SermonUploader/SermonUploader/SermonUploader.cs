using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Xml;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using MySql.Data.MySqlClient;
using SermonUploader.Properties;
using System.Globalization;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Threading.Tasks;
using System.Net.Http;
using System.Drawing.Imaging;

namespace SermonUploader
{
    public partial class SermonUploader : Form {
        //Web client used for all file uploads
        System.Net.WebClient FileUploadClient = new WebClient();
        //List of files to Upload
        Queue<FileInfo> FilesToUpload = new Queue<FileInfo>();
        Log TheLog;
        //To keep track of speaker titles 
        System.Collections.Generic.SortedDictionary<String, String> Speakers = new SortedDictionary<String, String>();
        //Tracking the progress of the Asych uploads so we don't have to pass the values back and forth
        int totalCompletedUploads = 0;
        //regarding the processing of the files
        private bool isCanceled = false;
        public bool IsCanceled {
            get {
                if ((this.isCanceled) && (this.TheLog != null)) {
                    this.TheLog.Write("-----Process Canceled by User-----");
                }
                return this.isCanceled;
            }
        }

        //if any aspect complains of an error during processing, this is false
        public bool isSuccessful;
        //Lame process, up here so we can cancel
        Process LameProcess;
        //what we doing?... check input, upload video, make HQ audio, make LQ audio, upload HQ audio, upload LQ audio, insert stuff into DB, email log. 
        private const int NUMOFTHINGSTODO = 7; //plus 1 for video
        //Let us know we are done uploading.. 
        private bool IsDoneUploading = false;
        //which service?
        public static bool IsThisAM { get; set; }
        //Keep track of Audio or Video
        public bool IsThisAudioOnly;
        //what's the video URL
        public string VideoLink;

        public SermonUploader() {
            InitializeComponent();

            //Connect the drag/drop 
            this.panelDropFile.DragEnter += new DragEventHandler(this.panelDropFile_DragEnter);
            this.panelDropFile.DragDrop += new DragEventHandler(this.panelDropFile_DragDrop);

            //Check settings...
            if (Settings.Default["LocalPathOfFiles"] == null || Settings.Default["LocalPathOfFiles"].ToString() == "" || Settings.Default["LocalPathOfFiles"].ToString() == "SetMe") {

                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                folderBrowserDialog1.Description = "Please select the default directory to browse for files";
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK) {
                    Settings.Default["LocalPathOfFiles"] = folderBrowserDialog1.SelectedPath;
                }
            }

            if (Settings.Default["LocalPathOfMP3s"] == null || Settings.Default["LocalPathOfMP3s"].ToString() == "" || Settings.Default["LocalPathOfMP3s"].ToString() == "SetMe") {

                FolderBrowserDialog folderBrowserDialog2 = new FolderBrowserDialog();
                folderBrowserDialog2.Description = "Please select where I'll save the MP3s";
                if (folderBrowserDialog2.ShowDialog() == DialogResult.OK) {
                    Settings.Default["LocalPathOfMP3s"] = folderBrowserDialog2.SelectedPath;
                }
            }

            Settings.Default.Save();

            //Attach the event handlers for the async uploads
            this.FileUploadClient.UploadFileCompleted += new UploadFileCompletedEventHandler(this.FileUpload_Completed);
            this.FileUploadClient.UploadProgressChanged += new UploadProgressChangedEventHandler(this.FileUpload_ProgressChanged);

            //Auto select the latest file in the sermon wave folder
            if (!Directory.Exists(Settings.Default["LocalPathOfFiles"].ToString())) {
                MessageBox.Show("The default file path doesn't exist, please go into the config and set a valid path", "Error in Config");
            }
            else {
                String mostCurrentFile = "";

                DateTime mostCurrentDate = DateTime.Parse("01/01/1870");
                foreach (String aFilePath in Directory.GetFiles(Settings.Default["LocalPathOfFiles"].ToString())) {
                    DateTime lookAtTheTime = File.GetCreationTime(aFilePath);
                    if (lookAtTheTime > mostCurrentDate) {

                        mostCurrentDate = lookAtTheTime;
                        mostCurrentFile = aFilePath;
                    }
                }

                this.fileSelectBox.Text = mostCurrentFile;
                this.fileBrowseDialog.FileName = mostCurrentFile;
            }

            ServiceType whichService = new ServiceType();
            whichService.ShowDialog();

            if (IsThisAM) {
                this.isAMCheckbox.Checked = true;
                this.isPMCheckbox.Checked = false;

            }
            else {
                this.isAMCheckbox.Checked = false;
                this.isPMCheckbox.Checked = true;
            }


            //Load autocomplete values for the speaker
            string mainMinister = "";
            if (File.Exists("speakers.config")) {
                FileStream stream = new FileStream("speakers.config", FileMode.Open, FileAccess.Read, FileShare.None);
                StreamReader read = new StreamReader(stream);
                AutoCompleteStringCollection complete = new AutoCompleteStringCollection();
                while (read.Peek() > 0) {
                    //Seperate the speaker's name from their title...
                    String theLine = read.ReadLine().Trim();
                    if (theLine != null) {
                        if (theLine.Contains(",")) {
                            String name = theLine.Substring(0, theLine.IndexOf(","));
                            String title = theLine.Substring(theLine.IndexOf(","));
                            name = name.Replace(",", "").Trim();
                            title = title.Replace(",", "").Trim();
                            complete.Add(name);
                            if (mainMinister == "") {
                                mainMinister = name;
                            }
                            this.Speakers.Add(name, title);
                        }
                    }
                }

                read.Close();
                read.Dispose();
                stream.Close();
                stream.Dispose();

                this.tagSpeakerTxt.AutoCompleteMode = AutoCompleteMode.Suggest;
                this.tagSpeakerTxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.tagSpeakerTxt.AutoCompleteCustomSource = complete;
            }

            //Load the config values 
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "mp3.jpg")) {
                MessageBox.Show("I cannot find the podcast image, it should be at: " + AppDomain.CurrentDomain.BaseDirectory + "mp3.jpg" + ", please correct this", "Error missing file");
            }

            // ahhh darn this.tagSpeakerTxt.Text = "Gordon Hugenberger";
            this.tagSpeakerTxt.Text = mainMinister;
            //if (this.isPMCheckbox.Checked)
            //{
            //    this.tagSpeakerTxt.Text = "Philip Thorne";
            //}
        }

        private string ValidateInput() {
            string errors = "";
            //Check that stuff is filled out
            if (!this.CheckInput()) {
                errors = errors + "You must fill out all the tag properties to continue\n";
            }

            //don't allow all caps
            if (this.IsAllUpperCase(this.tagTitleTxt.Text.Trim())) {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                this.tagTitleTxt.Text = textInfo.ToTitleCase(this.tagTitleTxt.Text);
                errors = errors + "You tried to write the sermon title in all caps, I fixed it, but please don't do that.\n";
            }

            //whhy???????
            if (this.tagTitleTxt.Text.Contains("??")) {
                this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace("??", "?");
                errors = errors + "You put in two question marks???? why????? I fixed it for you, please don't do that to me\n";

            }

            //don't allow all caps
            if (this.IsAllUpperCase(this.tagScriptureTxt.Text.Trim())) {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                this.tagScriptureTxt.Text = textInfo.ToTitleCase(this.tagScriptureTxt.Text);
                errors = errors + "You tried to write the scripture in all caps, I fixed it, but please don't do that.\n";
            }

            //make sure (evening) is added to the evening sermon
            if ((this.isPMCheckbox.Checked) && ((this.tagTitleTxt.Text.Contains("(evening sermon)") == false))) {
                this.tagTitleTxt.Text = "(evening sermon) " + this.tagTitleTxt.Text;
                errors = errors + "Please always add (evening sermon) to the front of the evening sermons.\n";
            }

            //replace stupid copy/paste problems

            if (this.tagTitleTxt.Text.IndexOf('\u2013') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u2013', '-');
            if (this.tagTitleTxt.Text.IndexOf('\u2014') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u2014', '-');
            if (this.tagTitleTxt.Text.IndexOf('\u2015') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u2015', '-');
            if (this.tagTitleTxt.Text.IndexOf('\u2017') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u2017', '_');
            if (this.tagTitleTxt.Text.IndexOf('\u2018') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u2018', '\'');
            if (this.tagTitleTxt.Text.IndexOf('\u2019') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u2019', '\'');
            if (this.tagTitleTxt.Text.IndexOf('\u201a') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u201a', ',');
            if (this.tagTitleTxt.Text.IndexOf('\u201b') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u201b', '\'');
            if (this.tagTitleTxt.Text.IndexOf('\u201c') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u201c', '\"');
            if (this.tagTitleTxt.Text.IndexOf('\u201d') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u201d', '\"');
            if (this.tagTitleTxt.Text.IndexOf('\u201e') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u201e', '\"');
            if (this.tagTitleTxt.Text.IndexOf('\u2026') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace("\u2026", "...");
            if (this.tagTitleTxt.Text.IndexOf('\u2032') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u2032', '\'');
            if (this.tagTitleTxt.Text.IndexOf('\u2033') > -1) this.tagTitleTxt.Text = this.tagTitleTxt.Text.Replace('\u2033', '\"');

            if (this.tagScriptureTxt.Text.IndexOf('\u2013') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u2013', '-');
            if (this.tagScriptureTxt.Text.IndexOf('\u2014') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u2014', '-');
            if (this.tagScriptureTxt.Text.IndexOf('\u2015') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u2015', '-');
            if (this.tagScriptureTxt.Text.IndexOf('\u2017') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u2017', '_');
            if (this.tagScriptureTxt.Text.IndexOf('\u2018') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u2018', '\'');
            if (this.tagScriptureTxt.Text.IndexOf('\u2019') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u2019', '\'');
            if (this.tagScriptureTxt.Text.IndexOf('\u201a') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u201a', ',');
            if (this.tagScriptureTxt.Text.IndexOf('\u201b') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u201b', '\'');
            if (this.tagScriptureTxt.Text.IndexOf('\u201c') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u201c', '\"');
            if (this.tagScriptureTxt.Text.IndexOf('\u201d') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u201d', '\"');
            if (this.tagScriptureTxt.Text.IndexOf('\u201e') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u201e', '\"');
            if (this.tagScriptureTxt.Text.IndexOf('\u2026') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace("\u2026", "...");
            if (this.tagScriptureTxt.Text.IndexOf('\u2032') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u2032', '\'');
            if (this.tagScriptureTxt.Text.IndexOf('\u2033') > -1) this.tagScriptureTxt.Text = this.tagScriptureTxt.Text.Replace('\u2033', '\"');

            //Try to avoid people picking the wrong audio file...
            if (this.isAMCheckbox.Checked) {
                if (this.fileSelectBox.Text.Contains("-pm")) {
                    errors = errors + "You've picked the wrong file...Please be more careful!";
                }
            }

            //Try to avoid people picking the wrong audio file...
            if (this.isPMCheckbox.Checked) {
                if (this.fileSelectBox.Text.Contains("-am")) {
                    errors = errors + "You've picked the wrong audio file...Please be more careful!";
                }
            }

            return errors;
        }

        private void btnUpload_Click(object sender, EventArgs e) {

            DialogResult resultFromToday;
            DateTime fileDateTime = File.GetCreationTime(this.fileSelectBox.Text);
            if (fileDateTime.Date != DateTime.Now.Date) {
                resultFromToday = MessageBox.Show("The sermon file you selected is not from today...Do you want to fix it?", "What day is this...", MessageBoxButtons.YesNo);
                if (resultFromToday == DialogResult.Yes) {
                    return;
                }
            }

            //Try and avoid the user from entering an incorrect date
            if (this.tagDate.Value.DayOfWeek != DayOfWeek.Sunday) {
                DialogResult resultNotSunday = MessageBox.Show("The date you selected is not a sunday. Do you want to fix that?", "What day is this...", MessageBoxButtons.YesNo);
                if (resultNotSunday == DialogResult.Yes) {
                    return;
                }
            }


            string errors = this.ValidateInput();
            if (errors.Trim() != string.Empty) {
                MessageBox.Show(errors, "errors");
                return;
            }

            //Show the cancel 
            this.btnCancel.Visible = true;
            this.btnCancel.Enabled = true;
            this.btnUpload.Enabled = false;
            this.isSuccessful = true;
            this.progress.Enabled = true;
            this.progress.Maximum = NUMOFTHINGSTODO;
            if (IsThisAudioOnly) {
                this.progress.Maximum--;
            }

            this.progress.Step = 1;

            //cancel check
            if (this.isCanceled) {
                return;
            }

            //Start the log
            this.TheLog = new Log(Settings.Default["LocalPathOfMP3s"].ToString());

            this.TheLog.Write("Started Processing on: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());

            this.progress.PerformStep();
            this.progress.Update();

            if (!this.IsThisAudioOnly) {

                string ClientSecretsFileName = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\youtubeAPIKey.json";

                try {
                    this.UploadVideo(ClientSecretsFileName);

                    int videoTimeCount = 0;
                    while ((this.VideoLink == null) && (videoTimeCount < 172800)) {
                        System.Threading.Thread.Sleep(1000);
                        Application.DoEvents();
                        videoTimeCount++;
                    }

                    this.progress.PerformStep();
                    this.progress.Update();

                }
                catch (AggregateException ex) {
                    string allErrors = "Things went wrong, error messages follow:\n";
                    foreach (var ee in ex.InnerExceptions) {
                        allErrors = allErrors + " " + ee.Message;
                    }
                    MessageBox.Show(allErrors);
                    this.isCanceled = true;
                    TheLog.Write("There was an error uploading to YouTube: " + allErrors);
                }

            }

            if (this.isCanceled) {
                return;
            }

            this.FileProgress.Value = 0;
            this.FileProgress.Maximum = 200;

            //create the mp3s 
            if (!this.EncodeMP3(this.fileSelectBox.Text.Trim(), false)) {
                TheLog.Write("Creating the LQ MP3 failed...");
            }

            if (!this.EncodeMP3(this.fileSelectBox.Text.Trim(), true)) {
                TheLog.Write("Creating the HQ MP3 failed...");
            }

            //cancel check
            if (this.isCanceled) {
                return;
            }

            if (this.isSuccessful) {

                //update the MP3 tags
                this.SetMP3Tag();

                this.FileProgress.Value = 0;
                this.FileProgress.Maximum = 100;

                //Setup the upload client
                string ftpUser = Settings.Default["FTPUser"].ToString();
                string ftpPassword = Settings.Default["FTPPassword"].ToString();
                this.FileUploadClient.Credentials = new NetworkCredential(ftpUser, ftpPassword);
                //Upload the files, it will keep going until there are no more uploads, or the process is canceled     
                this.FileUpload_Completed(null, null);
            }

            this.progress.PerformStep();
            this.progress.Update();

            //cancel check
            if (this.isCanceled) {
                return;
            }

            //Wait for uploads to complete
            int aTimeCount = 0;
            while ((!this.IsDoneUploading) && (aTimeCount < 172800)) {
                System.Threading.Thread.Sleep(1000);
                Application.DoEvents();
                aTimeCount++;
            }

            //Tell mysql
            if (!this.InsertMP3RecordIntoMySQL()) {
                this.isSuccessful = false;
            }

            this.progress.PerformStep();
            this.progress.Update();

            //cancel check
            if (this.isCanceled) {
                return;
            }

            this.progress.Value = this.progress.Maximum;
            this.FileProgress.Value = this.FileProgress.Maximum;
            this.progress.Update();
            this.FileProgress.Update();

            this.TheLog.Write("Ended Processing on: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
            this.TheLog.Close();

            //Email the log 
            this.EmailLogFile();

            //Report what happened
            if ((this.isSuccessful) && (this.closeWhenDoneChk.Checked)) {
                Application.Exit();
            }

            if (!this.isSuccessful) {
                MessageBox.Show("There were errors when processing, please review the log below or the log file itself.", "Error Proccessing");
            }

            if ((this.isSuccessful) && (!this.closeWhenDoneChk.Checked)) {
                MessageBox.Show("Successfully Completed, for further information, view the log file", "Success");
                this.ResetForm();
            }
        }

        #region Upload file to YouTube

        public async Task UploadVideo(string secretsFile) {
            UserCredential credential;
            using (var stream = new FileStream(secretsFile, FileMode.Open, FileAccess.Read)) {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] { YouTubeService.Scope.YoutubeUpload },
                    "PSCMedia",
                    CancellationToken.None
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name

            });

            youtubeService.HttpClient.Timeout = TimeSpan.FromMinutes(10);

            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = this.tagTitleTxt.Text.Trim() + " - " + this.tagScriptureTxt.Text.Trim();
            string date = this.tagDate.Value.ToString("yyyy-MM-dd") + ((this.isAMCheckbox.Checked) ? "am" : "pm");
            video.Snippet.Description = this.tagSpeakerTxt.Text.Trim() + ", " + this.tagSpeakerTitleTxt.Text.Trim() +
                "\nDate: " + " " + date;
            video.Snippet.CategoryId = "29"; // Nonprofits & Activism
            video.Status = new VideoStatus();
            //test - hide from youtube
            //video.Status.PrivacyStatus = "Private";            
            video.Status.PrivacyStatus = "Public";
            var filePath = this.fileSelectBox.Text;

            using (var fileStream = new FileStream(filePath, FileMode.Open)) {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ChunkSize = 30 * 1 * 1024 * 1024; //30MB chunks instead of 1MB
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                await videosInsertRequest.UploadAsync();
            }


           if ((youtubeService != null) && (!string.IsNullOrEmpty(video.Id))) {
                //generate image 
                bool IsThereAnImage = false;
                string tPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\thumbnails\\";
                string imageSaved = tPath + "\\generated\\" + this.nameFilesByConvention() + ".jpg";

                try {
                    string speaker = "";
                    string line1 = this.tagTitleTxt.Text.Trim();
                    string line2 = this.tagScriptureTxt.Text.Trim();

                    try {
                        int loc = this.tagSpeakerTxt.Text.IndexOf(" ");
                        if (loc > 0) {
                            string speakerLast = this.tagSpeakerTxt.Text.Substring(loc);
                            speakerLast = speakerLast.ToLower().Trim();
                            if (File.Exists(tPath + speakerLast + ".png")) {
                                speaker = speakerLast + ".png";
                            }
                            else {
                                speaker = "pulpit.png";
                            }
                        }
                    }
                    catch (Exception) {
                        //there are weird names
                        speaker = "pulpit.png";
                    }

                    Image imageBackground = Image.FromFile(tPath + "background.png");
                    Image imageSpeaker = Image.FromFile(tPath + speaker);

                    Image img = new Bitmap(imageBackground.Width, imageBackground.Height);
                    using (Graphics gr = Graphics.FromImage(img)) {
                        gr.DrawImage(imageBackground, new Point(0, 0));
                        gr.DrawImage(imageSpeaker, 0, 0, 1280, 720);

                        using (Font font1 = new Font("Open Sans", 100, FontStyle.Bold, GraphicsUnit.Pixel)) {
                            Rectangle rect1 = new Rectangle(0, 430, 1280, 150);

                            StringFormat stringFormat = new StringFormat();
                            stringFormat.Alignment = StringAlignment.Near;
                            stringFormat.LineAlignment = StringAlignment.Center;
                            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                            Font goodFont = FindFont(gr, line1, rect1.Size, font1);
                            gr.DrawString(line1, goodFont, Brushes.White, rect1, stringFormat);
                        }

                        using (Font font1 = new Font("Open Sans", 100, FontStyle.Bold, GraphicsUnit.Pixel)) {
                            Rectangle rect1 = new Rectangle(0, 580, 1280, 140);

                            StringFormat stringFormat = new StringFormat();
                            stringFormat.Alignment = StringAlignment.Near;
                            stringFormat.LineAlignment = StringAlignment.Near;
                            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                            Font goodFont = FindFont(gr, line2, rect1.Size, font1);
                            if (goodFont.Size > 100) {
                                goodFont = new Font(goodFont.FontFamily, 100);
                            }
                            gr.DrawString(line2, goodFont, Brushes.White, rect1, stringFormat);
                        }
                    }

                    img.Save(imageSaved, ImageFormat.Jpeg);

                }
                catch (Exception) {
                    IsThereAnImage = false;
                }

                if (IsThereAnImage) {
                    using (var tStream = new FileStream(imageSaved, FileMode.Open)) {
                        var tInsertRequest = youtubeService.Thumbnails.Set(video.Id, tStream, "image/jpeg");
                        await tInsertRequest.UploadAsync();
                    }
                }
            }
        }

        private void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress) {
            switch (progress.Status) {
                case UploadStatus.Uploading:
                    int fileSize = (int)progress.BytesSent / 100;
                    SetControlPropertyThreadSafe(this.YoutubeProgress, "Value", fileSize);
                    break;

                case UploadStatus.Failed:
                    MessageBox.Show("There was an error uploading...aborting the processing.. try again, the error was:\n" + progress.Exception);
                    this.isCanceled = true;
                    this.isSuccessful = false;
                    this.TheLog.Write("There was an error uploading the file to YouTube, error was: " + progress.Exception);
                    break;
            }
        }

        private void videosInsertRequest_ResponseReceived(Video video) {
            SetControlPropertyThreadSafe(this.YoutubeProgress, "Value", this.YoutubeProgress.Maximum);
            this.VideoLink = video.Id;
            this.TheLog.Write("Successfully uploaded the video to youtube: https://youtu.be/" + video.Id);

        }


        #endregion

        #region Normalize Audio Level
        //ffmpeg -i video.avi -af "volumedetect" -f null /dev/null
        //max_volume: -5.0 dB
        //then ffmpeg -i input.wav -af "volume=5dB" -vn -b:a 128k -c:a libmp3lame output.mp3
        //but after looking.. the videos are good.. and editing the audio.. you should do it nice.. so.. no need
        #endregion

        #region Set ID3 tag on the MP3s

        private bool SetMP3Tag()
        {
            foreach (FileInfo aFile in this.FilesToUpload)
            {
                try
                {
                    //v2.3
                    TagLib.Id3v2.Tag.DefaultVersion = 3;
                    TagLib.Id3v2.Tag.ForceDefaultVersion = true;
                   
                    TagLib.File f = TagLib.File.Create(aFile.FullName);

                    f.Tag.Title = this.tagTitleTxt.Text.Trim().Replace("\"", "");
                    f.Tag.Album = this.tagScriptureTxt.Text.Trim().Replace("\"", "").Replace("'", "");
                    f.Tag.Artists = new string[] { this.tagSpeakerTxt.Text.Trim() }; 
                    f.Tag.Year = (uint)DateTime.Today.Year;
                    f.Tag.Genres = new string[] { "Speech" };
                    f.Tag.Copyright = "Park Street Church " + DateTime.Today.Year.ToString();
                    string imageFile = AppDomain.CurrentDomain.BaseDirectory + "mp3.jpg";
                    if (System.IO.File.Exists(imageFile))
                    {
                        TagLib.IPicture newArt = new TagLib.Picture(imageFile);
                        f.Tag.Pictures = new TagLib.IPicture[1] { newArt };
                    }

                    f.Save();

                    //v2.4
                    TagLib.File f4 = TagLib.File.Create(aFile.FullName);

                    f4.Tag.Title = this.tagTitleTxt.Text.Trim().Replace("\"", "");
                    f4.Tag.Album = this.tagScriptureTxt.Text.Trim().Replace("\"", "").Replace("'", "");
                    f4.Tag.Artists = new string[] { this.tagSpeakerTxt.Text.Trim() };
                    f4.Tag.Year = (uint)DateTime.Today.Year;
                    f4.Tag.Copyright = "Park Street Church " + DateTime.Today.Year.ToString();
                    f4.Tag.Genres = new string[] { "Speech" };
                    if (System.IO.File.Exists(imageFile))
                    {
                        TagLib.IPicture newArt = new TagLib.Picture(imageFile);
                        f4.Tag.Pictures = new TagLib.IPicture[1] { newArt };
                    }

                    f4.Save();


                    this.TheLog.Write("Wrote the MP3 tag for " + aFile.FullName);
            
                }
                catch (Exception ex)
                {
                    this.TheLog.Write("ERROR writing the MP3 tag: "+ ex.Message);            
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Upload MP3s to the FTP

        /// <summary>
        /// Upload a file. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileUpload_ProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            if (this.isCanceled)
            {
                this.FileUploadClient.CancelAsync();
                //So the variables will be cleared out ...
                this.FilesToUpload.Clear();
                this.ResetForm();
            }
            else
            {
                try
                {
                    //Update the progress for the user                    
                    SetControlPropertyThreadSafe(this.FileProgress, "Value", e.ProgressPercentage);                    
                    this.Filelbl.Text = e.ProgressPercentage + "%";
                }
                catch (Exception)
                {
                    //Sometimes the percent change throws silly errors.. Have yet to understand it... 66 > 100 according to the exceptions
                }
            }
        }

        /// <summary>
        /// Either start the next file upload or tell the application that we are done
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileUpload_Completed(object sender, AsyncCompletedEventArgs e)
        {
            //If a thread completed...
            if ((e != null))
            {
                //if a download finished...
                if ((e.UserState != null) && (e.UserState is File)
                    && (e.Cancelled == false) && (e.Error == null))
                {
                    FileInfo justUploaded = (FileInfo)e.UserState;
                    this.totalCompletedUploads++;
                    //With a good upload, tell the log
                    this.TheLog.Write("Success: Upload of file: " + justUploaded.Name);
                    SetControlPropertyThreadSafe(this.FileProgress, "Value", 0); 

                }
            }

            //There was a problem 
            if ((e != null) && (e.Error != null) && (e.Error.Message != "") && (!e.Cancelled))
            {
                this.TheLog.Write("Error: Failed uploading a file, file & error message follows: " + ((FileInfo)e.UserState).Name + " "
                    + ((e.Error.InnerException == null) ? e.Error.Message : e.Error.InnerException.ToString()));

                this.isSuccessful = false;
            }

            //We are all done, tell someone
            if ((!this.isCanceled) && (this.FilesToUpload.Count <= 0))
            {
                this.IsDoneUploading = true;
            }

            //An upload finished, for one reason or another, start the next one
            if ((!this.isCanceled) && (this.FilesToUpload.Count > 0))
            {
                //Get the file we want to upload...
                FileInfo working = this.FilesToUpload.Dequeue();

                //Show the user what we are uploading
                //+1 on total upload completed so it says 1/1 or 1/2. as it is working on it...
                String msgOut = "Uploading file: " + working.Name;
                this.Filelbl.Text = msgOut;
                this.TheLog.Write(msgOut);

                //Upload the file, finally
                try
                {
                    String uploadPath = "ftp://" + "media.parkstreet.org/media/audio/" + working.Name;
                    this.FileUploadClient.UploadFileAsync(new Uri(uploadPath), null, working.FullName, working);
                }
                catch (Exception ex)
                {
                    //If we cannot upload the file.. say so
                    this.TheLog.Write("Failed to start the upload for the file: " + working.Name + "\r\nException follows\r\n" + ex.Message);
                    this.isSuccessful = false;
                }
            }
        }

        #endregion Upload

        #region Create MP3 from the file

        /// <summary>
        /// Create an MP3
        /// </summary>
        private bool EncodeMP3(String waveFileName, bool isHighQuality)
        {
            try
            {
                //Add mp3 encoding options
                // -i "test.avi" -codec:a libmp3lame -vn -f mp3 -qscale:a 4 "testout.mp3"
                String args = " -codec:a libmp3lame -vn -f mp3 ";
                String fileName = this.nameFilesByConvention() + ".mp3";

                if (isHighQuality)
                {
                    args = args + " -qscale:a 3 ";
                    fileName = fileName.Insert(fileName.LastIndexOf("."), "-hq");
                }
                else
                {
                    //mono, 22050 Hz, highest compression level / lowest bitrate, 16 bit-depth
                    args = args + " -qscale:a 9 -ar 22050 -sample_fmt s16 -ac 1 ";
                }

                FileInfo theMP3 = new FileInfo(fileName);
                args = "-i \"" + this.fileSelectBox.Text.Trim() + "\" " + args + "\"" + fileName + "\"";
                ProcessStartInfo lameInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "ffmpeg.exe", args);
                TheLog.Write("Converting to MP3: " + AppDomain.CurrentDomain.BaseDirectory + "ffmpeg.exe " + args);
                lameInfo.CreateNoWindow = true;
                lameInfo.ErrorDialog = false;
                lameInfo.RedirectStandardError = true;
                lameInfo.RedirectStandardOutput = true;
                lameInfo.UseShellExecute = false;

                //Setup the process
                this.LameProcess = new Process();
                this.LameProcess.StartInfo = lameInfo;

                //Monitor the process
                this.LameProcess.OutputDataReceived += new DataReceivedEventHandler(this.lameProcess_OutputDataReceived);
                this.LameProcess.ErrorDataReceived += new DataReceivedEventHandler(this.lameProcess_OutputDataReceived);

                //Start the process
                this.LameProcess.Start();
                this.LameProcess.BeginOutputReadLine();
                this.LameProcess.BeginErrorReadLine();

                //Wait for everything to finish but give up after 3 hours, it cannot take her that long to get ready, and by then you'll be out of beer
                int anTimeCount = 0;
                while ((!this.LameProcess.HasExited) && (anTimeCount < 1080))
                {
                    System.Threading.Thread.Sleep(1000);
                    Application.DoEvents();
                    anTimeCount++;
                }

                //We are either done or giving up
                this.LameProcess.CancelOutputRead();
                this.LameProcess.CancelErrorRead();
                this.LameProcess.Close();
                this.LameProcess.Dispose();

                //This looks really really silly.. but sometimes lame doesn't close, and somehow close returns true without actually stopping lame...
                try
                {
                    this.LameProcess.Kill();
                    this.LameProcess.Dispose();
                }
                catch { }

                if (!theMP3.Exists)
                {
                    TheLog.Write("Error: Writing the MP3 file: " + theMP3.FullName + " This is an error that cannot be ignored! Exiting.");
                    this.isSuccessful = false;
                    return false;
                }

                this.FilesToUpload.Enqueue(theMP3);

                this.progress.PerformStep();
                this.progress.Update();

                return true;
            }
            catch (Exception ex)
            {
                this.TheLog.Write("Error: There was an error starting the process to create an MP3 file, something very bad happened! Exception follows: " + ex.Message);
                this.isSuccessful = false;
                return false;
            }
        }

        /// <summary>
        /// Monitor the lame process, update the progress bar, and allow canceling 
        /// </summary>
        /// <param name="sendingProcess"></param>
        /// <param name="outLine"></param>
        private void lameProcess_OutputDataReceived(object sendingProcess, DataReceivedEventArgs outLine)
        {
            // Collect the lame process output.
            if (!String.IsNullOrEmpty(outLine.Data))
            {

                //The duration isn't usually correct from the video cutting.. so there's no way to be accurate
                SetControlPropertyThreadSafe(this.FileProgress, "Value", this.FileProgress.Value + 1);
                if (this.FileProgress.Value == 98)
                {
                    SetControlPropertyThreadSafe(this.FileProgress, "Value", 50);
                }
            }

            //Update the GUI
            Application.DoEvents();

            //if the user canceled.. exit
            if (this.isCanceled)
            {
                this.LameProcess.Kill();
                this.TheLog.Write("Success: Killed the MP3 Encoding Process at the user's request");
            }
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }

        #endregion CreateMP3

        #region helperMethods

        //This function checks the room size and your text and appropriate font for your text to fit in room
        private Font FindFont(System.Drawing.Graphics g, string longString, Size Room, Font PreferedFont) {
            StringFormat format = new StringFormat();
            RectangleF rect = new RectangleF(0, 0, 1000, 1000);
            CharacterRange[] ranges = { new CharacterRange(0, longString.Length) };
            Region[] regions = new Region[1];
            format.SetMeasurableCharacterRanges(ranges);
            regions = g.MeasureCharacterRanges(longString, PreferedFont, rect, format);
            RectangleF RealSize = regions[0].GetBounds(g);
            float HeightScaleRatio = Room.Height / RealSize.Height;
            float WidthScaleRatio = Room.Width / RealSize.Width;
            float ScaleRatio = WidthScaleRatio;
            if (HeightScaleRatio < WidthScaleRatio) {
                ScaleRatio = HeightScaleRatio;
            }
            float ScaleFontSize = PreferedFont.Size * ScaleRatio;
            return new Font(PreferedFont.FontFamily, ScaleFontSize);
        }

        bool IsAllUpperCase(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Reset the main form to the first start values
        /// </summary>
        private void ResetForm()
        {
            this.btnUpload.Enabled = true;
            this.btnCancel.Enabled = false;
            this.btnCancel.Visible = false;
            this.progress.Value = 0;
            this.progress.Update();
            this.progress.Enabled = false;
            this.FilePercentDoneLbl.Text = "";
            this.FileProgress.Value = 0;
            this.FileProgress.Update();
            this.FileProgress.Enabled = false;

            this.lblFileDrop.Text = "Select file using \"Browse\"";
            this.lblFileDrop2.Text = "or Drop the file here";
            this.lblFileDrop.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            this.lblFileDrop2.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);

            this.tagScriptureTxt.Text = "";
            this.tagSpeakerTitleTxt.Text = "";
            this.tagSpeakerTxt.Text = "";
            this.fileSelectBox.Text = "";

            this.closeWhenDoneChk.Checked = true;
            this.Refresh();
        }

        /// <summary>
        /// Allow the user to browse for files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileBrowseBtn_Click(object sender, EventArgs e)
        {
            if (this.fileBrowseDialog.ShowDialog() == DialogResult.OK)
            {
                if (this.CheckFileSelected(this.fileBrowseDialog.FileName))
                {
                    this.fileSelectBox.Text = this.fileBrowseDialog.FileName;
                }
            }
        }

        private void panelDropFile_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }


        private void panelDropFile_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 1)
            {
                MessageBox.Show("Whooa there, too many files... try again");
                return;
            }
            string file = files[0].Trim();
            if (file == "")
            {
                MessageBox.Show("No file...try again");
                return;
            }

            if (this.CheckFileSelected(this.fileSelectBox.Text))
            {
                //yay
            }
        }

        private bool CheckFileSelected(string file)
        {
            if (!File.Exists(file))
            {
                MessageBox.Show("File doesn't exist... what did you do?!?...try again");
                return false;
            }
            FileInfo fi = new FileInfo(file);
            if (fi == null)
            {
                MessageBox.Show("The file isn't available for some reason...try again");
                return false;
            }

            string ext = fi.Extension.ToLower().Trim();
            if ((ext == "") || ((ext != ".avi") && (ext != ".mp4") && (ext != ".mov") && (ext != ".wav") && (ext != ".mp3")))
            {
                MessageBox.Show("The file needs to be either a video or an audio file... an mp4, avi, mov, or wave...try again");
                return false;
            }

            if ((ext.Contains("wav")) || (ext.Contains("mp3")))
            {
                this.IsThisAudioOnly = true;
            }
            else
            {
                this.IsThisAudioOnly = false;
            }

            if (IsFileLocked(fi))
            {
                MessageBox.Show("The file is opened somewhere else... please close the video or audio editor or whereever else the video may be open...try again");
                return false;
            }

            this.fileSelectBox.Text = file;

            this.lblFileDrop.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            this.lblFileDrop.Text = fi.Name;
            this.lblFileDrop2.Text = "";
            //yes, this means you can't upload 2 TB files and get progress..that's the least of the future-proofing problems here :-) 
            this.YoutubeProgress.Maximum = (int)(fi.Length / 100);

            return true;
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }

        /// <summary>
        /// check, and perhaps one day validate, if we ever have something to do so against, the user input
        /// </summary>
        /// <returns>true if valid</returns>
        private bool CheckInput()
        {
            if ((this.tagDate.Value != null) && (!this.tagScriptureTxt.Text.Trim().Equals("")) &&
                (!this.tagSpeakerTxt.Text.Trim().Equals("")) &&
                (!this.tagTitleTxt.Text.Trim().Equals("")) &&
                (!this.fileSelectBox.Text.Trim().Equals("")) &&
                (!this.tagSpeakerTitleTxt.Text.Trim().Equals("")) &&
                (File.Exists(this.fileSelectBox.Text)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  Craft the name of the output file, the convention we follow is: YYYY-MM-DD-am/pm (with the am or pm in lowercase)
        /// </summary>
        /// <returns>The name that any outputed file should be called according to current convention</returns>
        private String nameFilesByConvention()
        {
            String dateInfo = "";
            String amOrPm = "am";
            if (this.isPMCheckbox.Checked)
            {
                amOrPm = "pm";
            }
            dateInfo = this.tagDate.Value.ToString("yyyy-MM-dd");

            string fullPath = Settings.Default["LocalPathOfMP3s"].ToString() + "\\" + dateInfo + "-" + amOrPm.ToLower();
            return fullPath;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Wouldn't some help be nice?\nAlways remember to double knot your shoes before taking the first step of a long journey.\n\nCheck github.com/parkstreetmedia if more specific detail is required.", "You're Welcome");
        }

        private void tagSpeakerTxt_TextChanged(object sender, EventArgs e)
        {
            if (this.tagSpeakerTxt.Text != null)
            {
                if (this.tagSpeakerTxt.Text != String.Empty)
                {
                    if (this.Speakers.ContainsKey(this.tagSpeakerTxt.Text.Trim()))
                    {
                        string possibleTitle = "";
                        this.Speakers.TryGetValue(this.tagSpeakerTxt.Text.Trim(), out possibleTitle);
                        if ((possibleTitle != null) && (possibleTitle != string.Empty))
                        {
                            this.tagSpeakerTitleTxt.Text = possibleTitle;
                        }
                    }
                    else
                    {

                        //if the speaker is not known, make sure the title is cleared...
                        this.tagSpeakerTitleTxt.Text = "";
                    }
                }

            }
        }


        private bool InsertMP3RecordIntoMySQL()
        {
            
            string isAMorPM = ((this.isAMCheckbox.Checked) ? "am" : "pm");

            string insertStatement = "INSERT INTO `sermons` (`FileName`, `Date`, `IsAM`, `SermonTitle`, `SpeakersName`, `SpeakersTitle`, `ScriptureReference`,"
             + "`HasAudioHQ`, `LengthInBytes`, `fromWhichService`, `VideoLink`) VALUES"
             + "(@fileName, @date, @isAM, @sermonTitle, @speakersName, @speakersTitle, @scriptureRef, @hasHQAudio,"
             + "@fileSize, @fromWhichService, @videoLink);";

            string fileName = this.tagDate.Value.ToString("yyyy-MM-dd") + "-" + isAMorPM;
            string date = this.tagDate.Value.ToString("yyyy-MM-dd");
            string isAM = ((this.isAMCheckbox.Checked) ? "1" : "0");
            string sermonTitle = this.tagTitleTxt.Text.Trim();
            string speakersName = this.tagSpeakerTxt.Text.Trim();
            string speakersTitle = this.tagSpeakerTitleTxt.Text.Trim();
            string scriptureRef = this.tagScriptureTxt.Text.Trim();
            string videoLink = "";
            if (this.VideoLink != String.Empty)
            {
                videoLink = "https://www.youtube.com/watch?v=" + this.VideoLink;
            }
            string hasHQAudio = "1";
            //this is wrong, but we know the file exists. 
            string fileSize = new FileInfo(this.fileSelectBox.Text).Length.ToString();
            if (File.Exists(this.nameFilesByConvention() + "-hq.mp3"))
            {
                fileSize = new FileInfo(this.nameFilesByConvention() + "-hq.mp3").Length.ToString();
            }
            string fromWhichService = "11:00";

            string connString = Settings.Default["MySQLConnString"].ToString();
            MySqlConnection conn = new MySqlConnection(connString);

            try
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = insertStatement;
                cmd.Parameters.Add("@fileName", MySqlDbType.VarChar).Value = fileName;
                cmd.Parameters.Add("@date", MySqlDbType.VarChar).Value = date;
                cmd.Parameters.Add("@isAM", MySqlDbType.VarChar).Value = isAM;
                cmd.Parameters.Add("@sermonTitle", MySqlDbType.VarChar).Value = sermonTitle;
                cmd.Parameters.Add("@speakersName", MySqlDbType.VarChar).Value = speakersName;
                cmd.Parameters.Add("@speakersTitle", MySqlDbType.VarChar).Value = speakersTitle;
                cmd.Parameters.Add("@scriptureRef", MySqlDbType.VarChar).Value = scriptureRef;
                cmd.Parameters.Add("@hasHQAudio", MySqlDbType.VarChar).Value = hasHQAudio;
                cmd.Parameters.Add("@fileSize", MySqlDbType.VarChar).Value = fileSize;
                cmd.Parameters.Add("@fromWhichService", MySqlDbType.VarChar).Value = fromWhichService;
                cmd.Parameters.Add("@videoLink", MySqlDbType.VarChar).Value = videoLink;

                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                this.TheLog.Write("Successfully inserted the mp3 meta information into the mysql database");

            }
            catch (Exception ex)
            {
                this.TheLog.Write("Error trying to insert the morning record into the MySQL database, Exception is: " + ex.InnerException);
                return false;
            }

            finally
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }

            return true;
        }

        #endregion helperMethods

        private void EmailLogFile()
        {
            try
            {
                string fromEmail = Settings.Default["SenderGmailAccount"].ToString();
                MailAddress fromAddress = new MailAddress(fromEmail, "RadioRoom-NoReply");
                MailMessage theMsg = new MailMessage();
                theMsg.From = fromAddress;
                //add other addresses with commas and spaces: person1, person2, person3
                string toEmail = Settings.Default["ReportEmail"].ToString();
                theMsg.To.Add(toEmail);
                theMsg.Subject = "Radio Room Log File for " + DateTime.Now.ToShortDateString();
                StreamReader readLog = new StreamReader(this.TheLog.Location());
                String body = readLog.ReadToEnd();
                theMsg.Body = body;
                String password = Settings.Default["SenderGmailPassword"].ToString();
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, password),
                    Timeout = 20000
                };

                smtp.Send(theMsg);

            }
            catch (Exception)
            {
            }
        }

        private void isAMCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.isPMCheckbox.Checked = !this.isAMCheckbox.Checked;
        }

        private void isPMCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.isAMCheckbox.Checked = !this.isPMCheckbox.Checked;
        }


    }
}
