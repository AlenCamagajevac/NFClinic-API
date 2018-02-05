using NFClinic.Core.DomainModels;
using NFClinic.Core.DomainModels.Pagination;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFClinic.Core.Repository
{
    public interface IPatientRepository : IRepository<Patient>
	{
		Task<Patient> GetByCardIdAsync(string cardId);

		PaginatedList<TimelineEvent> GetTimelineEvents(string patientId, int page);

		Task AddTimelineEventAsync(string patientId, TimelineEvent timelineEvent);
    }
}
