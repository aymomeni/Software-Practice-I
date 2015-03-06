using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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


        public Form1()
        {
            // Starts up the GUI
            InitializeComponent();

            // The SelectionChanged event is declared with a
            // delegate that specifies that all methods that register with it must
            // take a SpreadsheetPanel as its parameter and return nothing.  So we
            // register the displaySelection method below.
            spreadsheetPanel1.SelectionChanged += displaySelection;
            spreadsheetPanel1.SetSelection(0, 0);

            // Initializing our spreadsheet
            spreadsheet = new Spreadsheet();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            int col, row;
            spreadsheetPanel1.GetSelection(out col, out row);

            spreadsheetPanel1.SetValue(col, row, contents.Text);
        }



        // Every time the selection changes, this method is called with the
        // Spreadsheet as its parameter.  We display the current time in the cell.

        private void displaySelection(SpreadsheetPanel ss)
        {
            int row, col;
            String value;
            ss.GetSelection(out col, out row);
            ss.GetValue(col, row, out value);
            if (value == "")
            {
                ss.SetValue(col, row, DateTime.Now.ToLocalTime().ToString("T"));
                ss.GetValue(col, row, out value);
                MessageBox.Show("Selection: column " + col + " row " + row + " value " + value);
            }
        }

        /// <summary>
        /// A helper method that converts the base 0 system of the SpreadsheetPanel
        /// to a conventional cell name with letter and number
        /// </summary>
        /// <returns></returns>
        private string columnRowToCellNameConverter(int col, int row)
        {
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
        /// Method responsible for correctly saving the the spreadsheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Fallowing method opens a spreadsheet from a saved source compatible with the implementation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
            if (spreadsheet.Changed)
            {
                Close();
            } 

        }

        /// <summary>
        /// Method that handles the Event of the Evaluate buttom being clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Evaluate_Click(object sender, EventArgs e)
        {

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

            // Showing a dialog asking the user if he really wants to quit
        }



    }
}
