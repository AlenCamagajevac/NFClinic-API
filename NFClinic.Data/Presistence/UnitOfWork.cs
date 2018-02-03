using NFClinic.Core.Persistance;
using NFClinic.Core.Repository;
using NFClinic.Data.Data;
using NFClinic.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFClinic.Data.Presistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly NFClinicContext context;

		public UnitOfWork(NFClinicContext context)
		{
			this.context = context;
			Patients = new PatientRepository(context);
		}

		public IPatientRepository Patients { get; private set; }

		public async Task<int> CompleteAsync()
		{
			return await context.SaveChangesAsync();
		}

		public void Dispose()
		{
			context.Dispose();
		}
	}
}
