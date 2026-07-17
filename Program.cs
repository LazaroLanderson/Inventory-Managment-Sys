using Inventory_Managment_Sys.Services;
using Inventory_Managment_Sys.Models;
using Inventory_Managment_Sys.Data;

ProductRepository productRepository = new ProductRepository();
TransactionRepository transactionRepository = new TransactionRepository();

InventoryService inventoryService = new InventoryService(productRepository, transactionRepository);

Console.WriteLine("Welcome to the Inventory Management System!");


bool running = true;

while (running)
{
    Console.WriteLine("Inventory Management System");
    Console.WriteLine("1. Add Product");
    Console.WriteLine("2. View Products");
    Console.WriteLine("3. View Products Details");
    Console.WriteLine("4. Search Products by Name");
    Console.WriteLine("5. Update Product Price");
    Console.WriteLine("6. Receive Inventory");
    Console.WriteLine("7. Ship Inventory");
    Console.WriteLine("8. View In Stock Products");
    Console.WriteLine("9. Get Low Stock Products");
    Console.WriteLine("10. Get Product Inventory Value");
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
            ViewProductDetails();
            break;
        case "4":
            SearchProductsByName();
            break;
        case "5":
            UpdateProductPrice();
            break;
        case "6":
            ReceiveInventory();
            break;
        case "7":
            ShipInventory();
            break;
        case "8":
            ViewInStockProducts();
            break;
        case "9":
            GetLowStockProducts();
            break;
        case "10":
            GetProductInventoryValue();
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
        return;
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


    if (inventoryService.ProductExists(sku))
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
        DisplayProduct(product);
    }
}

void ViewProductDetails()
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
    DisplayProduct(product);
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
        DisplayProduct(product);
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


void ReceiveInventory()
{
    Console.Write("Enter Product SKU: ");
    string sku = Console.ReadLine();

    Console.Write("Enter quantity received: ");

    if (!int.TryParse(Console.ReadLine(), out int quantityReceived))

    {
        Console.WriteLine("Invalid quantity. Please enter a valid integer.");
        return;
    }

    bool sucess = inventoryService.ReceiveInventory(sku, quantityReceived);

    if (sucess)
    {
        Console.WriteLine("Inventory received sucessfully!");
    }
    else
    {
        Console.WriteLine("Failed to receive inventory. Please check the SKU and make sure the quantity is positive.");
    }

}


void ShipInventory()
{
    Console.Write("Enter Product SKU: ");
    string sku = Console.ReadLine();
    Console.Write("Enter quantity to ship: ");
    if (!int.TryParse(Console.ReadLine(), out int quantityToShip))
    {
        Console.WriteLine("Invalid quantity. Please enter a valid integer.");
        return;
    }
    bool success = inventoryService.ShipInventory(sku, quantityToShip);
    if (success)
    {
        Console.WriteLine("Inventory shipped successfully!");
    }
    else
    {
        Console.WriteLine("Failed to ship inventory. Please check the SKU and make sure the quantity is positive and does not exceed the available quantity.");
    }
}

void DisplayProduct(Product product)
{
    Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Description: {product.Description}, SKU: {product.SKU}, Price: {product.Price}, Quantity On Hand: {product.QuantityOnHand}");
}

void ViewInStockProducts()
{
    List<Product> products = inventoryService.GetInStockProducts();
    if (products.Count == 0)
    {
        Console.WriteLine("No products in stock.");
        return;
    }
    Console.WriteLine("In Stock Products:");
    foreach (var product in products)
    {
        DisplayProduct(product);
    }
}

void GetLowStockProducts()
{
    Console.Write("Enter stock threshold: ");
    if (!int.TryParse(Console.ReadLine(), out int threshold))
    {
        Console.WriteLine("Invalid threshold. Please enter a valid integer.");
        return;
    }
    List<Product> products = inventoryService.GetLowStockProducts(threshold);
    if (products.Count == 0)
    {
        Console.WriteLine("No products below the specified stock threshold.");
        return;
    }
    Console.WriteLine($"Products below stock threshold of {threshold}:");
    foreach (var product in products)
    {
        DisplayProduct(product);
    }
}

void GetProductInventoryValue()
{
    Console.Write("Enter Product SKU to get inventory value: ");
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
    decimal inventoryValue = inventoryService.GetProductInventoryValue(product);
    Console.WriteLine($"Inventory value for product {product.Name} (SKU: {product.SKU}): {inventoryValue}");

    decimal totalInventoryValue = inventoryService.GetTotalInventoryValue();
    Console.WriteLine($"Total inventory value: {totalInventoryValue}");
}