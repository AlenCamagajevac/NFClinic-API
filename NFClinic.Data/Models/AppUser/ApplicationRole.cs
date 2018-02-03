using Microsoft.AspNetCore.Identity;
using NFClinic.Core.DomainModels.AppUser;
using System;
using System.Collections.Generic;
using System.Text;

namespace NFClinic.Data.Models.AppUser
{
	public class ApplicationRole : IdentityRole, IApplicationRole
	{
		public string Description { get; set; }
	}
}