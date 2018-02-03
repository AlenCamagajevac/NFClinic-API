using System;
using System.Collections.Generic;
using System.Text;

namespace NFClinic.Core.DTOs.PatientDTOs
{
    public class TimelineEventDTO
    {
		public string Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime Time { get; set; }
	}
}
