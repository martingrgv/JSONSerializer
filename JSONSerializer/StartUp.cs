using MyJSONSerializer.IO;
using MyJSONSerializer.Models;
using MyJSONSerializer.Services;

namespace MyJSONSerializer
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>()
            {
                new Product(1, "Louis Vuitton Dress", 500.50m),
                new Product(2, "Gucci Bag", 300.00m),
                new Product(3, "Louis Vuitton High Heels", 340.00m),
                new Product(4, "Polo Ralph Lauren T-shirt", 90.00m),
                new Product(5, null, 0)
            };

            string productJSON = JSONSerializer.Instance().Serialize(products[0]);

            FileStreamer fs = new FileStreamer("data.json");
            StreamService streamService = new StreamService(fs);
            //streamService.Write(productJSON);

            string output = streamService.Read();
            Console.WriteLine(output);


            Product deserializedProduct = JSONSerializer.Instance().Deserialize<Product>(streamService.Read());
            Console.WriteLine(deserializedProduct);
        }
    }
}
