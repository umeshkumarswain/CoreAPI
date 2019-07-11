using CoreOperations.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CoreOperations.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal DbContext Context;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] navigationProperties)
        {
            IQueryable<TEntity> list;

            IQueryable<TEntity> dbQuery = DbSet;

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            list = dbQuery.AsNoTracking();
            //.ToList<TEntity>();

            return list;
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }
        public void Edit(TEntity entity)
        {
            var entry = Context.Entry(entity);
            entry.State = EntityState.Modified;
            DbSet.Update(entity);
        }
    }
}
