using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Retail.Models;

namespace Sport_Retail.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;
        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            var products = _repository.Products.Include(p => p.Category);
            return View(products);
        }
        
        [HttpPost]
        public IActionResult Edit(int id)
        {
            var productToEdit = _repository.Products.Single(p => p.Id == id);

            return View(productToEdit);
        }

        [HttpPost]
        public IActionResult Save(Product product)
        {
            _repository.SaveProduct(product);
            TempData["Success"] = "Zapisano produkty w bazie danych";

            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public IActionResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _repository.DeleteProduct(id);

            TempData["Success"] = "Usunieto produkt z bazy danych";

            return RedirectToAction("Index", "Admin");
        }
    }
}
