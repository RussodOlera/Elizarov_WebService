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
using Elizarov_WebService.Properties;

namespace Elizarov_WebService
{
    public partial class Menu2 : Form
    {
        public Menu2()
        {
            InitializeComponent();
            Earth.init_menu1();
            label2.Visible = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            data_Earth date = null;
            string data = dateTimePicker1.Value.Year.ToString() + "-" + dateTimePicker1.Value.Month.ToString() + "-" + dateTimePicker1.Value.Day.ToString();
            date = await Earth.GetAlbumAsync("planetary/earth/assets?lon=" + textBox1.Text + "&lat=" + textBox2.Text + "&date=" + data + "&dim=0.15&api_key=DEMO_KEY");
            if (date != null)
            {
                label2.Visible = true;
                label2.Text = date.url;
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
            
        
        }
    }

    public class Resource
    {
        public string dataset { get; set; }
        public string planet { get; set; }
    }
    class data_Earth
    {
        public string date { get; set; }
        public string id { get; set; }
        public Resource resource { get; set; }
        public string url { get; set; }
    }
    
    class Earth
    {
        static HttpClient client = new HttpClient();

        public static void init_menu1()
        {
            client.BaseAddress = new Uri("https://api.nasa.gov");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        static void ShowProduct(data_Earth album)
        {
            MessageBox.Show(album.ToString());
        }
        public static async Task<data_Earth> GetAlbumAsync(string path)
        {
            data_Earth product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await JsonSerializer.DeserializeAsync<data_Earth>(await response.Content.ReadAsStreamAsync());
            }
            return product;

        }
    }

}
