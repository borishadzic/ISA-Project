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
        UserItem GetUserItemWithBids(int theaterId, Guid userItemId);
        UserItem GetUserItemWithBids(Guid userItemId);
    }
}
