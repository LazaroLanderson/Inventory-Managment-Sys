using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory_Managment_Sys.Models
{
    internal class Product
    {

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int QuantityOnHand { get; set; }



    }
}
