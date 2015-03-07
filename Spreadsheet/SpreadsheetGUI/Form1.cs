using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formulas;
using System.IO;
using SS;

// PS7 - Ali Momeni - CS3500 - Joe Zackery
namespace SpreadsheetGUI
{
    /// <summary>
    /// This Class is the GUI component to our previously designed Spreadsheet
    /// </summary>
    public partial class Form1 : Form
    {
        // Declaring a spreadsheet 
        Spreadsheet spreadsheet;

        // boolean helper for the cancelation of files
        private Boolean cancel = true;

        // For keeping track of the file name
        private String fileName;


        /// <summary>
        /// Constructor that ignites the GUI application and sets up 
        /// an instance of our spreadsheet
        /// </summary>
        public Form1()
        {
            // Starts up the GUI
            InitializeComponent();

            // The SelectionChanged event is declared with a
            // delegate that specifies that all methods that register with it must
            // take a SpreadsheetPanel as its parameter and return nothing.  So we
            // register the displaySelection method below.
            spreadsheetPanel1.SelectionChanged += displaySelection;
            spreadsheetPanel1.SetSelection(2, 3);

            // Initializing our spreadsheet
            spreadsheet = new Spreadsheet();
        }

        /// <summary>
        /// Constructor that ignites the GUI application and sets up 
        /// Here a filename is used to create a new GUI form and for the initiation of Spreadsheet
        /// </summary>
        public Form1(TextReader source)
        {
            // Starts up the GUI
            InitializeComponent();

            // The SelectionChanged event is declared with a
            // delegate that specifies that all methods that register with it must
            // take a SpreadsheetPanel as its parameter and return nothing.  So we
            // register the displaySelection method below.
            spreadsheetPanel1.SelectionChanged += displaySelection;
            spreadsheetPanel1.SetSelection(2, 3);

            // Initializing our spreadsheet using a reader
            spreadsheet = new Spreadsheet(source);
            this.Text = fileName;
        }


        // Every time the selection changes, this method is called with the
        // Spreadsheet as its parameter.  We display the current time in the cell.

        private void displaySelection(SpreadsheetPanel ss)
        {
            int row, col;
            String value;
            ss.GetSelection(out col, out row);
            ss.GetValue(col, row, out value);

            // Setting the cell name textboxes based on whatever is selected
            BoxCellName.Text = columnRowToCellNameConverter(col, row);
            BoxCellValue.Text = spreadsheet.GetCellContents(BoxCellName.Text).ToString();

            Object temp = spreadsheet.GetCellContents(BoxCellName.Text);

            // checking if the value entered into the cell are of type formula
            if(temp is Formula)
            { BoxCellContent.Text = "=" + temp; }
            else { BoxCellContent.Text = temp.ToString(); }

        }

        /// <summary>
        /// A helper method that converts the base 0 system of the SpreadsheetPanel
        /// to a conventional cell name with letter and number
        /// </summary>
        /// <returns></returns>
        private string columnRowToCellNameConverter(int col, int row)
        {
            // Converting the column to a character and adding 1 to row for normalization purposes
            String tempCellName = "" + ((char)(col + 65)) + (row + 1);
            return tempCellName;
        }

        /// <summary>
        /// This helper method converts a conventional cell name to a 
        /// column row representation (accepted by spreadsheet panel)
        /// </summary>
        /// <returns></returns>
        private void cellNameToColumnRowConverter(string cellName, out int col, out int row)
        {

            // Since our spreadsheet only has columns (one letter a-z) and row(number from 1-99) we will 
            // parse it as such
            String ctemp, rtemp; // temporary variables that help parse the cellName string
            int conversionHelp; // temporary variable that helps convert the letter into a representive number

            // Storing the first letter
            ctemp = cellName.Substring(0, 1);
            // Storing the number 
            rtemp = cellName.Substring(1);
            //Converting the row from string to a nubmer
            conversionHelp = Convert.ToInt32(rtemp);
            // Subtracting 1 since everything starts at 1 (A1)
            row = conversionHelp - 1;
            
            //converting the string ctemp to a char array and then to a column
            col = ((int)ctemp.ToCharArray()[0]) - 65;           
        }

        /// <summary>
        /// Method responsible for saving the the spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // if we don't have a file name yet
            if (fileName == null)
            {
                // Creating a save Dialog with parsing definition for the extension
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Spreadsheet Doc (*.ss)| *.ss | All files (*.*) | *.*";
                saveDialog.Title = "Save";
                saveDialog.InitialDirectory = @"C:\";

                if(saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string name = saveDialog.FileName;
                    // Checking if an extension of .ss should be added or not
                    if (saveDialog.FilterIndex == 1)
                    {
                        saveDialog.AddExtension = true;
                        fileName = name; // saving the file name
                        spreadsheet.Save(File.CreateText(name));
                    }

                    else
                    {
                        saveDialog.AddExtension = false;
                        fileName = name; // saving the file name
                        spreadsheet.Save(File.CreateText(name));
                    }
                }
            }
            else
            {
                if (sender.ToString() == "Close")
                {
                    cancel = false;
                }
            }

        }

        /// <summary>
        /// Fallowing method opens a spreadsheet from a saved source compatible with the implementation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // creating an open Dialog window with extension properties
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Sprd Document (*.ss)|*.ss|All files(*.*)|*.*";
            openDialog.Title = "0pen";
            openDialog.InitialDirectory = @"C:\";
            int col, row;

