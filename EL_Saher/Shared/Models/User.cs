using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Saher.Shared.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string UserName { get; set; } = string.Empty;
		public byte[] PasswordHash { get; set; }
		public byte[] Passwordsalt { get; set; }
		public bool IsAdmin { get; set; } = false;
		public DateTime DateCreated { get; set; } = DateTime.Now;
	}
}
