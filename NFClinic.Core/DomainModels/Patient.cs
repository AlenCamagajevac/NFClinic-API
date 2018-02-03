using NFClinic.Core.DomainModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NFClinic.Core.DomainModels
{
    public class Patient : Entity<string>
    {
		[MaxLength(40), MinLength(3)]
		public string Name { get; set; }

		[MaxLength(50), MinLength(3)]
		public string Address { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		public DateTime DateOfBirth { get; set; }

		[MaxLength(50), MinLength(3)]
		public string CardId { get; set; }

		public ICollection<TimelineEvent> TimelineEvents { get; set; }

		public Patient()
		{
			TimelineEvents = new HashSet<TimelineEvent>();
		}
	}
}
