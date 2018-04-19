using ISofA.DAL.Core;
using ISofA.DAL.Core.Pantries;
using ISofA.DAL.Persistence;
using ISofA.DAL.Persistence.Pantries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Persistence
{
    public class TestUnitOfWork : ITestUnitOfWork, IDisposable
    {
        private ISofATestDbContext _context = new ISofATestDbContext("ISofATestDb");

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

        private IItemPantry _items;
        public IItemPantry Items { get { return _items ?? new ItemPantry(_context); } }

        private IFriendRequestPantry _friendRequests;
        public IFriendRequestPantry FriendRequests { get { return _friendRequests ?? new FriendRequestPantry(_context); } }

        private IUserItemPantry _userItems;
        public IUserItemPantry UserItems { get { return _userItems ?? new UserItemPantry(_context); } }

        private IBidPantry _bids;
        public IBidPantry Bids { get { return _bids ?? new BidPantry(_context); } }

        private IConfigPantry _configs;
        public IConfigPantry Configs { get { return _configs ?? new ConfigPantry(_context); } }

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

        public void NukeDatabase()
        {
            _context.NukeDatabase();
        }
    }
}
