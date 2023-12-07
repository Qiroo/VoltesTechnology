using VoltesTechnology.Models.Domain;

namespace VoltesTechnology.Models.DTO
{
    public class ProductListViewModel
    {
        public IQueryable<Product> ProductList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int? Term { get; set; }
    }
}
