using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Globalization;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System.Configuration;
using MimeKit;
using MailKit.Net.Smtp;

namespace SermonUploader
{
    public partial class SermonUploader : Form {
        //Config file management
        Configuration ConfigFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        KeyValueConfigurationCollection Settings;

        //Config file information we'll need
        string LocalPathOfMP3s = "";
        string EmailAddressesToSendResults = "";
        string EmailSenderAddress = "";
        string EmailSenderPassword = "";
        string EmailHost = "";
        string EmailPort = "";

        //S3 Details needed for MP3 uploads
        string S3BucketName = "";
        string S3Key = "";
        string S3Secret = "";
        string S3FolderPath = "";

        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        private static IAmazonS3 s3Client;

        //Where we expect files
        const string SpeakerFile = "speakers.config";
        const string PodcastFile = "podcast.jpg";
        const string YoutubeAPIKey = "youtubeAPIKey.json";

        //The Youtube Link after an upload
        public string VideoLink;

        //Setting how many things there are to do on the progress bar
        private const int NUMOFTHINGSTODO = 7; //1: check stuff, 2: upload video 3,4: convert 2 audio files 5,6: upload 2 audio files, 7: email

        //List of files to Upload (you know, both of them...)
        Queue<FileInfo> FilesToUpload = new Queue<FileInfo>();

        Log TheLog;

        //To keep track of speaker titles 
        System.Collections.Generic.SortedDictionary<String, String> Speakers = new SortedDictionary<String, String>();

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

        //ffmpeg process, up here so we can cancel
        Process FFMpegProcess;
      
        public SermonUploader() {
            InitializeComponent();

            //Check settings...
            try {
                Settings = ConfigFile.AppSettings.Settings;

                this.S3BucketName = this.GetConfigValue("S3BucketName", "S3 Bucket Name");
                this.S3Key = this.GetConfigValue("S3Key", "S3 Key");
                this.S3Secret = this.GetConfigValue("S3Secret", "S3 Secret");
                this.S3FolderPath = this.GetConfigValue("S3FolderPath", "S3 Folder Path");
                this.EmailAddressesToSendResults = this.GetConfigValue("EmailAddressesToSendResults", "Email Addresses to send results");
                this.EmailSenderAddress = this.GetConfigValue("EmailSenderAddress", "Email Sender Address");
                this.EmailSenderPassword = this.GetConfigValue("EmailSenderPassword", "Email Sender Password");
                this.EmailHost = this.GetConfigValue("EmailHost", "Email Host (e.g.: smtp.mailgun.org)");
                this.EmailPort = this.GetConfigValue("EmailPort", "Email Port (e.g.: 587)");

                if (Settings["LocalPathOfMP3s"] != null && !string.IsNullOrEmpty(Settings["LocalPathOfMP3s"].ToString())) {
                    this.LocalPathOfMP3s = Settings["LocalPathOfMP3s"].Value;
                }
                else {
                    string aResult = "";
                    FolderBrowserDialog folderBrowserDialog2 = new FolderBrowserDialog();
                    folderBrowserDialog2.Description = "Please select where the MP3s and log will be saved.";
                    if (folderBrowserDialog2.ShowDialog() == DialogResult.OK) {
                        aResult = folderBrowserDialog2.SelectedPath;

                    }

                    if (!string.IsNullOrEmpty(aResult)) {

                        if (Settings["LocalPathOfMP3s"] == null) {
                            Settings.Add("LocalPathOfMP3s", aResult);
                        }
                        else {
                            Settings["LocalPathOfMP3s"].Value = aResult;
                        }
                        ConfigFile.Save(ConfigurationSaveMode.Modified);
                        this.LocalPathOfMP3s = aResult;
                    }
                }
            }
            catch (Exception) {
                MessageBox.Show("There is an error in the app config file, this is in the same folder as the .exe, please either open the file in a text editor and fix the error, or delete it, reopen the program, and re-enter the S3 credentials and other info needed...");
                Application.Exit();               
            }

            if ((string.IsNullOrEmpty(this.S3BucketName) || 
                string.IsNullOrEmpty(this.S3Key) || 
                string.IsNullOrEmpty(this.S3Secret) || 
                string.IsNullOrEmpty(this.S3FolderPath) || 
                string.IsNullOrEmpty(this.EmailAddressesToSendResults) || 
                string.IsNullOrEmpty(this.EmailSenderAddress) || 
                string.IsNullOrEmpty(this.EmailSenderPassword) ||
                string.IsNullOrEmpty(this.LocalPathOfMP3s))) {
                MessageBox.Show("There is an error in the app config file, this is in the same folder as the .exe, please either open the file in a text editor and fix the error, or delete it, reopen the program, and re-enter the S3 credentials and other info needed...");
                Application.Exit();
            }

            //Load autocomplete values for the speaker
            string mainMinister = "";
            if (File.Exists(SpeakerFile)) {
                FileStream stream = new FileStream(SpeakerFile, FileMode.Open, FileAccess.Read, FileShare.None);
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
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + PodcastFile)) {
                MessageBox.Show("I cannot find the podcast image, it should be at: " + AppDomain.CurrentDomain.BaseDirectory + PodcastFile + ", please correct this", "Error missing file");
            }

