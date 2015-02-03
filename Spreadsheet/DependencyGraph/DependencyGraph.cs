// Skeleton implementation written by Joe Zachary for CS 3500, January 2015.
// Version 1.1 (1/28/15 7:00 p.m.): Changed name of namespace
/*
 * Ali Momeni - CS 3500 - Assignment 3 - February 1, 2015
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dependencies
{
    /// <summary>
    /// A DependencyGraph can be modeled as a set of dependencies, where a dependency is an ordered 
    /// pair of strings.  Two dependencies (s1,t1) and (s2,t2) are considered equal if and only if 
    /// s1 equals s2 and t1 equals t2.  (Recall that sets never contain duplicates.  If an attempt
    /// is made to add an element to a set, and the element is already in the set, the set remains unchanged.)
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that the dependency (s,t) is in DG 
    ///    is called the dependents of s, which we will denote as dependents(s).
    ///        
    ///    (2) If s is a string, the set of all strings t such that the dependency (t,s) is in DG 
    ///    is called the dependees of s, which we will denote as dependees(s).
    ///    
    /// The notations dependents(s) and dependees(s) are used in the specification of the methods of the class.
    ///
    /// For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    ///     dependents("a") = {"b", "c"}
    ///     dependents("b") = {"d"}
    ///     dependents("c") = {}k
    ///     dependents("d") = {"d"}
    ///     dependees("a") = {}
    ///     dependees("b") = {"a"}
    ///     dependees("c") = {"a"}
    ///     dependees("d") = {"b", "d"}
    ///
    /// IMPLEMENTATION NOTE:  The simplest way to describe a DependencyGraph is as a set of dependencies.
    /// This is neither the simplest nor the most efficient way to implement a DependencyGraph, though.  Choose
    /// a representation that is both easy to work with and acceptably efficient.  Some of the test cases
    /// with which you will be graded will create massive DependencyGraphs.
    /// </summary>
    public class DependencyGraph
    {
        // Creating two dictionaries that hold the relationships between dependents and dependees
        // dependents holds as values all of the dependents of the key string and dependees does the exact opposite

        private Dictionary<String, HashSet<String>> Dependents; // IMPORTANT: dependents' key is the dependee and the value is a Hashset of dependents for that key      
        private Dictionary<String, HashSet<String>> Dependees; // IMPORTANT: dependeees' key is the dependent and the value is a Hashset of dependees for that key
        
        // The size variable is dedicated to keep track of all the dependencies
        private int size;

        /// <summary>
        /// Creates a DependencyGraph containing no dependencies.
        /// </summary>
        public DependencyGraph()
        {
            // initiating the the dictionaries, and size for use
            Dependents = new Dictionary<string, HashSet<string>>();
            Dependees = new Dictionary<string, HashSet<string>>();
            size = 0;
        }

        /// <summary>
        /// The number of dependencies in the DependencyGraph.
        /// </summary>
        public int Size
        {
            get { return this.size; }
        }

        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            // checking if the key s exists in the dictionary
            return Dependents.ContainsKey(s);
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            // checking if the key s exists in dictionary dependees
            return Dependees.ContainsKey(s);
        }

        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            HashSet<String> dependents = new HashSet<String>();

            if (Dependents.ContainsKey(s))
            {
                if (Dependents.TryGetValue(s, out dependents))
                {
                    return dependents; 
                }
            }
            return dependents;
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            HashSet<String> dependees = new HashSet<String>();

            if (Dependents.ContainsKey(s))
            {
                if (Dependents.TryGetValue(s, out dependees))
                {
                    return dependees;
                }
            }
            return dependees;
        }

        /// <summary>
        /// Adds the dependency (s,t) to this DependencyGraph
        /// This has no effect if (s,t) already belongs to this DependencyGraph
        /// </summary>
        public void AddDependency(string s, string t)
        {
        }

        /// <summary>
        /// Removes the dependency (s,t) from this DependencyGraph
        /// Does nothing if (s,t) doesn't belong to this DependencyGraph
        /// </summary>
        public void RemoveDependency(string s, string t)
        {
        }

        /// <summary>
        /// Removes all existing dependencies of the form (s,r).  Then, for each
        /// t in newDependents, adds the dependency (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
        }

        /// <summary>
        /// Removes all existing dependencies of the form (r,s).  Then, for each 
        /// t in newDependees, adds the dependency (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
        }
    }
}
