namespace OnionApp.Domain.Core
{
    public class Car
    {
        public int IdCar { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public Taxist Taxist { get; set; }
    }
}
