using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL_Saher.Shared.Models.ServiceModels
{
	public class UserInfo
	{
		public bool IsAdmin { get; set; } = false;
		[Required, MaxLength(100)]
		public string UserName { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		[Compare("Password", ErrorMessage = "كلمة السر غير متطابقة") ]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
