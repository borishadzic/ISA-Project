using ISofA.DAL.Core;
using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence.Pantries
{
    public class Pantry<TEntity> : IPantry<TEntity> where TEntity : class
    {
        protected ISofADbContext Context { get; }

        public Pantry(ISofADbContext context)
        {
            Context = context;
        }

        public TEntity Get(params object[] keyValues)
        {
            return Context.Set<TEntity>().Find(keyValues);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public TEntity Add(TEntity entity)
        {
            return Context.Set<TEntity>().Add(entity);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return Context.Set<TEntity>().AddRange(entities);
        }

        public TEntity Remove(TEntity entity)
        {
            return Context.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            return Context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
