using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using static ServicePPTCreator.ReferenceParser;

namespace ServicePPTCreator.Tests
{
    [TestClass()]
    public class BibleReferenceTests
    {

        [TestMethod()]
        public void BibleReferenceTestBasicOnlyChapter() {
            string t = "2 Samuel 12";
            List<Reference> theRefs = ReferenceParser.ParseReference(t);
            Assert.AreEqual("2 Samuel", theRefs.First().Book.Name);
            Assert.AreEqual("12",theRefs.First().ChapterAndVerses.First().StartChapter);
            Assert.AreEqual("",theRefs.First().ChapterAndVerses.First().StartVerse);
            Assert.AreEqual("12",theRefs.First().ChapterAndVerses.First().EndChapter);
            Assert.AreEqual("",theRefs.First().ChapterAndVerses.First().EndVerse);
        }

        [TestMethod()]
        public void BibleReferenceTestBasicSingleVerse() {
            string t = "2 Samuel 12:7";
            List<Reference> theRefs = ReferenceParser.ParseReference(t);
            Assert.AreEqual("2 Samuel",theRefs.First().Book.Name);
            Assert.AreEqual("12",theRefs.First().ChapterAndVerses.First().StartChapter);
            Assert.AreEqual("7",theRefs.First().ChapterAndVerses.First().StartVerse);
            Assert.AreEqual("12",theRefs.First().ChapterAndVerses.First().EndChapter);
            Assert.AreEqual("7",theRefs.First().ChapterAndVerses.First().EndVerse);
        }

        [TestMethod()]
        public void BibleReferenceTestBasicMultipleVerses() {
            string t = "John 12:1–7";
            List<Reference> theRefs = ReferenceParser.ParseReference(t);
            Assert.AreEqual("John",theRefs.First().Book.Name);
            Assert.AreEqual("12",theRefs.First().ChapterAndVerses.First().StartChapter);
            Assert.AreEqual("1",theRefs.First().ChapterAndVerses.First().StartVerse);
            Assert.AreEqual("12",theRefs.First().ChapterAndVerses.First().EndChapter);
            Assert.AreEqual("7",theRefs.First().ChapterAndVerses.First().EndVerse);
        }

        [TestMethod()]
        public void BibleReferenceTestBasicWithSubVerse() {
           string t = "2 Samuel 12:1–7a";
           List<Reference> theRefs = ReferenceParser.ParseReference(t);
           Assert.AreEqual(theRefs.First().Book.Name, "2 Samuel");
           Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartChapter, "12");
           Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartVerse, "1");
           Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndChapter, "12");
           Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndVerse, "7");
        }
        

        [TestMethod()]
        public void BibleReferenceTestBasicTwo() {
            string t = "2 Samuel 12:1–7;Matthew 2:23-34";
            List<Reference> theRefs = ReferenceParser.ParseReference(t);
            Assert.AreEqual(theRefs.First().Book.Name, "2 Samuel");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartChapter, "12");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartVerse, "1");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndChapter, "12");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndVerse, "7");

            Assert.AreEqual(theRefs[1].Book.Name, "Matthew");
            Assert.AreEqual(theRefs[1].ChapterAndVerses.First().StartChapter, "2");
            Assert.AreEqual(theRefs[1].ChapterAndVerses.First().StartVerse, "23");
            Assert.AreEqual(theRefs[1].ChapterAndVerses.First().EndChapter, "2");
            Assert.AreEqual(theRefs[1].ChapterAndVerses.First().EndVerse, "34");
        }


        [TestMethod()]
        public void BibleReferenceTestCommad() {
            string t = "Psalm 139:1-12, 23-24";
            List<Reference> theRefs = ReferenceParser.ParseReference(t);
            Assert.AreEqual(theRefs.First().Book.Name, "Psalm");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartVerse, "1");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndVerse, "12");
          
            Assert.AreEqual(theRefs.First().ChapterAndVerses[1].StartChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses[1].StartVerse, "23");
            Assert.AreEqual(theRefs.First().ChapterAndVerses[1].EndChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses[1].EndVerse, "24");
        }

        [TestMethod()]
        public void BibleReferenceTestCommadSameChapterSingleVerse() {
            string t = "Psalm 139:1-12, 16";
            List<Reference> theRefs = ReferenceParser.ParseReference(t);
            Assert.AreEqual(theRefs.First().Book.Name, "Psalm");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartVerse, "1");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndVerse, "12");

            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].StartChapter, "139");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].StartVerse, "16");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].EndChapter, "139");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].EndVerse, "16");
        }

        [TestMethod()]
        public void BibleReferenceTestCommadDifferentChapterSingleVerse() {
            string t = "Psalm 139:1-12, 140:24";
            List<Reference> theRefs = ReferenceParser.ParseReference(t);
            Assert.AreEqual(theRefs.First().Book.Name, "Psalm");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartVerse, "1");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndVerse, "12");

            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].StartChapter, "140");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].StartVerse, "24");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].EndChapter, "140");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].EndVerse, "24");
        }

        [TestMethod()]
        public void BibleReferenceTestCommadWithChapters() {
            string t = "Psalm 139:1-12, 140:2-4";
            List<Reference> theRefs = ReferenceParser.ParseReference(t);
            Assert.AreEqual(theRefs.First().Book.Name, "Psalm");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartVerse, "1");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndVerse, "12");

            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].StartChapter, "140");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].StartVerse, "2");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].EndChapter, "140");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].EndVerse, "4");
        }

        [TestMethod()]
        public void BibleReferenceTestCommadWithTwoChapters() {
            string t = "Psalm 139:1-12, 140:2-141:3";
            List<Reference> theRefs = ReferenceParser.ParseReference(t);
            Assert.AreEqual(theRefs.First().Book.Name, "Psalm");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().StartVerse, "1");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndChapter, "139");
            Assert.AreEqual(theRefs.First().ChapterAndVerses.First().EndVerse, "12");

            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].StartChapter, "140");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].StartVerse, "2");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].EndChapter, "141");
            Assert.AreEqual(theRefs[0].ChapterAndVerses[1].EndVerse, "3");
        }
    }
}