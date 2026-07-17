using System;
using System.Collections.Generic;
using System.Text;
using Inventory_Managment_Sys.Models;
using System.Text.Json;

namespace Inventory_Managment_Sys.Data
{
    internal class ProductRepository
    {


        public ProductRepository()
        {
            Directory.CreateDirectory("Data");

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        private readonly string filePath = Path.Combine("Data", "Products.json");

        public List<Product> LoadProducts()
        {

            Console.WriteLine("Reading products...");

            try
            {

                string json = File.ReadAllText(filePath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<Product>();
                }

                List<Product>? products = JsonSerializer.Deserialize<List<Product>>(json);
                return products ?? new List<Product>();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error loading products: {ex.Message}");
                return new List<Product>();

            }


        }


        public void SaveProducts(List<Product> products)
        {

            Console.WriteLine("Saving products...");

            try
            {

                string json = JsonSerializer.Serialize(products);
                File.WriteAllText(filePath, json);

                Console.WriteLine("Products saved successfully.");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error saving products: {ex.Message}");
            }


        }



    }
}
