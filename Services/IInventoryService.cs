using System;
using System.Collections.Generic;
using System.Text;
using Inventory_Managment_Sys.Models;

namespace Inventory_Managment_Sys.Services
{
    internal interface IInventoryService
    {

        void AddProduct(Product product);
        List<Product> GetAllProducts();
        Product? SearchProductBySku(string sku);
        List<Product> SearchProductsByName(string name);
        List<Product> GetInStockProducts();
        bool UpdateProductPrice(string sku, decimal newPrice);
        bool ReceiveInventory(string sku, int quantityReceived);
        bool ShipInventory(string sku, int quantityShipped);
        bool ProductExists(string sku);


    }
}
