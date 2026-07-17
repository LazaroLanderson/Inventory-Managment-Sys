using System;
using System.Collections.Generic;
using System.Text;
using Inventory_Managment_Sys.Models;
using System.Text.Json;


namespace Inventory_Managment_Sys.Data
{
    internal class TransactionRepository
    {

        private readonly string filePath = Path.Combine("Data", "Transactions.json");


        public TransactionRepository()
        {
            Directory.CreateDirectory("Data");
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }

        public List<InventoryTransaction> LoadTransactions()
        {

            Console.WriteLine("Reading transactions...");

            try
            {
                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<InventoryTransaction>();
                }
                List<InventoryTransaction>? transactions = JsonSerializer.Deserialize<List<InventoryTransaction>>(json);
                return transactions ?? new List<InventoryTransaction>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading transactions: {ex.Message}");
                return new List<InventoryTransaction>();
            }
        }


        public void SaveTransactions(List<InventoryTransaction> transactions)
        {
            Console.WriteLine("Saving transactions...");

            try
            {
                string json = JsonSerializer.Serialize(transactions);
                File.WriteAllText(filePath, json);

                Console.WriteLine("Products saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving transactions: {ex.Message}");
            }
        }

    }
}
