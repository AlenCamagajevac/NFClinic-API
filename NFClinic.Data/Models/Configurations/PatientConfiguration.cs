using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NFClinic.Core.DomainModels;

namespace NFClinic.Data.Models.Configurations
{
	public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
		public void Configure(EntityTypeBuilder<Patient> builder)
		{
			builder.HasIndex(p => p.CardId).IsUnique();
		}
	}
}
