﻿using System;
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
    public partial class Menu3 : Form
    {
        public Menu3()
        {
            InitializeComponent();
            listBox2.Visible = false;
            listBox3.Visible = false;
            listBox4.Visible = false;
            label2.Visible = false;
            label7.Visible = false;
            label3.Visible = false;
            EPIC.init_menu1();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            listBox1.Items.Clear();
            List<Data_EPIC> date = null;
            string mese = dateTimePicker1.Value.Month.ToString();
            string giorno = dateTimePicker1.Value.Day.ToString();
            if (Convert.ToInt32(mese) < 10)
                mese = "0" + mese;
            if (Convert.ToInt32(giorno) < 10)
                giorno = "0" + giorno;
            string data = dateTimePicker1.Value.Year.ToString() + "-" + mese + "-" + giorno;
            string collection = comboBox1.Text;
            string type = comboBox2.Text;

            date = await EPIC.GetAlbumAsync("/EPIC/api/"+collection+"/date/"+data+"?api_key=DEMO_KEY");

            if(date!=null)
            {
                for (int i = 0; i < date.Count(); i++)
                {
                    listBox1.Items.Add(date[i].image);
                    listBox2.Items.Add(date[i].caption);
                    listBox3.Items.Add(date[i].centroid_coordinates.lat);
                    listBox4.Items.Add(date[i].centroid_coordinates.lon);
                }
            }
            
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string anno = dateTimePicker1.Value.Year.ToString();
            string mese = dateTimePicker1.Value.Month.ToString();
            string giorno = dateTimePicker1.Value.Day.ToString();
            string caption = listBox2.Items[listBox1.SelectedIndex].ToString();
            string lat = listBox3.Items[listBox1.SelectedIndex].ToString();
            string lon = listBox4.Items[listBox1.SelectedIndex].ToString();
            if (Convert.ToInt32(mese) < 10)
                mese = "0" + mese;
            if (Convert.ToInt32(giorno) < 10)
                giorno = "0" + giorno;
            string collection = comboBox1.Text;
            string type = comboBox2.Text;
            string url = "https://api.nasa.gov/EPIC/archive/" + collection + "/" + anno + "/" + mese + "/" + giorno + "/" + type+"/"+listBox1.SelectedItem.ToString() + "." + type + "?api_key=DEMO_KEY";
            byte[] image = (new WebClient()).DownloadData(url);
            Image a = ((Func<Image>)(() =>
            {
                using (var ms = new MemoryStream(image))
                {
                    return Image.FromStream(ms);
                }
            }))();
            label2.Visible = true;
            label3.Visible = true;
            label7.Visible = true;
            pictureBox1.Image = a;
            richTextBox1.Text = caption;
            label7.Text = lat;
            label3.Text = lon;
            label2.Text = url;
        }
    }
    public class AttitudeQuaternions
    {
        public double q0 { get; set; }
        public double q1 { get; set; }
        public double q2 { get; set; }
        public double q3 { get; set; }
    }

    public class CentroidCoordinates
    {
        public double lat { get; set; }
        public double lon { get; set; }
    }

    public class Coords
    {
        public CentroidCoordinates centroid_coordinates { get; set; }
        public DscovrJ2000Position dscovr_j2000_position { get; set; }
        public LunarJ2000Position lunar_j2000_position { get; set; }
        public SunJ2000Position sun_j2000_position { get; set; }
        public AttitudeQuaternions attitude_quaternions { get; set; }
    }

    public class DscovrJ2000Position
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }

    public class LunarJ2000Position
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }

    public class Data_EPIC
    {
        public string identifier { get; set; }
        public string caption { get; set; }
        public string image { get; set; }
        public string version { get; set; }
        public CentroidCoordinates centroid_coordinates { get; set; }
        public DscovrJ2000Position dscovr_j2000_position { get; set; }
        public LunarJ2000Position lunar_j2000_position { get; set; }
        public SunJ2000Position sun_j2000_position { get; set; }
        public AttitudeQuaternions attitude_quaternions { get; set; }
        public string date { get; set; }
        public Coords coords { get; set; }
    }

    public class SunJ2000Position
    {
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
    }


    class EPIC
    {
        static HttpClient client = new HttpClient();

        public static void init_menu1()
        {
            client.BaseAddress = new Uri("https://api.nasa.gov");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task <List<Data_EPIC>> GetAlbumAsync(string path)
        {
            List<Data_EPIC> product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await JsonSerializer.DeserializeAsync<List<Data_EPIC>>(await response.Content.ReadAsStreamAsync());
            }
            return product;
        }
    }
}
