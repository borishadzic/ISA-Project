using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Domain
{
    public class Bid
    {
        [Key, Column(Order = 0)]
        public Guid UserItemId { get; set; }
        [Key, Column(Order = 1)]
        public string BidderId { get; set; }
        public DateTime BidDate { get; set; }
        public float BidAmount { get; set; }
            
        [ForeignKey("BidderId")]
        public virtual ISofAUser Bidder { get; set; }
        [ForeignKey("UserItemId")]
        public virtual UserItem UserItem { get; set; }
    }
}
