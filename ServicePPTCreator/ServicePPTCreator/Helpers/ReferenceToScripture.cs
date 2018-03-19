using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServicePPTCreator.ReferenceParser;

namespace ServicePPTCreator
{
    public static class ReferenceToScripture
    {
        public static Queue<string> GetContentFromNIV(string connectionString, Reference aRef) {
            Queue<string> pagesOfVerses = new Queue<string>();
            string currentPage = "";
            int currentPageCharacterCount = 0;
            int maxCharactersOnAPage = 350;
            string sqlQuery = GetSQLQuery(aRef);
            if (string.IsNullOrEmpty(sqlQuery)) {
                return pagesOfVerses;
            }
            //get verses.. check there aren't too many
            using (SQLiteConnection conn = new SQLiteConnection(connectionString)) {
                try {
                    SQLiteDataAdapter da = new SQLiteDataAdapter(sqlQuery, conn);
                    conn.Open();
                    DataSet ds = new DataSet();
                    da.Fill(ds, "Verses");
                    var allVerses = ds.Tables[0].AsEnumerable().Select(r => r.Field<string>("text")).ToList();

                    if (allVerses.Count > 300) {
                        //to many, something went wrong
                        return pagesOfVerses;
                    }

                    foreach (string s in allVerses) {
                        string better = s.Replace("; ; ; ;", "");
                        better = better.Trim();
                        better = s.Replace("<pb />", "\n");
                        better = s.Replace("<pb/>", "\n");

                        if ((currentPageCharacterCount + better.Length) < maxCharactersOnAPage) {
                            currentPageCharacterCount = currentPageCharacterCount + better.Length;
                            currentPage = currentPage + " " + better;
                        }
                        else {
                            pagesOfVerses.Enqueue(currentPage);
                            currentPage = better;
                            currentPageCharacterCount = better.Length;
                        }
                    }
                    pagesOfVerses.Enqueue(currentPage);
                }
                catch (Exception) {
                }
            }

            return pagesOfVerses;
        }

        private static string GetSQLQuery(Reference aRef) {
            string q = "";
            foreach (ChapterAndVerse aCV in aRef.ChapterAndVerses) {
                q = q + "SELECT text from verses WHERE absoluteVerseNumber >= (SELECT absoluteVerseNumber from verses where book = '" + aRef.Book.Number + "'";
                q = q + " AND chapter = '" + aCV.StartChapter + "'  AND verse = '" + aCV.StartVerse + "')";
                q = q + " AND absoluteVerseNumber <= (SELECT absoluteVerseNumber from verses where book = '" + aRef.Book.Number + "'";
                q = q + " AND chapter = '" + aCV.EndChapter + "' AND verse = '" + aCV.EndVerse + "')";
                q = q + "order by absoluteVerseNumber;";
            }
            return q;
        }
    }
}
