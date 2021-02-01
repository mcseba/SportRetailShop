using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sport_Retail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_Retail.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiProductController : Controller
    {
        private readonly IProductRepository _context;
        public ApiProductController(IProductRepository repository)
        {
            _context = repository;
        }

        /// <summary>
        /// Gets the product according to it's ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        /// <summary>
        /// Gets all products in database
        /// </summary>
        /// <returns>IEnumerable<Product></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        /// <summary>
        /// Adds product to the database
        /// </summary>
        /// <param name="product">Product object to add</param>
        /// <returns>Added product</returns>
        [HttpPost]
        public ActionResult<Product> AddProduct(Product product)
        {
            _context.SaveProduct(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpDelete]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var productToDelete = await _context.Products.SingleOrDefaultAsync(p => p.Id == id);
            
            if (productToDelete == null)
            {
                return NotFound();
            }
            _context.DeleteProduct(id);

            return productToDelete;
        }

        [HttpPut]
        public ActionResult<Product> EditProduct(Product product)
        {
            _context.SaveProduct(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
    }
}
