using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServicePPTCreator
{
    /// <summary>
    /// Parses Bible verses of the following format 
    ///Book or book abbreviation followed by...
    ///2
    ///2:2
    ///2:2-2
    ///2:2-2,4
    ///2:2-2,2-3
    ///2:2-2,3:2
    ///2:2-2,3:2-3
    ///2:2-2,3:2-4:2
    /// </summary>
    public static class ReferenceParser
    {
        //Well, we could read this in from a file, but then this isn't self-contained. And I could 
        //have this in a format and then process it, or I could just add it manually. This isn't very 
        //sexy, but it is the simplist to meet the requirements here...And I'm pretty simple.
        #region AddAllTheBooks

        public static List<Book> Books = new List<Book>() {
            new Book(1, "Genesis", "Gen,Ge,Gn"),
            new Book(2, "Exodus", "Ex,Exod"),
            new Book(3, "Leviticus", "Lev,Le,Lv"),
            new Book(4, "Numbers", "Num,Nu,Nm,Nb"),
            new Book(5, "Deuteronomy", "Deut,De,Dt"),
            new Book(6, "Joshua", "Josh,Jos,Jsh"),
            new Book(7, "Judges", "Judg,Jdg,Jg,Jdgs"),
            new Book(8, "Ruth", "Ruth,Rth,Ru"),
            new Book(9, "1 Samuel", "1 Sam,1 Sm,1 Sa,1 S,I Samuel,I Sam,I Sm,I Sa,I S"),
            new Book(10, "2 Samuel", "2 Sam,2 Sm,2 Sa,2 S,II Samuel,II Sam,II Sm,II Sa,II S"),
            new Book(11, "1 Kings", "1 Kings,1 Kgs,1 Kin,1 Ki,1K,I Kings,I Kings,I Kgs,I Kin,I Ki,I K"),
            new Book(12, "2 Kings", "2 Kings,2 Kgs,2 Kin,2 Ki,II Kings,II Kings,II Kgs,II Kin,II Ki"),
            new Book(13, "1 Chronicles", "1 Chr,1 Chr,1 Ch,II Chronicles,I Chr,I Chr,I Ch"),
            new Book(14, "2 Chronicles", "2 Chr,2 Ch,2 Chron,II Chronicles,II Chr,II Ch,II Chron"),
            new Book(15, "Ezra", "Ezra,Ezr,Ez"),
            new Book(16, "Nehemiah", "Neh,Ne"),
            new Book(17, "Esther", "Esth,Est,Es"),
            new Book(18, "Job", "Job,Jb"),
            new Book(19, "Psalm", "Ps,Psalms,Pslm,Psa,Psm"),
            new Book(20, "Proverbs", "Prov,Pro,Prv,Pr"),
            new Book(21, "Ecclesiastes", "Ecc1,Eccles,Eccle,Ecc,Ec"),
            new Book(22, "Song of Solomon", "Songs,Song"),
            new Book(23, "Isaiah", "Isa,Is"),
            new Book(24, "Jeremiah", "Jer,Je,Jr"),
            new Book(25, "Lamentations", "Lam,La"),
            new Book(26, "Ezekial", "Ezek,Eze,Ezk"),
            new Book(27, "Daniel", "Dan,Da,Dn"),
            new Book(28, "Hosea", "Hos,Ho"),
            new Book(29, "Joel", "Joel,Jl"),
            new Book(30, "Amos", "Am"),
            new Book(31, "Obadiah", "Ob,Od"),
            new Book(32, "Jonah", "Jon,Jnh"),
            new Book(33, "Micah", "Mic,Mc"),
            new Book(34, "Nahum", "Nah,Na"),
            new Book(35, "Habakkuk", "Hab"),
            new Book(36, "Zephaniah", "Zeph,Zep,Zp"),
            new Book(37, "Haggai", "Hag,Hg "),
            new Book(38, "Zechariah", "Zech,Zec,Zc"),
            new Book(39, "Malachi", "Mal,Ml"),
            new Book(40, "Matthew", "Mt,Matt"),
            new Book(41, "Mark", "Mk,Mrk"),
            new Book(42, "Luke", "Lk,Luk"),
            new Book(43, "John", "Jn,Jhn"),
            new Book(44, "Acts", " Acts of the Apostles, Ac"),
            new Book(45, "Romans", "Rom,Ro,Rm"),
            new Book(46, "1 Corinthians", "1 Cor,1 Co,I Corinthians,I Cor,I Co"),
            new Book(47, "2 Corinthians", "2 Cor,2 Co,II Corinthians,II Cor,II Co"),
            new Book(48, "Galatians", "Gal,Ga"),
            new Book(49, "Ephesians", "Eph,Ephes"),
            new Book(50, "Philippians", "Phil,Php,Pp"),
            new Book(51, "Colossians", "Col"),
            new Book(52, "1 Thessalonians", "1 Thess,1 Thes,1 Th,I Thessalonians,I Thess,I Thes,I Th"),
            new Book(53, "2 Thessalonians", "2 Thess,2 Thes,2 Th,II Thessalonians,II Thess,II Thes,II Th"),
            new Book(54, "1 Timothy", "1 Tim,1 Ti,I Timothy,I Tim,I Ti"),
            new Book(55, "2 Timothy", "2 Tim,2 Ti,II Timothy,II Tim,II Ti"),
            new Book(56, "Titus", "Titus,Tit,Ti"),
            new Book(57, "Philemon", "Philemon,Philem,Phm,Pm"),
            new Book(58, "Hebrews", "Heb"),
            new Book(59, "James", "Jas,Jm"),
            new Book(60, "1 Peter", "1 Pet,1 Pe,1 Pt,1 P,I Peter,I Pet,I Pe,I Pt,I P"),
            new Book(61, "2 Peter", "2 Pet,2 Pe,2 Pt,2 P,II Peter,II Pet,II Pe,II Pt,II P"),
            new Book(62, "1 John", "1 Jn,1 Jhn,1 J,I John,I Jn,I Jhn,I J"),
            new Book(63, "2 John", "2 Jn,2 Jhn,2 J,II John,II Jn,II Jhn,II J"),
            new Book(64, "3 John", "3 Jn,3 Jhn,3 J,III John,III Jn,III Jhn,III J"),
            new Book(65, "Jude", "Jude,Jud,Jd"),
            new Book(66, "Revelation", "Rev") };

        #endregion books

        public static List<Reference> ParseReference(string refToParse) {
            List<Reference> allReferences = new List<Reference>();

            var allBooks = Books.SelectMany(x => x.AllNames.ToArray());

            while (!string.IsNullOrEmpty(refToParse) && refToParse.Length > 3) {
                Reference theRef = new Reference();
                theRef.OriginalReferenceText = refToParse;

                //get rid of pesky characters
                refToParse = refToParse.Replace("–", "-");
                refToParse = refToParse.Replace("\"", "");
                refToParse = refToParse.ToLower();

                if (allBooks.Any(s => refToParse.Contains(s.ToLower()))) {
                    //there's atleast 1 book reference                   
                    Book firstBook = new Book(refToParse);
                    if (firstBook.SelectedName.Length > 0) {
                        //remove bookname from the ref string
                        refToParse = refToParse.Substring(refToParse.IndexOf(firstBook.SelectedName.ToLower()) + firstBook.SelectedName.Length);

                        //find the next book, as that marks the end of this reference 
                        Book nextBook = new Book(refToParse);

                        int cutFirstRefLoc = refToParse.Length;
                        if (!string.IsNullOrEmpty(nextBook.SelectedName) && nextBook.SelectedName.Length > 0) {
                            cutFirstRefLoc = refToParse.IndexOf(nextBook.SelectedName.ToLower());
                        }

                        //Looks like we have a reference
                        string numReference = refToParse.Substring(0, cutFirstRefLoc);
                        theRef.Book = firstBook;

                        //deal with the rest next look
                        refToParse = refToParse.Substring(cutFirstRefLoc);

                        //add all the chapters and verses
                        string[] chapterDelims = { ";" };
                        string[] verses = numReference.Split(chapterDelims, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string aVerse in verses) {
                            //if there's a comma... add the previous chapter onto the string and process it 
                            if (aVerse.Contains(",")) {
                                string[] subVerses = aVerse.Split(',');
                                ChapterAndVerse withChapter = new ChapterAndVerse(subVerses[0]);
                                string previousChapter = withChapter.StartChapter;
                                theRef.ChapterAndVerses.Add(withChapter);
                                int count = 1;
                                while (count < subVerses.Length) {
                                    string subVerseRef = subVerses[count];
                                    if (!subVerses[count].Contains(":")) {
                                        subVerseRef = withChapter.StartChapter + ":" + subVerses[count];
                                    }
                                    theRef.ChapterAndVerses.Add(new ChapterAndVerse(subVerseRef));
                                    count++;
                                }
                            }
                            else {
                                theRef.ChapterAndVerses.Add(new ChapterAndVerse(aVerse));
                            }
                        }
                    }
                }

                allReferences.Add(theRef);
            }
            return allReferences;
        }

        public class Reference
        {
            public Book Book { get; set; }
            public List<ChapterAndVerse> ChapterAndVerses { get; set; }
            public string OriginalReferenceText { get; set; }


            public Reference() {
                this.ChapterAndVerses = new List<ChapterAndVerse>();
            }

            public string PrettyOutput() {
                string pretty = "";
                pretty = this.Book.Name + " ";
                //TODO: I should make this look nicer...
                foreach (ChapterAndVerse aCV in this.ChapterAndVerses) {
                    pretty = pretty + " " + aCV.PrettyOutput();
                }
                return pretty.Trim();
            }
        }

        public class Book
        {

            public string Name { get; set; }
            public string SelectedName { get; set; }
            public int Number { get; set; }
            public List<string> AllNames = new List<string>();

            public Book() { }

            public Book(string bookToParse) {
                //find out which book
                int firstBookLoc = 9999999; //a bug if this string is suuuuper long ;-) 
                int firstNameLen = -1;
                foreach (Book aBook in Books) {
                    foreach (string aBookName in aBook.AllNames) {
                        int aBookLoc = bookToParse.IndexOf(aBookName.ToLower());
                        if (aBookLoc >= 0 && aBookLoc <= firstBookLoc) {
                            //we are dealing with abbreviations, we want the longest in the string
                           if (aBookName.Length > firstNameLen) {
                                firstBookLoc = aBookLoc;
                                firstNameLen = aBookName.Length;
                                //store which book
                                this.AllNames = aBook.AllNames;
                                this.Name = aBook.Name;
                                this.Number = aBook.Number;
                                this.SelectedName = aBookName;
                            }
                        }
                    }
                }
            }

            public Book(int num, string name, string listOfAbbr) {
                this.Number = num;
                this.Name = name;
                this.AllNames = listOfAbbr.Split(',').ToList<string>();
                this.AllNames.Add(this.Name);
            }
        }

        public class ChapterAndVerse
        {
            public string StartChapter;
            public string EndChapter;

            public string StartVerse;
            public string EndVerse;

            public ChapterAndVerse() { }

            public ChapterAndVerse(string cv) {
                /* different verse options here...
                2
                2:2
                2:2 - 2
                2:2 - 3:2
                */

                //read until colon
                if (cv.Length > 0) {
                    //no subverses..
                    cv = cv.TrimEnd('a', 'b','c','d','e');

                    int firstColon = cv.IndexOf(":");
                    if (firstColon <= 0) {
                        //No colon -> this is a whole chapter
                        StartChapter = cv;
                        EndChapter = cv;
                        StartVerse = "";
                        EndVerse = "";
                    }
                    else {
                        //Yes colon-> there's chapter and verse
                        StartChapter = cv.Substring(0, firstColon);
                        //Read until dash
                        cv = cv.Substring(firstColon + 1);
                        int firstDash = cv.IndexOf("-");
                        if (firstDash <= 0) {
                            //No dash->book, chapter, verse
                            StartVerse = cv;
                            EndVerse = cv;
                            EndChapter = StartChapter;
                        }
                        else {
                            StartVerse = cv.Substring(0, firstDash);
                            int nextColon = cv.IndexOf(":");

                            if (nextColon <= 0) {
                                EndVerse = cv.Substring(firstDash + 1);
                                EndChapter = StartChapter;
                            }
                            else {
                                EndChapter = cv.Substring(StartVerse.Length, nextColon - 1);
                                EndVerse = cv.Substring(nextColon);
                            }
                        }
                    }
                }

                //to avoid triming everywhere
                StartChapter = Regex.Replace(StartChapter, "[^0-9]", "");
                EndChapter = Regex.Replace(EndChapter, "[^0-9]", "");
                StartVerse = Regex.Replace(StartVerse, "[^0-9]", "");
                EndVerse = Regex.Replace(EndVerse, "[^0-9]", "");

                if (!string.IsNullOrEmpty(StartChapter)) StartChapter = StartChapter.Trim();
                if (!string.IsNullOrEmpty(EndChapter)) EndChapter = EndChapter.Trim();
                if (!string.IsNullOrEmpty(StartVerse)) StartVerse = StartVerse.Trim();
                if (!string.IsNullOrEmpty(EndVerse)) EndVerse = EndVerse.Trim();

            }

            public string PrettyOutput() {
                string pretty = "";
                if (StartChapter != "") {
                    pretty = StartChapter + " ";
                    if (StartVerse != "") {
                        pretty = pretty + ":" + StartVerse;
                    }
                    if (StartChapter == EndChapter) {
                        pretty = pretty + "-" + EndVerse;
                    }
                    else {
                        pretty = pretty + "-" + EndChapter + ":" + EndVerse;
                    }
                }
                return pretty.Trim();
            }
        }
    }
}