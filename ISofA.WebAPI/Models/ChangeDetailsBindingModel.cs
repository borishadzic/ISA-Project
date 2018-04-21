using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISofA.WebAPI.Models
{
    public class ChangeDetailsBindingModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
    }
}