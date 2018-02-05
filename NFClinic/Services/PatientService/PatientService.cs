using NFClinic.Core.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFClinic.Core.DomainModels;
using NFClinic.Core.DomainModels.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace NFClinic.Services.PatientService
{
    public class PatientService : IPatientService
    {
		private readonly IUnitOfWork unitOfWork;

		public PatientService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public async Task AddAsync(Patient patient)
		{
			await unitOfWork.Patients.AddAsync(patient);
			await unitOfWork.CompleteAsync();
			
		}

		public async Task AddTimelineEventAsync(string patientId, TimelineEvent timelineEvent)
		{
			await unitOfWork.Patients.AddTimelineEventAsync(patientId, timelineEvent);
			await unitOfWork.CompleteAsync();
		}

		public async Task<Patient> GetByCardIdAsync(string cardId)
		{
			return await unitOfWork.Patients.GetByCardIdAsync(cardId);
		}

		public PaginatedList<TimelineEvent> GetTimelineEvents(string patientId, int page)
		{
			return unitOfWork.Patients.GetTimelineEvents(patientId, page);
		}

		public void Remove(Patient patient)
		{
			unitOfWork.Patients.Remove(patient);
		}
	}
}
