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

    }
}
