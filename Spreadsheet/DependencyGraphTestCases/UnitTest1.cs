/*
 * Ali Momeni - CS 3500 - Assignment 3 - February 1, 2015
 */
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using Dependencies;
using System.Collections.Generic;

namespace DependencyGraphTestCases
{
    [TestClass]
    public class UnitTest1
    {
        // Testing simple functionalities of the Dependency Graph

        /// <summary>
        /// Testing without actual dependencies
        ///</summary>
        [TestMethod()]
        public void SimpleTest1()
        {
            DependencyGraph dg = new DependencyGraph();
            Assert.AreEqual(0, dg.Size);
        }

        /// <summary>
        /// Testing without actual dependencies
        ///</summary>
        [TestMethod()]
        public void SimpleTest2()
        {
            DependencyGraph dg = new DependencyGraph();
            Assert.AreEqual(false, dg.HasDependees("c"));
        }

        /// <summary>
        /// Testing without actual dependencies
        ///</summary>
        [TestMethod()]
        public void SimpleTest3()
        {
            DependencyGraph dg = new DependencyGraph();
            Assert.AreEqual(false, dg.HasDependents("m"));
        }

        /// <summary>
        /// Testing the enumerator method, with an empty dependency graph
        ///</summary>
        [TestMethod()]
        public void SimpleTest4()
        {
            DependencyGraph dg = new DependencyGraph();
            Assert.IsFalse(dg.GetDependees("a").GetEnumerator().MoveNext());
        }

        /// <summary>
        /// Testing the enumerator method, with an empty dependency graph
        ///</summary>
        [TestMethod()]
        public void SimpleTest5()
        {
            DependencyGraph dg = new DependencyGraph();
            Assert.IsFalse(dg.GetDependents("a").GetEnumerator().MoveNext());
        }

        /// <summary>
        /// Testing the remove method
        ///</summary>
        [TestMethod()]
        public void SimpleTest6()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.RemoveDependency("m", "b");
            Assert.AreEqual(0, dg.Size);
        }

