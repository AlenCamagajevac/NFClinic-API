using System;
using System.Collections.Generic;
using System.Text;

namespace NFClinic.Core.DTOs.PatientDTOs
{
    public class CreateTimelineEventDTO
    {
		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime Time { get; set; }
	}
}
