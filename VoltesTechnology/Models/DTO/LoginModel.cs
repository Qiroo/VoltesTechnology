using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace VoltesTechnology.Models.DTO
{
    public class LoginModel
    {
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
