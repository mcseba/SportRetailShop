using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_Retail.Models
{
    public class EfProductRepository : IProductRepository
    {
        private readonly AppDbContext _ctx;

        public EfProductRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Product> Products => _ctx.Products;
        public IQueryable<Category> Categories => _ctx.Categories;

        public void DeleteProduct(int id)
        {
            Product product = _ctx.Products.Find(id);
            _ctx.Remove(product);
            _ctx.SaveChanges();   
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                _ctx.Add(product);
            }
            else
            {
                Product productInDb = _ctx.Products.Single(p => p.Id == product.Id);

                productInDb.Name = product.Name;
                productInDb.Price = product.Price;
                productInDb.Description = product.Description;
                productInDb.CategoryId = product.CategoryId;
            }
            _ctx.SaveChanges();
        }
    }
}
