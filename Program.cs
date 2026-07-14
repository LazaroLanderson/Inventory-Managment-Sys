using Inventory_Managment_Sys.Services;
using Inventory_Managment_Sys.Models;

InventoryService inventoryService = new InventoryService();


bool running = true;

while (running)
{
    Console.WriteLine("Inventory Management System");
    Console.WriteLine("1. Add Product");
    Console.WriteLine("2. View Products");
    Console.WriteLine("3. Search Product by SKU");
    Console.WriteLine("4. Search Products by Name");
    Console.WriteLine("5. Update Product Price");
    Console.WriteLine("6. Update Product Quantity");
    Console.WriteLine("0. Exit");
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
            SearchProductBySku();
            break;
        case "4":
            SearchProductsByName();
            break;
        case "5":
            UpdateProductPrice();
            break;
        case "6":
            UpdateProductQuantity();
            break;
        case "0":
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

void SearchProductBySku()
{
    Console.Write("Enter Product SKU to search: ");
    string sku = Console.ReadLine();

    if (string.IsNullOrEmpty(sku))

    {
        Console.WriteLine("Product SKU cannot be empty. Please try again.");
        return;
    }

    Product? product = inventoryService.SearchProductBySku(sku);
    if (product == null)
    {
        Console.WriteLine("Product not found.");
        return;
    }
    Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Description: {product.Description}, SKU: {product.SKU}, Price: {product.Price}, Quantity On Hand: {product.QuantityOnHand}");
}

void SearchProductsByName()
{
    Console.Write("Enter Product Name to search: ");
    string name = Console.ReadLine();
    if (string.IsNullOrEmpty(name))
    {
        Console.WriteLine("Product name cannot be empty. Please try again.");
        return;
    }
    List<Product> products = inventoryService.SearchProductsByName(name);
    if (products.Count == 0)
    {
        Console.WriteLine("No products found with the given name.");
        return;
    }
    Console.WriteLine("Search Results:");
    foreach (var product in products)
    {
        Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Description: {product.Description}, SKU: {product.SKU}, Price: {product.Price}, Quantity On Hand: {product.QuantityOnHand}");
    }
}


void UpdateProductPrice()
{
    Console.Write("Enter Product SKU to update price: ");
    string sku = Console.ReadLine();
    if (string.IsNullOrEmpty(sku))
    {
        Console.WriteLine("Product SKU cannot be empty. Please try again.");
        return;
    }
    Console.Write("Enter new price: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice))
    {
        Console.WriteLine("Invalid price. Please enter a valid decimal number.");
        return;
    }
    bool success = inventoryService.UpdateProductPrice(sku, newPrice);
    if (success)
    {
        Console.WriteLine("Product price updated successfully!");
    }
    else
    {
        Console.WriteLine("Failed to update product price. Please check the SKU and make sure thr price is not negative.");
    }
}

void UpdateProductQuantity()
{
    Console.Write("Enter Product SKU to update quantity: ");
    string sku = Console.ReadLine();
    if (string.IsNullOrEmpty(sku))
    {
        Console.WriteLine("Product SKU cannot be empty. Please try again.");
        return;
    }
    Console.Write("Enter new quantity: ");
    if (!int.TryParse(Console.ReadLine(), out int newQuantity))
    {
        Console.WriteLine("Invalid quantity. Please enter a valid integer.");
        return;
    }
    bool success = inventoryService.UpdateProductQuantity(sku, newQuantity);
    if (success)
    {
        Console.WriteLine("Product quantity updated successfully!");
    }
    else
    {
        Console.WriteLine("Failed to update product quantity. Please check the SKU and make sure the quantity is not negative.");
    }
}