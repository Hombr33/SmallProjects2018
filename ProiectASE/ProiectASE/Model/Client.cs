using System.Collections.Generic;

namespace ProiectASE.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Budget { get; set; }
        public List<Product> Products { get; set; }

        public Client(int Id, string Name, float Budget)
        {
            this.Id = Id;
            this.Name = Name;
            this.Budget = Budget;
            this.Products = new List<Product>();
        }
    }
}
