using NFClinic.Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFClinic.Services.PatientService
{
    public interface IPatientService
    {
		Task Add(Patient patient);

		void Remove(Patient patient);

		Task<Patient> GetByCardIdAsync(string cardId);

		Task<IEnumerable<TimelineEvent>> GetTimelineEventsAsync(string patientId);

		Task AddTimelineEventAsync(string patientId, TimelineEvent timelineEvent);
	}
}
