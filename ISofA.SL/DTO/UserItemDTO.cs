﻿using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.DTO
{
    public class UserItemDTO
    {
        public UserItemDTO(UserItem userItem)
        {
            TheaterId = userItem.TheaterId;
            UserItemId = userItem.UserItemId;
            UserId = userItem.ISofAUserId;
            Name = userItem.Name;
            Description = userItem.Description;
            ImageUrl = userItem.ImageUrl;
            HighestBid = userItem.HighestBid;
        }

        public int TheaterId { get; set; }
        public Guid UserItemId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public float? HighestBid { get; set; }
    }
}
