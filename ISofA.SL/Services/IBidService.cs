using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Services
{
    public interface IBidService
    {
        IEnumerable<Bid> GetAll(Guid userItemId);
        Bid Get(Guid userItemId, string userId);
        Bid AddBid(Guid userItemId, string userId, Bid bid);
    }
}
