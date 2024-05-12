namespace JSONSerializer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Product product = new Product(1, "Nike Air Force 1", 250);

            List<Product> products = new List<Product>()
            {
                new Product(1, "Louis Vuitton Dress", 500.50m),
                new Product(2, "Gucci Bag", 300.00m),
                new Product(3, "Louis Vuitton High Heels", 340.00m),
                new Product(4, "Polo Ralph Lauren T-shirt", 90.00m),
                new Product(5, null, 0)
            };

            string productJSON = JSONService.Serialize(products);

            StreamService.Write(productJSON);

            string output = StreamService.Read();
            Console.WriteLine(output);
        }
    }
}
