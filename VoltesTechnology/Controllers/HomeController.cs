using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VoltesTechnology.Models;
using VoltesTechnology.Repositories.Abstract;

namespace VoltesTechnology.Controllers
{
	public class HomeController : Controller
	{
		private readonly IProductService _productsService;

		public HomeController(IProductService productService)
		{
			_productsService = productService;
		}

		public IActionResult Index(string term="", int currentPage = 1)
		{
			var products = _productsService.List(term, true, currentPage);
			return View(products);
		}

		public IActionResult About()
		{
			return View();
		}

		public IActionResult ProductDetail(int productId)
		{
			var product = _productsService.GetById(productId);
			return View(product);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
