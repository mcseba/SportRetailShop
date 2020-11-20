using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Retail.Models;
using Sport_Retail.ViewModels;

namespace Sport_Retail.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;
        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _repository.Products.Include(p => p.Category);
            return View(products);
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var productToEdit = _repository.Products.SingleOrDefault(p => p.Id == id);

            var viewModel = new ProductFormViewModel
            {
                Product = productToEdit,
                Categories = _repository.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            _repository.SaveProduct(product);
            TempData["Success"] = "Zapisano produkty w bazie danych";

            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _repository.Categories.ToList();
            var viewModel = new ProductFormViewModel
            {
                Categories = categories
            };

            return View("Edit", viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _repository.DeleteProduct(id);

            TempData["Success"] = "Usunieto produkt z bazy danych";

            return RedirectToAction("Index", "Admin");
        }
    }
}
