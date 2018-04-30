using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using static ServicePPTCreator.ReferenceParser;
using Office = Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace ServicePPTCreator
{
    public partial class PPTCreator : Form
    {
        public ConfigurationForm theConfig = new ConfigurationForm();
        const string NoJoy = "Sorry, no luck, something went wrong and you'll have to do it manually";

        public PPTCreator()
        {
            InitializeComponent();
          
            //Set PDF bulletin link
            string remoteUri = theConfig.BulletinBaseURL + Helpers.GetNextSunday() + "_web.pdf";
            this.morningServiceLinkTxtBox.Text = remoteUri;

            //Set PCO plan link
            this.eveningServiceLinkTxtBox.Text = "https://api.planningcenteronline.com/services/v2/service_types/" + theConfig.PCOServiceID + "/plans?filter=future";
        }
        
        #region MorningPPT

        private void createSlideshow_Click(object sender, EventArgs e) {
            string fileName = this.theConfig.MorningBulletins + Helpers.GetNextSunday() + ".pdf";
            WebClient myWebClient = new WebClient();
            try {

                if (File.Exists(fileName)) {
                    File.Delete(fileName);
                }

                myWebClient.DownloadFile(this.morningServiceLinkTxtBox.Text, fileName);
            }
            catch (Exception) {
                MessageBox.Show("No Luck, couldn't download the PDF bulletin, see if the bulletin is on the parkstreet.org website, you can always paste the link in here");
                return;
            }

            if (!File.Exists(fileName)) {
                MessageBox.Show("No Luck, couldn't download the PDF bulletin, you'll have to do it manually");
                return;
            }

            this.CreateMorningPresentation(fileName);
        }

        private void selectLocalPDF_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.Title = "Select the PDF bulletin";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "pdf";
            openFileDialog1.Filter = "PDF files (*.pdf)|*.pdf";
             openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                if (!File.Exists(openFileDialog1.FileName)) {
                    MessageBox.Show("No Luck, couldn't download the PDF bulletin, you'll have to do it manually");
                    return;
                }

                this.CreateMorningPresentation(openFileDialog1.FileName);
            }
        }

        private void CreateMorningPresentation(string fileName) {
            string messagesToShowUserAfter = "";

            // Create an instance of Microsoft PowerPoint
            PowerPoint.Application oPowerPoint;

            // Create a new Presentation
            PowerPoint.Presentation oPre;

            // Create an instance of Microsoft PowerPoint
            oPowerPoint = new PowerPoint.Application();

            // Create a new Presentation
            oPre = oPowerPoint.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoTrue);

            oPre.ApplyTheme(this.theConfig.MasterTemplate);
            oPre.PageSetup.SlideSize = PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen16x9;

            PowerPoint.Slide warningSlide = oPre.Slides.AddSlide(1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.WarningText]);
            warningSlide.Shapes[1].TextFrame.TextRange.Text = "PLEASE REMEMBER TO CHECK THIS PRESENTATION";

            //Convert the PDF to Text
            int[] pageWidthStart = new int[] { 0, 410 };
            foreach (int aPageWidthStart in pageWidthStart) {
                String bString = "";
                PdfReader pdfReader = new PdfReader(fileName);
                PdfDocument pdfDoc = new PdfDocument(pdfReader);
                PageSize theSize = pdfDoc.GetDefaultPageSize();
                float height = theSize.GetHeight();
                float width = 410;

                iText.Kernel.Geom.Rectangle rect = new iText.Kernel.Geom.Rectangle(aPageWidthStart, 0, width, height);
                TextRegionEventFilter regionFilter = new TextRegionEventFilter(rect);

                ITextExtractionStrategy strategy = new FilteredTextEventListener(new LocationTextExtractionStrategy(), regionFilter);
                try {
                    bString = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(2), strategy) + "\n";

                }
                catch (Exception) {
                    MessageBox.Show(NoJoy);
                    return;
                }

                bString = Helpers.CleanString(bString);

                //split our one long string into the various service elements
                bString = bString.Replace("MORNING WORSHIP", "!!!!GG MORNING WORSHIP");
                bString = bString.Replace("COMMUNION", "!!!!GG COMMUNION");
                bString = bString.Replace("PRELUDE", "!!!!GG PRELUDE");
                bString = bString.Replace("INTROIT", "!!!!GG INTROIT");
                bString = bString.Replace("CALL TO WORSHIP", "!!!!GG CALL TO WORSHIP");
                bString = bString.Replace("HYMN", "!!!!GG HYMN");
                bString = bString.Replace("INVOCATION", "!!!!GG INVOCATION");
                bString = bString.Replace("PEACE", "!!!!GG PEACE");
                bString = bString.Replace("LIFE", "!!!!GG LIFE");
                bString = bString.Replace("ANTHEM", "!!!!GG ANTHEM");
                bString = bString.Replace("SCRIPTURE", "!!!!GG SCRIPTURE");
                bString = bString.Replace("OLD TESTAMENT READING", "!!!!GG OLD TESTAMENT READING");
                bString = bString.Replace("NEW TESTAMENT READING", "!!!!GG NEW TESTAMENT READING");
                bString = bString.Replace("GLORIA", "!!!!GG GLORIA");
                bString = bString.Replace("SERMON", "!!!!GG SERMON");
                bString = bString.Replace("CONGREGATIONAL", "!!!!GG CONGREGATIONAL");
                bString = bString.Replace("OFFERTORY", "!!!!GG OFFERTORY");
                bString = bString.Replace("DOXOLOGY", "!!!!GG DOXOLOGY");
                bString = bString.Replace("BENEDICTION", "!!!!GG BENEDICTION");
                bString = bString.Replace("POSTLUDE", "!!!!GG POSTLUDE");
                bString = bString.Replace("If you would like", "!!!!GG If you would like");

                string[] delims = { "!!!!GG" };
                string[] service = bString.Split(delims, StringSplitOptions.RemoveEmptyEntries);

                if (service.Length < 0) {
                    MessageBox.Show(NoJoy);
                    return;
                }

                //Add slides for each element of the service 
                foreach (string anElement in service) {
                    //do nothing
                    if ((anElement.Contains("MORNING WORSHIP")) || (anElement.Contains("BENEDICTION"))
                        || (anElement.Contains("GLORIA")) || (anElement.Contains("PEACE"))) {
                        continue;
                    }

                    if (anElement.Contains("SUPPER")) {
                        Helpers.FindHymn("731", true, this.theConfig.MorningHymnal, false, "");
                        oPre.Slides.InsertFromFile(this.theConfig.MorningStandardSlides + "Communion.pptx", oPre.Slides.Count);


                        string[] diffHymns = { "\n" };
                        string[] allHymns = anElement.Split(diffHymns, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string eachHymn in allHymns) {
                            string communionHymnsToAdd = Helpers.FindHymn(eachHymn, true, this.theConfig.MorningHymnal, false, "");                            
                            if (!string.IsNullOrEmpty(communionHymnsToAdd)) {
                                oPre.Slides.InsertFromFile(communionHymnsToAdd, oPre.Slides.Count);
                            }
                        }
                    }

                    if ((anElement.Contains("PRELUDE")) || (anElement.Contains("INTROIT")) || (anElement.Contains("POSTLUDE")) ||
                        (anElement.Contains("OFFERTORY")) || (anElement.Contains("LIFE OF THE CHURCH")) || (anElement.Contains("ANTHEM"))) {

                        //clean up some text we don't want
                        string slideText = anElement.Replace("PRELUDE", "");
                        slideText = slideText.Replace("INTROIT", "");
                        slideText = slideText.Replace("POSTLUDE", "");
                        slideText = slideText.Replace("Congregation standing as able", "");
                        slideText = slideText.Replace(" * ", "");
                        slideText = slideText.Replace("OFFERTORY", "");
                        slideText = slideText.Replace("LIFE OF THE CHURCH", "");
                        slideText = slideText.Replace("ANTHEM", "");
                        slideText = Regex.Replace(slideText, @" ?\(.*?\)", string.Empty);
                        slideText = Regex.Replace(slideText, @" ?\[.*?\]", string.Empty);

                        string theText = "";

                        //if there is an 8:30 and 11:00 slide.. create each
                        string[] diffServices = { "8:30" };
                        string[] eachService = slideText.Split(diffServices, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string aService in eachService) {

                            string eachSlideText = aService.Replace("\n", "\n!!!!GG");
                            string[] newDelim = { "!!!!GG" };
                            string[] allText = eachSlideText.Split(newDelim, StringSplitOptions.RemoveEmptyEntries);
                            
                            foreach (string aBit in allText) {
                                string toAdd = aBit;
                                string howLong = aBit.Trim();
                                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                                howLong = rgx.Replace(howLong, "");
                                howLong = howLong.Replace("\n", "");

                                if (howLong.Length > 4) {
                                    toAdd = toAdd.TrimStart();
                                    RegexOptions options = RegexOptions.None;
                                    Regex regex = new Regex("[ ]{2,}", options);
                                    toAdd = regex.Replace(toAdd, " - ");

                                    if ((theText.Length + toAdd.Length) > 40) {
                                        toAdd = toAdd.Replace("\n", "");
                                        toAdd = Regex.Replace(toAdd, @"(^[\W_]*)|([\W_]*$)", "");
                                        theText = theText + "\n" + toAdd;
                                    }
                                    else {
                                        toAdd = toAdd.Replace("\n", " - ");
                                        theText = theText + toAdd;
                                    }
                                }
                            }

                            theText = theText.Trim();
                            if (theText.StartsWith("\n")) {
                                theText = theText.TrimStart('\n');
                            }
                            theText = Regex.Replace(theText, @"(^[\W_]*)|([\W_]*$)", "");
                            theText = theText.Trim();
                            if (theText.StartsWith("\n")) {
                                theText = theText.TrimStart('\n');
                            }

                            //remove anything after 3 lines...
                            try {
                                int offset = theText.IndexOf("\n");
                                offset = theText.IndexOf("\n", offset + 1);
                                int result = theText.IndexOf("\n", offset + 1);
                                theText = theText.Substring(0, result);
                            }
                            catch (Exception) { }

                            theText = theText.Replace("Words:", " - Words: ");
                            theText = theText.Replace("Music:", " - Music: ");

                            string removeParens = "(\\[.*\\])|(\".*\")";
                            theText = Regex.Replace(theText, removeParens, "");
                            theText = theText.Replace("-  -", "-");
                            theText = theText.Replace("- -", "-");

                            int doubleEnds = theText.IndexOf("\n\n");
                            
                            if (doubleEnds > 0) {
                                theText = theText.Substring(0, doubleEnds);
                            }

                            doubleEnds = theText.IndexOf("\n \n");

                            if (doubleEnds > 0) {
                                theText = theText.Substring(0, doubleEnds);
                            }

                            theText = theText.TrimEnd('\r', '\n');

                            //still 3 lines? cut to 2
                            try {
                                int offset = theText.IndexOf("\n");
                                offset = theText.IndexOf("\n", offset + 1);                              
                                theText = theText.Substring(0, offset);
                            }
                            catch (Exception) { }

                            PowerPoint.Slide titleSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.ChromakeyLowerThirdText]);
                            titleSlide.Shapes[1].TextFrame.TextRange.Text = theText;
                        }

                        continue;
                    }

                    if (anElement.Contains("SERMON")) {
                        string slideText = anElement.Replace("SERMON", "");
                        slideText = slideText.Replace("\n", " - ");
                        int titleLoc = slideText.LastIndexOf("\"");
                        string title = "";
                        string speaker = "";
                        if (titleLoc > 0) {
                            title = slideText.Substring(0, titleLoc + 1).Trim();
                            speaker = slideText.Substring(titleLoc + 1).Trim();
                            speaker = Regex.Replace(speaker, @"(^[\W_]*)|([\W_]*$)", "");
                            speaker = speaker.Trim();
                        }
                        else {
                            title = slideText.Trim();
                        }


                        PowerPoint.Slide titleSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.ChromakeySermonInfo]);
                        titleSlide.Shapes[1].TextFrame.TextRange.Text = speaker;
                        titleSlide.Shapes[2].TextFrame.TextRange.Text = title;
                        continue;
                    }

                    if (anElement.Contains("CONGREGATIONAL")) {
                        PowerPoint.Slide titleSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.ChromakeyLowerThirdText]);
                        titleSlide.Shapes[1].TextFrame.TextRange.Text = "Congregational Prayer";
                        continue;
                    }

                    if (anElement.Contains("INVOCATION")) {
                        oPre.Slides.InsertFromFile(this.theConfig.MorningStandardSlides + "LordsPrayer.pptx", oPre.Slides.Count);
                        continue;
                    }

                    if (anElement.Contains("DOXOLOGY")) {
                        oPre.Slides.InsertFromFile(this.theConfig.MorningStandardSlides + "Doxology.pptx", oPre.Slides.Count);
                        continue;
                    }

                    if (anElement.Contains("CALL TO WORSHIP")) {

                        PowerPoint.Slide oSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.WhiteCallToWorship]);
                        oSlide.Shapes[1].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;

                        //add in communion slides
                        string slideText = "";
                        string header = "";
                        string[] call = anElement.Split('\n');
                        foreach (string aLine in call) {
                            if (aLine.Contains("CALL")) {
                                header = aLine.Replace("8:30", "");
                                header = header.Replace("11:00", "");
                                header = header.Replace("8: 30", "");
                                header = header.Replace("11:00", "");
                                int versePos = header.LastIndexOfAny("0123456789".ToCharArray());
                                header = header.Substring(0, versePos + 1);
                                header = header.Replace("CALL TO WORSHIP", "Call to Worship");
                                oSlide.Shapes[2].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                                PowerPoint.TextRange objTextRng = oSlide.Shapes[2].TextFrame.TextRange;
                                objTextRng.Text = header;
                                objTextRng.Font.Size = 32;
                                objTextRng.Font.Color.RGB = Color.Black.ToArgb();
                                oSlide.Shapes[1].TextFrame.TextRange.Text.Insert(0, header);
                                continue;
                            }
                            if ((aLine.Contains("Leader:")) || (aLine.Contains("All:")) || (aLine.Contains("People:"))) {
                                if (aLine.Trim().Length < 2) {
                                    continue;
                                }

                                if ((aLine.Contains("11:00")) || (aLine.Contains("8:30"))) {
                                    continue;
                                }

                                string newLine = aLine.Trim();
                                slideText = slideText + "\n" + newLine;
                            }
                            else {
                                if ((aLine.Contains("11:00")) || (aLine.Contains("8:30"))) {
                                    continue;
                                }

                                //must be a line from the last line...
                                slideText = slideText + " " + aLine.Trim();
                            }

                        }

                        slideText = slideText.Replace("*", "").Trim();

                        oSlide.Shapes[1].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                        oSlide.Shapes[1].TextFrame2.TextRange.Text = slideText;
                    }

                    if (anElement.Contains("HYMN")) {
                        string slideText = anElement.Replace("HYMN", "");
                        if (slideText.IndexOf("\n") > 0) {
                            slideText = slideText.Substring(0, slideText.IndexOf("\n"));
                        }

                        string aHymnToAdd = Helpers.FindHymn(slideText, true, this.theConfig.MorningHymnal, true, this.theConfig.MorningHymns);
                        if (!string.IsNullOrEmpty(aHymnToAdd)) {
                            oPre.Slides.InsertFromFile(aHymnToAdd, oPre.Slides.Count);
                        }
                        else {
                            PowerPoint.Slide titleSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.WarningText]);
                            titleSlide.Shapes[1].TextFrame.TextRange.Text = "YOU need to add the hymn I cannot find: " + slideText;
                        }
                        continue;
                    }

                    if ((anElement.Contains("SCRIPTURE")) || (anElement.Contains("NEW TESTAMENT READING")) || (anElement.Contains("OLD TESTAMENT READING"))) {
                        string slideText = anElement.Replace("SCRIPTURE", "");
                        slideText = slideText.Replace("NEW TESTAMENT READING", "");
                        slideText = slideText.Replace("OLD TESTAMENT READING", "");
                        slideText = slideText.Replace("READING", "");
                        slideText = slideText.Trim();

                        slideText = slideText.Replace("[omitted in web version]", "");
                        slideText = slideText.Replace("omitted in web version", "");
                        slideText = slideText.Replace("*", "");
                        slideText = slideText.Trim();
                        slideText = slideText.TrimStart('\n', ' ');
                        slideText = slideText.Trim();
                        slideText = slideText.TrimEnd('\n', ' ');
                        slideText = slideText.Trim();

                        //remove the speaker
                        int indexOfFirstNewLine = slideText.IndexOf("\n");
                        if (indexOfFirstNewLine > 0) {
                            slideText = slideText.Substring(indexOfFirstNewLine);
                        }

                        List<Reference> theRefs = ReferenceParser.ParseReference(slideText);

                        foreach (Reference aRef in theRefs) {
                            string aString = aRef.PrettyOutput();
                            //Insert the title
                            PowerPoint.Slide opsSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.ChromakeyScriptureTitle]);
                            opsSlide.Shapes[1].TextFrame.TextRange.Text = aString;

                            var pagesOfVerses = ReferenceToScripture.GetContentFromNIV(this.theConfig.BibleConnectionString, aRef);

                            if (pagesOfVerses.Count <= 0) {
                                messagesToShowUserAfter = messagesToShowUserAfter + "\nThere was an error trying to get the scripture, use the Scripture Reference to PPT tool to create the slides manually";
                            }

                            foreach (string aPage in pagesOfVerses) {
                                PowerPoint.Slide oSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.WhiteScriptureText]);
                                oSlide.Shapes[1].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                                oSlide.Shapes[1].TextFrame2.TextRange.ParagraphFormat.SpaceAfter = 6;
                                oSlide.Shapes[1].TextFrame2.TextRange.ParagraphFormat.SpaceBefore = 0;
                                oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceAfter = 6;
                                oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceBefore = 0;
                                oSlide.Shapes[1].TextFrame2.TextRange.Text = aPage;
                            }
                        }

                    }
                }

                //cleanup
                pdfReader.Close();
                pdfDoc.Close();
            }

            //save and close 
            string amFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\AM " + Helpers.GetNextSunday() + ".pptx";
            oPre.SaveAs(amFile, PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation, Office.MsoTriState.msoTriStateMixed);
            MessageBox.Show("All Done - Please check the PPT against the PDF bulletin" + messagesToShowUserAfter, "Complete", MessageBoxButtons.OK,
                new MessageBoxIcon(), MessageBoxDefaultButton.Button1,  (MessageBoxOptions)0x40000); // Be on top of everything
            Process.Start(fileName);
        }

        #endregion MorningPPT

        #region EveningPPT

        private void createEveningPPT_Click(object sender, EventArgs e) {
            // Create an instance of Microsoft PowerPoint
            PowerPoint.Application oPowerPoint;

            // Create a new Presentation
            PowerPoint.Presentation oPre;

            string messagesForUser = "";

            pcoItems.Items serviceItems;

            try {
                serviceItems = PCO.GetNextPlanItems(this.eveningServiceLinkTxtBox.Text, this.theConfig.PCOAPIKey, this.theConfig.PCOAPISecret);
            }
            catch (Exception ex) {
                MessageBox.Show("No Luck: Error\n" + ex.Message);
                return;
            }

            if (serviceItems == null || serviceItems.data == null || serviceItems.data.Count <= 0) {
                MessageBox.Show("No Luck - the service came back empty :-/");
                return;
            }

            var serviceData = serviceItems.data;

            // Create an instance of Microsoft PowerPoint
            oPowerPoint = new PowerPoint.Application();

            // Create a new Presentation
            oPre = oPowerPoint.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoTrue);

            oPre.ApplyTheme(this.theConfig.MasterTemplate);
            oPre.PageSetup.SlideSize = PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen16x9;

            PowerPoint.Slide warningSlide = oPre.Slides.AddSlide(1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.WarningText]);
            warningSlide.Shapes[1].TextFrame.TextRange.Text = "PLEASE REMEMBER TO CHECK THIS PRESENTATION";

            bool wasScriptureLast = false;

            //add things to the powerpoint file
            foreach (pcoItems.ItemsDatum anElement in serviceData) {
                if (anElement.attributes == null) {
                    continue;
                }
                var sItem = anElement.attributes;
                string itemType = anElement.attributes.item_type.ToLower().Trim();

                //ignore headers
                if (itemType == "header") {
                    if (sItem.title.ToLower().Contains("scripture reading")) {
                        wasScriptureLast = true;
                    }
                    continue;
                }

                if (wasScriptureLast) {
                    wasScriptureLast = false;
                    string slideText = sItem.title + " Read by: " + sItem.description;
                    PowerPoint.Slide opsSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.BlackScriptureTitle]);
                    opsSlide.Shapes[1].TextFrame.TextRange.Text = slideText;

                    List<Reference> theRefs = ReferenceParser.ParseReference(sItem.title);

                    foreach (Reference aRef in theRefs) {                       
                        var pagesOfVerses = ReferenceToScripture.GetContentFromNIV(this.theConfig.BibleConnectionString, aRef);

                        if (pagesOfVerses.Count <= 0) {
                            messagesForUser = messagesForUser + "\nThere was an error trying to get the scripture, use the Scripture Reference to PPT tool to create the slides manually";
                        }

                        foreach (string aPage in pagesOfVerses) {
                            PowerPoint.Slide oSlide = oPre.Slides.AddSlide(pagesOfVerses.Count, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.BlackScriptureText]);
                            oSlide.Shapes[1].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                            oSlide.Shapes[1].TextFrame2.TextRange.ParagraphFormat.SpaceAfter = 6;
                            oSlide.Shapes[1].TextFrame2.TextRange.ParagraphFormat.SpaceBefore = 0;
                            oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceAfter = 6;
                            oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceBefore = 0;
                            oSlide.Shapes[1].TextFrame2.TextRange.Text = aPage;
                        }
                    }
                }

                if (sItem.item_type.ToLower() == ("song")) {
                    string possibleHymn = Helpers.FindHymn(sItem.title, false, "", true, this.theConfig.EveningHymns);
                    if (string.IsNullOrEmpty(possibleHymn)) {
                        PowerPoint.Slide titleSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.WarningText]);
                        titleSlide.Shapes[1].TextFrame.TextRange.Text = "YOU need to add the hymn I cannot find: " + possibleHymn;
                    }
                    else {
                        oPre.Slides.InsertFromFile(possibleHymn, oPre.Slides.Count);
                    }

                    continue;
                }

                if (itemType == "item") {

                    if (sItem.title.ToLower().Contains("communion served")) {
                        oPre.Slides.InsertFromFile(this.theConfig.EveningStandardSlides + "Communion.pptx", oPre.Slides.Count);
                    }

                    if (sItem.length > 1000) {
                        string title = sItem.title;
                        string speaker = sItem.description;
                        PowerPoint.Slide titleSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.BlackSermonInfo]);
                        titleSlide.Shapes[1].TextFrame.TextRange.Text = speaker;
                        titleSlide.Shapes[2].TextFrame.TextRange.Text = title;
                        continue;
                    }

                    if (sItem.title.ToLower().Contains("adoration")) {
                        oPre.Slides.InsertFromFile(this.theConfig.EveningStandardSlides + "TheLordsPrayer.pptx", oPre.Slides.Count);
                        continue;
                    }

                    if (sItem.title.ToLower() == "welcome") {
                        oPre.Slides.InsertFromFile(this.theConfig.EveningStandardSlides + "Welcome.pptx", oPre.Slides.Count);
                        continue;
                    }

                    if (sItem.title.ToLower().Contains("life of the church")) {
                        oPre.Slides.InsertFromFile(this.theConfig.EveningStandardSlides + "LifeOfTheChurch.pptx", oPre.Slides.Count);
                        continue;
                    }

                    if (sItem.title.ToLower().Contains("congregational prayer")) {
                        oPre.Slides.InsertFromFile(this.theConfig.EveningStandardSlides + "CongregationalPrayer.pptx", oPre.Slides.Count);
                        continue;
                    }

                    if (sItem.title.ToLower().Contains("offering/offertory")) {
                        oPre.Slides.InsertFromFile(this.theConfig.EveningStandardSlides + "Offering.pptx", oPre.Slides.Count);
                        continue;
                    }

                    if (sItem.title.ToLower().Contains("benediction")) {
                        oPre.Slides.InsertFromFile(this.theConfig.EveningStandardSlides + "Benediction.pptx", oPre.Slides.Count);
                        continue;
                    }

                    if (sItem.title.ToLower().Contains("announcements")) {
                        //Add in the video that Elizabeth has on the shared drive...
                        string newLocation = this.theConfig.EveningAnnoucementVideoSourceDir + "\\" + DateTime.Now.ToString("yyyy-MM-dd-hhmm") + ".wmv";

                        string wmvPath = this.theConfig.EveningAnnoucementVideoSourceDir + DateTime.Now.ToString("yyyy") + " Displays\\" + Helpers.GetNextSunday() + ".wmv";

                        //Videos are any random day, not just sundays....
                        int count = 0;
                        while (!File.Exists(wmvPath) && count < 90) {
                            wmvPath = this.theConfig.EveningAnnoucementVideoSourceDir + DateTime.Now.AddDays(-count).ToString("yyyy") + " Displays\\" + DateTime.Now.AddDays(-count).ToString("MMddyy") + ".wmv";
                            count = count + 1;
                        }

                        if (!File.Exists(wmvPath)) {
                            //we tried 
                            messagesForUser = messagesForUser + "Could not find the announcement video, if you want to look it should be around: " + wmvPath;
                            continue;
                        }
                        else {
                            try {
                                File.Copy(wmvPath, newLocation);
                            }
                            catch (Exception) {
                                messagesForUser = messagesForUser + "\nCould not copy the announcement video at: " + wmvPath + " To: " + newLocation;
                            }
                            try {
                                PowerPoint.Slide titleSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.BlackBlank]);
                                var theShape = titleSlide.Shapes.AddMediaObject2(newLocation, Office.MsoTriState.msoFalse, Office.MsoTriState.msoTrue, 0, 0, 1280, 720);
                                messagesForUser = messagesForUser + "\nI added the announcement video but I cannot set the video to loop, please do this manually\n\nSelect the last slide ->\nselect the video by clicking on it->\nselect Playback from the top right of the ribbon strip->\nselect Loop until stopped.\n\nThanks!";

                            }
                            catch (Exception ex) {
                                messagesForUser = messagesForUser + "\nCould not add the announcement video to the slides, go to Desktop\\evening\\announcements\\ grab the most recent, and add it yourself :-) \nError\n " + ex.Message;
                            }
                        }
                        continue;
                    }

                    if (sItem.title.ToLower().Contains("call to worship")) {

                        //add title slide
                        PowerPoint.Slide oSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.BlackCallToWorship]);
                        oSlide.Shapes[1].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;

                        string titleText = sItem.title;

                        oSlide.Shapes[1].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                        PowerPoint.TextRange objTextRng = oSlide.Shapes[1].TextFrame.TextRange;
                        objTextRng.Text = titleText;
                        objTextRng.Font.Size = 32;
                        objTextRng.Font.Color.RGB = Color.White.ToArgb();
                        oSlide.Shapes[1].TextFrame.TextRange.Text.Insert(0, titleText);

                        string fullReading = sItem.description;
                        fullReading = Regex.Replace(fullReading, "(\\[.*\\])|(\\(.*\\))", "");
                        string[] slideText = Regex.Split(fullReading, @"(?=Leader:)");

                        if (slideText.Count() > 0) {
                            foreach (string aSlideText in slideText) {
                                string toWrite = aSlideText;
                                if (toWrite.Length < 5) { continue; }
                                if (aSlideText.StartsWith("\r\n")) {
                                    toWrite = toWrite.Substring(4);
                                }
                                if (aSlideText.StartsWith("\r")) {
                                    toWrite = toWrite.Substring(2);
                                }
                                if (aSlideText.StartsWith("\n")) {
                                    toWrite = toWrite.Substring(2);
                                }

                                toWrite = toWrite.TrimEnd('\r', '\n');

                                toWrite = Regex.Replace(toWrite, @"\t|\n|\r", " ");

                                toWrite = toWrite.Replace("All:", "\n\nAll:");
                                toWrite = toWrite.Replace("all:", "\n\nAll:");
                                toWrite = toWrite.Replace("ALL:", "\n\nAll:");
                                toWrite = toWrite.Replace("People:", "\n\nPeople:");
                                toWrite = toWrite.Replace("people:", "\n\nPeople:");
                                toWrite = toWrite.Replace("PEOPLE:", "\n\nPeople:");

                                PowerPoint.Slide aSlide = oPre.Slides.AddSlide(oPre.Slides.Count + 1, oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.BlackCallToWorship]);
                                aSlide.Shapes[1].TextFrame.TextRange.Text = toWrite;
                            }
                        }

                        continue;
                    }
                }
            }

            //save and close 
            string amFile = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Evening " + Helpers.GetNextSunday() + ".pptx";
            oPre.SaveAs(amFile, PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation, Office.MsoTriState.msoTriStateMixed);
            MessageBox.Show("Done\n" + messagesForUser, "Complete");
            return;
        }

        #endregion EveningPPT
        
        #region PP6ToPPT

        private void createPP6HymnsPPT_Click(object sender, EventArgs e) {
            try {
                string fileLocation = this.theConfig.EveningHymnFolder + "\\pro6\\";
                var files = Directory.GetFiles(fileLocation, "*.pro6");
                if (files.Length <= 0) {
                    MessageBox.Show("No Pro6 files were found at: " + fileLocation, "Oops");
                    return;
                }
                foreach (string aFilePath in files.OrderBy(n => n)) {
                    this.CreatePPTFromPP6(aFilePath);
                }
            }
            catch (Exception ex) {
                MessageBox.Show("ERROR:  \n" + ex.Message);
            }
        }

        private void createPP6HymnPPT_Click(object sender, EventArgs e) {
            try {
                string fileLocation = "";
                OpenFileDialog picker = new OpenFileDialog();
                picker.Title = "Pick the PP6 File";
                picker.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                picker.Filter = "PP6 files (*.pro6)|*.pro6|All files (*.*)|*.*";
                picker.FilterIndex = 1;
                picker.RestoreDirectory = true;
                if (picker.ShowDialog() == DialogResult.OK) {
                    fileLocation = picker.FileName;
                    if (fileLocation != "") {
                        this.CreatePPTFromPP6(fileLocation);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show("ERROR:  \n" + ex.Message);
            }
        }

        private void CreatePPTFromPP6(string aFilePath) {
            StreamReader readIn = new StreamReader(aFilePath);
            string aHymn = readIn.ReadToEnd();
            readIn.Close();
            readIn.Dispose();

            FileInfo fi = new FileInfo(aFilePath);
            string title = fi.Name;
            title = title.Replace(".pro6", "");

            //if arrangements is empty, just get all slides...
            Queue<string> hymnOrder = new Queue<string>();
            Queue<string> verses = new Queue<string>();
            try {
                int isThereAnArrangement = aHymn.IndexOf("arrangements\">");
                if (isThereAnArrangement > 0) {
                    //find the order of the song 
                    //use Normal, default is the other option 
                    using (XmlReader reader = XmlReader.Create(new StringReader(aHymn))) {
                        reader.ReadToFollowing("RVSongArrangement");
                        reader.ReadToFollowing("array");

                        if (reader.ReadToDescendant("NSString")) {
                            do {
                                string anID = reader.ReadElementContentAsString();
                                if (!string.IsNullOrWhiteSpace(anID)) {
                                    hymnOrder.Enqueue(anID);
                                }
                            } while (reader.ReadToNextSibling("NSString"));
                        }
                    }

                    foreach (string anId in hymnOrder) {
                        string aVerseChunk = aHymn;
                        int readTillId = aVerseChunk.IndexOf(anId);
                        if (readTillId > -1) {
                            aVerseChunk = aVerseChunk.Substring(readTillId);
                            int readTillTextChunk = aVerseChunk.IndexOf("PlainText");
                            if (readTillTextChunk > -1) {
                                aVerseChunk = aVerseChunk.Substring(readTillTextChunk);
                                int endOfVerse = aVerseChunk.IndexOf("<");
                                if (endOfVerse > -1) {
                                    aVerseChunk = aVerseChunk.Substring(0, endOfVerse);
                                    aVerseChunk = aVerseChunk.Replace("PlainText\">", "");
                                    aVerseChunk = aVerseChunk.Replace("\"", "");
                                    aVerseChunk = aVerseChunk.Replace(">", "");
                                    aVerseChunk = aVerseChunk.Replace("<", "");

                                    //encoded as base64
                                    byte[] aVerseEncoded = Convert.FromBase64String(aVerseChunk);
                                    string aVerseDecoded = Encoding.UTF8.GetString(aVerseEncoded);
                                    if (!string.IsNullOrEmpty(aVerseDecoded)) {
                                        verses.Enqueue(aVerseDecoded);
                                    }
                                }
                            }
                        }
                    }
                }
                else {
                    //there is no verse order...just go through the file 
                    while (aHymn.Contains("PlainText")) {
                        int readTillTextChunk = aHymn.IndexOf("PlainText");
                        if (readTillTextChunk > -1) {
                            aHymn = aHymn.Substring(readTillTextChunk);
                            int endOfVerse = aHymn.IndexOf("<");

                            if (endOfVerse > -1) {
                                string aVerseChunk = aHymn.Substring(0, endOfVerse);
                                aHymn = aHymn.Substring(endOfVerse);
                                aVerseChunk = aVerseChunk.Replace("PlainText\">", "");
                                aVerseChunk = aVerseChunk.Replace("\"", "");
                                aVerseChunk = aVerseChunk.Replace(">", "");
                                aVerseChunk = aVerseChunk.Replace("<", "");

                                //encoded as base64
                                byte[] aVerseEncoded = Convert.FromBase64String(aVerseChunk);
                                string aVerseDecoded = Encoding.UTF8.GetString(aVerseEncoded);
                                if (!string.IsNullOrEmpty(aVerseDecoded)) {
                                    verses.Enqueue(aVerseDecoded);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                string theMessage = ex.Message + "\n" + ex.InnerException;

            }

            if (verses.Count == 0) {
                string theMessage = "\n" + "ERROR NO SLIDES FOR THE FILE: " + title;
                return;
            }

            // Create an instance of Microsoft PowerPoint
            PowerPoint.Application oPowerPoint = new PowerPoint.Application();

            // Create a new Presentation
            PowerPoint.Presentation oPre = oPowerPoint.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoTrue);

            oPre.ApplyTheme(this.theConfig.MasterTemplate);
            oPre.PageSetup.SlideSize = PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen16x9;

            PowerPoint.CustomLayout titleMasterSlide = oPre.SlideMaster.CustomLayouts[3];
            PowerPoint.CustomLayout verseSlide = oPre.SlideMaster.CustomLayouts[4]; //[PowerPoint.PpSlideLayout.ppLayoutBlank];

            // Insert a new Slide and add some text to it.
            int countSlides = 1;
            //Insert the title
            // PP6 files have this in there...  PowerPoint.Slide titleSlide = oPre.Slides.AddSlide(countSlides, titleMasterSlide);

            //  titleSlide.Shapes[1].TextFrame.TextRange.Text = title;
            //  countSlides++;

            foreach (string aVerse in verses) {
                if (aVerse.ToLower().Contains("to edit")) {
                    continue;
                }
                string toWrite = aVerse;
                toWrite = toWrite.TrimEnd('\r', '\n');
                toWrite = toWrite.Trim();
                PowerPoint.Slide oSlide = oPre.Slides.AddSlide(countSlides, verseSlide);
                oSlide.Shapes[1].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceWithin = 1;
                oSlide.Shapes[1].TextFrame2.TextRange.Text = aVerse;
                countSlides++;
            }

            //save and close 
            string fileName = aFilePath.Replace(".pro6", ".pptx");
            oPre.SaveAs(fileName, PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation, Office.MsoTriState.msoTriStateMixed);
            oPre.Close();

            foreach (var process in Process.GetProcessesByName("POWERPNT")) {
                process.Kill();
            }

            //and cleanup

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            System.Threading.Thread.Sleep(500);
        }    

        #endregion PP6ToPPT

        #region HTMLToPPT

        private void createHTMLHymnsPPT_Click(object sender, EventArgs e) {
            try {
                DialogResult dialogResult = MessageBox.Show("This process will take at least 30 to 60 minutes AND you CANNOT use the computer while it is working as it will open and close PowerPoint too fast, stealing focus again and again.\n\nDo you want to still do it?", "Are you sure you want to?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No) {
                    return;
                }

                string fileLocation = this.theConfig.MorningHymnal + "\\html\\";
                bool doTheTextFilesExist = false;
                if(File.Exists(fileLocation + "1.html")) {
                    doTheTextFilesExist = true;
                }

                if (doTheTextFilesExist == false) {
                    MessageBox.Show("The HTML Files were not available. We have to download them, this will take 30 minutes.");
                    int maxHymns = 742;
                    int count = 1;
                    Random r = new Random();
                    System.IO.Directory.CreateDirectory(fileLocation);
                    while (count <= maxHymns) {
                        using (WebClient wc = new WebClient()) {
                            string page = wc.DownloadString(this.theConfig.HymnaryBaseURL + count + "#text");
                            StreamWriter newFile = new StreamWriter(fileLocation + count + ".html");
                            newFile.Write(page);
                            newFile.Flush();
                            newFile.Close();
                            newFile.Dispose();

                        }
                        count++;
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(r.Next(10) * 100);
                    }
                }

                foreach (string aFilePath in Directory.GetFiles(fileLocation, "*.html").OrderBy(n => n)) {

                    StreamReader readIn = new StreamReader(aFilePath);
                    string aHymn = readIn.ReadToEnd();
                    readIn.Close();
                    readIn.Dispose();
                    //process the lyrics 

                    string title = "";
                    int beginingOfTitle = aHymn.IndexOf("<title>Trinity Hymnal (Rev. ed.)");
                    int endOfTitle = aHymn.IndexOf(" - Hymnary.org</title>");
                    int lenOfTitle = endOfTitle - beginingOfTitle;
                    title = aHymn.Substring(beginingOfTitle + 32, lenOfTitle - 32);
                    title = title.Substring(title.IndexOf(".") + 1);
                    title = title.Trim();
                    string number = aFilePath.Replace(fileLocation, "");
                    number = number.Replace(".html", "");

                    //get just the words to the song 
                    int wordsStartHere = aHymn.IndexOf("<div id=\"text\">") + 15;
                    string justTheWords = aHymn.Substring(wordsStartHere);
                    justTheWords = justTheWords.Substring(0, justTheWords.IndexOf("</div>"));

                    //get verses 
                    ArrayList allVerses = new ArrayList();
                 
                    string refrain = "";
                    Queue<string> verses = new Queue<string>();

                    while (justTheWords.IndexOf("<p>") >= 0) {
                        int howMuchToTake = justTheWords.Length - 1;
                        if (justTheWords.IndexOf("</p>") > 0) {
                            howMuchToTake = justTheWords.IndexOf("</p>");
                        }

                        string aVerse = justTheWords.Substring(0, howMuchToTake);
                        justTheWords = justTheWords.Substring(howMuchToTake);
                        if (justTheWords.StartsWith("</p>")) {
                            justTheWords = justTheWords.Substring(4);
                        }
                        //clean the verse

                        //remove <br />
                        aVerse = aVerse.Replace("<br />", "");
                        aVerse = aVerse.Replace("<p>", "");
                        aVerse = aVerse.Replace("</p>", "");
                        //aVerse = aVerse.Replace("\r", "");
                        aVerse = aVerse.Replace("\n\n", "\n");
                        aVerse = aVerse.Replace("\n\n\n", "\n");
                        //remove number of verses...
                        aVerse = aVerse.Replace("1", "");
                        aVerse = aVerse.Replace("2", "");
                        aVerse = aVerse.Replace("3", "");
                        aVerse = aVerse.Replace("4", "");
                        aVerse = aVerse.Replace("5", "");
                        aVerse = aVerse.Replace("6", "");
                        aVerse = aVerse.Replace("7", "");
                        aVerse = aVerse.Replace("8", "");
                        aVerse = aVerse.Replace("9", "");


                        aVerse = aVerse.Replace("â€™", "'");

                        aVerse = aVerse.Replace("<div id=\"text\">", "");
                        aVerse = aVerse.Replace("</div>", "");


                        aVerse = aVerse.Trim();

                        aVerse = aVerse.TrimEnd('\r', '\n');
                        aVerse = aVerse.TrimStart('\r', '\n');
                        aVerse = aVerse.TrimEnd(Environment.NewLine.ToCharArray());
                        aVerse = aVerse.TrimStart(Environment.NewLine.ToCharArray());

                        aVerse = aVerse.Replace("<p", "");
                        aVerse = aVerse.Replace("p>", "");
                        aVerse = aVerse.Replace("<p>", "");
                        aVerse = aVerse.Replace("</p>", "");
                        aVerse = aVerse.Replace("<br>", "");
                        aVerse = aVerse.Replace("<br/>", "");



                        if (aVerse.ToLower().Contains("refrain:")) {
                            aVerse = aVerse.Replace("Refrain:", "");
                            aVerse = aVerse.Replace("refrain:", "");

                            aVerse = aVerse.TrimEnd('\r', '\n');
                            aVerse = aVerse.TrimStart('\r', '\n');
                            aVerse = Regex.Replace(aVerse, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

                            refrain = aVerse;
                        }

                        if (aVerse.ToLower().Contains("[refrain]")) {
                            aVerse = aVerse.Replace("[Refrain]", "");
                            aVerse = aVerse.Replace("[refrain]", "");
                            aVerse = aVerse.Replace("[", "");
                            aVerse = aVerse.Replace("]", "");

                            aVerse = aVerse.TrimEnd('\r', '\n');
                            aVerse = aVerse.TrimStart('\r', '\n');
                            aVerse = Regex.Replace(aVerse, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

                            refrain = refrain.TrimEnd('\r', '\n');
                            refrain = refrain.TrimStart('\r', '\n');
                            refrain = Regex.Replace(refrain, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

                            verses.Enqueue(aVerse);
                            verses.Enqueue(refrain);

                        }
                        else {
                            aVerse = aVerse.TrimEnd('\r', '\n');
                            aVerse = aVerse.TrimStart('\r', '\n');
                            aVerse = Regex.Replace(aVerse, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);

                            if (!string.IsNullOrEmpty(aVerse)) {
                                verses.Enqueue(aVerse);
                            }
                        }

                    }


                    // Create an instance of Microsoft PowerPoint
                    PowerPoint.Application oPowerPoint = new PowerPoint.Application();

                    // Create a new Presentation
                    PowerPoint.Presentation oPre = oPowerPoint.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoTrue);

                    oPre.ApplyTheme(this.theConfig.MasterTemplate);
                    oPre.PageSetup.SlideSize = PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen16x9;

                    PowerPoint.CustomLayout titleMasterSlide = oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.ChromakeyHymnTitle];
                    PowerPoint.CustomLayout verseSlide = oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.WhiteHymnText]; 

                    // Insert a new Slide and add some text to it.
                    int countSlides = 1;
                    //Insert the title
                    PowerPoint.Slide titleSlide = oPre.Slides.AddSlide(countSlides, titleMasterSlide);

                    titleSlide.Shapes[2].TextFrame.TextRange.Text = "Hymn #" + number;
                    titleSlide.Shapes[1].TextFrame.TextRange.Text = title;
                    countSlides++;

                    string fileName = aFilePath.Replace(".html", ".pptx");

                    if (verses.Count == 0) {
                        PowerPoint.Slide oSlide = oPre.Slides.AddSlide(countSlides, verseSlide);
                        oSlide.Shapes[1].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                        oSlide.Shapes[1].TextFrame2.TextRange.ParagraphFormat.SpaceAfter = 0;
                        oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceAfter = 0;
                        oSlide.Shapes[1].TextFrame2.TextRange.ParagraphFormat.SpaceBefore = 0;
                        oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceBefore = 0;
                        oSlide.Shapes[1].TextFrame2.TextRange.Text = "The text is copywritten, you'll have to get it by searching the web.. sorry";
                        countSlides++;
                        fileName = aFilePath.Replace(".html", "-MissingLyrics.pptx");
                    }
                    else {

                        int slideCount = 1;
                        foreach (string aVerse in verses) {
                            PowerPoint.Slide oSlide = oPre.Slides.AddSlide(countSlides, verseSlide);
                            oSlide.Shapes[1].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                            oSlide.Shapes[1].TextFrame2.TextRange.ParagraphFormat.SpaceAfter = 6;
                            oSlide.Shapes[1].TextFrame2.TextRange.ParagraphFormat.SpaceBefore = 0;
                            oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceAfter = 6;
                            oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceBefore = 0;
                            oSlide.Shapes[1].TextFrame2.TextRange.Text = aVerse;
                            string counter = slideCount.ToString() + "/" + verses.Count.ToString();
                            slideCount++;
                            /// oSlide.Shapes[2].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                            oSlide.Shapes[2].TextFrame2.TextRange.Text = counter;
                            countSlides++;
                        }
                    }

                    //save and close 
                    oPre.SaveAs(fileName, PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation, Office.MsoTriState.msoTriStateMixed);
                    oPre.Close();

                    // Quit the PowerPoint application            
                    //oPowerPoint.Quit();

                    foreach (var process in Process.GetProcessesByName("POWERPNT")) {
                        process.Kill();
                    }

                    //and cleanup

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    System.Threading.Thread.Sleep(500);
                }
            }

            catch (Exception ex) {
                MessageBox.Show("ERROR:  \n" + ex.Message);
            }

        }

        #endregion HTMLToPPT
        
        private void scriptureRefToPPT_Click(object sender, EventArgs e) {
            Form bible = new NIVtoPPT(this.theConfig.MasterTemplate, this.theConfig.BibleConnectionString);
            bible.Show();
        }

        private void defaultsToolStripMenuItem_Click(object sender, EventArgs e) {
            this.theConfig.Show();
        }

        private void howToToolStripMenuItem_Click(object sender, EventArgs e) {
            string helpFile = this.theConfig.RootDirectory + "Help.html";
            HelpForm h = new HelpForm(helpFile);
            h.Show();
        }
    }
}

