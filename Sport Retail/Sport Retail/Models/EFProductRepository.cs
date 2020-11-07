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
    }
}
