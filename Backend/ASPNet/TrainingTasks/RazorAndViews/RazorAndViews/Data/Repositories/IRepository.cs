using System;
using System.Collections.Generic;

namespace Introduction.Data.Repositories
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<T> GetAllList();
        T GetElement(int id);
        void Add(T item);
        void Update(T item);
        void Remove(T item);

        void Save();
    }
}
