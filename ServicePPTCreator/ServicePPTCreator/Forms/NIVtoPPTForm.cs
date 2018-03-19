using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace ServicePPTCreator
{
    public partial class NIVtoPPT : Form
    {
        string connString;
        string templateFile;
        public NIVtoPPT(string templateDir, string nivLoc)
        {
            InitializeComponent();
            this.connString = nivLoc;
            this.templateFile = templateDir;

            //we are using this as the auto-creation failed, so there's lots of duplicated logic so we can't possibly mess up
            //the reference
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                try
                {
                    string query = "select book_number, long_name from books order by book_number;";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
                    conn.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Books");
                    this.selectBookFrom.DisplayMember = "long_name";
                    this.selectBookFrom.ValueMember = "book_number";
                    this.selectBookFrom.DataSource = ds.Tables["Books"];

                    SQLiteDataAdapter daTo = new SQLiteDataAdapter(query, conn);
                    DataSet dsTo = new DataSet();
                    daTo.Fill(dsTo, "Books");
                    this.selectBookTo.DisplayMember = "long_name";
                    this.selectBookTo.ValueMember = "book_number";
                    this.selectBookTo.DataSource = dsTo.Tables["Books"];

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SelectBookFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.selectBookFrom.SelectedItem != null)
            {
                DataRowView drv = this.selectBookFrom.SelectedItem as DataRowView;

                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    try
                    {
                        //
                        string query = "select distinct(chapter) from verses where book = '" + drv.Row["book_number"].ToString() + "' order by chapter;";
                        SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
                        conn.Open();
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Chapters");
                        this.selectChapterFrom.DisplayMember = "chapter";
                        this.selectChapterFrom.ValueMember = "chapter";
                        this.selectChapterFrom.DataSource = ds.Tables["Chapters"];

                        //update the To
                        if (this.selectBookFrom.SelectedIndex > 0)
                        {
                            this.selectBookTo.SelectedIndex = this.selectBookFrom.SelectedIndex;
                        }
                    

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void SelectBookTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.selectBookTo.SelectedItem != null)
            {
                DataRowView drv = this.selectBookTo.SelectedItem as DataRowView;

                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    try
                    {
                        //
                        string query = "select distinct(chapter) from verses where book = '" + drv.Row["book_number"].ToString() + "' order by chapter;";

                        SQLiteDataAdapter daTo = new SQLiteDataAdapter(query, conn);
                        conn.Open();
                        DataSet dsTo = new DataSet();
                        daTo.Fill(dsTo, "Chapters");
                        this.selectChapterTo.DisplayMember = "chapter";
                        this.selectChapterTo.ValueMember = "chapter";
                        this.selectChapterTo.DataSource = dsTo.Tables["Chapters"];

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }


        private void SelectChapterFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.selectChapterFrom.SelectedItem != null)
            {
                DataRowView drv = this.selectChapterFrom.SelectedItem as DataRowView;
                DataRowView drc = this.selectBookFrom.SelectedItem as DataRowView;

                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    try
                    {
                        //
                        string query = "select distinct(verse) from verses where book = '" + drc.Row["book_number"].ToString() + "' AND chapter = '" + drv.Row["chapter"].ToString() + "' order by verse;";
                        SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
                        conn.Open();
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Verses");
                        this.selectVerseFrom.DisplayMember = "verse";
                        this.selectVerseFrom.ValueMember = "verse";
                        this.selectVerseFrom.DataSource = ds.Tables["Verses"];
                        if (this.selectChapterFrom.SelectedIndex > 0)
                        {
                            this.selectChapterTo.SelectedIndex = this.selectChapterFrom.SelectedIndex;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void SelectChapterTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.selectChapterTo.SelectedItem != null)
            {
                DataRowView drv = this.selectChapterTo.SelectedItem as DataRowView;
                DataRowView drc = this.selectBookTo.SelectedItem as DataRowView;

                using (SQLiteConnection conn = new SQLiteConnection(connString))
                {
                    try
                    {
                        //
                        string query = "select distinct(verse) from verses where book = '" + drc.Row["book_number"].ToString() + "' AND chapter = '" + drv.Row["chapter"].ToString() + "' order by verse;";
                        SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
                        conn.Open();
                        DataSet ds = new DataSet();
                        da.Fill(ds, "Verses");
                        this.selectVerseTo.DisplayMember = "verse";
                        this.selectVerseTo.ValueMember = "verse";
                        this.selectVerseTo.DataSource = ds.Tables["Verses"];

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void selectVerseFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.selectBookFrom.SelectedIndex == this.selectBookTo.SelectedIndex)
            {
                if (this.selectChapterFrom.SelectedIndex == this.selectChapterTo.SelectedIndex)
                {

                    this.selectVerseTo.SelectedIndex = this.selectVerseFrom.SelectedIndex;
                }
            }
        }


        private void buttonCreateSlides_Click(object sender, EventArgs e)
        {

            DataRowView fromBook = this.selectBookFrom.SelectedItem as DataRowView;
            DataRowView fromChap = this.selectChapterFrom.SelectedItem as DataRowView;
            DataRowView fromVerse = this.selectVerseFrom.SelectedItem as DataRowView;

            string fB = fromBook.Row["book_number"].ToString();
            string fC = fromChap.Row["chapter"].ToString();
            string fV = fromVerse.Row["verse"].ToString();

            DataRowView toBook = this.selectBookTo.SelectedItem as DataRowView;
            DataRowView toChap = this.selectChapterTo.SelectedItem as DataRowView;
            DataRowView toVerse = this.selectVerseTo.SelectedItem as DataRowView;

            string tB = toBook.Row["book_number"].ToString();
            string tC = toChap.Row["chapter"].ToString();
            string tV = toVerse.Row["verse"].ToString();

            if (Int32.Parse(tB) < Int32.Parse(fB))
            {
                MessageBox.Show("You selected a book in the To part that is before the one in the from :-/ Try again");
                return;
            }

            if ((Int32.Parse(tB) == Int32.Parse(fB)) && (Int32.Parse(tC) < Int32.Parse(fC)))
            {
                MessageBox.Show("You selected a chapter in the To part that is before the one in the from :-/ Try again");
                return;
            }

            if ((Int32.Parse(tB) < Int32.Parse(fB)) && (Int32.Parse(tC) < Int32.Parse(fC)))
            {
                MessageBox.Show("You selected a chapter or book in the To part that is before the one in the from :-/ Try again");
                return;
            }

            if ((Int32.Parse(tB) == Int32.Parse(fB)) && (Int32.Parse(tC) == Int32.Parse(fC)) && (Int32.Parse(tV) < Int32.Parse(fV)))
            {
                MessageBox.Show("You selected a verse in the To part that is before the one in the from :-/ Try again");
                return;
            }

            Queue<string> pagesOfVerses = new Queue<string>();
            string currentPage = "";
            int currentPageCharacterCount = 0;
            int maxCharactersOnAPage = 350;
            
            //get verses.. check there aren't too many
            using (SQLiteConnection conn = new SQLiteConnection(connString))
            {
                try
                {
                    string query = "SELECT text from verses WHERE absoluteVerseNumber >= " +
                        "(SELECT absoluteVerseNumber from verses where book = '" + fB + "' AND chapter = '" + fC + "' AND verse = '" + fV + "')" +
                        "AND absoluteVerseNumber <= (SELECT absoluteVerseNumber from verses where book = '" + tB + "' AND chapter = '" + tC + "' AND verse = '" + tV + "')" +
                        "order by absoluteVerseNumber;";


                    SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
                    conn.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Verses");
                    var allVerses = ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("text")).ToList();

                    if (allVerses.Count > 100)
                    {
                        MessageBox.Show("Whoa there! You selected waaay tooo many verses.. over 100!! Try again, I'm not working this hard");
                        return;
                    }

                    foreach (string s in allVerses)
                    {
                        string better = s.Replace("; ; ; ;", "");
                        better = better.Trim();
                        better = s.Replace("<pb />", "\n");
                        better = s.Replace("<pb/>", "\n");

                        if ((currentPageCharacterCount + better.Length) < maxCharactersOnAPage)
                        {
                            currentPageCharacterCount = currentPageCharacterCount + better.Length;
                            currentPage = currentPage + " " + better;
                        }
                        else
                        {
                            pagesOfVerses.Enqueue(currentPage);
                            currentPage = better;
                            currentPageCharacterCount = better.Length;
                        }
                    }
                    pagesOfVerses.Enqueue(currentPage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


            //create slides

            // Create an instance of Microsoft PowerPoint
            PowerPoint.Application oPowerPoint = new PowerPoint.Application();

            // Create a new Presentation
            PowerPoint.Presentation oPre = oPowerPoint.Presentations.Add(Microsoft.Office.Core.MsoTriState.msoTrue);

            oPre.ApplyTheme(this.templateFile);
            oPre.PageSetup.SlideSize = PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen16x9;

            PowerPoint.CustomLayout titleMasterSlide = oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.ChromakeyScriptureTitle];
            PowerPoint.CustomLayout verseSlide = oPre.SlideMaster.CustomLayouts[Enums.SlidesMaster.WhiteScriptureText]; //[PowerPoint.PpSlideLayout.ppLayoutBlank];

            // Insert a new Slide and add some text to it.
            int countSlides = 1;
            //Insert the title
            PowerPoint.Slide titleSlide = oPre.Slides.AddSlide(countSlides, titleMasterSlide);
            string scriptureRef = fromBook.Row["long_name"].ToString() + " " + fC + ":" + fV + " - " + toBook.Row["long_name"].ToString() + " " + tC + ":" + tV;

            if (fromBook.Row["long_name"].ToString() == toBook.Row["long_name"].ToString())
            {
                scriptureRef = fromBook.Row["long_name"].ToString() + " " + fC + ":" + fV + " - " + " " + tC + ":" + tV;
            }

            titleSlide.Shapes[1].TextFrame.TextRange.Text = scriptureRef;
            countSlides++;            

            foreach (string aPage in pagesOfVerses)
            {
                PowerPoint.Slide oSlide = oPre.Slides.AddSlide(countSlides, verseSlide);
                oSlide.Shapes[1].TextFrame2.AutoSize = Office.MsoAutoSize.msoAutoSizeTextToFitShape;
                oSlide.Shapes[1].TextFrame2.TextRange.ParagraphFormat.SpaceAfter = 6;
                oSlide.Shapes[1].TextFrame2.TextRange.ParagraphFormat.SpaceBefore = 0;
                oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceAfter = 6;
                oSlide.Shapes[1].TextFrame2.TextRange.Paragraphs.ParagraphFormat.SpaceBefore = 0;
                oSlide.Shapes[1].TextFrame2.TextRange.Text = aPage;
                countSlides++;
            }
            
        }

        
    }
}

