using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadsheet;
using System.Collections.Generic;
using Formulas;


namespace SpreadsheetTestCases
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SpreadSheetTestMethod1()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }

        [TestMethod]
        public void SpreadSheetTestMethod2()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            double d = (double)s.GetCellContents("A1");
            String s = (string)s.GetCellContents("A2");
            Assert.AreEqual(d, 10);
            Assert.AreEqual(s, "");
        }


        [TestMethod]
        public void SpreadSheetTestMethod3()
        {
            Spreadsheet s = new Spreadsheet();
            s.GetCellContents(null);
        }

        [TestMethod]
        public void SpreadSheetTestMethod4()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }

        [TestMethod]
        public void SpreadSheetTestMethod5()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }

        [TestMethod]
        public void SpreadSheetTestMethod6()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }


        [TestMethod]
        public void SpreadSheetTestMethod7()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }


        [TestMethod]
        public void SpreadSheetTestMethod8()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod9()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod10()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod11()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod12()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod13()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }


        [TestMethod]
        public void SpreadSheetTestMethod14()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod15()
        {
            Spreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNoneemptyCells();
        }

    }
}
