using Introduction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Introduction.Data.Repositories
{
    public class SuppliersRepository : IRepository<Supplier>
    {
        private readonly NorthwindContext context;

        public SuppliersRepository(NorthwindContext context)
        {
            this.context = context;
        }

        public void Add(Supplier item)
            => context.Suppliers.Add(item);

        public IEnumerable<Supplier> GetAllList()
            => context.Suppliers;

        public Supplier GetElement(int id)
            => context.Suppliers.FirstOrDefault(n => n.SupplierId == id);

        public void Remove(Supplier item)
            => context.Suppliers.Remove(item);

        public void Save()
            => context.SaveChanges();

        public void Update(Supplier item)
            => context.Suppliers.Update(item);

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
