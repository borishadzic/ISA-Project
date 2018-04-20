using ISofA.DAL.Core.Domain;
using ISofA.SL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.Tests.Unit.Student_2.BidTest
{
    public class MockEmailService : IEmailService
    {
        public Task SendMovieInvites(IEnumerable<ISofAUser> users, IEnumerable<int> projectionIds, IEnumerable<int> rows, IEnumerable<int> columns)
        {
            return Task.FromResult<object>(null);
        }

        public Task UserItemSoldNotification(UserItem userItem, Bid winningBid)
        {
            return Task.FromResult<object>(null);
        }
    }
}
