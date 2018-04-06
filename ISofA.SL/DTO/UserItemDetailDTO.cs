using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ISofA.SL.DTO
{
    public class UserItemDetailDTO
    {
        public UserItemDetailDTO(UserItem userItem)
        {
            UserItemId = userItem.UserItemId;
            Name = userItem.Name;
            Description = userItem.Description;
            ImageUrl = userItem.ImageUrl;
            //Approved = userItem.Approved != null ? userItem.Approved : false;
            HighestBid = userItem.HighestBid;
            Bids = userItem.Bids.Select(x => new BidDTO(x));
        }

        public Guid UserItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public float? HighestBid { get; set; }
        //public bool? Approved { get; set; }
        public IEnumerable<BidDTO> Bids { get; set; }
    }
}
