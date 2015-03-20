/*
 * Ali Momeni - CS 3500 - Assignment 1 - January 25, 2015
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Dependencies;
using Formulas;

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
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct8()
        {
            Formula f = new Formula("-3");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Construct9()
        {
            Formula f = new Formula("2 5 + 3");
        }

        [TestMethod]
        public void Construct10()
        {
            Formula f = new Formula("_");
        }

        [TestMethod]
        public void Construct11()
        {
            Formula f = new Formula("_x6872xcv");
        }

        [TestMethod]
        public void Construct12()
        {
            Formula f = new Formula("_x6872xcv + 2");
            Assert.AreEqual(f.Evaluate(s => 2), 4);
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


        [TestMethod]
        public void Evaluate13()
        {
            Formula f = new Formula("((x2)+2)*8");
            Assert.AreEqual(f.Evaluate(s => 2), 32);
        }


        [TestMethod()]
        public void Evaluate14()
        {
            Formula f = new Formula("5 - _x666234");
            Assert.AreEqual(1, f.Evaluate(s => 4), 1e-6);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Evaluate15()
        {
            Formula f = new Formula("x5 + .");
        }


        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Evaluate16()
        {
            Formula f = new Formula("x5", s => s, s => false);
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Evaluate17()
        {
            Formula f = new Formula("_x_");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Evaluate18()
        {
            Formula f = new Formula("_x94562_");
        }

        [TestMethod]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Evaluate19()
        {
            Formula f = new Formula("x5", s => s + "_", s => false);
        }

        [TestMethod()]
        public void EvaluateGetVariables1()
        {
            Formula f = new Formula("A1-B2/A3*B2+C7+A1+B9-C9");
            List<string> temp = new List<string>(f.GetVariables());
            Assert.AreEqual(temp.Count, 6);
            HashSet<string> comparison = new HashSet<string>() { "A1", "B2", "A3", "C7", "B9", "C9" };

            Assert.IsTrue(comparison.SetEquals(temp));
        }

        [TestMethod()]
        public void EvaluateGetVariables2()
        {
            Formula f = new Formula("A1+A1+A1");
            List<string> temp = new List<string>(f.GetVariables());
            Assert.AreEqual(temp.Count, 1);
            HashSet<string> comparison = new HashSet<string>() { "A1"};
            Assert.IsTrue(comparison.SetEquals(temp));
        }

        [TestMethod()]
        public void EvaluateGetVariables3()
        {
            Formula f = new Formula("a1 + a2 + a3 + a4", s => "_" + s, s => true);
            List<string> temp = new List<string>(f.GetVariables());
            Assert.AreEqual(temp.Count, 4);
            HashSet<string> comparison = new HashSet<string>() { "_a1", "_a2", "_a3", "_a4"};
            Assert.IsTrue(comparison.SetEquals(temp));
        }


        // Added Grading Test cases for debugging
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////
        // Tests of syntax errors detected by the constructor
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test1()
        {
            Formula f = new Formula("        ");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test2()
        {
            Formula f = new Formula("((2 + 5))) + 8");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test3()
        {
            Formula f = new Formula("2+5*8)");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test4()
        {
            Formula f = new Formula("((3+5*7)");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test5()
        {
            Formula f = new Formula("+3");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test6()
        {
            Formula f = new Formula("-y2");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test7()
        {
            Formula f = new Formula("*7");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test8()
        {
            Formula f = new Formula("/z8");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test9()
        {
            Formula f = new Formula(")");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test10()
        {
            Formula f = new Formula("(*5)");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test11()
        {
            Formula f = new Formula("2 5");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test12()
        {
            Formula f = new Formula("x5 y7");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test13()
        {
            Formula f = new Formula("((((((((((2)))))))))");
        }

        [TestMethod()]
        public void Test14()
        {
            Formula f = new Formula("_x");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void Test15()
        {
            Formula f = new Formula("x5 + x6 + x7 + (x8) +");
        }

        // Simple tests that throw FormulaEvaluationExceptions
        [TestMethod()]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Test16()
        {
            Formula f = new Formula("2+X1");
            f.Evaluate(s => { throw new ArgumentException(); });
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Test17()
        {
            Formula f = new Formula("5/0");
            f.Evaluate(s => 0);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Test18()
        {
            Formula f = new Formula("(5 + X1) / (X1 - 3)");
            f.Evaluate(s => 3);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Test19()
        {
            Formula f = new Formula("x1 + x2");
            f.Evaluate(s => { if (s == "x1") return 0; else throw new ArgumentException(); });
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaEvaluationException))]
        public void Test20()
        {
            Formula f = new Formula("x1 + x2 * x3 + x4 * x5 * x6 + x7");
            f.Evaluate(s => { if (s == "x7") throw new ArgumentException(); else return 1; });
        }

        // Simple formulas
        [TestMethod()]
        public void Test21()
        {
            Formula f = new Formula("4.5e1");
            Assert.AreEqual(45, f.Evaluate(s => 0), 1e-6);
        }

        [TestMethod()]
        public void Test22()
        {
            Formula f = new Formula("a0");
            Assert.AreEqual(10, f.Evaluate(s => 10), 1e-6);
        }

        [TestMethod()]
        public void Test23()
        {
            Formula f = new Formula("5 + x6");
            Assert.AreEqual(9, f.Evaluate(s => 4), 1e-6);
        }

        [TestMethod()]
        public void Test24()
        {
            Formula f = new Formula("5 - x6");
            Assert.AreEqual(1, f.Evaluate(s => 4), 1e-6);
        }

        [TestMethod()]
        public void Test25()
        {
            Formula f = new Formula("5 * x6");
            Assert.AreEqual(20, f.Evaluate(s => 4), 1e-6);
        }

        [TestMethod()]
        public void Test26()
        {
            Formula f = new Formula("8 / x6");
            Assert.AreEqual(2, f.Evaluate(s => 4), 1e-6);
        }

        [TestMethod()]
        public void Test27()
        {
            Formula f = new Formula("(5 + 4) * 2");
            Assert.AreEqual(18, f.Evaluate(s => 0), 1e-6);
        }

        [TestMethod()]
        public void Test28()
        {
            Formula f = new Formula("1 + 2 + 3 * 4 + 5");
            Assert.AreEqual(20, f.Evaluate(s => 0), 1e-6);
        }

        [TestMethod()]
        public void Test29()
        {
            Formula f = new Formula("(1 + 2 + 3 * 4 + 5) * 2");
            Assert.AreEqual(40, f.Evaluate(s => 0), 1e-6);
        }

        [TestMethod()]
        public void Test30()
        {
            Formula f = new Formula("((((((((((((3))))))))))))");
            Assert.AreEqual(3, f.Evaluate(s => 0), 1e-6);
        }

        [TestMethod()]
        public void Test31()
        {
            Formula f = new Formula("((((((((((((x7))))))))))))");
            Assert.AreEqual(7, f.Evaluate(s => 7), 1e-6);
        }

        // Some more complicated formula evaluations
        [TestMethod()]
        public void Test32()
        {
            Formula f = new Formula("y1*3-8/2+4*(8-9*2)/14*x7");
            Assert.AreEqual(5.14285714285714, (double)f.Evaluate(s => (s == "x7") ? 1 : 4), 1e-9);
        }

        [TestMethod()]
        public void Test33()
        {
            Formula f = new Formula("x1+(x2+(x3+(x4+(x5+x6))))");
            Assert.AreEqual(6, (double)f.Evaluate(s => 1), 1e-9);
        }

        [TestMethod()]
        public void Test34()
        {
            Formula f = new Formula("((((x1+x2)+x3)+x4)+x5)+x6");
            Assert.AreEqual(12, (double)f.Evaluate(s => 2), 1e-9);
        }

        [TestMethod()]
        public void Test35()
        {
            Formula f = new Formula("a4-a4*a4/a4");
            Assert.AreEqual(0, (double)f.Evaluate(s => 3), 1e-9);
        }

        // Tests to make sure there can be more than one formula at a time
        [TestMethod()]
        public void Test36()
        {
            Formula f1 = new Formula("x5+3");
            Formula f2 = new Formula("x5-3");
            Assert.AreEqual(6, f1.Evaluate(s => 3), 1e-6);
            Assert.AreEqual(0, f2.Evaluate(s => 3), 1e-6);
        }

        [TestMethod()]
        public void Test37()
        {
            Formula f1 = new Formula("x5+3");
            Formula f2 = new Formula("x5-3");
            Assert.AreEqual(6, f1.Evaluate(s => 3), 1e-6);
            Assert.AreEqual(0, f2.Evaluate(s => 3), 1e-6);
        }

        [TestMethod()]
        public void Test38()
        {
            Formula f1 = new Formula("x5+3");
            Formula f2 = new Formula("x5-3");
            Assert.AreEqual(6, f1.Evaluate(s => 3), 1e-6);
            Assert.AreEqual(0, f2.Evaluate(s => 3), 1e-6);
        }

        [TestMethod()]
        public void Test39()
        {
            Formula f1 = new Formula("x5+3");
            Formula f2 = new Formula("x5-3");
            Assert.AreEqual(6, f1.Evaluate(s => 3), 1e-6);
            Assert.AreEqual(0, f2.Evaluate(s => 3), 1e-6);
        }

        [TestMethod()]
        public void Test40()
        {
            Formula f1 = new Formula("x5+3");
            Formula f2 = new Formula("x5-3");
            Assert.AreEqual(6, f1.Evaluate(s => 3), 1e-6);
            Assert.AreEqual(0, f2.Evaluate(s => 3), 1e-6);
        }

        // Stress test for constructor
        [TestMethod()]
        public void Test41()
        {
            Formula f = new Formula("(((((2+3*X1)/(7e-5+X2-X4))*X5+.0005e+92)-8.2)*3.14159) * ((x2+3.1)-.00000000008)");
        }

        // Stress test for constructor
        [TestMethod()]
        public void Test42()
        {
            Formula f = new Formula("(((((2+3*X1)/(7e-5+X2-X4))*X5+.0005e+92)-8.2)*3.14159) * ((x2+3.1)-.00000000008)");
        }

        // Stress test for constructor
        [TestMethod()]
        public void Test43()
        {
            Formula f = new Formula("(((((2+3*X1)/(7e-5+X2-X4))*X5+.0005e+92)-8.2)*3.14159) * ((x2+3.1)-.00000000008)");
        }

        // Stress test for constructor
        [TestMethod()]
        public void Test44()
        {
            Formula f = new Formula("(((((2+3*X1)/(7e-5+X2-X4))*X5+.0005e+92)-8.2)*3.14159) * ((x2+3.1)-.00000000008)");
        }

        // Stress test for constructor
        [TestMethod()]
        public void Test45()
        {
            Formula f = new Formula("(((((2+3*X1)/(7e-5+X2-X4))*X5+.0005e+92)-8.2)*3.14159) * ((x2+3.1)-.00000000008)");
        }

        //////////////////////////////// Test Cases for PS4  ///////////////////////////////////////////////////////////////////

        // ************************** TESTS ON EMPTY DGs ************************* //

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyTest1()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyTest2()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.IsFalse(t.HasDependees("x"));
            Assert.IsFalse(t.HasDependents("x"));
        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyTest3()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.IsFalse(t.GetDependees("x").GetEnumerator().MoveNext());
            Assert.IsFalse(t.GetDependents("x").GetEnumerator().MoveNext());
        }

        /// <summary>
        ///Removing from an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void EmptyTest5()
        {
            DependencyGraph t = new DependencyGraph();
            t.RemoveDependency("x", "y");
        }

        /// <summary>
        ///Replace on an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void EmptyTest6()
        {
            DependencyGraph t = new DependencyGraph();
            t.ReplaceDependents("x", new HashSet<string>());
            t.ReplaceDependees("y", new HashSet<string>());
        }


        // ************************ MORE TESTS ON EMPTY DGs *********************** //

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyTest7()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
            t.RemoveDependency("x", "y");
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyTest8()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.IsTrue(t.HasDependees("y"));
            Assert.IsTrue(t.HasDependents("x"));
            t.RemoveDependency("x", "y");
            Assert.IsFalse(t.HasDependees("y"));
            Assert.IsFalse(t.HasDependents("x"));
        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyTest9()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            IEnumerator<string> e1 = t.GetDependees("y").GetEnumerator();
            Assert.IsTrue(e1.MoveNext());
            Assert.AreEqual("x", e1.Current);
            IEnumerator<string> e2 = t.GetDependents("x").GetEnumerator();
            Assert.IsTrue(e2.MoveNext());
            Assert.AreEqual("y", e2.Current);
            t.RemoveDependency("x", "y");
            Assert.IsFalse(t.GetDependees("y").GetEnumerator().MoveNext());
            Assert.IsFalse(t.GetDependents("x").GetEnumerator().MoveNext());
        }

        /// <summary>
        ///Removing from an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void EmptyTest11()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(t.Size, 1);
            t.RemoveDependency("x", "y");
            t.RemoveDependency("x", "y");
        }

        /// <summary>
        ///Replace on an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void EmptyTest12()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(t.Size, 1);
            t.RemoveDependency("x", "y");
            t.ReplaceDependents("x", new HashSet<string>());
            t.ReplaceDependees("y", new HashSet<string>());
        }


        // ********************** Making Sure that Static Variables Weren't Used ****************** //
        ///<summary>
        ///It should be possibe to have more than one DG at a time.  This test is
        ///repeated because I want it to be worth more than 1 point.
        ///</summary>
        [TestMethod()]
        public void StaticTest1()
        {
            DependencyGraph t1 = new DependencyGraph();
            DependencyGraph t2 = new DependencyGraph();
            t1.AddDependency("x", "y");
            Assert.AreEqual(1, t1.Size);
            Assert.AreEqual(0, t2.Size);
        }

        [TestMethod()]
        public void StaticTest2()
        {
            StaticTest1();
        }

        [TestMethod()]
        public void StaticTest3()
        {
            StaticTest1();
        }

        [TestMethod()]
        public void StaticTest4()
        {
            StaticTest1();
        }

        [TestMethod()]
        public void StaticTest5()
        {
            StaticTest1();
        }

        /**************************** SIMPLE NON-EMPTY TESTS ****************************/

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest1()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest3()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            Assert.IsTrue(t.HasDependents("a"));
            Assert.IsFalse(t.HasDependees("a"));
            Assert.IsTrue(t.HasDependents("b"));
            Assert.IsTrue(t.HasDependees("b"));
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest4()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest5()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");

            IEnumerator<string> e = t.GetDependents("a").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "b") && (s2 == "c")) || ((s1 == "c") && (s2 == "b")));

            e = t.GetDependents("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("d", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependents("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependents("d").GetEnumerator();
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest6()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "b");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            t.AddDependency("c", "b");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest8()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "b");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            t.AddDependency("c", "b");
            Assert.IsTrue(t.HasDependents("a"));
            Assert.IsFalse(t.HasDependees("a"));
            Assert.IsTrue(t.HasDependents("b"));
            Assert.IsTrue(t.HasDependees("b"));
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest9()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "b");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            t.AddDependency("c", "b");

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest10()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "b");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            t.AddDependency("c", "b");

            IEnumerator<string> e = t.GetDependents("a").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "b") && (s2 == "c")) || ((s1 == "c") && (s2 == "b")));

            e = t.GetDependents("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("d", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependents("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependents("d").GetEnumerator();
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest11()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "d");
            t.AddDependency("c", "b");
            t.RemoveDependency("a", "d");
            t.AddDependency("e", "b");
            t.AddDependency("b", "d");
            t.RemoveDependency("e", "b");
            t.RemoveDependency("x", "y");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest13()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "d");
            t.AddDependency("c", "b");
            t.RemoveDependency("a", "d");
            t.AddDependency("e", "b");
            t.AddDependency("b", "d");
            t.RemoveDependency("e", "b");
            t.RemoveDependency("x", "y");
            Assert.IsTrue(t.HasDependents("a"));
            Assert.IsFalse(t.HasDependees("a"));
            Assert.IsTrue(t.HasDependents("b"));
            Assert.IsTrue(t.HasDependees("b"));
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest14()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "d");
            t.AddDependency("c", "b");
            t.RemoveDependency("a", "d");
            t.AddDependency("e", "b");
            t.AddDependency("b", "d");
            t.RemoveDependency("e", "b");
            t.RemoveDependency("x", "y");

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest15()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "d");
            t.AddDependency("c", "b");
            t.RemoveDependency("a", "d");
            t.AddDependency("e", "b");
            t.AddDependency("b", "d");
            t.RemoveDependency("e", "b");
            t.RemoveDependency("x", "y");

            IEnumerator<string> e = t.GetDependents("a").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "b") && (s2 == "c")) || ((s1 == "c") && (s2 == "b")));

            e = t.GetDependents("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("d", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependents("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependents("d").GetEnumerator();
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest16()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "b");
            t.AddDependency("a", "z");
            t.ReplaceDependents("b", new HashSet<string>());
            t.AddDependency("y", "b");
            t.ReplaceDependents("a", new HashSet<string>() { "c" });
            t.AddDependency("w", "d");
            t.ReplaceDependees("b", new HashSet<string>() { "a", "c" });
            t.ReplaceDependees("d", new HashSet<string>() { "b" });
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest18()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "b");
            t.AddDependency("a", "z");
            t.ReplaceDependents("b", new HashSet<string>());
            t.AddDependency("y", "b");
            t.ReplaceDependents("a", new HashSet<string>() { "c" });
            t.AddDependency("w", "d");
            t.ReplaceDependees("b", new HashSet<string>() { "a", "c" });
            t.ReplaceDependees("d", new HashSet<string>() { "b" });
            Assert.IsTrue(t.HasDependents("a"));
            Assert.IsFalse(t.HasDependees("a"));
            Assert.IsTrue(t.HasDependents("b"));
            Assert.IsTrue(t.HasDependees("b"));
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest19()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "b");
            t.AddDependency("a", "z");
            t.ReplaceDependents("b", new HashSet<string>());
            t.AddDependency("y", "b");
            t.ReplaceDependents("a", new HashSet<string>() { "c" });
            t.AddDependency("w", "d");
            t.ReplaceDependees("b", new HashSet<string>() { "a", "c" });
            t.ReplaceDependees("d", new HashSet<string>() { "b" });

            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));

            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void NonEmptyTest20()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "b");
            t.AddDependency("a", "z");
            t.ReplaceDependents("b", new HashSet<string>());
            t.AddDependency("y", "b");
            t.ReplaceDependents("a", new HashSet<string>() { "c" });
            t.AddDependency("w", "d");
            t.ReplaceDependees("b", new HashSet<string>() { "a", "c" });
            t.ReplaceDependees("d", new HashSet<string>() { "b" });

            IEnumerator<string> e = t.GetDependents("a").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "b") && (s2 == "c")) || ((s1 == "c") && (s2 == "b")));

            e = t.GetDependents("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("d", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependents("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());

            e = t.GetDependents("d").GetEnumerator();
            Assert.IsFalse(e.MoveNext());
        }


        // ************************** STRESS TESTS REPEATED MULTIPLE TIMES ******************************** //
        /// <summary>
        ///Using lots of data
        ///</summary>
        [TestMethod()]
        public void StressTest1()
        {
            // Dependency graph
            DependencyGraph t = new DependencyGraph();

            // A bunch of strings to use
            const int SIZE = 200;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                letters[i] = ("" + (char)('a' + i));
            }

            // The correct answers
            HashSet<string>[] dents = new HashSet<string>[SIZE];
            HashSet<string>[] dees = new HashSet<string>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                dents[i] = new HashSet<string>();
                dees[i] = new HashSet<string>();
            }

            // Add a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j++)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 4; j < SIZE; j += 4)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Add some back
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j += 2)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove some more
            for (int i = 0; i < SIZE; i += 2)
            {
                for (int j = i + 3; j < SIZE; j += 3)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Make sure everything is right
            for (int i = 0; i < SIZE; i++)
            {
                Assert.IsTrue(dents[i].SetEquals(new HashSet<string>(t.GetDependents(letters[i]))));
                Assert.IsTrue(dees[i].SetEquals(new HashSet<string>(t.GetDependees(letters[i]))));
            }
        }

        [TestMethod()]
        public void StressTest2()
        {
            StressTest1();
        }
        [TestMethod()]
        public void StressTest3()
        {
            StressTest1();
        }


        // ********************************** ANOTHER STESS TEST, REPEATED ******************** //
        /// <summary>
        ///Using lots of data with replacement
        ///</summary>
        [TestMethod()]
        public void StressTest8()
        {
            // Dependency graph
            DependencyGraph t = new DependencyGraph();

            // A bunch of strings to use
            const int SIZE = 400;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                letters[i] = ("" + (char)('a' + i));
            }

            // The correct answers
            HashSet<string>[] dents = new HashSet<string>[SIZE];
            HashSet<string>[] dees = new HashSet<string>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                dents[i] = new HashSet<string>();
                dees[i] = new HashSet<string>();
            }

            // Add a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j++)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 2; j < SIZE; j += 3)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Replace a bunch of dependents
            for (int i = 0; i < SIZE; i += 2)
            {
                HashSet<string> newDents = new HashSet<String>();
                for (int j = 0; j < SIZE; j += 5)
                {
                    newDents.Add(letters[j]);
                }
                t.ReplaceDependents(letters[i], newDents);

                foreach (string s in dents[i])
                {
                    dees[s[0] - 'a'].Remove(letters[i]);
                }

                foreach (string s in newDents)
                {
                    dees[s[0] - 'a'].Add(letters[i]);
                }

                dents[i] = newDents;
            }

            // Make sure everything is right
            for (int i = 0; i < SIZE; i++)
            {
                Assert.IsTrue(dents[i].SetEquals(new HashSet<string>(t.GetDependents(letters[i]))));
                Assert.IsTrue(dees[i].SetEquals(new HashSet<string>(t.GetDependees(letters[i]))));
            }
        }

        [TestMethod()]
        public void StressTest9()
        {
            StressTest8();
        }
        [TestMethod()]
        public void StressTest10()
        {
            StressTest8();
        }


        // ********************************** A THIRD STESS TEST, REPEATED ******************** //
        /// <summary>
        ///Using lots of data with replacement
        ///</summary>
        [TestMethod()]
        public void StressTest15()
        {
            // Dependency graph
            DependencyGraph t = new DependencyGraph();

            // A bunch of strings to use
            const int SIZE = 800;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                letters[i] = ("" + (char)('a' + i));
            }

            // The correct answers
            HashSet<string>[] dents = new HashSet<string>[SIZE];
            HashSet<string>[] dees = new HashSet<string>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                dents[i] = new HashSet<string>();
                dees[i] = new HashSet<string>();
            }

            // Add a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j++)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 2; j < SIZE; j += 3)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Replace a bunch of dependees
            for (int i = 0; i < SIZE; i += 2)
            {
                HashSet<string> newDees = new HashSet<String>();
                for (int j = 0; j < SIZE; j += 9)
                {
                    newDees.Add(letters[j]);
                }
                t.ReplaceDependees(letters[i], newDees);

                foreach (string s in dees[i])
                {
                    dents[s[0] - 'a'].Remove(letters[i]);
                }

                foreach (string s in newDees)
                {
                    dents[s[0] - 'a'].Add(letters[i]);
                }

                dees[i] = newDees;
            }

            // Make sure everything is right
            for (int i = 0; i < SIZE; i++)
            {
                Assert.IsTrue(dents[i].SetEquals(new HashSet<string>(t.GetDependents(letters[i]))));
                Assert.IsTrue(dees[i].SetEquals(new HashSet<string>(t.GetDependees(letters[i]))));
            }
        }

        [TestMethod()]
        public void StressTest16()
        {
            StressTest15();
        }
        [TestMethod()]
        public void StressTest17()
        {
            StressTest15();
        }
    }
}
