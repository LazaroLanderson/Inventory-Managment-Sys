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


        public bool ReceiveInventory(string sku, int quantityReceived)
        {
            Product? product = SearchProductBySku(sku);
            if (product == null)
            {
                return false;
            }
            if (quantityReceived <= 0)
            {
                return false;
            }
            product.QuantityOnHand += quantityReceived;
            return true;
        }


        public bool ShipInventory(string sku, int quantityToShip)

        {
            Product? product = SearchProductBySku(sku);
            if (product == null)
            {
                return false;
            }
            if (quantityToShip <= 0)
            {
                return false;
            }

            if (quantityToShip > product.QuantityOnHand)
            {
                return false;
            }

            product.QuantityOnHand -= quantityToShip;
            return true;
        }


        public bool productExists(string sku)
        {
            return products.Exists(product => product.SKU == sku);
        }


    }
}
