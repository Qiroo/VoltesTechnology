using Microsoft.AspNetCore.Identity;

namespace VoltesTechnology.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
