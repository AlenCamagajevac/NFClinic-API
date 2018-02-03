using Microsoft.EntityFrameworkCore;
using NFClinic.Core.DomainModels;
using NFClinic.Core.Repository;
using NFClinic.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NFClinic.Data.Repository
{
    public class PatientRepository :  Repository<Patient>, IPatientRepository
    {
		public PatientRepository(NFClinicContext context) : base(context)
        {
		}

		public NFClinicContext NFClinicContext
		{
			get { return Context as NFClinicContext; }
		}

		public async Task AddTimelineEventAsync(string patientId, TimelineEvent timelineEvent)
		{
			var patient = await NFClinicContext.Patients.SingleOrDefaultAsync(p => p.Id == patientId);

			if (patient == null)
				return;

			patient.TimelineEvents.Add(timelineEvent);
		}

		public async Task<Patient> GetByCardIdAsync(string cardId)
		{
			return await NFClinicContext.Patients.SingleOrDefaultAsync(p => p.CardId == cardId);
		}

		public async Task<IEnumerable<TimelineEvent>> GetTimelineEventsAsync(string patientId)
		{
			var patient = await NFClinicContext.Patients
				.Include(p => p.TimelineEvents)
				.SingleOrDefaultAsync(p => p.Id == patientId);

			return patient.TimelineEvents ?? null;
		}
	}
}
