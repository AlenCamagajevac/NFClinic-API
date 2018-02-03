using Microsoft.AspNetCore.Identity;
using NFClinic.Core.DomainModels.AppUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NFClinic.Data.Models.AppUser
{
    public class ApplicationUser : IdentityUser, IApplicationUser
	{
		[MaxLength(50), MinLength(3)]
		public string Name { get; set; }

		[MaxLength(50), MinLength(2)]
		public string Address { get; set; }

		[MaxLength(50), MinLength(2)]
		public string City { get; set; }
	}
}
