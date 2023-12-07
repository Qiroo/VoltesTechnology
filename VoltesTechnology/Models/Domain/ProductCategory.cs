using System.ComponentModel.DataAnnotations;

namespace VoltesTechnology.Models.Domain
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }
}
