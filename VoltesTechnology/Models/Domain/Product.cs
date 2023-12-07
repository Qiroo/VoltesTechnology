using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoltesTechnology.Models.Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public decimal? ProductPrice {  get; set; }
        public string? ProductImage { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        [NotMapped]
        [Required]
        public List<int>? Categories { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem>? CategoryList;
        [NotMapped]
        public string? CategoryNames { get; set; }
        [NotMapped]
        public MultiSelectList? MultiCategoryList { get; set; }
    }
}
