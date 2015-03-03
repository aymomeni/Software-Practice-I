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
            // Initializing our spreadsheet
            spreadsheet = new Spreadsheet();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            int col, row;
            spreadsheetPanel1.GetSelection(out col, out row);

            spreadsheetPanel1.SetValue(col, row, contents.Text);
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


    }
}
