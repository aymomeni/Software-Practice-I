﻿using System;
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

namespace GitHubBrowser
{
    public partial class form1 : Form
    {



        // You'll need to put your own OAuth token here
        // It needs to have repo deletion capability
        private const string TOKEN = "b8afeb0df84d83c2aa6d558fd2f5e5e793449677";

        // You'll need to put your own GitHub user name here
        private const string USER_NAME = "u0665392";

        // You'll need to put your own login name here
        private const string EMAIL = "aymomeni@gmail.com";

        // You'll need to put one of your public REPOs here
        private const string PUBLIC_REPO = "repo1";

        // using a cancellation token
        private CancellationTokenSource tokenSource;

        // Holds the different data fields
        static ArrayList name = new ArrayList(), login = new ArrayList(), description = new ArrayList();
        static int count = 0; // counter that helps in collecting the data fields
        static ArrayList avatarArr = new ArrayList();

        // used to keep track of positions of what next and prev
        static int startIndex = 0;
        static int endIndex = 30;
        static int amountOfDataElements = 0; // keeps track of how many data elements have been grabed from the gitHub server

        public static Task task;

        public form1()
        {
            InitializeComponent();
        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Creates a generic client for communicating with GitHub
        /// </summary>
        public static HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.Add("User-Agent", Uri.EscapeDataString(EMAIL));
            client.DefaultRequestHeaders.Add("Authorization", "token " + TOKEN);
            return client;
        }

        /// <summary>
        /// Prints out the names of the organizations to which the user belongs
        /// </summary>
        public static async void GetDemo()
        {
            using (HttpClient client = CreateClient())
            {
                HttpResponseMessage response = await client.GetAsync("/user/orgs");
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    dynamic orgs = JsonConvert.DeserializeObject(result);
                    foreach (dynamic org in orgs)
                    {
                        Console.WriteLine(org.login);
                    }
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Prints out (some of) the users of GitHub, along with the link to use to get more.
        /// </summary>
        public static async void GetAllDemo()
        {
            using (HttpClient client = CreateClient())
            {
                HttpResponseMessage response = await client.GetAsync("/users");
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    dynamic users = JsonConvert.DeserializeObject(result);
                    foreach (dynamic user in users)
                    {
                        Console.WriteLine(user.login);
                    }
                    foreach (String link in response.Headers.GetValues("Link"))
                    {
                        Console.WriteLine(link);
                    }
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Prints out all of the commits on the master branch
        /// </summary>
        public static async void GetWithParamsDemo()
        {
            using (HttpClient client = CreateClient())
            {
                String url = String.Format("/repos/{0}/{1}/commits?sha={2}", USER_NAME, PUBLIC_REPO, Uri.EscapeDataString("master"));
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    dynamic commits = JsonConvert.DeserializeObject(result);
                    Console.WriteLine(commits);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Creates a new public repository
        /// </summary>
        public static async void PostDemo()
        {
            using (HttpClient client = CreateClient())
            {
                dynamic data = new ExpandoObject();
                data.name = "TestRepo";
                StringContent content = new StringContent(JsonConvert.SerializeObject(data));
                HttpResponseMessage response = await client.PostAsync("/user/repos", content);
                if (response.IsSuccessStatusCode)
                {
                    String result = await response.Content.ReadAsStringAsync();
                    dynamic newRepo = JsonConvert.DeserializeObject(result);
                    Console.WriteLine(newRepo);
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Deletes an existing public repository
        /// </summary>
        public static async void DeleteDemo()
        {
            using (HttpClient client = CreateClient())
            {
                String url = String.Format("/repos/{0}/TestRepo", USER_NAME);
                Console.WriteLine(url);
                HttpResponseMessage response = await client.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Deleted");
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Grabs all of the data fields (users, login, avatar, description)
        /// </summary>
        private static async void searchHelper(CancellationToken cancel, DataGridView grid)
        {
            
            String imageURL = "";
            WebClient tempWebClient = new WebClient();         
            int count = 1;
                try
                {
                    cancel.ThrowIfCancellationRequested();
                   
                    using (HttpClient client = CreateClient())
                    {
                        HttpResponseMessage response = await client.GetAsync("/repositories");
                        if (response.IsSuccessStatusCode)
                        {
                            String result = await response.Content.ReadAsStringAsync();
                            dynamic users = JsonConvert.DeserializeObject(result);
                            foreach (dynamic user in users)
                            {
                                // Grabbing the data elements from the website and 
                                // adding it to the arraylist
                                name.Add(user.name);
                                login.Add(user.owner.login);
                                description.Add(user.description); 
                                imageURL = user.owner.avatar_url;
                                byte[] imageData = tempWebClient.DownloadData(imageURL); 
                                MemoryStream stream = new MemoryStream(imageData);
                                Image img = Image.FromStream(stream);

                                //Image Processing
                                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(() => false);
                                Bitmap myBitmap = new Bitmap(img);
                                img = myBitmap.GetThumbnailImage(20, 20, myCallback, IntPtr.Zero);

                                avatarArr.Add(img);
                                stream.Close();


                                


                                amountOfDataElements++;
                                count++;
                                // TODO: Grab the

                            }

                            //Update the grid
                            updateDataGrid(grid, startIndex, endIndex);
                        }
                        else
                        {
                            Console.WriteLine("Error: " + response.StatusCode);
                            Console.WriteLine(response.ReasonPhrase);
                        }
                     
                       // task = new Thread(new ThreadStart(populateDataGrid(name, login, description)));
                        
                    }
                }
                catch (OperationCanceledException)
                {
                    // just catch the exception and cancel
                }
                
        }


        /// <summary>
        /// Updates the Data Grid
        /// </summary>
        private static void updateDataGrid(DataGridView dataGrid, int startIndex, int endIndex)
        {

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
        private void SearchButton_Click(object sender, EventArgs e)
        {
            tokenSource = new CancellationTokenSource();

            //task = Task.Run(() => searchHelper(tokenSource.Token), tokenSource.Token);

            searchHelper(tokenSource.Token, searchGrid);
            try
            {
                //await task;

            } catch(OperationCanceledException)
            {
                //
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
           System.Windows.Forms.MessageBox.Show("cancelled"); //TODO: ROWS COLUMS
            //Close();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nextButton_Click(object sender, EventArgs e)
        {
            // checks if we have enough elements in our collection
            if(){


            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previousButton_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        private static void resetAll(){


        }



        /// <summary>
        /// resets the current grid
        /// </summary>
        private static void resetGrid(DataGridView grid)
        {
            grid.Rows.Clear();

        }

    }
}
