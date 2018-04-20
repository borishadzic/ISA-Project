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
            TheaterId = userItem.TheaterId;
            UserItemId = userItem.UserItemId;
            UserId = userItem.ISofAUserId;
            Name = userItem.Name;
            Description = userItem.Description;
            ImageUrl = userItem.ImageUrl;
            HighestBid = userItem.HighestBid;
            Sold = userItem.Sold;
            ExpirationDate = userItem.ExpirationDate;
            Approved = userItem.Approved;
            Bids = userItem.Bids.Select(x => new BidDTO(x));
        }

        public int TheaterId { get; set; }
        public Guid UserItemId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public float? HighestBid { get; set; }
        public bool Sold{ get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool? Approved { get; set; }
        public IEnumerable<BidDTO> Bids { get; set; }
    }
}
