using Introduction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Introduction.Data.Repositories
{
    public class ProductsRepository : IRepository<Product>
    {
        private readonly NorthwindContext context;

        public ProductsRepository(NorthwindContext context)
        {
            this.context = context;
        }
        
        public void Add(Product item)
            => context.Products.Add(item);

        public IEnumerable<Product> GetAllList()
            => context.Products
            .AsNoTracking()
            .Include(n => n.Supplier)
            .Include(n => n.Category);

        public Product GetElement(int id)
            => context.Products.AsNoTracking().FirstOrDefault(n => n.ProductId == id);

        public void Remove(Product item)
            => context.Products.Remove(item);

        public void Save()
            => context.SaveChanges();

        public void Update(Product item)
            => context.Products.Update(item);

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    context.Dispose();

            disposed = true;
        }
    }
}
