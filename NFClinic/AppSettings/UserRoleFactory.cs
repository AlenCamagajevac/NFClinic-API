using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NFClinic.Data.Models.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFClinic.AppSettings
{
    public class UserRoleFactory
    {
		public static async Task CreateRolesAsync(IServiceProvider serviceProvider)
		{
			//Create role manager
			var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

			//Create new application roles
			var roles = new List<ApplicationRole>();
			roles.Add(new ApplicationRole
			{
				Name = "staff",
				Description = "staff personel of NFClinic app"
			});

			roles.Add(new ApplicationRole
			{
				Name = "admin",
				Description = "administrator power user of NFClinic app"
			});

			//Add those roles
			foreach (var role in roles)
			{
				var roleExist = await roleManager.RoleExistsAsync(role.Name);
				if (!roleExist)
				{
					var roleResult = await roleManager.CreateAsync(role);
				}
			}
		}

		public static async Task CreateAdminUserAsync(IServiceProvider serviceProvider)
		{
			//Create user manager
			var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var user = await UserManager.FindByEmailAsync("admin@email.com");

			// check if the user exists
			if (user == null)
			{
				var adminUser = new ApplicationUser
				{
					UserName = "admin",
					Email = "admin@email.com",
				};
				string adminPassword = "password";

				var createPowerUser = await UserManager.CreateAsync(adminUser, adminPassword);
				if (createPowerUser.Succeeded)
				{
					//Add user to admin role
					await UserManager.AddToRoleAsync(adminUser, "admin");

				}
			}
		}
	}
}
