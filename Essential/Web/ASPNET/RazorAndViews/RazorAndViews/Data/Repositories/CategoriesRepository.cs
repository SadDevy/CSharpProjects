using Introduction.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Introduction.Data.Repositories
{
    public class CategoriesRepository : IRepository<Category>
    {
        private readonly NorthwindContext context;

        public CategoriesRepository(NorthwindContext context)
            => this.context = context;

        public void Add(Category item)
            => context.Categories.Add(item);

        public IEnumerable<Category> GetAllList()
            => context.Categories.AsNoTracking();

        public Category GetElement(int id)
            => context.Categories
            .AsNoTracking()
            .FirstOrDefault(n => n.CategoryId == id);

        public void Remove(Category item)
            => context.Categories.Remove(item);

        public void Save()
            => context.SaveChanges();

        public void Update(Category item)
            => context.Categories.Update(item);

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    context.Dispose();
            }
         
            disposed = true;
        }
    }
}
