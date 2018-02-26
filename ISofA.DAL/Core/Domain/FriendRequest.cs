using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.DAL.Core.Domain
{

    public class FriendRequest
    {
        [Key]
        [Column(Order = 0)]
        public string SenderId { get; set; }
        [Key]
        [Column(Order = 1)]
        public string RecieverId { get; set; }
        [ForeignKey("SenderId")]
        public virtual ISofAUser Sender { get; set; }
        [ForeignKey("RecieverId")]
        public virtual ISofAUser Reciever { get; set; }
        
    }
}
