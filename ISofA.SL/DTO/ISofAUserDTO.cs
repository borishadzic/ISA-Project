using ISofA.DAL.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.DTO
{
	public class ISofAUserDTO
	{
		public ISofAUserDTO(ISofAUser Isauser)
		{
			Id = Isauser.Id;
			Email = Isauser.Email;
			Name = Isauser.Name;
			Surname = Isauser.Surname;
			City = Isauser.City;
			PhoneNumber = Isauser.PhoneNumber;
		}

		public string Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string City { get; set; }
		public string PhoneNumber { get; set; }


	}
}
