using System;

namespace ProiectASE.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime ProductionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ClientId { get; set; }

        public Product(int Id, string Name, float Price, DateTime ProductionDate, DateTime ExpirationDate)
        {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
            this.ProductionDate = ProductionDate;
            this.ExpirationDate = ExpirationDate;
            this.ClientId = -1;
        }
    }
}
