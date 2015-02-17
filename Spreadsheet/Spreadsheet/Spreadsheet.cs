using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulas; 
using Dependencies;
using System.Text.RegularExpressions;

/**
 * PS5 - Ali Momeni - February 14, 2015 
 * CS 3500 - Joe Zackary
 */
namespace SS
{

    /// <summary>
    /// Cell class defines a single unit of a Cell
    /// Cells have either content or a value (for the purpose of this assignment we will simplify to just content)
    /// 
    /// Contents: String, Double, Formula
    /// Value: String, Double, FormulaError
    /// </summary>
    public class Cell{

        // NameOfCell defines the name of the cell
        private String nameOfCell;
        // Content is an object that contains the content of the cell
        private object content;

        /// <summary>
        /// Cell constructor that uses the name of the cell and the content as parameters,
        /// to create an instance of a cell object
        /// </summary>
        /// <param name="NameOfCell"></param>
        /// <param name="Content"></param>
        public Cell(String NameOfCell, Object Content)
        {
            this.nameOfCell = NameOfCell;
            this.content = Content;
        }

        /// <summary>
        /// Returns the name of the cell
        /// </summary>
        /// <returns></returns>
        public string GetName()
        { return nameOfCell; }

        /// <summary>
        /// Returns the contents of the cell
        /// </summary>
        /// <returns></returns>
        public Object GetContent()
        { return content; }

        /// <summary>
        /// Sets the contents of the cell
        /// </summary>
        /// <returns></returns>
        public void SetCellContent(Object o)
        { content = o; }

    }



    /// <summary>
    /// An AbstractSpreadsheet object represents the state of a simple spreadsheet.  A 
    /// spreadsheet consists of an infinite number of named cells.
    /// 
    /// A string is a cell name if and only if it consists of one or more letters, 
    /// followed by a non-zero digit, followed by zero or more digits.
    /// 
    /// For example, "A15", "a15", "XY32", and "BC7" are cell names.  (Note that despite
    /// their similarity, "A15" and "a15" are different cell names.)  On the other hand, 
    /// "Z", "X07", and "hello" are not cell names."
    /// 
    /// A spreadsheet contains a cell corresponding to every possible cell name.  
    /// In addition to a name, each cell has a contents and a value.  The distinction is
    /// important.
    /// 
    /// The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)
    /// 
    /// In an empty spreadsheet, the contents of every cell is the empty string.
    ///  
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError.
    /// The value of a Formula, of course, can depend on the values of variables.  The value 
    /// of a Formula variable is the value of the spreadsheet cell it names (if that cell's 
    /// value is a double) or is undefined (otherwise).  If a Formula depends on an undefined
    /// variable or on a division by zero, its value is a FormulaError.  Otherwise, its value
    /// is a double, as specified in Formula.Evaluate.
    /// 
    /// Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        // using DependencyGraph created in an earlier assignment, to hold the cells of our spreadsheet
        private DependencyGraph DGSpreadsheet;

        // Dictionary is used to keep track of our cells. This makes accessing the cells easier
        // Dictionary "Cells" uses the name of the cell as the key, and the cell itself as the value associated with that given key
        private Dictionary<String, Cell> Cells;

        /// <summary>
        /// Spreadsheet Constructor, responsible for creating an instance
        /// of our spreasheet class. 
        /// </summary>
        public Spreadsheet()
        {
            // Initiating our DependencyGraph and our Cells Dictionary
            DGSpreadsheet = new DependencyGraph();
            Cells = new Dictionary<string, Cell>();
        }

        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<String> GetNamesOfAllNonemptyCells()
        {
            HashSet<String> namesOfCells = new HashSet<string>();
            foreach (Cell c in Cells.Values)
            {
                // going through our cells dictionary, and grabbing each cell that 
                // doesn't have null as it's contents
                if (!(c.GetContent().Equals(null))) 
                { namesOfCells.Add(c.GetName()); }
            }

            return namesOfCells;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, returns the contents (as opposed to the value) of the named cell.  The return
        /// value should be either a string, a double, or a Formula.
        /// </summary>
        public override object GetCellContents(String name)
        {
            Object contents = null;

            // Checking if the name is valid
            if (!nameValidation(name))
            { throw new InvalidNameException(); }
            
            // Grabbing the the cells value based on the parameter key
            if (Cells.ContainsKey(name))
            { contents = Cells[name].GetContent(); } 

            return contents;
        }

        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes a number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<String> SetCellContents(String name, double number)
        {
            HashSet<String> namesOfTheCellAndDependents = new HashSet<string>();
            
            // Checking if the name is valid
            if(!nameValidation(name))
            { throw new InvalidNameException(); }

            // Grabbing the the cells value based on the parameter key
            if (Cells.ContainsKey(name))
            {
                HashSet<String> recalculate = new HashSet<string>(GetCellsToRecalculate(name));
                Cells[name].SetCellContent((Object)number);
                return recalculate;
            
            }
                // if the cell name does not exist, we don't have any
                // dependencies to worry about and we can simply return a 
                // hashset with the new cell name
                Cells.Add(name, new Cell(name, (Object)number));
                namesOfTheCellAndDependents.Add(name);
                return namesOfTheCellAndDependents;
        }

        /// <summary>
        /// If text is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes text.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<String> SetCellContents(String name, String text)
        {
            // we have to look for the cell in our data structure that contains the cells and then we have to adjust
            // the contents of the cell is changed to hold the parameter string
            // returns a set of all the dependents of the cell -> first element is the name in question and the rest are the dependents of that cell?

            return new HashSet<String>();
        }

        /// <summary>
        /// If formula parameter is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, if changing the contents of the named cell to be the formula would cause a 
        /// circular dependency, throws a CircularException.
        /// 
        /// Otherwise, the contents of the named cell becomes formula.  The method returns a
        /// Set consisting of name plus the names of all other cells whose value depends,
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<String> SetCellContents(String name, Formula formula)
        {

            return new HashSet<String>();
        }

        /// <summary>
        /// If name is null, throws an ArgumentNullException.
        /// 
        /// Otherwise, if name isn't a valid cell name, throws an InvalidNameException.
        /// 
        /// Otherwise, returns an enumeration, without duplicates, of the names of all cells whose
        /// values depend directly on the value of the named cell.  In other words, returns
        /// an enumeration, without duplicates, of the names of all cells that contain
        /// formulas containing name.
        /// 
        /// For example, suppose that
        /// A1 contains 3
        /// B1 contains the formula A1 * A1
        /// C1 contains the formula B1 + A1
        /// D1 contains the formula B1 - C1
        /// The direct dependents of A1 are B1 and C1
        /// </summary>
        protected override IEnumerable<String> GetDirectDependents(String name)
        {
            // checking if parameter name is null
            if (!(nameValidation(name)))
            { throw new ArgumentNullException(); }

            // returning a IEnumerable of all the direct dependents of the given cell name
            return DGSpreadsheet.GetDependents(name);
        }



        /// <summary>
        /// Method that validates if a correct name was used for the cells
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True if name is valid, false otherwise.</returns>
        private Boolean nameValidation(String name)
        {
            // Checking if the name is null
            if (name == null)
            { return false; }

            // Using a regular expression match to identify if the given name is 
            // valid (based on the class description of what a valid name is)
            Regex regex = new Regex(@"^[a-zA-Z]+[1-9]+[0-9]*$");
            Match match = regex.Match(name);
            if (match.Success)
            {
                return true;
            }

            return false;
        }

    }
}
