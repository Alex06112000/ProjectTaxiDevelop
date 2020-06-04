using Microsoft.AspNetCore.Identity;
namespace OnionApp.Domain.Core
{
    public class Order
    {   public string From { get; set; }
        public string To { get; set; }
        public double Price { get; set; }
        public int IdUser { get; set; }
        public int IdTaxist { get; set; }
        public IdentityUser User { get; set; }
        public Taxist Taxist { get; set; }
    }
}