        /// <summary>
        ///Replacing elements elements with an empty hashset
        ///</summary>
        [TestMethod()]
        public void SimpleTest7()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.ReplaceDependents("a", new HashSet<string>());
            Assert.AreEqual(0, dg.Size);
        }

        /// <summary>
        /// Pressing for HasDependees false return
        ///</summary>
        [TestMethod()]
        public void SimpleTest8()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("m", "b");
            dg.AddDependency("m", "c");
            Assert.IsFalse(dg.HasDependees("m"));
        }


        /// <summary>
        /// Adding 3 dependencies to a1
        ///</summary>
        [TestMethod()]
        public void AddTest1()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a1", "a2");
            dg.AddDependency("a1", "a3");
            dg.AddDependency("a1", "a4");
            Assert.AreEqual(3, dg.Size);
        }

        /// <summary>
        /// adding same dependency multiple times
        ///</summary>
        [TestMethod()]
        public void AddTest2()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "a1");
            dg.AddDependency("a", "a1");
            dg.AddDependency("a", "a1");
            dg.AddDependency("a", "a1");
            Assert.AreEqual(1, dg.Size);
        }

        /// <summary>
        /// adding and checking for correct dependee dependent relationships
        ///</summary>
        [TestMethod()]
        public void AddTest3()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "c");
            dg.AddDependency("d", "c");
            dg.AddDependency("g", "c");

            Assert.AreEqual(true, dg.HasDependents("a"));
            Assert.AreEqual(true, dg.HasDependees("b"));
            Assert.AreEqual(false, dg.HasDependents("b"));
            Assert.AreEqual(true, dg.HasDependees("c"));
            Assert.AreEqual(true, dg.HasDependents("d"));
            Assert.AreEqual(false, dg.HasDependents("c"));
            Assert.AreEqual(4, dg.Size);
        }

        /// <summary>
        /// adding and checking for correct dependee dependent relationships
        ///</summary>
        [TestMethod()]
        public void AddTest4()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a", "b");
            dg.AddDependency("a", "c");
            dg.AddDependency("a", "c");
            dg.AddDependency("d", "c");

            Assert.AreEqual(true, dg.HasDependents("a"));
            Assert.AreEqual(true, dg.HasDependees("b"));
            Assert.AreEqual(false, dg.HasDependents("b"));
            Assert.AreEqual(true, dg.HasDependees("c"));
            Assert.AreEqual(true, dg.HasDependents("d"));
            Assert.AreEqual(false, dg.HasDependents("c"));
            Assert.AreEqual(3, dg.Size);

            dg.RemoveDependency("a", "b");
            dg.RemoveDependency("a", "c");
            dg.RemoveDependency("d", "c");
            Assert.AreEqual(0, dg.Size);

        }


        /// <summary>
        /// testing getDepnendents
        ///</summary>
        [TestMethod()]
        public void AddTest5()
        {

            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a1", "a2");
            dg.AddDependency("a1", "a3");
            dg.AddDependency("a1", "a4");
            dg.AddDependency("a1", "a5");
            dg.AddDependency("a2", "a6");
            dg.AddDependency("a2", "a7");
            dg.AddDependency("a2", "a8");
            dg.AddDependency("a2", "a9");

            HashSet<String> dependents = new HashSet<string>(dg.GetDependents("a1"));

            Assert.IsTrue(dependents.Count == 4);
            Assert.IsTrue(dependents.Contains("a2"));
            Assert.IsTrue(dependents.Contains("a3"));
            Assert.IsTrue(dependents.Contains("a4"));
            Assert.IsTrue(dependents.Contains("a5"));


            HashSet<String> dependents2 = new HashSet<string>(dg.GetDependents("a2"));

            Assert.IsTrue(dependents2.Count == 4);
            Assert.IsTrue(dependents2.Contains("a6"));
            Assert.IsTrue(dependents2.Contains("a7"));
            Assert.IsTrue(dependents2.Contains("a8"));
            Assert.IsTrue(dependents2.Contains("a9"));

        }


        /// <summary>
        /// testing getDepnendees
        ///</summary>
        [TestMethod()]
        public void AddTest6()
        {

            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a2", "a1");
            dg.AddDependency("a3", "a1");
            dg.AddDependency("a4", "a1");
            dg.AddDependency("a5", "a1");
            dg.AddDependency("a6", "a1");


            HashSet<String> dependees = new HashSet<string>(dg.GetDependees("a1"));

            Assert.IsTrue(dependees.Count == 5);
            Assert.IsTrue(dependees.Contains("a2"));
            Assert.IsTrue(dependees.Contains("a3"));
            Assert.IsTrue(dependees.Contains("a4"));
            Assert.IsTrue(dependees.Contains("a5"));
            Assert.IsTrue(dependees.Contains("a6"));

        }

        /// <summary>
        /// testing replaceDependents method
        ///</summary>
        [TestMethod()]
        public void replaceDependents()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a1", "a2");
            dg.AddDependency("a1", "a2");
            dg.AddDependency("a1", "a3");
            dg.AddDependency("a1", "a4");
            dg.AddDependency("a1", "a5");

            dg.ReplaceDependents("a1", new HashSet<string>() { "a7", "a8", "a9", "a10", "a11", "a12" });
            HashSet<String> testDependentsHashTable = new HashSet<string>(dg.GetDependents("a1"));
            Assert.IsTrue(testDependentsHashTable.Contains("a8"));
            Assert.IsTrue(testDependentsHashTable.Contains("a9"));
            Assert.IsTrue(testDependentsHashTable.Contains("a10"));
            Assert.IsTrue(testDependentsHashTable.Contains("a11"));
            Assert.IsTrue(testDependentsHashTable.Contains("a12"));
            Assert.IsTrue(testDependentsHashTable.Contains("a7"));

        }

        /// <summary>
        /// testing replaceDependees method
        ///</summary>
        [TestMethod()]
        public void replaceDependees()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a2", "a1");
            dg.AddDependency("a3", "a1");
            dg.AddDependency("a4", "a1");
            dg.AddDependency("a5", "a1");
            dg.AddDependency("a6", "a1");

            dg.ReplaceDependees("a1", new HashSet<string>() { "a7", "a8", "a9", "a10", "a11", "a12" });

            HashSet<String> testDependentsHashTable = new HashSet<string>(dg.GetDependees("a1"));
            Assert.IsTrue(testDependentsHashTable.Contains("a8"));
            Assert.IsTrue(testDependentsHashTable.Contains("a9"));
            Assert.IsTrue(testDependentsHashTable.Contains("a10"));
            Assert.IsTrue(testDependentsHashTable.Contains("a11"));
            Assert.IsTrue(testDependentsHashTable.Contains("a12"));
            Assert.IsTrue(testDependentsHashTable.Contains("a7"));
        }


        /// <summary>
        /// testing removal more thouroughly
        ///</summary>
        [TestMethod()]
        public void RemoveTest()
        {
            DependencyGraph dg = new DependencyGraph();
            dg.AddDependency("a2", "a1");
            dg.AddDependency("a3", "a1");
            dg.AddDependency("a4", "a1");
            dg.AddDependency("a5", "a1");
            dg.AddDependency("a6", "a1");

            dg.RemoveDependency("a2", "a1");
            dg.RemoveDependency("a3", "a1");
            dg.RemoveDependency("a4", "a1");
            dg.RemoveDependency("a5", "a1");
            dg.RemoveDependency("a6", "a1");

            Assert.AreEqual(0, dg.Size);

        }
    }
}
