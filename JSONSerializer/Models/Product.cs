namespace JSONSerializer.Models
{
    public class Product
    {
        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
            Rating = new Rating();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Rating Rating { get; set; }

        public override string ToString()
        {
            return $"{Id}. {Name} - ${Price:F2}";
        }
    }
}
