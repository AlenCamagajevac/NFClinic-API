using System;
using System.Collections.Generic;
using System.Text;

namespace NFClinic.Core.DTOs.PatientDTOs
{
    public class PatientDTO
    {
		public string Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string Email { get; set; }

		public DateTime DateOfBirth { get; set; }
	}
}
