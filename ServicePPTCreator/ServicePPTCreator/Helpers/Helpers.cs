using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServicePPTCreator
{
    public static class Helpers
    {
        public static string GetNextSunday() {
            DateTime start = DateTime.Now;
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)DayOfWeek.Sunday - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd).ToString("MMddyy");
        }


        public static DateTime GetPreviousSunday(int daysBack) {
            DateTime start = DateTime.Now.AddDays(-daysBack);
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysToAdd = ((int)DayOfWeek.Sunday - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        public static IEnumerable<string> SplitAndKeepDelim(string text, string[] delimiters) {
            var split = text.Split(delimiters, StringSplitOptions.None);

            foreach (string part in split) {
                yield return part;
                text = text.Substring(part.Length);

                string delim = delimiters.FirstOrDefault(x => text.StartsWith(x));
                if (delim != null) {
                    yield return delim;
                    text = text.Substring(delim.Length);
                }
            }
        }

        public static string FindHymn(string possibleHymn, bool shouldSearchHymnal, string hymnalFolder, bool shouldSearchSpecifedFolder, string hymnFolder) {
            string ext = ".pptx";

            if (shouldSearchHymnal) {
                String aHymn = Regex.Replace(possibleHymn, "[^0-9]", "");
                int hymnNo;
                Int32.TryParse(aHymn, out hymnNo);
                if (hymnNo > 0 && hymnNo != 731) {
                    string possibleFileByNum = hymnalFolder + hymnNo.ToString() + ".pptx";
                    if (File.Exists(possibleFileByNum)) {
                        return possibleFileByNum;
                    }
                }

                //clearly it wasn't a number
                if (shouldSearchSpecifedFolder == false) {
                    return "";
                }
            }

            if (shouldSearchSpecifedFolder) {

                possibleHymn = possibleHymn.Replace("HYMN", "");
                if (possibleHymn.IndexOf("\n") > 0) {
                    possibleHymn = possibleHymn.Substring(0, possibleHymn.IndexOf("\n"));
                }

                possibleHymn = Regex.Replace(possibleHymn, @"(^[\W_]*)|([\W_]*$)", "");

                string possibleFile = hymnFolder + possibleHymn + ext;

                if (!File.Exists(possibleFile)) {
                    string noSpaces = possibleHymn.Replace(" ", "");
                    possibleFile = hymnFolder + noSpaces + ext;
                }
                if (!File.Exists(possibleFile)) {
                    //try and guess

                    possibleHymn = Helpers.GuessAtHymn(possibleHymn, hymnFolder);
                    possibleFile = hymnFolder + possibleHymn + ext;
                }

                if (File.Exists(possibleFile)) {
                    return possibleFile;
                }
            }
            return "";
        }

        public static string GuessAtHymn(string hymnName, string directory) {
            Dictionary<string, int> allHymns = new Dictionary<string, int>();
            var allHymnsFromDir = Directory
                .EnumerateFiles(directory, "*.pptx", SearchOption.TopDirectoryOnly)
                .Select(System.IO.Path.GetFileNameWithoutExtension);

            foreach (string aHymn in allHymnsFromDir) {
                allHymns.Add(aHymn, LevenshteinDistance(hymnName, aHymn));
            }

            if (allHymns.Count > 0) {
                var bestMatch = allHymns.OrderBy(h => h.Value).FirstOrDefault();
                if (bestMatch.Value < 40) {
                    return bestMatch.Key;
                }
            }
            return "";
        }

        private static int LevenshteinDistance(string s, string t) {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0) {
                return m;
            }

            if (m == 0) {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++) {
            }

            for (int j = 0; j <= m; d[0, j] = j++) {
            }

            // Step 3
            for (int i = 1; i <= n; i++) {
                //Step 4
                for (int j = 1; j <= m; j++) {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }

        public static string CleanString(string aDirtyString) {
            string cleaned = aDirtyString.Replace('\u2013', '-');
            cleaned = cleaned.Replace('\u2014', '-');
            cleaned = cleaned.Replace('\u2015', '-');
            cleaned = cleaned.Replace('\u2017', '_');
            cleaned = cleaned.Replace('\u2018', '\'');
            cleaned = cleaned.Replace('\u2019', '\'');
            cleaned = cleaned.Replace('\u201a', ',');
            cleaned = cleaned.Replace('\u201b', '\'');
            cleaned = cleaned.Replace('\u201c', '\"');
            cleaned = cleaned.Replace('\u201d', '\"');
            cleaned = cleaned.Replace('\u201e', '\"');
            cleaned = cleaned.Replace("\u2026", "...");
            cleaned = cleaned.Replace('\u2032', '\'');
            cleaned = cleaned.Replace('\u2033', '\"');
            return cleaned;
        }
    }
}
