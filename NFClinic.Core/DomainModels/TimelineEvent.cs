using NFClinic.Core.DomainModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NFClinic.Core.DomainModels
{
    public class TimelineEvent : Entity<string>
    {
		[MaxLength(50), MinLength(3)]
		public string Name { get; set; }

		[MaxLength(500), MinLength(3)]
		public string Description { get; set; }

		public DateTime Time { get; set; }

		public string PatientId { get; set; }

		public virtual Patient Patient { get; set; }
	}
}
