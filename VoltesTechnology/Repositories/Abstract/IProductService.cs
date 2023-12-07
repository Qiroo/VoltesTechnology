using VoltesTechnology.Models.Domain;
using VoltesTechnology.Models.DTO;

namespace VoltesTechnology.Repositories.Abstract
{
    public interface IProductService
    {
        bool Add(Product model);
        bool Update(Product model);
        bool Delete(int id);
        Product GetById(int id);
        ProductListViewModel List(string term = "", bool paging = false, int currentPage = 0);
        List<int> GetCategoryByProductId(int productId);
    }
}
