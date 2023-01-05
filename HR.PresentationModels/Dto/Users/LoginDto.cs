using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace HR.PresentationModels.Dto.Users
{
	public class LoginDto
	{

		[Required]
	
		public string Email { get; set; }
	
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
		public string returnUrl { get; set; } = "";

	}
}
