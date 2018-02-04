using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private IISofADbContext _context;

        private Dictionary<Type, object> _pantries;

        public UnitOfWork(IISofADbContext context, IPlayPantry playPantry, IProjectionPantry projectionPantry, ISeatPantry seatPantry, IStagePantry stagePantry, IUserPantry userPantry)
        {
            _context = context;
            _pantries = new Dictionary<Type, object>();

            RegisterPantry(playPantry);
            RegisterPantry(projectionPantry);
            RegisterPantry(seatPantry);
            RegisterPantry(stagePantry);
            RegisterPantry(userPantry);
        }

        private void RegisterPantry<TEntity>(IPantry<TEntity> pantry) where TEntity : class
        {
            _pantries.Add(typeof(TEntity), pantry);
        }

        public IPantry<TEntity> Pantry<TEntity>() where TEntity : class
        {
            return (IPantry<TEntity>)_pantries[typeof(TEntity)];
        }

        public void Modified<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }        
    }
}
