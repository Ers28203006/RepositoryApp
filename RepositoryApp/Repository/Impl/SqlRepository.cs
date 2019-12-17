using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RepositoryApp.Repository.Impl
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        DbContext _context;
        DbSet<T> db;

        public SqlRepository(DbContext context)
        {
            _context = context;
            db = context.Set<T>();
        }
        public void Create(T item)
        {
            db.Add(item);
            _context.SaveChanges();
        }

        public void Delete(T item)
        {
            db.Remove(item);
            _context.SaveChanges();

        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Edit(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public T GetEntity(int id)
        {
            return db.Find(id);
        }

        public IEnumerable<T> GetEntitysList()
        {
            return db.ToList();
        }
    }
}