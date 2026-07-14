using System;
using System.Collections.Generic;
using System.Text;
using Inventory_Managment_Sys.Models;

namespace Inventory_Managment_Sys.Services
{
    internal class InventoryService
    {

        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {

            products.Add(product);



        }


        public List<Product> GetAllProducts()
        {
            return products;
        }

        public Product? SearchProductBySku(string sku)

        {
            return products.Find(product => product.SKU == sku);
        }

        public List<Product> SearchProductsByName(string name)
        {
            return products.FindAll(product => product.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }

        public bool UpdateProductPrice(string sku, decimal newPrice)

        {
            Product? Product = SearchProductBySku(sku);

            if (Product == null)
            {
                return false;
            } 

            if (newPrice < 0)
            {
                return false;
            }

            Product.Price = newPrice;
            return true;
        }



        public bool UpdateProductQuantity(string sku, int newQuantity)
        {
            Product? product = SearchProductBySku(sku);
            if (product == null)
            {
                return false;
            }
            if (newQuantity < 0)
            {
                return false;
            }
            product.QuantityOnHand = newQuantity;
            return true;
        }

       

    }
}
