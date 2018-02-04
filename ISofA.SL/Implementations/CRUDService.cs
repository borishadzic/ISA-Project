using ISofA.DAL.Core;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Implementations
{
    public abstract class CRUDService<TEntity> : ICRUDService<TEntity> where TEntity : class
    {
        protected IUnitOfWork UnitOfWork { get; }

        public CRUDService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return UnitOfWork.Pantry<TEntity>().GetAll();
        }

        public virtual TEntity Get(params object[] keyValues)
        {
            return UnitOfWork.Pantry<TEntity>().Get(keyValues);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return UnitOfWork.Pantry<TEntity>().Find(predicate);
        }

        public virtual TEntity Add(TEntity entity)
        {
            entity = UnitOfWork.Pantry<TEntity>().Add(entity);
            UnitOfWork.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            entities = UnitOfWork.Pantry<TEntity>().AddRange(entities);
            UnitOfWork.SaveChanges();
            return entities;
        }

        public abstract TEntity Update(TEntity entity);

        public virtual TEntity Remove(TEntity entity)
        {
            entity = UnitOfWork.Pantry<TEntity>().Remove(entity);
            UnitOfWork.SaveChanges();
            return UnitOfWork.Pantry<TEntity>().Remove(entity);
        }

        public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            entities = UnitOfWork.Pantry<TEntity>().RemoveRange(entities);
            UnitOfWork.SaveChanges();
            return entities;
        }        
    }
}
