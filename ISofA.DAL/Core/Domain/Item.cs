using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISofA.DAL.Core.Domain
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ItemId { get; set; }
        public int TheaterId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public string ImageUrl { get; set; }
        public string BuyerId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? BoughtDate { get; set; }

        [ForeignKey("TheaterId")]
        public virtual Theater Theater { get; set; }
        [ForeignKey("BuyerId")]
        public virtual ISofAUser Buyer { get; set; }
    }
}
