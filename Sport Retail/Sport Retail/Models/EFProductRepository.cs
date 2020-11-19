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

        public void DeleteProduct(int id)
        {
            _ctx.Remove(_ctx.Products.Single(p => p.Id == id));
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
                var productInDb = _ctx.Products.Single(p => p.Id == product.Id);

                productInDb.Name = product.Name;
                productInDb.Price = product.Price;
                productInDb.Description = product.Description;
            }
            _ctx.SaveChanges();
        }
    }
}
