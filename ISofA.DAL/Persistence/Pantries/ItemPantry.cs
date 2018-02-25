using ISofA.DAL.Core.Domain;
using ISofA.DAL.Core.Pantries;

namespace ISofA.DAL.Persistence.Pantries
{
    public class ItemPantry : Pantry<Item>, IItemPantry
    {
        public ItemPantry(ISofADbContext context) : base(context)
        {
        }
    }
}
