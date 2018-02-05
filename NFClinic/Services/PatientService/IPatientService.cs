using NFClinic.Core.DomainModels;
using NFClinic.Core.DomainModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFClinic.Services.PatientService
{
    public interface IPatientService
    {
		Task AddAsync(Patient patient);

		void Remove(Patient patient);

		Task<Patient> GetByCardIdAsync(string cardId);

		PaginatedList<TimelineEvent> GetTimelineEvents(string patientId, int page);

		Task AddTimelineEventAsync(string patientId, TimelineEvent timelineEvent);
	}
}
