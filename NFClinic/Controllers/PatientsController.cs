using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NFClinic.Services.PatientService;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using NFClinic.ErrorValidation;
using NFClinic.Core.DTOs.PatientDTOs;
using NFClinic.Core.DomainModels;
using NFClinic.Core.DomainModels.Pagination;

namespace NFClinic.Controllers
{
    [Produces("application/json")]
    [Route("api/Patients")]
    public class PatientsController : Controller
    {
		private readonly IPatientService patientService;
		private readonly IMapper mapper;

		public PatientsController(IPatientService patientService, IMapper mapper)
		{
			this.patientService = patientService;
			this.mapper = mapper;
		}

		//POST: api/Patients/Card/5
		[HttpGet("card/{id}")]
		[AllowAnonymous]
		[ValidateModelAttributes]
		public async Task<IActionResult> GetPatient([FromRoute] string id)
		{
			//get patient
			var patient = await patientService.GetByCardIdAsync(id);

			if(patient == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<Patient, PatientDTO>(patient));
		}

		// GET: api/Patients/5/Timeline
		[HttpGet("{id}/Timeline")]
		[AllowAnonymous]
		[ValidateModelAttributes]
		public async Task<IActionResult> GetPatientTimeline([FromRoute] string id, [FromQuery] int? page)
		{
			var timelineEvents = patientService.GetTimelineEvents(id, page ?? 1);

			if (timelineEvents == null)
			{
				return NotFound();
			}

			return Ok(mapper.Map<PaginatedList<TimelineEvent>, PaginatedListDTO<TimelineEventDTO>>(timelineEvents));
		}

		// POST: api/Patients
		[HttpPost]
		[AllowAnonymous]
		[ValidateModelAttributes]
		public async Task<IActionResult> CreatePatient([FromBody] CreatePatientDTO patientDTO)
		{
			var patient = mapper.Map<CreatePatientDTO, Patient>(patientDTO);

			var existingPatient = await patientService.GetByCardIdAsync(patientDTO.CardId);

			if (existingPatient != null)
			{
				ModelState.AddModelError("CardId", "Patient With given card already exists");
				return BadRequest(ModelState);
			}

			await patientService.AddAsync(patient);
			return CreatedAtAction("GetPatient", new { id = patient.CardId }, mapper.Map<Patient, PatientDTO>(patient));
		}

		// POST: api/Patients/5/Timeline/Event
		[HttpPost("{id}/Timeline/Event")]
		[AllowAnonymous]
		[ValidateModelAttributes]
		public async Task<IActionResult> CreateTimelineEvent([FromRoute] string id, [FromBody] CreateTimelineEventDTO timelineEventDTO)
		{
			var timelineEvent = mapper.Map<CreateTimelineEventDTO, TimelineEvent>(timelineEventDTO);

			await patientService.AddTimelineEventAsync(id, timelineEvent);

			return CreatedAtAction("GetPatientTimeline", new { id = id }, mapper.Map<TimelineEvent, TimelineEventDTO>(timelineEvent));
		}
	}
}