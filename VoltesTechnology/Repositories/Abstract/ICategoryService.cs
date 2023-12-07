using VoltesTechnology.Models.Domain;
using VoltesTechnology.Models.DTO;

namespace VoltesTechnology.Repositories.Abstract
{
    public interface ICategoryService
    {
        bool Add(Category model);
        bool Update(Category model);
        bool Delete(int id);
        Category GetById(int id);
        IQueryable<Category> List();
    }
}
