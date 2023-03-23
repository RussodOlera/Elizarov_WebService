/*using System;
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

namespace Elizarov_WebService
{
    internal class ClientREST
    {
            static HttpClient client = new HttpClient();
        
            public static void init_menu1()
            {
                client.BaseAddress = new Uri("https://api.nasa.gov");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("planetary/apod"));

            }

            static void ShowProduct(data1 album)
            {
                Console.WriteLine(album.ToString());
            }

            public static async Task<data1> GetAlbumAsync(string path)
            {
                data1 product = null;
                HttpResponseMessage response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    product = await JsonSerializer.DeserializeAsync<data1>(await response.Content.ReadAsStreamAsync());
                }
                return product;
            }

            static async Task<Uri> CreateProductAsync(data1 product)
            {
                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "/albums", product);
                response.EnsureSuccessStatusCode();

                // return URI of the created resource.
                return response.Headers.Location;
            }

            *//*static async Task<data1> UpdateProductAsync(data1 product)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync(
                    $"/albums/{product.id}", product);
                response.EnsureSuccessStatusCode();

                // Deserialize the updated product from the response body.
                product = await JsonSerializer.DeserializeAsync<data1>(await response.Content.ReadAsStreamAsync());
                return product;
            }*/

            /*static async Task<HttpStatusCode> DeleteProductAsync(string id)
            {
                HttpResponseMessage response = await client.DeleteAsync(
                    $"/albums/{id}");
                return response.StatusCode;
            }*/


            /*static void Main()
            {
                RunAsync().GetAwaiter().GetResult();
            }*//*

            static async Task RunAsync()
            {
                client.BaseAddress = new Uri("https://api.nasa.gov");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("planetary/apod"));


        *//*public string title { get; set; }
        public string date { get; set; }
        public string explanation { get; set; }
        public string url { get; set; }*//*

       // es: https://api.nasa.gov/planetary/apod?date=2023-03-07&api_key=DEMO_KEY
            try
                {
                    data1 album = null;
                string path = "/date=";
                    // Get an existing product
                    album = await GetAlbumAsync("/date=");
                    ShowProduct(album);

                    *//*//Create a new product
                    album = new data1();
                    //album.id = 99; album.title = "Test"; album.userId = 1;
                    var url = await CreateProductAsync(album);
                    Console.WriteLine($"New album reated at {url}");
                    *//*
                    album = await GetAlbumAsync(url);
                    ShowProduct(album);*//*

                    // Update the product
                    Console.WriteLine("Updating title...");
                    album.title = "Test2";
                    await UpdateProductAsync(album);

                    // Delete the product
                    var statusCode = await DeleteProductAsync(album.id + "");
                    Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");*//*

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.ReadLine();
            }
    }
}
*/