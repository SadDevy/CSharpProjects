using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebAPI.Models;

namespace ConsoleUI
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowProduct(Product product)
        {
            Console.WriteLine($"Name: {product.ProductName}");
        }

        static async Task<Uri> CreateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "webapi/api/product", product);
            response.EnsureSuccessStatusCode();

            return response.Headers.Location;
        }

        static async Task<Product> GetProductAsync(string path)
        {
            Product product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
                product = await response.Content.ReadFromJsonAsync<Product>();

            return product;
        }

        static async Task UpdateProductAsync(Product product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"webapi/api/product/{product.ProductId}", product);
            response.EnsureSuccessStatusCode();
        }

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"webapi/api/product/{id}");
            return response.StatusCode;
        }

        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost/WebAPI/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Product product = new Product
                {
                    ProductName = "Some",
                    Discontinued = false
                };

                var url = await CreateProductAsync(product);
                Console.WriteLine($"Created at {url}");

                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                Console.WriteLine("Updating name...");
                product.ProductName = "No Some";
                await UpdateProductAsync(product);

                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                var statusCode = await DeleteProductAsync(product.ProductId.ToString());
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
