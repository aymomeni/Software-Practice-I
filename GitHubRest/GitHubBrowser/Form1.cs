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
    public partial class GITBrowserForm : Form
    {

        // using a cancellation token
        private CancellationTokenSource cancelToken;

        // used to keep track of positions of what next and prev
        static private int startIndex = 0;
        static private int endIndex = 30;
        static private int totalAmountOfElementsInCollection = 0; // keeps track of how many data elements have been grabed from the gitHub server

        // Instiance of the Search Class that is used to collect 
        // data that is that utilized for the display of the form
        private SearchClass newSearch;

        private static Task task; 


        /// <summary>
        /// Initiates the Form
        /// </summary>
        public GITBrowserForm()
        {
            InitializeComponent();
            cancelToken = new CancellationTokenSource();
            CheckForIllegalCrossThreadCalls = false; // turning off cross-thread exceptions (Jake)
            comboBox1.SelectedIndex = 0; // creating a initial element that is shown in the combo box
            searchGrid.AllowUserToAddRows = false;
            
            // Disabling next and previous buttons since they don't serve any functionality
            nextButton.Enabled = false;
            previousButton.Enabled = false;
        }
    


        /// <summary>
        /// Updates the Data Grid
        /// </summary>
        private async Task updateDataGrid(DataGridView dataGrid, int startIndex, int endIndex, CancellationToken cancel)
        {
            await updateDataGridHelper(dataGrid, startIndex, endIndex, cancel);
        }



        /// <summary>
        /// Helper method that updates the grid
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="cancel"></param>
        /// <returns></returns>
        private async Task updateDataGridHelper(DataGridView dataGrid, int startIndex, int endIndex, CancellationToken cancel)
        {
            // Holds the different data fields
            ArrayList name = new ArrayList(), login = new ArrayList(), description = new ArrayList();
            ArrayList avatarArr = new ArrayList();

            name = newSearch.getNameArr();
            login = newSearch.getLoginArr();
            description = newSearch.getDescriptionArr();
            avatarArr = newSearch.getAvatarArr();

            // adding elements to the grid
            for (int i = startIndex; i < endIndex; i++)
            {
                // Checking for cancellation
                cancel.ThrowIfCancellationRequested();

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
                MessageBox.Show("Please select a search item and enter select the appropriate search criteria.");
                return; 
            }
            
            //NEW Cancellation Exception
            try
            {
                if (comboBox1.SelectedIndex == 0)
                {

                    await newSearch.searchHelper(searchItem, cancelToken.Token);

                    // NEW: Deactivate the search button after the search is run
                    SearchButton.Enabled = false;

                    nextButton.Enabled = true;
                    previousButton.Enabled = false;

                    // updating the grid based on the new indecies
                    await updateDataGrid(searchGrid, startIndex, endIndex, cancelToken.Token);

                    return;
                }

                else if (comboBox1.SelectedIndex == 1)
                {
                    await newSearch.searchHelperLanguage(searchItem, cancelToken.Token);
                    
                    // NEW: Deactivate the search button after the search is run
                    SearchButton.Enabled = false;

                    nextButton.Enabled = true;
                    previousButton.Enabled = false;

                    // updating the grid based on the new indecies
                    await updateDataGrid(searchGrid, startIndex, endIndex, cancelToken.Token);

                    return;
                }

                else if (comboBox1.SelectedIndex == 2)
                {
                    int starRating = 0;
                    //checking that searchItem is an int
                    if (int.TryParse(searchItem, out starRating))
                    {
                        searchItem = "" + starRating;
                        await newSearch.searchHelperStars(searchItem, cancelToken.Token);

                        // NEW: Deactivate the search button after the search is run
                        SearchButton.Enabled = false;

                        nextButton.Enabled = true;
                        previousButton.Enabled = false;

                        // updating the grid based on the new indecies
                        await updateDataGrid(searchGrid, startIndex, endIndex, cancelToken.Token);

                        return;
                    }

                }

            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Search has been Cancelled.");
            }
           
         }


        /// <summary>
        /// Method that throws a cancellation exception when the cancel button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            SearchField.Clear();
            cancelToken.Cancel();
            cancelToken = new CancellationTokenSource();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void nextButton_Click(object sender, EventArgs e)
        {
            // checks if we have enough elements in our collection
            if((endIndex + 30) > newSearch.getTotalSeachElements())
            {             

                // update the grid based on elements in our collection that already exist
                startIndex += 30;
                endIndex += 30;

                // clearing our grid
                clearGrid(searchGrid);

                //enableling previous Button
                previousButton.Enabled = true;

                // add more elements to our collection
                await newSearch.collectingData();

                // updating the grid based on the new indecies
                await updateDataGrid(searchGrid, startIndex, endIndex, cancelToken.Token);

            } else {
                // update the grid based on elements in our collection that already exist
                startIndex += 30;
                endIndex += 30;

                //enableling previous Button
                previousButton.Enabled = true;

                // clearing our grid
                clearGrid(searchGrid);
                
                // updating the grid based on the new indecies
                await updateDataGrid(searchGrid, startIndex, endIndex, cancelToken.Token);

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

                //if startIndex is 0 we disable the previous Button
                previousButton.Enabled = false;

                endIndex -= 30;
                
                // clearing our grid
                clearGrid(searchGrid);

                // updating the grid based on the new indecies
                updateDataGrid(searchGrid, startIndex, endIndex, cancelToken.Token);
            
            }
    
        }


        /// <summary>
        /// clears the current grid
        /// </summary>
        private static void clearGrid(DataGridView grid)
        {
            grid.Rows.Clear();

        }

        /// <summary>
        /// Responsible for handling messages when 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        /// <summary>
        /// Creates a new search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newSearchButton_Click(object sender, EventArgs e)
        {
            // New
            SearchField.Text = "";
            SearchButton.Enabled = true;
            newSearch = new SearchClass();
            clearGrid(searchGrid);

        }


    }

}
