using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_Retail.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        IQueryable<Category> Categories { get; }

        void DeleteProduct(int id);

        void SaveProduct(Product p);
    }
}
