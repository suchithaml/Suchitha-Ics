using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

class Product
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"ID: {ProductId}, Name: {ProductName}, Price: {Price:C}";
    }
}

class Program
{
    static void Main()
    {
        List<Product> products = new List<Product>();

        Console.WriteLine("Enter details for 10 products:");
        for (int i = 0; i < 10; i++)
        {
            Console.Write($"Enter Product ID for product {i + 1}: ");
            string productId = Console.ReadLine();

            Console.Write($"Enter Product Name for product {i + 1}: ");
            string productName = Console.ReadLine();

            decimal price;
            while (true)
            {
                Console.Write($"Enter Price for product {i + 1}: ");
                if (decimal.TryParse(Console.ReadLine(), out price))
                    break;
                else
                    Console.WriteLine("Invalid price. Please enter a valid decimal number.");
            }

            products.Add(new Product
            {
                ProductId = productId,
                ProductName = productName,
                Price = price
            });
        }

        products = products.OrderBy(p => p.Price).ToList();

        Console.WriteLine("\nProducts sorted by price:");
        foreach (Product product in products)
        {
            Console.WriteLine(product);
        }

        Console.WriteLine("\nPress Enter to exit...");
        Console.ReadLine();
    }
}


