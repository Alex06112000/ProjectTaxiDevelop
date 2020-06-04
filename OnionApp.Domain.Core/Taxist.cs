namespace OnionApp.Domain.Core
{
    public class Taxist
    {
        public int IdTaxist { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IdCar { get; set; }
        public User TaxiCar { get; set; }
        public Order GlobalOrders { get; set; }
    }
}
