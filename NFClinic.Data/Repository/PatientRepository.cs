using Microsoft.EntityFrameworkCore;
using NFClinic.Core.DomainModels;
using NFClinic.Core.DomainModels.Pagination;
using NFClinic.Core.Repository;
using NFClinic.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public PaginatedList<TimelineEvent> GetTimelineEvents(string patientId, int page)
		{
			var timelineEvents = NFClinicContext
				.TimelineEvents
				.Where(te => te.PatientId == patientId)
				.AsQueryable()
				.AsNoTracking();

			return PaginatedList<TimelineEvent>.Create(timelineEvents, page, 10);
		}
	}
}
