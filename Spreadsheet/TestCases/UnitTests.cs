/*
 * Ali Momeni - CS 3500 - Assignment 1 - January 25, 2015
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;

namespace TestCases
{
    /// <summary>
    /// These test cases are in no sense comprehensive!  They are intended primarily to show you
    /// how to create your own, which we strong recommend that you do!  To run them, pull down
    /// the Test menu and do Run > All Tests.
    /// </summary>
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct1()
        {
            Formula f = new Formula("x");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct2()
        {
            Formula f = new Formula("2++3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct3()
        {
            Formula f = new Formula("2 3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct4()
        {
            Formula f = new Formula(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct5()
        {
            Formula f = new Formula(")");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct6()
        {
            Formula f = new Formula("(3))");
        }


        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct7()
        {
            Formula f = new Formula("(3)+");
        }

        [TestMethod]
        public void Evaluate1()
        {
            Formula f = new Formula("2+3");
            Assert.AreEqual(f.Evaluate(s => 0), 5.0, 1e-6);
        }

        [TestMethod]
        public void Evaluate2()
        {
            Formula f = new Formula("x5");
            Assert.AreEqual(f.Evaluate(s => 22.5), 22.5, 1e-6);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Evaluate3()
        {
            Formula f = new Formula("x5 + y6");
            f.Evaluate(s => { throw new ArgumentException(); });
        }

        [TestMethod]
        public void Evaluate4()
        {
            Formula f = new Formula("2/4");
            Assert.AreEqual(f.Evaluate(s => 0), .5);
        }

        [TestMethod]
        public void Evaluate5()
        {
            Formula f = new Formula("2/4+2");
            Assert.AreEqual(f.Evaluate(s => 0), 2.5);
        }

        [TestMethod]
        public void Evaluate6()
        {
            Formula f = new Formula("(8/4+2)+3");
            Assert.AreEqual(f.Evaluate(s => 0), 7);
        }

        [TestMethod]
        public void Evaluate7()
        {
            Formula f = new Formula("((8/4+2)+3)*100");
            Assert.AreEqual(f.Evaluate(s => 0), 700);
        }

        [TestMethod]
        public void Evaluate8()
        {
            Formula f = new Formula("(((8/4+2)+3)*100/7)");
            Assert.AreEqual(f.Evaluate(s => 0), 100);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Evaluate9()
        {
            Formula f = new Formula("6/0");
            Assert.AreEqual(f.Evaluate(s => 0), 7);
        }


        [TestMethod]
        public void Evaluate10()
        {
            Formula f = new Formula("(((8/4+2)+3)*2)+8");
            Assert.AreEqual(f.Evaluate(s => 0), 22);
        }

        [TestMethod]
        public void Evaluate11()
        {
            Formula f = new Formula("(2)+8");
            Assert.AreEqual(f.Evaluate(s => 0), 10);
        }

        [TestMethod]
        public void Evaluate12()
        {
            Formula f = new Formula("((2+8)+2)*8");
            Assert.AreEqual(f.Evaluate(s => 0), 96);
        }
    }
}
