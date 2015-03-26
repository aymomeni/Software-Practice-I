using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class SearchClass
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
        private CancellationTokenSource cancelToken;

        // Holds the different data fields
        private ArrayList name = new ArrayList(), login = new ArrayList(), description = new ArrayList();
        private ArrayList avatarArr = new ArrayList();

        // used to keep track of positions of what next and prev
        private int totalSearchElements = 0; // keeps track of how many data elements have been grabed from the gitHub server
        private string nextlink = ""; // used to grab data elements from the server (updates as more values are needed)


        private static Task task; 


        /// <summary>
        /// Instantiates a SearchClass
        /// </summary>
        public SearchClass()
        {

        }

        /// <summary>
        /// Returns the total amount of data elements in the search instance
        /// </summary>
        /// <returns></returns>
        public int getTotalSeachElements()
        {
            return totalSearchElements;
        }

        /// <summary>
        /// Returns name arraylist
        /// </summary>
        /// <returns></returns>
        public ArrayList getNameArr()
        {
            return name;
        }

        /// <summary>
        /// Returns login arraylist
        /// </summary>
        /// <returns></returns>
        public ArrayList getLoginArr()
        {
            return login;
        }

        /// <summary>
        /// Returns description arraylist
        /// </summary>
        /// <returns></returns>
        public ArrayList getDescriptionArr()
        {
            return description;
        }

        /// <summary>
        /// Returns avatar arraylist
        /// </summary>
        /// <returns></returns>
        public ArrayList getAvatarArr()
        {
            return avatarArr;
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
        /// Grabs all of the data fields (users, login, avatar, description)
        /// </summary>
        public async Task searchHelper(String searchText, CancellationToken cancel)
        {

            String imageURL = "";
            WebClient tempWebClient = new WebClient();

                using (HttpClient client = CreateClient())
                {
                    HttpResponseMessage response = await client.GetAsync("/search/repositories?q=" + searchText);

                    
                    if (response.IsSuccessStatusCode)
                    {
                        String result = await response.Content.ReadAsStringAsync();
                        dynamic users = JsonConvert.DeserializeObject(result);
                        foreach (dynamic user in users.items)
                        {
                            // Checking for cancellation
                            cancel.ThrowIfCancellationRequested();

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

                            totalSearchElements++;

                        }
                        foreach (String link in response.Headers.GetValues("Link"))
                        {
                            nextlink = link;
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
        /// Collects the next badge of data values
        /// </summary>
        /// <param name="cancel"></param>
        public async Task collectingData()
        {

            //Updates the data
            String imageURL = "";
            WebClient tempWebClient = new WebClient();

                // manipulating the link
                nextlink = nextlink.Replace("<https://api.github.com", "");
                char[] temp = nextlink.ToCharArray();
                string templink = "";
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] == '>')
                        break;

                    templink += temp[i];
                }

                nextlink = templink;

                MessageBox.Show(nextlink);
                using (HttpClient client = CreateClient())
                {
                    HttpResponseMessage response = await client.GetAsync(nextlink);
                    if (response.IsSuccessStatusCode)
                    {
                        String result = await response.Content.ReadAsStringAsync();
                        dynamic users = JsonConvert.DeserializeObject(result);


                        foreach (dynamic user in users.items)
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

                            totalSearchElements++;
                        }

                        foreach (String link in response.Headers.GetValues("Link"))
                        {
                            nextlink = link;
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
        /// Grabs all of the data fields (users, login, avatar, description)
        /// </summary>
        public async Task searchHelperLanguage(string searchLanguage, CancellationToken cancel)
        {

            String imageURL = "";
            WebClient tempWebClient = new WebClient();

                using (HttpClient client = CreateClient())
                {
                    HttpResponseMessage response = await client.GetAsync("/search/repositories?q=language:" + searchLanguage);
                    if (response.IsSuccessStatusCode)
                    {
                        String result = await response.Content.ReadAsStringAsync();
                        dynamic users = JsonConvert.DeserializeObject(result);
                        

                        foreach (dynamic user in users.items)
                        {

                            // Checking for cancellation
                            cancel.ThrowIfCancellationRequested();

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

                            totalSearchElements++;

                        }
                        foreach (String link in response.Headers.GetValues("Link"))
                        {
                            nextlink = link;
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
        /// Grabs all of the data fields (users, login, avatar, description) based on selected star ranking
        /// </summary>
        public async Task searchHelperStars(string searchLanguage, CancellationToken cancel)
        {

            String imageURL = "";
            WebClient tempWebClient = new WebClient();

                using (HttpClient client = CreateClient())
                {
                    HttpResponseMessage response = await client.GetAsync("/search/repositories?q=stars:" + searchLanguage);
                    if (response.IsSuccessStatusCode)
                    {
                        String result = await response.Content.ReadAsStringAsync();
                        dynamic users = JsonConvert.DeserializeObject(result);


                        foreach (dynamic user in users.items)
                        {

                            // Checking for cancellation
                            cancel.ThrowIfCancellationRequested();

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

                            totalSearchElements++;

                        }
                        foreach (String link in response.Headers.GetValues("Link"))
                        {
                            nextlink = link;
                        }


                    }
                    else
                    {
                        Console.WriteLine("Error: " + response.StatusCode);
                        Console.WriteLine(response.ReasonPhrase);
                    }
            
                }
        
        }
    
    }

}
