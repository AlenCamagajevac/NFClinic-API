using AutoMapper;
using NFClinic.Core.DTOs;
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
			//CLINICS
			CreateMap<RegisterDTO, ApplicationUser>();
		}
	}
}
