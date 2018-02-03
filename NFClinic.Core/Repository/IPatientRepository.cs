using NFClinic.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFClinic.Core.Repository
{
    public interface IPatientRepository : IRepository<Patient>
	{
		Task<Patient> GetByCardIdAsync(string cardId);

		Task<IEnumerable<TimelineEvent>> GetTimelineEventsAsync(string patientId);

		Task AddTimelineEventAsync(string patientId, TimelineEvent timelineEvent);
    }
}
