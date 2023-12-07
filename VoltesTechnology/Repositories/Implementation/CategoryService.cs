using VoltesTechnology.Models.Domain;
using VoltesTechnology.Repositories.Abstract;

namespace VoltesTechnology.Repositories.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly DatabaseContext ctx;
        public CategoryService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Category model)
        {
            try
            {
                ctx.Categories.Add(model);
                ctx.SaveChanges();
                return true;
            }
            catch(Exception ex) 
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                {
                    return false;
                }
                ctx.Categories.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Category GetById(int id)
        {
            return ctx.Categories.Find(id);
        }

        public IQueryable<Category> List()
        {
            var data = ctx.Categories.AsQueryable();
            return data;
        }

        public bool Update(Category model)
        {
            try
            {
                ctx.Categories.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
