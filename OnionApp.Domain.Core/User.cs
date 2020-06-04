
using Microsoft.AspNetCore.Identity;
namespace OnionApp.Domain.Core
{
    public class User : IdentityUser
    {
    public int IdUser { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Password { get; set; }
    public User DiscountPrice { get; set; }
    public Order UserOrder { get; set; }

    }
}
