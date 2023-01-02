using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetExpert.Domain
{
	public class ApplicationUser
	{
		public Guid Id { get; set; }

		public string UserName { get; set; } = string.Empty;

		public byte[] PasswordHash { get; set; } = new byte[1024];

		public byte[] PasswordSalt { get; set; } = new byte[1024];

		public string Role { get; set; } = string.Empty;

		public ApplicationUser()
		{
			Id = Guid.NewGuid();
		}
	}
}
