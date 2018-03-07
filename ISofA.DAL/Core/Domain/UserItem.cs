using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Domain
{
    public class UserItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserItemId { get; set; }
        public string ISofAUserId { get; set; }
        public int TheaterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool? Approved { get; set; }
        public bool? Sold { get; set; }
        public float? HighestBid { get; set; }
        public string HighestBidderId { get; set; }

        [ForeignKey("ISofAUserId")]
        public virtual ISofAUser ISofaUser { get; set; }
        [ForeignKey("HighestBidderId")]
        public virtual ISofAUser HighestBidder { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        [ForeignKey("TheaterId")]
        public virtual Theater Theater { get; set; }
    }
}
