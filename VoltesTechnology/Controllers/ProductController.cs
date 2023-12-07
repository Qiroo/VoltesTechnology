using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VoltesTechnology.Models.Domain;
using VoltesTechnology.Repositories.Abstract;

namespace VoltesTechnology.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, IFileService fileService, ICategoryService categoryService)
        {
            _productService = productService;
            _fileService = fileService;
            _categoryService = categoryService;
        }

        public IActionResult Add()
        {
            var model = new Product();
            model.CategoryList = _categoryService.List().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() });
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Product model)
        {
            model.CategoryList = _categoryService.List().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() });
            if (!ModelState.IsValid)
                return View(model);

            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not be saved";
                }
                var imageName = fileReult.Item2;
                model.ProductImage = imageName;
            }

            var result = _productService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }
        public IActionResult Edit(int id)
        {
            var model = _productService.GetById(id);
            var selectedCategories = _productService.GetCategoryByProductId(model.Id);
            MultiSelectList multiCategoryList = new MultiSelectList(_categoryService.List(), "Id", "CategoryName", selectedCategories);
            model.MultiCategoryList = multiCategoryList;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Product model)
        {
            var selectedCategories = _productService.GetCategoryByProductId(model.Id);
            MultiSelectList multiCategoryList = new MultiSelectList(_categoryService.List(), "Id", "CategoryName", selectedCategories);
            model.MultiCategoryList = multiCategoryList;
            if (!ModelState.IsValid)
                return View(model);
            
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not be saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.ProductImage = imageName;
            }

            var result = _productService.Update(model);
            if (result)
            {
                TempData["msg"] = "Updated Successfully";
                return RedirectToAction(nameof(ProductList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult ProductList()
        {
            var data = this._productService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);
            return RedirectToAction(nameof(ProductList));
        }
    }
}
