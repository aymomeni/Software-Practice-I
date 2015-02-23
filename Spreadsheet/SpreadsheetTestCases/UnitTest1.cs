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

        // Couple of minor stress tests using formula and double methods

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

        // EMPTY SPREADSHEETS
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSP1()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.GetCellContents(null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSP2()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.GetCellContents("AA");
        }

        [TestMethod()]
        public void TestSP3()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            Assert.AreEqual("", s.GetCellContents("A2"));
        }

        // SETTING CELL TO A DOUBLE
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSP4()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents(null, 1.5);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSP5()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1A", 1.5);
        }

        [TestMethod()]
        public void TestSP6()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("Z7", 1.5);
            Assert.AreEqual(1.5, (double)s.GetCellContents("Z7"), 1e-9);
        }

        // SETTING CELL TO A STRING
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSP7()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A8", (string)null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSP8()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents(null, "hello");
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSP9()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("AZ", "hello");
        }

        [TestMethod()]
        public void TestSP10()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("Z7", "hello");
            Assert.AreEqual("hello", s.GetCellContents("Z7"));
        }

        // SETTING CELL TO A FORMULA
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestSP11()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A8", (Formula)null);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSP12()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents(null, new Formula("2"));
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestSP13()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("AZ", new Formula("2"));
        }

        [TestMethod()]
        public void TestSP14()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("Z7", new Formula("3"));
            Formula f = (Formula)s.GetCellContents("Z7");
            Assert.AreEqual(3, f.Evaluate(x => 0));
        }

        // CIRCULAR FORMULA DETECTION
        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void TestSP15()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A2"));
            s.SetCellContents("A2", new Formula("A1"));
        }

        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void TestSP16()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A2+A3"));
            s.SetCellContents("A3", new Formula("A4+A5"));
            s.SetCellContents("A5", new Formula("A6+A7"));
            s.SetCellContents("A7", new Formula("A1+A1"));
        }

        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void TestSP17()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            try
            {
                s.SetCellContents("A1", new Formula("A2+A3"));
                s.SetCellContents("A2", 15);
                s.SetCellContents("A3", 30);
                s.SetCellContents("A2", new Formula("A3*A1"));
            }
            catch (CircularException e)
            {
                Assert.AreEqual(15, (double)s.GetCellContents("A2"), 1e-9);
                throw e;
            }
        }

        // NONEMPTY CELLS
        [TestMethod()]
        public void TestSP18()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        [TestMethod()]
        public void TestSP19()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", "");
            Assert.IsFalse(s.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        [TestMethod()]
        public void TestSP20()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", "hello");
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void TestSP21()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", 52.25);
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void TestSP22()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", new Formula("3.5"));
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void TestSP23()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 17.2);
            s.SetCellContents("C1", "hello");
            s.SetCellContents("B1", new Formula("3.5"));
            Assert.IsTrue(new HashSet<string>(s.GetNamesOfAllNonemptyCells()).SetEquals(new HashSet<string>() { "A1", "B1", "C1" }));
        }

        // RETURN VALUE OF SET CELL CONTENTS
        [TestMethod()]
        public void TestSP24()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("B1", "hello");
            s.SetCellContents("C1", new Formula("5"));
            Assert.IsTrue(s.SetCellContents("A1", 17.2).SetEquals(new HashSet<string>() { "A1" }));
        }

        [TestMethod()]
        public void TestSP25()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 17.2);
            s.SetCellContents("C1", new Formula("5"));
            Assert.IsTrue(s.SetCellContents("B1", "hello").SetEquals(new HashSet<string>() { "B1" }));
        }

        [TestMethod()]
        public void TestSP26()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", 17.2);
            s.SetCellContents("B1", "hello");
            Assert.IsTrue(s.SetCellContents("C1", new Formula("5")).SetEquals(new HashSet<string>() { "C1" }));
        }

        [TestMethod()]
        public void TestSP27()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A2+A3"));
            s.SetCellContents("A2", 6);
            s.SetCellContents("A3", new Formula("A2+A4"));
            s.SetCellContents("A4", new Formula("A2+A5"));
            Assert.IsTrue(s.SetCellContents("A5", 82.5).SetEquals(new HashSet<string>() { "A5", "A4", "A3", "A1" }));
        }

        // CHANGING CELLS
        [TestMethod()]
        public void TestSP28()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A2+A3"));
            s.SetCellContents("A1", 2.5);
            Assert.AreEqual(2.5, (double)s.GetCellContents("A1"), 1e-9);
        }

        [TestMethod()]
        public void TestSP29()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("A2+A3"));
            s.SetCellContents("A1", "Hello");
            Assert.AreEqual("Hello", (string)s.GetCellContents("A1"));
        }

        [TestMethod()]
        public void TestSP30()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", "Hello");
            s.SetCellContents("A1", new Formula("23"));
            Assert.AreEqual(23, ((Formula)s.GetCellContents("A1")).Evaluate(x => 0));
        }

        // STRESS TESTS
        [TestMethod()]
        public void TestSP31()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            s.SetCellContents("A1", new Formula("B1+B2"));
            s.SetCellContents("B1", new Formula("C1-C2"));
            s.SetCellContents("B2", new Formula("C3*C4"));
            s.SetCellContents("C1", new Formula("D1*D2"));
            s.SetCellContents("C2", new Formula("D3*D4"));
            s.SetCellContents("C3", new Formula("D5*D6"));
            s.SetCellContents("C4", new Formula("D7*D8"));
            s.SetCellContents("D1", new Formula("E1"));
            s.SetCellContents("D2", new Formula("E1"));
            s.SetCellContents("D3", new Formula("E1"));
            s.SetCellContents("D4", new Formula("E1"));
            s.SetCellContents("D5", new Formula("E1"));
            s.SetCellContents("D6", new Formula("E1"));
            s.SetCellContents("D7", new Formula("E1"));
            s.SetCellContents("D8", new Formula("E1"));
            ISet<String> cells = s.SetCellContents("E1", 0);
            Assert.IsTrue(new HashSet<string>() { "A1", "B1", "B2", "C1", "C2", "C3", "C4", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "E1" }.SetEquals(cells));
        }
        [TestMethod()]
        public void TestSP32()
        {
            TestSP31();
        }
        [TestMethod()]
        public void TestSP33()
        {
            TestSP31();
        }
        [TestMethod()]
        public void TestSP34()
        {
            TestSP31();
        }

        [TestMethod()]
        public void Test35()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            ISet<String> cells = new HashSet<string>();
            for (int i = 1; i < 200; i++)
            {
                cells.Add("A" + i);
                Assert.IsTrue(cells.SetEquals(s.SetCellContents("A" + i, new Formula("A" + (i + 1)))));
            }
        }
        [TestMethod()]
        public void TestSP36()
        {
            Test35();
        }
        [TestMethod()]
        public void Test37()
        {
            Test35();
        }
        [TestMethod()]
        public void Test38()
        {
            Test35();
        }
        [TestMethod()]
        public void TestSP39()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            for (int i = 1; i < 200; i++)
            {
                s.SetCellContents("A" + i, new Formula("A" + (i + 1)));
            }
            try
            {
                s.SetCellContents("A150", new Formula("A50"));
                Assert.Fail();
            }
            catch (CircularException)
            {
            }
        }
        [TestMethod()]
        public void TestSP40()
        {
            TestSP39();
        }
        [TestMethod()]
        public void Test41()
        {
            TestSP39();
        }
        [TestMethod()]
        public void Test42()
        {
            TestSP39();
        }

        [TestMethod()]
        public void Test43()
        {
            AbstractSpreadsheet s = new Spreadsheet();
            for (int i = 0; i < 500; i++)
            {
                s.SetCellContents("A1" + i, new Formula("A1" + (i + 1)));
            }

            ISet<string> sss = s.SetCellContents("A1499", 25.0);
            Assert.AreEqual(500, sss.Count);
            for (int i = 0; i < 500; i++)
            {
                Assert.IsTrue(sss.Contains("A1" + i));
            }

            sss = s.SetCellContents("A1249", 25.0);
            Assert.AreEqual(250, sss.Count);
            for (int i = 0; i < 250; i++)
            {
                Assert.IsTrue(sss.Contains("A1" + i));
            }


        }

        [TestMethod()]
        public void TestSP44()
        {
            Test43();
        }
        [TestMethod()]
        public void TestSP45()
        {
            Test43();
        }
        [TestMethod()]
        public void TestSP46()
        {
            Test43();
        }

        [TestMethod()]
        public void TestSP47()
        {
            RunRandomizedTest(47, 2519);
        }
        [TestMethod()]
        public void TestSP48()
        {
            RunRandomizedTest(48, 2521);
        }
        [TestMethod()]
        public void TestSP49()
        {
            RunRandomizedTest(49, 2526);
        }
        [TestMethod()]
        public void TestSP50()
        {
            RunRandomizedTest(50, 2521);
        }

        public void RunRandomizedTest(int seed, int size)
        {
            AbstractSpreadsheet s = new Spreadsheet();
            Random rand = new Random(seed);
            for (int i = 0; i < 10000; i++)
            {
                try
                {
                    switch (rand.Next(3))
                    {
                        case 0:
                            s.SetCellContents(randomName(rand), 3.14);
                            break;
                        case 1:
                            s.SetCellContents(randomName(rand), "hello");
                            break;
                        case 2:
                            s.SetCellContents(randomName(rand), randomFormula(rand));
                            break;
                    }
                }
                catch (CircularException)
                {
                }
            }
            ISet<string> set = new HashSet<string>(s.GetNamesOfAllNonemptyCells());
            Assert.AreEqual(size, set.Count);
        }

        private String randomName(Random rand)
        {
            return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Substring(rand.Next(26), 1) + (rand.Next(99) + 1);
        }

        private String randomFormula(Random rand)
        {
            String f = randomName(rand);
            for (int i = 0; i < 10; i++)
            {
                switch (rand.Next(4))
                {
                    case 0:
                        f += "+";
                        break;
                    case 1:
                        f += "-";
                        break;
                    case 2:
                        f += "*";
                        break;
                    case 3:
                        f += "/";
                        break;
                }
                switch (rand.Next(2))
                {
                    case 0:
                        f += 7.2;
                        break;
                    case 1:
                        f += randomName(rand);
                        break;
                }
            }
            return f;
        }
    }
}
