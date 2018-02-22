﻿using ISofA.DAL.Core;
using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;
using ISofA.DAL.Persistence.Pantries;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ISofADbContext _context;

        private ITheaterPantry _theaters;
        public ITheaterPantry Theaters { get { return _theaters ?? new TheaterPantry(_context); } }

        private IPlayPantry _plays;
        public IPlayPantry Plays { get { return _plays ?? new PlayPantry(_context); } }

        private IUserPantry _users;
        public IUserPantry Users { get { return _users ?? new UserPantry(_context); } }

        private IStagePantry _stages;
        public IStagePantry Stages { get { return _stages ?? new StagePantry(_context); } }

        private ISeatPantry _seats;
        public ISeatPantry Seats { get { return _seats ?? new SeatPantry(_context); } }

        private IProjectionPantry _projections;
        public IProjectionPantry Projections { get { return _projections ?? new ProjectionPantry(_context); } }

        public void Modified<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
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
    }
}
