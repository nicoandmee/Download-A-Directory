using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using HtmlAgilityPack;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Net;

namespace Download_A_Directory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                     //Initializes HTML Document, parses user input and begins dissecting meta tags.
            string url = textBox1.Text.ToString();
            HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
            html = LoadHtml(url);


            //Get the links.
            string[] hrefs;
            hrefs = GetLinks(html);


            //Download the files.
            try
            {


                foreach (string b in hrefs)
                {

                    var chartest = new Regex("^[A-Za-z][A-Za-z0-9!@#$%^&._*]*$");
                    if (chartest.IsMatch(b))
                    {

                        WebClient webclient = new WebClient();
                        webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                        webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progressBar12_Click);
                        webclient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                        
                        
                        //Checks if user inputted different download path.
                        string downloadpath;
                        if (textBox2.Text.ToString() == "")
                        {

                             downloadpath = @"c:\" + b;
                        }
                        else
                        {
                            downloadpath = @"c:\" + textBox2.Text.ToString() + b;
                        }

                        webclient.DownloadFileAsync(new Uri(url + b), downloadpath);
                   




                    }
                }

            }







            catch (Exception f)
            {
                MessageBox.Show("An error occured:", f.ToString());
            }



           

        }

        private static string[] GetLinks(HtmlAgilityPack.HtmlDocument html)
        {


            List<string> hrefstring = new List<string>();


            foreach (HtmlNode link in html.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute tag = link.Attributes["href"];
                hrefstring.Add(tag.Value);
            }
            string[] hrefs = hrefstring.ToArray();
            return hrefs;
            
        }

        private static HtmlAgilityPack.HtmlDocument LoadHtml(string url)
        {


            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument html = web.Load(url);

            List<string> hrefstring = new List<string>();
            return html;
        }

        private void progressBar12_Click(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }


        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Usage tips: Enter url in format- http://fakesite.net/directory/" + Environment.NewLine + "For custom folders: enter path in C: drive that you want to download files to." + Environment.NewLine + "ALWAYS RUN AS ADMINISTRATOR. If you do not the program cannot write to your C: drive in many cases.");
        }


        }
    }

