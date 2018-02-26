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
			Email = Isauser.Email;
			Id = Isauser.Id;
			Name = Isauser.Name;
			Surname = Isauser.Surname;
		}

		public string Email { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }

	}
}
