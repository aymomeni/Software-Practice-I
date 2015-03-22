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
            // Holds the different data fields
            string[] name = new String[100], login = new String[100], description = new String[100];
            int count = 0; // counter that helps in collecting the data fields
            Image[] avatarArr = new Image[100];
            String imageURL = "";
            WebClient tempWebClient = new WebClient();

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
                                // Console.WriteLine(user.login);
                                name[count] = user.name;
                                login[count] = user.owner.login;
                                description[count] = user.description; //TODO: must be set to description
                                imageURL = user.owner.avatar_url;
                                byte[] imageData = tempWebClient.DownloadData(imageURL); //DownloadData function from here
                                MemoryStream stream = new MemoryStream(imageData);
                                Image img = Image.FromStream(stream);
                                
                                //Image Processing
                                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(() => false);
                                Bitmap myBitmap = new Bitmap(img);
                                img = myBitmap.GetThumbnailImage(20, 20, myCallback, IntPtr.Zero);
                               

                                
                                avatarArr[count] = img;
                                stream.Close();
                                // TODO: Grab the



                                if (count < 30)
                                {
                                    Object[] row1 = {avatarArr[count], name[count], login[count], description[count] };
                                    grid.Rows.Add(row1[0], row1[1], row1[2], row1[3]);
                                }
                                //System.Windows.Forms.MessageBox.Show("" + user.login); //TODO: ROWS COLUMS
                                grid.Refresh();
                                count++; // increment count
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: " + response.StatusCode);
                            Console.WriteLine(response.ReasonPhrase);
                        }
                     

                       // task = new Thread(new ThreadStart(populateDataGrid(name, login, description)));
                        
                        //populateDataGrid(name, login, description);
                        //Populating the DataGrid
                        //populateDataGrid(name, login, description);

                        //string[] row1 = { null, name[0], login[0], description[0] };
                        //searchGrid.Rows.Add(row1[0]);
                
                    }
                }
                catch (OperationCanceledException)
                {
                    // just catch the exception and cancel
                }
                
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

        ///// <summary>
        ///// Populates the datagrid in the GUI 
        ///// </summary>
        ///// <param name="user"></param>
        ///// <param name="login"></param>
        ///// <param name="avatar"></param>
        ///// <param name="description"></param>
        //private async void  populateDataGrid(string[] user, string[] login, string[] description) // Image[] avatar, 
        //{

        //    string[] row1 = {null, user[0], login[0], description[0]};

        //    //for(int i = 0; i < 30; i++){
        //    //    searchGrid.Column[i] = 
        //    //}
        //    searchGrid.Rows.Add(row1[0]);

        //}

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

    }
}
