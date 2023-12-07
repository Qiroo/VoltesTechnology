using VoltesTechnology.Models.Domain;
using VoltesTechnology.Models.DTO;
using VoltesTechnology.Repositories.Abstract;

namespace VoltesTechnology.Repositories.Implementation
{
    public class ProductService : IProductService
    {
        private readonly DatabaseContext ctx;
        public ProductService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }
        public bool Add(Product model)
        {
            try
            {
                ctx.Products.Add(model);
                ctx.SaveChanges();
                foreach (int categoryId in model.Categories)
                {
                    var productCategory = new ProductCategory
                    {
                        ProductId = model.Id,
                        CategoryId = categoryId
                    };
                    ctx.ProductCategories.Add(productCategory);
                }
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
                var productCategories = ctx.ProductCategories.Where(a=>a.ProductId == data.Id);
                foreach(var productCategory in productCategories)
                {
                    ctx.ProductCategories.Remove(productCategory);
                }
                ctx.Products.Remove(data);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Product GetById(int id)
        {
            return ctx.Products.Find(id);
        }

        public ProductListViewModel List(string term="", bool paging=false, int currentPage=0)
        {
            var data = new ProductListViewModel();

            var list = ctx.Products.ToList();

            if(!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(a => a.ProductName.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                int pageSize = 5;
                int count = list.Count;
                int totalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = totalPages;
            }

            foreach (var product in list)
            {
                var categories = (from category in ctx.Categories 
                                  join pc in ctx.ProductCategories
                                  on category.Id equals pc.CategoryId
                                  where pc.ProductId == product.Id
                                  select category.CategoryName
                                  ).ToList();
                var categoryNames = string.Join(',', categories);
                product.CategoryNames = categoryNames;
            }
            data.ProductList = list.AsQueryable();
            return data;
        }

        public bool Update(Product model)
        {
            try
            {
                var categoriesToDeleted = ctx.ProductCategories.Where(a => a.ProductId == model.Id && !model.Categories.Contains(a.CategoryId)).ToList();
                foreach (var pCategory in categoriesToDeleted)
                {
                    ctx.ProductCategories.Remove(pCategory);
                }
                foreach (int catId in model.Categories)
                {
                    var productCategory = ctx.ProductCategories.FirstOrDefault(a => a.ProductId == model.Id && a.CategoryId == catId);
                    if (productCategory == null)
                    {
                        productCategory = new ProductCategory { CategoryId = catId, ProductId = model.Id };
                        ctx.ProductCategories.Add(productCategory);
                    }
                }

                ctx.Products.Update(model);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetCategoryByProductId(int productId)
        {
            var categoryIds = ctx.ProductCategories.Where(a => a.ProductId == productId).Select(a => a.CategoryId).ToList();
            return categoryIds;
        }
    }
}
