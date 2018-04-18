using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Pantries
{
    public interface IUserItemPantry : IPantry<UserItem>
    {
        UserItem GetUserItemWithBids(Guid userItemId);
        UserItem GetUserItemWithBids(Guid userItemId, int theaterId);
        UserItem GetUserItemWithBids(Guid userItemId, string ISofAUserId);
    }
}
