using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core
{
    public interface IUnitOfWork
    {
        IPantry<TEntity> Pantry<TEntity>() where TEntity : class;

        void Modified<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();    

    }
}
