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

        public IActionResult Edit(int id)
        {
            var productToEdit = _repository.Products.Single(p => p.Id == id);

            return View(productToEdit);
        }

        public IActionResult Save(Product product)
        {
            _repository.SaveProduct(product);
            TempData["SuccessSave"] = "Zapisano produkty w bazie danych";

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Create()
        {
            return View("Edit", new Product());
        }

        public IActionResult Delete(int id)
        {
            _repository.DeleteProduct(id);
            TempData["SuccessDelete"] = "Usunieto produkt z bazy danych";

            return RedirectToAction("Index", "Admin");
        }
    }
}
