using System;
using System.Linq;
using System.Linq.Expressions;

namespace CoreOperations.Repositories.Interfaces
{

    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties);

        void Add(TEntity entity);

        void Remove(TEntity entity);
        void Edit(TEntity entity);
    }
}

