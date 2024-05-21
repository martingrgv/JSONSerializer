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

            List<Customer> customers = new List<Customer>()
            {
                new Customer(1, "Petio", "Ivanov", "petio.ivanov@gmail.com"),
                new Customer(2, "Georgi", "Georgiev", "georgi.gg@gmail.com", 1, 1),
                new Customer(3, "Mitko", "Libkov", "mitaka.maafaka@gmail.com", 3, 105),
                new Customer(4, "Ivayla", "Peshova", "ivka.beibe@gmail.com", 5, 359),
                new Customer(5, "Nai-divia", "Opasen", "nzkudesum.emaila@gmail.com", 0, 0),
            };

            //string productJSON = JSONSerializer.Instance().Serialize(products[0]);
            string productJSON = JSONSerializer.Instance().Serialize(products);

            FileStreamer productsFileStreamer = new FileStreamer("products.json");
            FileStreamer customersFileStreamer = new FileStreamer("customer.json");

            StreamService streamService = new StreamService(productsFileStreamer);
            streamService.Write(productJSON);

            string output = streamService.Read();

            //string customersJSON = JSONSerializer.Instance().Serialize(customers[0]);
            string customersJSON = JSONSerializer.Instance().Serialize(customers);

            streamService = new StreamService(customersFileStreamer);
            streamService.Write(customersJSON);

            output += Environment.NewLine + streamService.Read();
            Console.WriteLine(output);

            streamService = new StreamService(productsFileStreamer);
            Product product = JSONSerializer.Instance().Deserialize<Product>(streamService.Read());

            streamService = new StreamService(customersFileStreamer);
            Customer customer = JSONSerializer.Instance().Deserialize<Customer>(streamService.Read());
            Console.WriteLine(product);
            Console.WriteLine(customer);
        }
    }
}
