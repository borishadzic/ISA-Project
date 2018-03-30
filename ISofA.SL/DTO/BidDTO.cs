﻿using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.DTO
{
    public class BidDTO
    {
        public BidDTO(Bid bid)
        {
            BidderId = bid.BidderId;
            BidAmount = bid.BidAmount;
            BidDate = bid.BidDate;
        }

        public string BidderId { get; set; }
        public float BidAmount { get; set; }
        public DateTime BidDate { get; set; }
    }
}