            // Opening the file selection Dialog box for the user to select a spreadsheet class
            if(openDialog.ShowDialog() == DialogResult.OK)
            {
                string name = openDialog.FileName;
                Form1 openForm = new Form1(File.OpenText(name));
                openForm.Text = name;
                openForm.fileName = name;
                MyApplicationContext.getAppContext().RunForm(openForm);

                try
                {
                    // Going through all the cells that exist in our spreadsheet class and adding it to the GUI
                    foreach(string cellName in openForm.spreadsheet.GetNamesOfAllNonemptyCells())
                    {
                        cellNameToColumnRowConverter(cellName, out col, out row);
                        openForm.spreadsheetPanel1.SetValue(col, row, openForm.spreadsheet.GetCellValue(cellName).ToString());
                        openForm.spreadsheetPanel1.SetSelection(col, row);
                        openForm.displaySelection(spreadsheetPanel1);
                    }      
                } catch (Exception creatingFileException)
                { MessageBox.Show(creatingFileException.Message, "An Error occured reading in creating the spreadshit from the file given."); }

            }

        }


        /// <summary>
        /// When the menu's close button is clicked this method first checks if it was changed and then
        /// closes the spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // checking if the spreadsheet has changed
            if (!spreadsheet.Changed)
            {
                Close();
            }
            else
            {
                DialogResult closeDialog = MessageBox.Show("You have made changes to the document, would you like to save your document before cancelling?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if(closeDialog == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    if (cancel)
                        Close();
                    else
                        cancel = false;
                
                } else if(closeDialog == DialogResult.No)  { Close(); }
                else if(closeDialog == DialogResult.Cancel)
                {
                    if (e is FormClosingEventArgs)
                        ((FormClosingEventArgs)e).Cancel = true;
                }
            } 

        }

        /// <summary>
        /// Method that handles the Event of the Evaluate buttom being clicked, which means that
        /// we add elements of the cell to the spread sheet and calculate the value while we are at it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Evaluate_Click(object sender, EventArgs e)
        {
            int row, col;
            spreadsheetPanel1.GetSelection(out col, out row);

            try
            {
                // Setting the value in spreadsheet based on the GUI cell
                spreadsheet.SetContentsOfCell(columnRowToCellNameConverter(col, row), BoxCellContent.Text);
            }
            // Catching numerous exceptions that otherwise would terminate the GUI program
            // A Message Dialogue is shown to prevent abropt cancellation of the program
            catch (EvaluateException evaluateError)
            { MessageBox.Show(evaluateError.Message, "Note: Error occured evaulating your cell", MessageBoxButtons.OK); }
            catch (FormulaEvaluationException evaluateError)
            { MessageBox.Show(evaluateError.Message, "Note: Error occured evaulating your cell", MessageBoxButtons.OK); }
            catch (CircularException circularError)
            { MessageBox.Show(circularError.Message, "An Error occured because your data entry causes a Circular Dependency", MessageBoxButtons.OK); }

            BoxCellValue.Text = spreadsheet.GetCellValue(columnRowToCellNameConverter(col, row)).ToString();

            // Going the all of the empty cells that aren't empty in the spreadsheet and setting the different cells in the GUI
            foreach(String cellName in spreadsheet.GetNamesOfAllNonemptyCells())
            {
                cellNameToColumnRowConverter(cellName, out col, out row);

                spreadsheetPanel1.SetValue(col, row, spreadsheet.GetCellValue(cellName).ToString());

            }

        }

        /// <summary>
        /// Does the evaluation button through a enter press
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxCellContent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Evaluate_Click(sender, e);
            }
        }

        /// <summary>
        /// Method that creates a new instance of the GUI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyApplicationContext.getAppContext().RunForm(new Form1());
        }

        /// <summary>
        /// When the instance of the Spreadsheet GUI is about to be closed,
        /// this method makes sure to ask the user if he is sure (if the spreadsheet had been changed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // First checking if the spreadsheet has been changed
            if(spreadsheet.Changed)
            {
                // Showing a dialog asking the user if he really wants to quit
                DialogResult dialog = MessageBox.Show("You have made changes to the document, would you like to save your document before cancelling?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, e);
                    if (!cancel)
                        cancel = true;
                }
                else if (dialog == DialogResult.Cancel)
                {
                    if (e is FormClosingEventArgs)
                        ((FormClosingEventArgs)e).Cancel = true;

                }
            }
        }


        /// <summary>
        /// Method that handles the event of the User needing help using the GUI
        /// Here we state the main functionality of the spreadsheet and some guidance on how to use it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult helpDialog = MessageBox.Show("This Spreadsheet program is written to aid the user in tasks that require a table that has cells which can be manipulated, and respects dependencies between cells through using =(Function+valid Cell Names). You can enter values into cells by first selecting the cell (using a mouse click selection). Here you can manipulate the content of cells by using the content box. Clicking Evaluate or simply pressing enter will not only store the value inside the cell but also evaluate a value and display it next to the Value textbox, in a Value Box. \n\n Next, you are able to save the spreadsheet that you have created through clicking File->Save and selecting a destination path of your choice. Obviously you are also in turn able to open Spreadsheets that you have created by using File->Open and selecting a valid (compatible) .ss file", "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
