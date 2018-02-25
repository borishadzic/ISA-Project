using ISofA.DAL.Core.Domain;
using System;

namespace ISofA.SL.DTO
{
    public class ItemDTO
    {
        public ItemDTO(Item item)
        {
            ItemId = item.ItemId;
            Name = item.Name;
            Description = item.Description;
            ImageUrl = item.ImageUrl;
            Price = item.Price;
        }

        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public float Price { get; set; }
    }
}
