using JSONSerializer.Models;
using System.CodeDom.Compiler;
using System.Text.Json;

namespace JSONLibraryReference
{
    public class Program
    {
        static void Main(string[] args)
        {
            // See how to make deep copy

            List<Product> products = new List<Product>()
            {
                new Product(1, "Louis Vuitton Dress", 500.50m),
                new Product(2, "Gucci Bag", 300.00m),
                new Product(3, "Louis Vuitton High Heels", 340.00m),
                new Product(4, "Polo Ralph Lauren T-shirt", 90.00m),
                new Product(5, null, 0)
            };

            var options = new JsonSerializerOptions(){ WriteIndented = true };
            string productsJSON = JsonSerializer.Serialize(products, options);
            Console.WriteLine(productsJSON);
        }
    }
}
