using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NFClinic.Core.DomainModels;
using NFClinic.Data.Models.AppUser;
using NFClinic.Data.Models.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NFClinic.Data.Data
{
    public class NFClinicContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
	{

		public NFClinicContext(DbContextOptions<NFClinicContext> options) : base(options)
        {

		}

		public virtual DbSet<Patient> Patients { get; set; }
		public virtual DbSet<TimelineEvent> TimelineEvents { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//Apply configurations
			modelBuilder.ApplyConfiguration(new PatientConfiguration());
		}
	}
}
