using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Managment_Sys.Models
{
    internal class InventoryTransaction
    {
        // Identificador
        public string SKU { get; set; }

        // Receive ou Ship
        public string TransactionType { get; set; }

        // Quantidade de produtos recebidos ou enviados
        public int Quantity { get; set; }

        // Data e hora da transação
        public DateTime CreatedAt { get; set; }
    }
}
