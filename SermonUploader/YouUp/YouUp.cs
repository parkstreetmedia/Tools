using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Globalization;

namespace YouUp
{
    public partial class YouUp : Form
    {
        string TheVideoFile = "";
        public YouUp()
        {
            InitializeComponent();
            this.panelDropFile.DragEnter += new DragEventHandler(this.panelDropFile_DragEnter);
            this.panelDropFile.DragDrop += new DragEventHandler(this.panelDropFile_DragDrop);
            this.ResetForm();
        }

        private void ResetForm()
        {
            this.btnStartUpload.Enabled = true;
            this.txtBoxTitle.Enabled = true;
            this.txtBoxDescription.Enabled = true;
            this.dropDownPrivacy.Enabled = true;
            this.lblFileDrop.Text = "Drop Video Here";
            this.lblFileDrop.Font = new Font("Open Sans", 27, FontStyle.Regular);
            this.txtBoxTitle.Text = "Write some useful Title";
            this.progressBar.Value = 0;
            this.txtBoxDescription.Text = "Short description" + Environment.NewLine + "Date in format like 2017-01-25-am";
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
            if (!File.Exists(file))
            {
                MessageBox.Show("File doesn't exist... what did you do?!?...try again");
                return;
            }
            FileInfo fi = new FileInfo(file);
            if ((fi.Extension == null) || (fi.Extension.Trim() == "") || ((fi.Extension != ".avi") && (fi.Extension != ".mp4") && (fi.Extension != ".mov")))
            {
                MessageBox.Show("The file isn't a video... it has to be an mp4, avi, or mov...try again");
                return;
            }

            if (IsFileLocked(fi))
            {
                MessageBox.Show("The video is opened somewhere else... please close the video editor or whereever else the video may be open...try again");
                return;
            }

            this.TheVideoFile = file;

            this.lblFileDrop.Font = new Font("Open Sans", 16, FontStyle.Regular);
            this.lblFileDrop.Text = fi.Name;
            //yes, this means you can't upload 2 TB files and get progress..that's the least of the future-proofing problems here :-) 
            this.progressBar.Maximum = (int)(fi.Length / 100);

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


        private void btnStartUpload_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string ClientSecretsFileName = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\youtubeAPIKey.json";

            if ((this.txtBoxTitle.Text == "Write some useful Title") ||
                (this.txtBoxDescription.Text.Contains("2017-01-25-am")))
            {
                MessageBox.Show("Please update the title and description!", "Error");
                return;
            }

            if (this.dropDownPrivacy.SelectedItem.ToString().Trim() == "")
            {
                MessageBox.Show("Please select a privacy option", "Error");
                return;
            }

            if (this.txtBoxTitle.Text.Length < 5)
            {
                MessageBox.Show("Your title is waaay to short, 5 characters? Try again there Mr. Brevity", "This just won't do");
                return;

            }


            this.btnStartUpload.Enabled = false;

            try
            {
                this.btnStartUpload.Enabled = false;
                this.txtBoxTitle.Enabled = false;
                this.txtBoxDescription.Enabled = false;
                this.dropDownPrivacy.Enabled = false;
                this.UploadVideo(ClientSecretsFileName);

            }
            catch (AggregateException ex)
            {
                string allErrors = "Things went wrong, error messages follow:\n";
                foreach (var ee in ex.InnerExceptions)
                {
                    allErrors = allErrors + " " + ee.Message;
                }
                MessageBox.Show(allErrors);
                this.progressBar.Value = 0;
                this.progressBar.Update();

                this.btnStartUpload.Enabled = true;
                this.txtBoxTitle.Enabled = true;
                this.txtBoxDescription.Enabled = true;
                this.dropDownPrivacy.Enabled = true;
            }
           
        }


        private async Task UploadVideo(string secretsFile)
        {
            UserCredential credential;
            using (var stream = new FileStream(secretsFile, FileMode.Open, FileAccess.Read))
            {
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

            
            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = this.txtBoxTitle.Text.Trim();
            video.Snippet.Description = this.txtBoxDescription.Text.Trim();
            video.Snippet.CategoryId = "29"; // Nonprofits & Activism
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = this.dropDownPrivacy.SelectedItem.ToString().Trim();
            var filePath = this.TheVideoFile;

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ChunkSize = 30 *1 * 1024 * 1024; //30MB chunks instead of 1MB
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                await videosInsertRequest.UploadAsync();
            }
            this.ResetForm();
        }

        private void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    int fileSize = (int)progress.BytesSent / 100;
                    SetControlPropertyThreadSafe(this.progressBar, "Value", fileSize);
                    break;

                case UploadStatus.Failed:
                    MessageBox.Show("There was an error uploading...it was:\n" + progress.Exception);
                    break;
            }
        }

        private void videosInsertRequest_ResponseReceived(Video video)
        {
            SetControlPropertyThreadSafe(this.progressBar, "Value", this.progressBar.Maximum);

            if (MessageBox.Show(
          "The video was successfully uploaded, would you like to go to the video at:\nhttps://youtu.be/" + video.Id,
    "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start("https://youtu.be/" + video.Id);
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


        public void GUIClosing(Object sender, FormClosingEventArgs e)
        {
            if (this.btnStartUpload.Enabled)
            {
                //we are not uploading
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("You have an upload running...");
            }
        }
    }
}
