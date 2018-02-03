using NFClinic.Core.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFClinic.Core.Persistance
{
	public interface IUnitOfWork : IDisposable
	{
		IPatientRepository Patients { get; }
		Task<int> CompleteAsync();
	}
}
