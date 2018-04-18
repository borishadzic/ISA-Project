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
        public Task UserItemSoldNotification(UserItem userItem, Bid winningBid)
        {
            return Task.FromResult<object>(null);
        }
    }
}
