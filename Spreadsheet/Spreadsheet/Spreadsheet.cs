using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulas; 
using Dependencies;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;
using Formulas;

/**
 * PS6 - Ali Momeni - February 23, 2015 
 * CS 3500 - Joe Zackary
 * 
 * PS5 - Abstract Spreadsheet assignment February 14, 2015
 * 
 * Update PS6: Assignment changed abstract spreadsheet which had additional alterations,
 * that are described below in the
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
        // Value is either a String, a double, or a formulaError
        private object value;

        /// <summary>
        /// PS6 UPDATE: Added a lookup function as the parameter that immidiately sets the value variable and a value variable
        /// 
        /// Cell constructor that uses the name of the cell and the content as parameters,
        /// to create an instance of a cell object
        /// </summary>
        /// <param name="NameOfCell"></param>
        /// <param name="Content"></param>
        public Cell(String NameOfCell, Object Content, Func<string, double> lookup)
        {           
            // Setting up class variables
            this.nameOfCell = NameOfCell.ToUpper(); // saving each name in captial case
            this.content = Content;

            // If we have a formula or a double we can evaluate the method and grab its value for our
            // class variable value
            if (content is Formula || content is double)
            {
                Formula f = new Formula(content.ToString());
                try
                {
                    this.value = (object)f.Evaluate(s => lookup(nameOfCell));
                }
                catch (FormulaEvaluationException)
                {
                    this.value = new FormulaError();
                }
            }
            // else the content and the value are simply a string and the same
            else { value = content; }
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
        /// Returns the value of the cell
        /// </summary>
        /// <returns></returns>
        public object GetValue()
        { return value; }

    }



    /// <summary>
    /// PS6 Update Paragraph 1-3:
    /// 
    /// An AbstractSpreadsheet object represents the state of a simple spreadsheet.  A 
    /// spreadsheet consists of a case-insensitive regular expression (called IsValid
    /// below) and an infinite number of named cells.
    /// 
    /// A string is a valid cell name if and only if (1) it consists of one or more letters, 
    /// followed by a non-zero digit, followed by zero or more digits AND (2) it is 
    /// accepted by IsValid.
    /// 
    /// For example, "A15", "a15", "XY32", and "BC7" are valid cell names, so long as they also
    /// are accepted by IsValid.  On the other hand, 
    /// "Z", "X07", and "hello" are not valid cell names, regardless of IsValid.
    /// 
    /// 
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
        // Dictionary "cellDictionary" uses the name of the cell as the key, and the cell itself as the value associated with that given key
        private Dictionary<String, Cell> cellDictionary;

        // Class level regular expression
        private Regex isValid = new Regex(@"^[a-zA-Z]+[1-9]+[0-9]*$"); // Setting it to the standard conditions

        // Boolean expression that keeps track of if a regular expression had been passed in
        private Boolean isValidBool = true; // initially set to true, expecting that there is a isValid variable


        /// <summary>
        /// Spreadsheet Constructor, responsible for creating an instance
        /// of our spreasheet class.
        /// 
        /// PS6 Update: Creates an empty Spreadsheet whose IsValid regular expression accepts every string. (represented by the boolean isValid)
        /// </summary>
        public Spreadsheet()
        {
            // Initiating our DependencyGraph and our Cells Dictionary
            DGSpreadsheet = new DependencyGraph();
            cellDictionary = new Dictionary<string, Cell>();
            // Setting the boolean isValid to false => meaning no extra checks on the validity of variables is performed
            isValidBool = false;
        }


        /// <summary>
        /// Spreadsheet Constructor, responsible for creating an instance
        /// of our spreasheet class. Creates an empty Spreadsheet whose IsValid regular expression is provided as the parameter
        /// 
        ///
        /// PS6 Update: Creates an empty Spreadsheet whose IsValid regular expression accepts string only if the
        /// regex expression is also passed. (represented by the boolean isValid and the isValid regex)
        /// </summary>

        public Spreadsheet(Regex isValid)
        {
            // Initiating our DependencyGraph and our Cells Dictionary
            DGSpreadsheet = new DependencyGraph();
            cellDictionary = new Dictionary<string, Cell>();
            // Setting the boolean isValid to false => meaning no extra checks on the validity of variables is performed
            isValidBool = true;
            // Setting the regex expression to the given parameter regex expression
            this.isValid = isValid;       
        }

        /// <summary>
        /// Creates a Spreadsheet that is a duplicate of the spreadsheet saved in source.
        /// See the AbstractSpreadsheet.Save method for the file format specification.
        /// If there's a problem reading source, throws an IOException
        /// If the contents of source is not formatted properly, throws a SpreadsheetReadException
        /// 
        /// Sample reading file:
        ///
        /// <spreadsheet isvalid="IsValid regex goes here">
        ///   <cell>
        ///     <name>
        ///       cell name goes here
        ///     </name>
        ///     <contents>
        ///       cell contents goes here
        ///     </contents>
        ///   </cell>
        /// </spreadsheet>
        ///
        /// The value of the isvalid attribute should be IsValid.ToString()
        /// </summary>
        public Spreadsheet(TextReader source)
        {

            // Local variables to hold the xml data (name of cell and content of cell)
            string tempCellName = "";
            string tempContent = "";
            isValidBool = true;

            // must grab the regular expression from the text reader
            // then create a spreadsheet based on source
            // create a boolean
            try
            {
                using (XmlReader xmlReader = XmlReader.Create(source))
                {
                    while (xmlReader.Read())
                    {
                        if (xmlReader.IsStartElement())
                        {
                            if (xmlReader.Name.Equals("spreadsheet"))
                            {
                                isValid = new Regex(xmlReader.GetAttribute("isvalid"));
                                continue;
                            }

                            if (xmlReader.Name.Equals("cell"))
                            {
                                continue;
                            }

                            switch (xmlReader.Name.ToString())
                            {
                                case "name":
                                    tempCellName = xmlReader.ReadString();
                                    // Checking if the name is valid
                                    if (!nameValidation(tempCellName))
                                    { throw new InvalidNameException(); }
                                    break;

                                case "contents":
                                    tempContent = xmlReader.ReadString();
                                    SetContentsOfCell(tempCellName, tempContent);
                                    break;

                            }
                        }
                    }
                }
            }
            catch (IOException) { throw new IOException("There was an issue reading from the source file"); }
        
        }
        

        // ADDED FOR PS6
        /// <summary>
        /// True if this spreadsheet has been modified since it was created or saved
        /// (whichever happened most recently); false otherwise.
        /// </summary>
        public override bool Changed { get; protected set; }


        // ADDED FOR PS6
        /// <summary>
        /// Writes the contents of this spreadsheet to dest using an XML format.
        /// The XML elements should be structured as follows:
        ///
        /// <spreadsheet isvalid="IsValid regex goes here">
        ///   <cell>
        ///     <name>
        ///       cell name goes here
        ///     </name>
        ///     <contents>
        ///       cell contents goes here
        ///     </contents>
        ///   </cell>
        /// </spreadsheet>
        ///
        /// The value of the isvalid attribute should be IsValid.ToString()
        /// 
        /// There should be one cell element for each non-empty cell in the spreadsheet.
        /// If the cell contains a string, the string (without surrounding double quotes) should be written as the contents.
        /// If the cell contains a double d, d.ToString() should be written as the contents.
        /// If the cell contains a Formula f, f.ToString() with "=" prepended should be written as the contents.
        /// IMPLEMENTATION NOTE:  You'll have to override the ToString method in the Formula class
        ///
        /// If there are any problems writing to dest, the method should throw an IOException.
        /// </summary>
        public override void Save(TextWriter dest)
        {
            try
            {
                using (XmlWriter writer = XmlWriter.Create(dest))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");
                    writer.WriteAttributeString("isvalid", isValid.ToString());

                    foreach (String s in this.GetNamesOfAllNonemptyCells())
                    {
                        Cell c;
                        if (cellDictionary.TryGetValue(s, out c))
                        {
                            // Writing the name of each cell 
                            writer.WriteStartElement("cell");

                            writer.WriteElementString("name", s); // must be the name after it has beeen capitalized

                            // grabing the element from our cell dictionary for type checking
                            object check = cellDictionary[s].GetContent();

                            // if our check reveals a formula we simply use the formula toString() method
                            if (check is Formula)
                            {
                                writer.WriteElementString("contents", "=" + check.ToString());
                            }
                            // else if its a double or any other string we simply use the inherited toString() method
                            else
                            {
                                writer.WriteElementString("contents", check.ToString());
                            }
                            writer.WriteEndElement(); // ending cell                   
                        }
                    }
                    // ending cell
                    writer.WriteEndDocument();
                    writer.Close();
                }
            }
            catch (IOException)
            {
                throw new IOException("There was a problem writing to the destination TextWriter");
            }
        }

        // ADDED FOR PS6
        /// <summary>
        /// If name is null or invalid, throws an InvalidNameException.
        ///
        /// Otherwise, returns the value (as opposed to the contents) of the named cell.  The return
        /// value should be either a string, a double, or a Spreadsheet.FormulaError.
        /// </summary>
        public override object GetCellValue(String name)
        {
            // Checking if the name is valid
            if (!nameValidation(name))
            { throw new InvalidNameException(); }

            //name is converted to upper
            name = name.ToUpper();

            // if the name exists in our dictionary we grab it
            if (cellDictionary.ContainsKey(name)) { return cellDictionary[name].GetValue(); }
            // else we return an empty string
            return "";
        }


        // ADDED FOR PS6
        /// <summary>
        /// If content is null, throws an ArgumentNullException.
        ///
        /// Otherwise, if name is null or invalid, throws an InvalidNameException.
        ///
        /// Otherwise, if content parses as a double, the contents of the named
        /// cell becomes that double.
        ///
        /// Otherwise, if content begins with the character '=', an attempt is made
        /// to parse the remainder of content into a Formula f using the Formula
        /// constructor.  There are then three possibilities:
        ///
        ///   (1) If the remainder of content cannot be parsed into a Formula, a
        ///       SpreadsheetUtilities.FormulaFormatException is thrown.
        ///
        ///   (2) Otherwise, if changing the contents of the named cell to be f
        ///       would cause a circular dependency, a CircularException is thrown.
        ///
        ///   (3) Otherwise, the contents of the named cell becomes f.
        ///
        /// Otherwise, the contents of the named cell becomes content.
        ///
        /// If an exception is not thrown, the method returns a set consisting of
        /// name plus the names of all other cells whose value depends, directly
        /// or indirectly, on the named cell.
        ///
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        public override ISet<String> SetContentsOfCell(String name, String content)
        {
            // if content is null, throw argumentNullException
            if (content == null)
            { throw new ArgumentNullException();}

            // checking for the validity of the name parameter
            if (!nameValidation(name)) { throw new InvalidNameException(); }

            //name is converted to upper
            name = name.ToUpper();

            Changed = true;

            // checkdouble is used for the parse checking
            Double checkdouble = 0.0;

            // if content begins with the character '=', an attempt is made
            // to parse the remainder of content into a Formula f using the Formula
            // constructor
            if(content.StartsWith("=")){

                Formula f = new Formula(content.Substring(1));
                return SetCellContents(name, f);

            // if content parses as a double, the contents of the named
            // cell becomes that double.
            } else if(Double.TryParse(content, out checkdouble)){
                return SetCellContents(name, checkdouble);
            }

            return new HashSet<String>();
        }



        /// <summary>
        /// This is a helper method that looks up the value of a given cell
        /// its main use is when cells are created. Here when in the creation of a cell
        /// we want to immidiately use lookup as the delegete for the evaluator method in formula
        /// </summary>
        /// <returns></returns>
        private double lookup(string nameOfCell)
        {
            //name is converted to upper
            nameOfCell = nameOfCell.ToUpper();

            Cell tempCell = cellDictionary[nameOfCell];

            if (tempCell.GetValue() is double)
            {
                return (double)tempCell.GetValue();
            }

            return 0.0; //TODO: what to do here with exceptions?
        }


        /// <summary>
        /// Enumerates the names of all the non-empty cells in the spreadsheet.
        /// </summary>
        public override IEnumerable<String> GetNamesOfAllNonemptyCells()
        {
            HashSet<String> namesOfCells = new HashSet<string>();
            foreach (Cell c in cellDictionary.Values)
            {
                // going through our cells dictionary, and grabbing each cell that 
                // doesn't have null as it's contents
                if (!(c.GetContent().Equals(""))) 
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
            Object contents = "";

            // Checking if the name is valid
            if (!nameValidation(name))
            { throw new InvalidNameException(); }

            //name is converted to upper
            name = name.ToUpper();
            
            // Grabbing the the cells value based on the parameter key
            if (cellDictionary.ContainsKey(name))
            { contents = cellDictionary[name].GetContent(); } 

            return contents;
        }

        /// <summary>
        /// MODIFIED PROTECTION FOR PS5
        /// If name is null or invalid, throws an InvalidNameException.
        /// 
        /// Otherwise, the contents of the named cell becomes a number.  The method returns a
        /// set consisting of name plus the names of all other cells whose value depends, 
        /// directly or indirectly, on the named cell.
        /// 
        /// For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        /// set {A1, B1, C1} is returned.
        /// </summary>
        protected override ISet<String> SetCellContents(String name, double number)
        {
            HashSet<String> recalculate;
            
            // Checking if the name is valid
            if(!nameValidation(name))
            { throw new InvalidNameException(); }

            //name is converted to upper
            name = name.ToUpper();

            // Remove all the dependents of the cell whose value is set
            DGSpreadsheet.ReplaceDependents(name, new HashSet<string>());
            // Grab all of the cells that are dependent on the cell that changed
            recalculate = new HashSet<string>(GetCellsToRecalculate(name));

            // Grabbing the the cells value based on the parameter key
            if (cellDictionary.ContainsKey(name))
            {
                // First remove the cell
                cellDictionary.Remove(name);
                // create a new cell that contains the original name, with the new content
                cellDictionary.Add(name, new Cell(name, number, lookup));
            }
            else
            {
                // if the cell name does not exist, we don't have any
                // dependencies to worry about and we can simply return a 
                // hashset with the new cell name and insert the new cell and its content
                cellDictionary.Add(name, new Cell(name, (Object)number, lookup));
            }
       
                return recalculate;   
        }

        /// <summary>
        /// MODIFIED PROTECTION FOR PS5
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
        protected override ISet<String> SetCellContents(String name, String text)
        {
            HashSet<String> recalculate = new HashSet<string>();

            // Checking if the text is null
            if (text == null)
            { throw new ArgumentNullException(); }

            // Checking if the name is valid
            if (!nameValidation(name))
            { throw new InvalidNameException(); }

            //name is converted to upper
            name = name.ToUpper();

            // Remove all the dependents of the cell whose value is set
            DGSpreadsheet.ReplaceDependents(name, new HashSet<string>());

            // Grab all of the cells that are dependent on the cell that changed
            recalculate = new HashSet<string>(GetCellsToRecalculate(name));

            // Grabbing the the cells value based on the parameter key
            if (cellDictionary.ContainsKey(name))
            {
                // First remove the cell
                cellDictionary.Remove(name);
                // create a new cell that contains the original name, with the new content
                cellDictionary.Add(name, new Cell(name, text, lookup));

            }
            else
            {
                // if the cell name does not exist, we don't have any
                // dependencies to worry about and we can simply return a 
                // hashset with the new cell name and insert the new cell and its content
                cellDictionary.Add(name, new Cell(name, (Object)text, lookup));
            }

            return recalculate; 
        }

        /// <summary>
        /// MODIFIED PROTECTION FOR PS5
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
        protected override ISet<String> SetCellContents(String name, Formula formula)
        {

            //Checking if formula is null
            if (formula == null)
            { throw new ArgumentNullException(); }

            // Checking if the name is valid
            if (!nameValidation(name))
            { throw new InvalidNameException(); }

            //name is converted to upper
            name = name.ToUpper();

            HashSet<String> recalculate;

            try
            {

                    // Remove all the dependents of the cell whose value is set
                    DGSpreadsheet.ReplaceDependents(name, new HashSet<string>());

                    // we need to go through the variables of formula and make sure 
                    // any additional dependencies are inserted into our Dependency Graph of cells
                    foreach (String s in formula.GetVariables())
                    {
                        DGSpreadsheet.AddDependency(name, s);
                    }
 

                // Grab all of the cells that are dependent on the cell that changed
                recalculate = new HashSet<string>(GetCellsToRecalculate(name));
            }
            catch (CircularException exception)
            {
                throw exception;
            }

            // Grabbing the the cells value based on the parameter key
            if (cellDictionary.ContainsKey(name))
            {
                // First remove the cell
                cellDictionary.Remove(name);
                // create a new cell that contains the original name, with the new content
                cellDictionary.Add(name, new Cell(name, formula, lookup));

            }
            else
            {
                // if the cell name does not exist, we don't have any
                // dependencies to worry about and we can simply return a 
                // hashset with the new cell name and insert the new cell and its content
                cellDictionary.Add(name, new Cell(name, (Object)formula, lookup));
            }

            return recalculate;
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

            //name is converted to upper
            name = name.ToUpper();

            // returning a IEnumerable of all the direct dependents of the given cell name
            return DGSpreadsheet.GetDependees(name);
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

            //name is converted to upper
            name = name.ToUpper();

            // Using a regular expression match to identify if the given name is 
            // valid (based on the class description of what a valid name is)
            Regex regex = new Regex(@"^[a-zA-Z]+[1-9]+[0-9]*$");
            Match match = regex.Match(name);
            if (match.Success)
            {
                // checking for the validity based on the isValid regex expression
                if (isValidBool)
                {
                    Match matchIsValid = isValid.Match(name);
                    if (match.Success)
                    { return true; } // if it machesthe is valid regex we return true
                    else { return false; } // if the name does not mach is valid we return false
                }

                return true;
            }

            return false;
        }

    }
}
