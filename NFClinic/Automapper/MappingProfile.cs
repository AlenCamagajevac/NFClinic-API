using AutoMapper;
using NFClinic.Core.DomainModels;
using NFClinic.Core.DomainModels.Pagination;
using NFClinic.Core.DTOs;
using NFClinic.Core.DTOs.PatientDTOs;
using NFClinic.Data.Models.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFClinic.Automapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			//Auth
			CreateMap<RegisterDTO, ApplicationUser>();

			//Patients
			CreateMap<Patient, PatientDTO>();
			CreateMap<TimelineEvent, TimelineEventDTO>();
			CreateMap<CreatePatientDTO, Patient>();
			CreateMap<CreateTimelineEventDTO, TimelineEvent>();

			//Pagination
			CreateMap<PaginatedList<TimelineEvent>, PaginatedListDTO<TimelineEventDTO>>()
				.ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.ToList()));
		}
	}
}
