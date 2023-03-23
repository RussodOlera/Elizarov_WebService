using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Elizarov_WebService
{
    public partial class Menu1 : Form
    {
        public Menu1()
        {
            InitializeComponent();
            APOD.init_menu1();
            label1.Visible = false;
            label2.Visible = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            richTextBox1.Text = "";
            data_APOD date =null;

            string data = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
            date = await APOD.GetAlbumAsync("/planetary/apod?date=" + data+ "&api_key=DEMO_KEY");
            if (date != null)
            {
                label2.Text = (date.url);
                richTextBox1.Text = date.explanation.ToString();
                label5.Text = date.title.ToString();
            }
                
            label1.Visible = true;
            label2.Visible = true;

            string url = date.url.ToString();
            byte[] image = (new WebClient()).DownloadData(url);
            Image a = ((Func<Image>)(() =>
            {
                using (var ms = new MemoryStream(image))
                {
                    return Image.FromStream(ms);
                }
            }))();

            pictureBox1.Image = a;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }

    class data_APOD
    {
        public string title { get; set; }
        public string date { get; set; }
        public string explanation { get; set; }
        public string url { get; set; }
        public string copyright { get; set; }
        public string hdurl { get; set; }
        public string media_type { get; set; }
        public string service_version { get; set; }

        override
        public string ToString()
        {
            return "Title: " + title + " Date: " + date + " Explanation: " + explanation + " URL: " + url;
        }
    }

    class APOD
    {
        static HttpClient client = new HttpClient();

        public static void init_menu1()
        {
            client.BaseAddress = new Uri("https://api.nasa.gov");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<data_APOD> GetAlbumAsync(string path)
        {
            data_APOD product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await JsonSerializer.DeserializeAsync<data_APOD>(await response.Content.ReadAsStreamAsync());
            }
            return product;
        }
    }
}
