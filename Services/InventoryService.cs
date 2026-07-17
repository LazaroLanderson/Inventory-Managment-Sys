using System;
using System.Collections.Generic;
using System.Text;
using Inventory_Managment_Sys.Models;
using Inventory_Managment_Sys.Data;

namespace Inventory_Managment_Sys.Services
{
    internal class InventoryService : IInventoryService
    {

        // Campos privados para armazenar produtos e trans
        private List<Product> products;
        private readonly ProductRepository productRepository;
        private List<InventoryTransaction> transactions;
        private readonly TransactionRepository transactionRepository;

        public void AddProduct(Product product)
        {

            products.Add(product);
            productRepository.SaveProducts(products);



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
            Product? product = SearchProductBySku(sku);

            if (product == null)
            {
                return false;
            } 

            if (newPrice < 0)
            {
                return false;
            }

            product.Price = newPrice;

            productRepository.SaveProducts(products);

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

            productRepository.SaveProducts(products);

            InventoryTransaction transaction = new InventoryTransaction
            {
                SKU = sku,
                TransactionType = "Receive",
                Quantity = quantityReceived,
                CreatedAt = DateTime.Now
            };

            transactions.Add(transaction);
            transactionRepository.SaveTransactions(transactions);

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

            productRepository.SaveProducts(products);

            InventoryTransaction transaction = new InventoryTransaction
            {
                SKU = sku,
                TransactionType = "Ship",
                Quantity = quantityToShip,
                CreatedAt = DateTime.Now
            };

            transactions.Add(transaction);
            transactionRepository.SaveTransactions(transactions);

            return true;
        }


        public bool ProductExists(string sku)
        {
            return products.Exists(product => product.SKU == sku);
        }


        public List<Product> GetInStockProducts()
        {
            return products.FindAll(product => product.QuantityOnHand > 0);
        }

        public List<Product> GetLowStockProducts(int threshold)
        {
            return products.FindAll(product => product.QuantityOnHand < threshold);
        }

        public decimal GetTotalInventoryValue()
        {
            decimal totalValue = 0;
            foreach (var product in products)
            {
                totalValue += product.Price * product.QuantityOnHand;
            }
            return totalValue;
        }

        public decimal GetProductInventoryValue(Product product)
        {
            return product.Price * product.QuantityOnHand;
        }




        public InventoryService(ProductRepository productRepository, TransactionRepository transactionRepository)
        {
            this.productRepository = productRepository;
            this.transactionRepository = transactionRepository;
            products = productRepository.LoadProducts();
            transactions = transactionRepository.LoadTransactions();
        }


    }
}
