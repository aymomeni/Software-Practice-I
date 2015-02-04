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
            get { return size; }
        }

        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        public bool HasDependents(string s)
        {
            // check if elements exist in dependents
            if (Dependents.Count == 0)
            {
                return false;
            }
            // if elements exist we want to see if dependent s
            if (Dependents.ContainsKey(s))
            {
                HashSet<string> tempDependents = new HashSet<string>();
                if (Dependents.TryGetValue(s, out tempDependents))
                {
                    return (tempDependents.Count > 0); // return the number of elements contained in s if there exists a dependees of "s"
                }
            }
            return false;
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        public bool HasDependees(string s)
        {
            // check if elements exist in dependees hashset
            if (Dependees.Count == 0)
            {
                return false;
            }
            // if elements exist we want to see if dependee has any dependents s
            if (Dependees.ContainsKey(s))
            {
                HashSet<string> tempDependees = new HashSet<string>();
                if (Dependees.TryGetValue(s, out tempDependees))
                {
                    return (tempDependees.Count > 0); // return true if hashset value of "s", which is a dependent, contains any values
                }
            }
            return false; // return false if no dependees exist for "s"
        }

        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            // in case we have no elements to return, an empty hashset is returned
            HashSet<string> empty = new HashSet<string>();

            //checking if key s exists in dependents
            if (Dependents.ContainsKey(s))
            {
                // returning a hashset that is part of collections and IEnumerable
                return new HashSet<string>(Dependents[s]);
            }

            return empty;
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            // in case we have no elements to return, an empty hashset is returned
            HashSet<string> empty = new HashSet<string>();
            //checking if key s exists in dependents
            if (Dependees.ContainsKey(s))
            {
                // returning a hashset that is part of collections and IEnumerable
                return new HashSet<string>(Dependees[s]);
            }

            return empty;
        }

        /// <summary>
        /// Adds the dependency (s,t) to this DependencyGraph
        /// This has no effect if (s,t) already belongs to this DependencyGraph
        /// </summary>
        public void AddDependency(string s, string t)
        {
            // both dependee dictionary and dependent dictionary have to add (s,t) if they don't exist yet.

            // check if s is contained in dependent dictionary      
            if (!Dependents.ContainsKey(s))
            {
                // if s is not contained insert s->t pair
                HashSet<string> temp = new HashSet<string>();
                temp.Add(t);
                Dependents.Add(s, temp); // insert s->t into dependent dictionary 
                size++;
            }
            else if (Dependents.ContainsKey(s))
            {
                // if s is contained we need to check if t exists
                HashSet<string> tempDependents = new HashSet<string>();
                if (Dependents.TryGetValue(s, out tempDependents))
                {
                    if (!tempDependents.Contains(t)) // check if t is contained in key s
                    {
                        tempDependents.Add(t);
                        Dependents.Remove(s); // first remove the previous value pair since duplicates are not allowed
                        Dependents.Add(s, tempDependents); // insert s->t into dependent dictionary
                        size++;
                    }
                }
            }

            // check if t is contained in dependee dictionary
            if (!Dependees.ContainsKey(t))
            {
                // if t is not contained insert s->t pair
                HashSet<string> temp = new HashSet<string>();
                temp.Add(s);
                Dependees.Add(t, temp);

            }
            // check if dependees already has key t 
            else if (Dependees.ContainsKey(t))
            {
                HashSet<String> tempDependee = new HashSet<string>();
                if (Dependees.TryGetValue(t, out tempDependee))
                {
                    if (!tempDependee.Contains(s))
                    {
                        tempDependee.Add(s);
                        Dependees.Remove(t); // first remove the privious value pair since duplicates are not allowed
                        Dependees.Add(t, tempDependee); // insert the new value pair and maintain the old dependee hashset              
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Removes the dependency (s,t) from this DependencyGraph
        /// Does nothing if (s,t) doesn't belong to this DependencyGraph
        /// </summary>
        public void RemoveDependency(string s, string t)
        {
            // find out if s exists in dependendents
            if (Dependents.ContainsKey(s))
            {
                // if yes we need to find t, its dependent and remove it and decrement the size
                HashSet<String> tempDependents;
                if (Dependents.TryGetValue(s, out tempDependents))
                {
                    if (tempDependents.Contains(t))
                    {
                        tempDependents.Remove(t);
                        size--;
                    }

                    // in case our key location holds an empty value hashset
                    if (tempDependents.Count == 0)
                    {
                        Dependents.Remove(s);
                    }
                }
            }

            // check if the dependent t exists if yes we want to remove s from the hashset of dependees
            if (Dependees.ContainsKey(t))
            {
                HashSet<String> tempDependee;
                if (Dependees.TryGetValue(t, out tempDependee))
                {
                    if (tempDependee.Contains(s))
                    {
                        tempDependee.Remove(s);
                        //orderedPairSize++;
                    }

                    // in clase our key location holds an empty value hashset
                    if (tempDependee.Count == 0)
                    {
                        Dependees.Remove(t);
                    }
                }
            }
            return;
        }

        /// <summary>
        /// Removes all existing dependencies of the form (s,r).  Then, for each
        /// t in newDependents, adds the dependency (s,t).
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            // using the classes method GetDependents and iterating
            // throough the IEnumerable that it returns to delete all of the dependencies
            foreach (String r in this.GetDependents(s))
            {
                RemoveDependency(s, r);
            }
            // we then go through the IEnumerable newDependents and add
            // each new dependent to our key s (dependee)
            foreach (String tempS in newDependents)
            {
                AddDependency(s, tempS);
            }
        }

        /// <summary>
        /// Removes all existing dependencies of the form (r,s).  Then, for each 
        /// t in newDependees, adds the dependency (t,s).
        /// </summary>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            // using the classes method GetDependees and iterating
            // through the IEnumerable that it returns to delete all of the dependees from r
            foreach (String r in this.GetDependees(s))
            {
                RemoveDependency(r, s);
            }
            // we then go through the IEnumerable newDependents and add
            // each new dependeee using our addDependency method
            foreach (String tempS in newDependees)
            {
                AddDependency(tempS, s);
            }
        }
    }
}
