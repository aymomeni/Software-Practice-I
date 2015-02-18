using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Formulas;
using SS;


/**
 * PS5 - Ali Momeni - February 14, 2015 
 * CS 3500 - Joe Zackary
 */
namespace SpreadsheetTestCases
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void SpreadSheetTestMethod1()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }


        [TestMethod]
        public void SpreadSheetTestMethod2()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            AbstractSpreadsheet s2 = new Spreadsheet();
            s.SetCellContents("A1", 10);
            //s2.SetCellContents("A2", "");
            double d = (double)s.GetCellContents("A1");
            //String s1 = (string)s2.GetCellContents("A2");
            Assert.AreEqual(d, 10);
            //Assert.AreEqual(s1, "");
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SpreadSheetTestMethod3()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.GetCellContents(null);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SpreadSheetTestMethod4()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("Aaa01", 10);
        }


        [TestMethod]
        public void SpreadSheetTestMethod5()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("Aaaxcvxc7803", 10);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SpreadSheetTestMethod6()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A01", 10);
        }


        [TestMethod]
        public void SpreadSheetTestMethod7()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.SetCellContents("A1", 10);
            HashSet<String> test = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            HashSet<string> comparison = new HashSet<string>() {"A1"};

            Assert.IsTrue(comparison.SetEquals(test));
            
        }


        [TestMethod]
        public void SpreadSheetTestMethod8()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.SetCellContents("A2", 11);
            s.SetCellContents("B3", 12);
            s.SetCellContents("B3", 13);
            HashSet<String> test = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            HashSet<string> comparison = new HashSet<string>() { "A1", "A2", "B3"};

            Assert.IsTrue(comparison.SetEquals(test));
        }


        [TestMethod]
        public void SpreadSheetTestMethod9()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("2+3"));
            s.SetCellContents("B5", new Formula("5+3"));
            s.SetCellContents("B2", new Formula("9+3"));
            s.SetCellContents("C234", new Formula("22+3"));
            s.SetCellContents("C234", new Formula("22+3"));
            HashSet<String> test = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            HashSet<string> comparison = new HashSet<string>() { "A1", "B5", "B2", "C234"};

            Assert.IsTrue(comparison.SetEquals(test));
        }

        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void SpreadSheetTestMethod10()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("2+B5"));
            s.SetCellContents("B5", new Formula("5+A1"));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SpreadSheetTestMethod11()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A", new Formula("2+B5"));

        }


        [TestMethod]
        public void SpreadSheetTestMethod12()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", "2+3");
            s.SetCellContents("B5", "5+3");
            s.SetCellContents("B2", "9+3");
            s.SetCellContents("C234", "22+3");
            s.SetCellContents("C234", "22+3");
            HashSet<String> test = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            HashSet<string> comparison = new HashSet<string>() { "A1", "B5", "B2", "C234" };

            Assert.IsTrue(comparison.SetEquals(test));
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SpreadSheetTestMethod13()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A", "2+B5");

        }


        [TestMethod]
        public void SpreadSheetTestMethod14()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("B1", 10);
            ss.SetCellContents("B1", 5);
            Assert.AreEqual(ss.GetCellContents("B1"), 5.0);
        }


        [TestMethod]
        public void SpreadSheetTestMethod15()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }


        [TestMethod]
        [ExpectedException(typeof(CircularException))]
        public void SpreadSheetTestMethod16()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("2+C234"));
            s.SetCellContents("B5", new Formula("5+A1"));
            s.SetCellContents("B2", new Formula("9+B5"));
            s.SetCellContents("C234", new Formula("22+B2"));
           
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SpreadSheetTestMethod17()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("A1", (Formula)null);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void SpreadSheetTestMethod18()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("1A", 10);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SpreadSheetTestMethod19()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("A1", (String)null);
        }


        [TestMethod]
        public void SpreadSheetTestMethod20()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents("B1", "Boooom");
            ss.SetCellContents("B1", "DongDong");
            Assert.AreEqual(ss.GetCellContents("B1"), "DongDong");
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidNameException))]
        public void Test21()
        {
            Spreadsheet ss = new Spreadsheet();
            ss.SetCellContents(null, 10);
        }


        [TestMethod]
        public void SpreadSheetTestMethod22()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", "B5");
            s.SetCellContents("B5", "A1");
            Assert.AreEqual(s.GetCellContents("A1"), "B5");
            Assert.AreEqual(s.GetCellContents("B5"), "A1");
            HashSet<String> test = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            HashSet<string> comparison = new HashSet<string>() { "A1", "B5"};
            Assert.IsTrue(comparison.SetEquals(test));
        }


        [TestMethod]
        public void SpreadSheetTestMethod23()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("2+C234"));
            s.SetCellContents("B5", new Formula("5+A1"));
            s.SetCellContents("B2", new Formula("9+B5"));
            s.SetCellContents("C234", new Formula("22"));

        }


        [TestMethod]
        public void SpreadSheetTestMethod24()
        {
            AbstractSpreadsheet s = new Spreadsheet();

            for (int i = 1; i < 1000; i++ )
                s.SetCellContents("A" + "" + i, new Formula("2+C234" + i));
            HashSet<String> test = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            Assert.IsTrue(test.Count == 999);

        }


        [TestMethod]
        public void SpreadSheetTestMethod25()
        {
            AbstractSpreadsheet s = new Spreadsheet();

            for (int i = 1; i < 1000; i++)
                s.SetCellContents("A" + "" +i, i);
            HashSet<String> test = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            Assert.IsTrue(test.Count == 999);
        }
    }
}
