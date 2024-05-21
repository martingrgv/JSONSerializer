namespace MyJSONSerializer.Models
{
    public class Customer
    {
        public Customer()
        {
            
        }

        public Customer(int id, string firstName, string lastName, string email) 
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public Customer(int id, string firstName, string lastName, string email, int productsInDeliverCount, int boughtProductsCount) : this(id, firstName, lastName, email)
        {
            ProductsInDeliverCount = productsInDeliverCount;
            BoughtProductsCount = boughtProductsCount;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int ProductsInDeliverCount { get; set; }
        public int BoughtProductsCount { get; set; }

        //public Rating Rating { get; set; }

        public override string ToString()
        {
            return "First Name";
        }
    }
}
