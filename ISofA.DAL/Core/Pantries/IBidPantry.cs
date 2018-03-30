using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Pantries
{
    public interface IBidPantry : IPantry<Bid>
    {
        Bid GetBidWithUserItem(Guid userItemId, string bidderId);
        void SaveOrUpdateBid(Bid bid);
    }
}
