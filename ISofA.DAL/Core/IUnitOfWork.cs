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
        ITheaterPantry Theaters { get; }
        IUserPantry Users { get; }
        IPlayPantry Plays { get; }
        IProjectionPantry Projections { get; }
        ISeatPantry Seats { get; }
        IStagePantry Stages { get; }

        void Modified<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();    

    }
}
