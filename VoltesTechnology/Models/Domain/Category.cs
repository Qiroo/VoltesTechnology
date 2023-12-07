using System.ComponentModel.DataAnnotations;

namespace VoltesTechnology.Models.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? CategoryName { get; set; }
    }
}
