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
            s.SetCellContents("A01", 10);
        }

        [TestMethod]
        public void SpreadSheetTestMethod5()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }

        [TestMethod]
        public void SpreadSheetTestMethod6()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }


        [TestMethod]
        public void SpreadSheetTestMethod7()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }


        [TestMethod]
        public void SpreadSheetTestMethod8()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod9()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod10()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod11()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod12()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod13()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }


        [TestMethod]
        public void SpreadSheetTestMethod14()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }



        [TestMethod]
        public void SpreadSheetTestMethod15()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 10);
            s.GetNamesOfAllNonemptyCells();
        }

    }
}
