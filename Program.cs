using Inventory_Managment_Sys.Services;
using Inventory_Managment_Sys.Models;

InventoryService inventoryService = new InventoryService();


bool running = true;

while (running)
{
    Console.WriteLine("Inventory Management System");
    Console.WriteLine("1. Add Product");
    Console.WriteLine("2. View Products");
    Console.WriteLine("3. Exit");
    Console.Write("Select an option: ");
    string option = Console.ReadLine();
    switch (option)
    {
        case "1":
            AddProduct();
            break;
        case "2":
            ViewProducts();
            break;
        case "3":
            running = false;
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

void AddProduct()
{
    Console.Write("Enter Product Name: ");
    string name = Console.ReadLine();
    Console.Write("Enter Product Description: ");
    string description = Console.ReadLine();
    Console.Write("Enter Product SKU: ");
    string sku = Console.ReadLine();
    Console.Write("Enter Product Price: ");
    
    if (!decimal.TryParse(Console.ReadLine(), out decimal price))
    {
        Console.WriteLine("Invalid price. Please enter a valid decimal number.");
        return;
    }

    Console.Write("Enter Quantity On Hand: ");
    if (!int.TryParse(Console.ReadLine(), out int quantityOnHand))
    {
        Console.WriteLine("Invalid quantity. Please enter a valid integer.");
    }

    if (string.IsNullOrEmpty(name))
    {
        Console.WriteLine("Product name cannot be empty. Please try again.");
        return;
    }

    if (string.IsNullOrEmpty(sku))
    {
        Console.WriteLine("Product sku cannot be empty. Please try again.");
        return;
    }

    if (price < 0)
    {
        Console.WriteLine("Product price cannot be negative. Please try again.");
        return;
    }

    if (quantityOnHand < 0)
    {
        Console.WriteLine("Quantity on hand cannot be negative. Please try again.");
        return;
    }


    if (inventoryService.GetAllProducts().Exists(product => product.SKU == sku))
    {
        Console.WriteLine("Product with the same SKU already exists. Please try again.");
        return;
    }


    Product product = new Product
    {
        Name = name,
        Description = description,
        SKU = sku,
        Price = price,
        QuantityOnHand = quantityOnHand
    };
    inventoryService.AddProduct(product);
    Console.WriteLine("Product added successfully!");
}



void ViewProducts()
{
    List<Product> products = inventoryService.GetAllProducts();
    if (products.Count == 0)
    {
        Console.WriteLine("No products available.");
        return;
    }
    Console.WriteLine("Product List:");
    foreach (var product in products)
    {
        Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Description: {product.Description}, SKU: {product.SKU}, Price: {product.Price}, Quantity On Hand: {product.QuantityOnHand}");
    }
}