using System;
using System.Collections.Generic;
using System.Text;

namespace NFClinic.Core.DomainModels.AppUser
{
	public interface IApplicationUser 
	{
		string Name { get; set; }

		string Address { get; set; }

		string City { get; set; }
	}
}
