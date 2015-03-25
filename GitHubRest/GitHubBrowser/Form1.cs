using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Drawing;
using System.Collections;

/*
 * Elyx Jolley
 * Ali Momeni
 * CS 3500 Joe Zachary - Assignment 8 - Creating a Git Search GUI
 */

namespace GitHubBrowser
{
    /// <summary>
    /// A partial class responsible to create a GitHub Server access GUI that
    /// displays various search functions using the server and Json
    /// </summary>
    public partial class form1 : Form
    {

        // using a cancellation token
        private CancellationTokenSource tokenSource;

        // used to keep track of positions of what next and prev
        static private int startIndex = 0;
        static private int endIndex = 30;
        static private int totalAmountOfElementsInCollection = 0; // keeps track of how many data elements have been grabed from the gitHub server

        //New 
        private SearchClass newSearch;

        private static Task task; 

        public form1()
        {
            InitializeComponent();
            tokenSource = new CancellationTokenSource();
            CheckForIllegalCrossThreadCalls = false; // turning off cross-thread exceptions (Jake)
            comboBox1.SelectedIndex = 0; // creating a initial element that is shown in the combo box
        }
    


        /// <summary>
        /// Updates the Data Grid
        /// </summary>
        private void updateDataGrid(DataGridView dataGrid, int startIndex, int endIndex)
        {
            // Holds the different data fields
            ArrayList name = new ArrayList(), login = new ArrayList(), description = new ArrayList();
            ArrayList avatarArr = new ArrayList();

            name = newSearch.getNameArr();
            login = newSearch.getLoginArr();
            description = newSearch.getDescriptionArr();
            avatarArr = newSearch.getAvatarArr();

            // adding elements to the grid
            for(int i = startIndex; i < endIndex; i++)
            {
                Object[] row1 = { avatarArr[i], name[i], login[i], description[i] };
                dataGrid.Rows.Add(row1[0], row1[1], row1[2], row1[3]);
                dataGrid.Refresh();
            }

            return;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SearchButton_Click(object sender, EventArgs e)
        {
            // New
            newSearch = new SearchClass();

            nextButton.Enabled = false;
            previousButton.Enabled = false; 

            //getting string from search box
            String searchItem = SearchField.Text;
   
            //if nothing has been entered into the search box, show message
            if (searchItem == "")
            {
                SearchButton.Enabled = true;
                MessageBox.Show("Please select a search item and enter a search token");
            }
            
            if (comboBox1.SelectedIndex == 0)
            {
                
                
                nextButton.Enabled = true;
                previousButton.Enabled = true;

                task = Task.Run(() =>newSearch.searchHelper(searchItem), tokenSource.Token);


                try
                {

                    Thread.Sleep(5000);
                    await task; 
                    // updating the grid based on the new indecies
                    updateDataGrid(searchGrid, startIndex, endIndex);
                }
                catch (Exception)
                {
                    throw;
                }


                return; 
            } 
            
            else if (comboBox1.SelectedIndex == 1)
            {
                newSearch.searchHelperLanguage(searchItem);

                nextButton.Enabled = true;
                previousButton.Enabled = true;

                // updating the grid based on the new indecies
                updateDataGrid(searchGrid, startIndex, endIndex);

                return;
            }
            
            else if (comboBox1.SelectedIndex == 2)
            {
                int starRating = 0;
            //checking that searchItem is an int
                if (int.TryParse(searchItem, out starRating))
            {
                searchItem = "" + starRating;
                newSearch.searchHelperStars(searchItem);
               
                nextButton.Enabled = true;
                previousButton.Enabled = true;

                // updating the grid based on the new indecies
                updateDataGrid(searchGrid, startIndex, endIndex);

                return;
            }

            }
           
         }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, EventArgs e)
        {
            // checks if we have enough elements in our collection
            if((endIndex + 30) > newSearch.getTotalSeachElements())
            {             

                // update the grid based on elements in our collection that already exist
                startIndex += 30;
                endIndex += 30;

                // clearing our grid
                clearGrid(searchGrid);

                //Task collectionTask;

                // add more elements to our collection
                newSearch.collectingData();

                // updating the grid based on the new indecies
                updateDataGrid(searchGrid, startIndex, endIndex);

            } else {
                // update the grid based on elements in our collection that already exist
                startIndex += 30;
                endIndex += 30;

                // clearing our grid
                clearGrid(searchGrid);
                
                // updating the grid based on the new indecies
                updateDataGrid(searchGrid, startIndex, endIndex);

            }

        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousButton_Click(object sender, EventArgs e)
        {
            // Checking if next has been used
            if(startIndex >= 30){
                
                startIndex -= 30;
                endIndex -= 30;
                
                // clearing our grid
                clearGrid(searchGrid);

                // updating the grid based on the new indecies
                updateDataGrid(searchGrid, startIndex, endIndex);
            
            }
    
        }


        /// <summary>
        /// clears the current grid
        /// </summary>
        private static void clearGrid(DataGridView grid)
        {
            grid.Rows.Clear();

        }

    }

}
