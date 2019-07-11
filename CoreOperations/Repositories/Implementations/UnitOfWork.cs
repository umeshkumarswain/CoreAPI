using CoreOperations.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;

namespace CoreOperations.Repositories.Implementations
{
    public class UnitOfWork<TC> : IUnitOfWork where TC : DbContext, new()
    {
        private readonly TC _context = new TC();
        private readonly Hashtable _repositories = new Hashtable();
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.Contains(typeof(T)))
            {
                _repositories.Add(typeof(T), new GenericRepository<T>(_context));
            }
            return (IGenericRepository<T>)_repositories[typeof(T)];
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}