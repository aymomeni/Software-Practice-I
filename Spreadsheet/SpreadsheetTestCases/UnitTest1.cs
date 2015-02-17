using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadsheet;
using System.Collections.Generic;
using Formulas;

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
            s.GetNamesOfAllNoneemptyCells();
        }

        [TestMethod]
        public void SpreadSheetTestMethod2()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            double d = (double)s.GetCellContents("A1");
            String s = (string)s.GetCellContents("A2");
            Assert.AreEqual(d, 10);
            Assert.AreEqual(s, "");
        }


        [TestMethod]
        public void SpreadSheetTestMethod3()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.GetCellContents(null);
        }

        [TestMethod]
        public void SpreadSheetTestMethod4()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }

        [TestMethod]
        public void SpreadSheetTestMethod5()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }

        [TestMethod]
        public void SpreadSheetTestMethod6()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }


        [TestMethod]
        public void SpreadSheetTestMethod7()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }


        [TestMethod]
        public void SpreadSheetTestMethod8()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod9()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod10()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod11()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod12()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod13()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }


        [TestMethod]
        public void SpreadSheetTestMethod14()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod15()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }

    }
}
