using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Retail.Models;

namespace Sport_Retail.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public IActionResult ListAll() => View(_repository.Products.Include(t => t.Category));

        public IActionResult List(string category)
        {
            IEnumerable<Product> products = _repository.Products.Where(p => p.Category.Name == category)
                .Include(t => t.Category).AsEnumerable<Product>();
            return View(products);
        }
    }
}
