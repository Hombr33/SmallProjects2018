namespace ProiectASE.Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public float Payment { get; set; }

        public Transaction(int Id, Client Client)
        {
            this.Id = Id;
            this.Client = Client;
        }
    }
}
