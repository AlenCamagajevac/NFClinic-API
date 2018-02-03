using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NFClinic.Core.DTOs
{
    public class LoginDTO
    {
		[MaxLength(50), MinLength(3)]
		public string Email { get; set; }

		[MaxLength(20), MinLength(3)]
		public string Password { get; set; }
	}
}