            this.tagSpeakerTxt.Text = mainMinister;           
        }

        private string GetConfigValue(string key, string promptText) {
            if (Settings[key] != null && !string.IsNullOrEmpty(Settings[key].ToString())) {
                return Settings[key].Value;
            }
            else {
                string aResult = "";
                ShowInputDialog(promptText + ":", ref aResult);
                if (!string.IsNullOrEmpty(aResult)) {
                   
                    if (Settings[key] == null) {
                        Settings.Add(key, aResult.Trim());
                    }
                    else {
                        Settings[key].Value = aResult.Trim();
                    }
                    ConfigFile.Save(ConfigurationSaveMode.Modified);
                    return aResult.Trim();
                }
            }
            return "";
        }

        private string ValidateInput() {
            string errors = "";
            //Check that stuff is filled out
            if (!this.CheckInput()) {
                errors = errors + "You must fill out all the tag properties to continue\n";
            }

            //don't allow all caps
            if (!string.IsNullOrEmpty(this.tagTitleTxt.Text.Trim()) && this.IsAllUpperCase(this.tagTitleTxt.Text.Trim())) {
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
            if (!string.IsNullOrEmpty(this.tagTitleTxt.Text.Trim()) && this.IsAllUpperCase(this.tagScriptureTxt.Text.Trim())) {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                this.tagScriptureTxt.Text = textInfo.ToTitleCase(this.tagScriptureTxt.Text);
                errors = errors + "You tried to write the scripture in all caps, I fixed it, but please don't do that.\n";
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

            return errors;
        }

        private void btnUpload_Click(object sender, EventArgs e) {
            string errors = this.ValidateInput();
            if (errors.Trim() != string.Empty) {
                MessageBox.Show(errors, "Errors");
                return;
            }

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

            this.DoAllTheThings();
        }

        private async Task DoAllTheThings() {
            //Show the cancel 
            this.btnCancel.Visible = true;
            this.btnCancel.Enabled = true;
            this.btnUpload.Enabled = false;
            this.isSuccessful = true;
            this.progress.Enabled = true;
            this.progress.Maximum = NUMOFTHINGSTODO;
            if (this.shouldSkipVideoUploadChk.Checked) {
                this.progress.Maximum--;
            }
            if (this.shouldSkipS3UploadChk.Checked) {
                this.progress.Maximum = this.progress.Maximum - 2;
            }

            this.progress.Step = 1;

            //cancel check
            if (this.isCanceled) {
                return;
            }

            //Start the log
            this.TheLog = new Log(this.LocalPathOfMP3s);

            this.TheLog.Write("Started Processing on: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());

            this.progress.PerformStep();
            this.progress.Update();

            if (!this.shouldSkipVideoUploadChk.Checked) {

                try {
                    await this.UploadVideo();

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
                this.FileProgress.Value = this.FileProgress.Maximum;
                this.FileProgress.Update();

                //update the MP3 tags
                this.SetMP3Tag();

                if (!this.shouldSkipS3UploadChk.Checked) {
                    this.FileProgress.Value = 0;
                    this.FileProgress.Maximum = 100;

                    //Setup the upload client
                    var creds = new Amazon.Runtime.BasicAWSCredentials(this.S3Key, this.S3Secret);
                    s3Client = new AmazonS3Client(creds, bucketRegion);
                    await this.UploadFilesToS3Async();
                }
            }

            this.progress.PerformStep();
            this.progress.Update();

            //cancel check
            if (this.isCanceled) {
                return;
            }

            this.progress.Value = this.progress.Maximum;
            this.progress.Update();

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

        public async Task UploadVideo() {
            string secretsFile = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\" + YoutubeAPIKey;
            this.TheLog.Write("Started uploading the video");
            //generate image 
            bool IsThereAnImage = false;
            string tPath = this.LocalPathOfMP3s + "\\thumbnails\\";
            string dateForThumb = this.tagDate.Value.ToString("yyyy-MM-dd");
            string imageSaved = tPath + "\\generated\\" + dateForThumb + ".jpg";

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
                if (File.Exists(imageSaved)) {
                    IsThereAnImage = true;
                }
            }
            catch (Exception) {
                IsThereAnImage = false;
                this.TheLog.Write("Oh well, failed to create a thumbnail for youtube");
            }

            //Upload the video
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

            this.TheLog.Write("Successfully authenticated with youtube");

            youtubeService.HttpClient.Timeout = TimeSpan.FromMinutes(10);

            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = this.tagTitleTxt.Text.Trim() + " - " + this.tagScriptureTxt.Text.Trim();
            if (video.Snippet.Title.Length > 99) {
                video.Snippet.Title = video.Snippet.Title.Substring(0, 98);
            }
            string date = this.tagDate.Value.ToString("yyyy-MM-dd") + ((this.isAMCheckbox.Checked) ? "am" : "pm");
            video.Snippet.Description = this.tagSpeakerTxt.Text.Trim() + ", " + this.tagSpeakerTitleTxt.Text.Trim() +
                "\nDate: " + " " + date;
            video.Snippet.CategoryId = "29"; // Nonprofits & Activism
            video.Status = new VideoStatus();
            //TESTING - hide from youtube
            video.Status.PrivacyStatus = "Private";            
            //video.Status.PrivacyStatus = "Public";
            var filePath = this.fileSelectBox.Text;

            using (var fileStream = new FileStream(filePath, FileMode.Open)) {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ChunkSize = 1 * 1 * 1024 * 1024; //30MB chunks instead of 1MB
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;
                this.TheLog.Write("Uploading the video");
                await videosInsertRequest.UploadAsync();            
            }

            this.TheLog.Write("Finished uploading the video");

            //upload the thumbnail
            if ((youtubeService != null) && (!string.IsNullOrEmpty(video.Id))) {
                if (IsThereAnImage) {
                    using (var tStream = new FileStream(imageSaved, FileMode.Open)) {
                        var tInsertRequest = youtubeService.Thumbnails.Set(video.Id, tStream, "image/jpeg");
                        tInsertRequest.Upload();
                    }
                }
            }
        }

        private void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress) {
            switch (progress.Status) {
                case UploadStatus.Uploading:
                    int fileSize = (int)progress.BytesSent / 100;

                    //youtube throws weird numbers sometimes
                    if (fileSize > this.YoutubeProgress.Maximum)
                    {
                        fileSize = this.YoutubeProgress.Maximum - 1;
                    }

                    if (fileSize < this.YoutubeProgress.Minimum)
                    {
                        fileSize = this.YoutubeProgress.Minimum;
                    }                   
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
                    f.Tag.Performers = new string[] { this.tagSpeakerTxt.Text.Trim() };
                    f.Tag.AlbumArtists = new string[] { this.tagSpeakerTxt.Text.Trim() };
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
                    f.Tag.Performers = new string[] { this.tagSpeakerTxt.Text.Trim() };
                    f.Tag.AlbumArtists = new string[] { this.tagSpeakerTxt.Text.Trim() };
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

        #region Upload MP3s to S3

        private async Task UploadFilesToS3Async() {
            try {
                var fileTransferUtility =
                    new TransferUtility(s3Client);
             
                //upload each file
                foreach (FileInfo working in this.FilesToUpload) {
                    String msgOut = "Uploading file: " + working.Name;
                    this.TheLog.Write(msgOut);

                    //Upload the file, finally
                    try {
                        // keyname is the folder \ filename - so it should be audio\filename.mp3 - eg audio\2021-01-07-am-hq.mp3
                        if (!this.S3FolderPath.Trim().EndsWith("/")) {
                            this.S3FolderPath = this.S3FolderPath.Trim() + "/";
                        }
                        await fileTransferUtility.UploadAsync(working.FullName, this.S3BucketName, this.S3FolderPath + working.Name);
                        //success
                        this.TheLog.Write("Success: Upload of file: " + working.Name);
                        if (working.Name.Contains("hq")) {
                            this.TheLog.Write("The Podcast Link will be: https://" + this.S3BucketName + "/"+ this.S3FolderPath + working.Name);
                        }
                        this.progress.Value++;
                    }
                    catch (Exception e) {
                        this.TheLog.Write("Error: Failed uploading a file, file & error message follows: "
                           + ((e.InnerException == null) ? e.Message : e.InnerException.ToString()));

                        this.isSuccessful = false;
                    }
                }
            }
            catch (Exception e) {
                this.TheLog.Write("Error: Failed uploading a file, file & error message follows: " + ((e.InnerException == null) ? e.Message : e.InnerException.ToString()));
                this.isSuccessful = false;
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

                //ffmpeg doesn't handle overwriting the best
                if (File.Exists(fileName)) {
                    try {
                        File.Delete(fileName);
                    }
                    catch (Exception) { }
                }

                FileInfo theMP3 = new FileInfo(fileName);
                args = "-i \"" + this.fileSelectBox.Text.Trim() + "\" " + args + "\"" + fileName + "\"";
                ProcessStartInfo ffmpegInfo = new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "ffmpeg.exe", args);
                TheLog.Write("Converting to MP3: " + "ffmpeg.exe " + args);
                ffmpegInfo.CreateNoWindow = true;
                ffmpegInfo.ErrorDialog = false;
                ffmpegInfo.RedirectStandardError = true;
                ffmpegInfo.RedirectStandardOutput = true;
                ffmpegInfo.UseShellExecute = false;

                //Setup the process
                this.FFMpegProcess = new Process();
                this.FFMpegProcess.StartInfo = ffmpegInfo;

                //Monitor the process
                this.FFMpegProcess.OutputDataReceived += new DataReceivedEventHandler(this.FFMpegProcess_OutputDataReceived);
                this.FFMpegProcess.ErrorDataReceived += new DataReceivedEventHandler(this.FFMpegProcess_OutputDataReceived);

                //Start the process
                this.FFMpegProcess.Start();
                this.FFMpegProcess.BeginOutputReadLine();
                this.FFMpegProcess.BeginErrorReadLine();

                //Wait for everything to finish but give up after 3 hours - TaskCompletionSource doesn't have a timeout option, so don't mind the hack
                int anTimeCount = 0;
                while ((!this.FFMpegProcess.HasExited) && (anTimeCount < 1080))
                {
                    System.Threading.Thread.Sleep(1000);
                    Application.DoEvents();
                    anTimeCount++;
                }

                //We are either done or giving up
                this.FFMpegProcess.CancelOutputRead();
                this.FFMpegProcess.CancelErrorRead();
                this.FFMpegProcess.Close();
                this.FFMpegProcess.Dispose();

                //This looks really really silly.. but sometimes ffmpeg doesn't close, and somehow close returns true without actually stopping ffmpeg...
                try
                {
                    this.FFMpegProcess.Kill();
                    this.FFMpegProcess.Dispose();
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
        /// Monitor the ffmpeg process, update the progress bar, and allow canceling 
        /// </summary>
        /// <param name="sendingProcess"></param>
        /// <param name="outLine"></param>
        private void FFMpegProcess_OutputDataReceived(object sendingProcess, DataReceivedEventArgs outLine)
        {
            // Collect the ffmpeg process output.
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
                this.FFMpegProcess.Kill();
                this.TheLog.Write("Success: Killed the MP3 Encoding Process at the user's request");
            }
        }

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue) {
            if (control.InvokeRequired) {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }

        #endregion CreateMP3

        #region Helper Methods
        //prompt the user for config info
        private static DialogResult ShowInputDialog(string prompt, ref string input) {
            System.Drawing.Size size = new System.Drawing.Size(600, 100);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Text = "Error! Some configuration values are missing, please enter what is needed:";

            System.Windows.Forms.Label lbl = new Label();
            lbl.Size = new System.Drawing.Size(size.Width - 10, 23);
            lbl.Location = new System.Drawing.Point(5, 5);
            lbl.Text = prompt;
            inputBox.Controls.Add(lbl);

            System.Windows.Forms.TextBox textBox = new TextBox();
            textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            textBox.Location = new System.Drawing.Point(5, 30);
            textBox.Text = input;
            inputBox.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39+25);
            inputBox.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39+25);
            inputBox.Controls.Add(cancelButton);

            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            return result;
        }

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
            this.tagScriptureTxt.Text = "";
            this.tagSpeakerTitleTxt.Text = "";
            this.tagSpeakerTxt.Text = "";
            this.fileSelectBox.Text = "";
            this.isAMCheckbox.Checked = false;
            this.isPMCheckbox.Checked = false;
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
            if ((ext == "") || ((ext != ".avi") && (ext != ".mp4") && (ext != ".mov") && (ext != ".wav")))
            {
                MessageBox.Show("The file needs to be either a video or an audio file... an mp4, avi, mov, or wave...try again");
                return false;
            }

            if (IsFileLocked(fi))
            {
                MessageBox.Show("The file is opened somewhere else... please close the video or audio editor or whereever else the video may be open...try again");
                return false;
            }

            if (ext == ".wav") {
                this.shouldSkipVideoUploadChk.Checked = true;
            }

                this.fileSelectBox.Text = file;

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
                ((this.isAMCheckbox.Checked || this.isPMCheckbox.Checked)) && 
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

            string fullPath = this.LocalPathOfMP3s + "\\" + dateInfo + "-" + amOrPm.ToLower();
            return fullPath;
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a utility app, there is no installation, it is just an .exe and a few config files that are expected in the same folder as the program.\nPlease see the readme.txt that is next to the .exe for further information on editing the config.", "Quick help");
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


        #endregion Helper Methods

        private void EmailLogFile()
        {
            try {
                MimeMessage mail = new MimeMessage();
                mail.From.Add(new MailboxAddress("RadioRoom-NoReply", this.EmailSenderAddress));
                foreach(string anAddress in this.EmailAddressesToSendResults.Split(',')) {
                    mail.To.Add(MailboxAddress.Parse(anAddress));
                }
                mail.Subject = "Radio Room Log File for " + DateTime.Now.ToShortDateString();
                StreamReader readLog = new StreamReader(this.TheLog.Location());
                String body = readLog.ReadToEnd();             
                mail.Body = new TextPart("plain")
                {
                    Text = body,
                };
                int thePort = 587;
                Int32.TryParse(this.EmailPort, out thePort);
                // Send it!
                using (var client = new SmtpClient()) {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(this.EmailHost, thePort, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(this.EmailSenderAddress, this.EmailSenderPassword);

                    client.Send(mail);
                    client.Disconnect(true);
                }
            }
            catch (Exception) { }           
        }

        private void isAMCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.isPMCheckbox.Checked = !this.isAMCheckbox.Checked;
        }

        private void isPMCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.isAMCheckbox.Checked = !this.isPMCheckbox.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.isCanceled = true;
        }
    }
}
