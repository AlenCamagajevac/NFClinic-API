using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NFClinic.Core.DomainModels.Base
{
	public abstract class Entity<T> : IEntity<T>
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public T Id { get; set; }

		object IEntity.Id
		{
			get { return Id; }
		}

		private DateTime? createdDate;

		[DataType(DataType.DateTime)]
		public DateTime CreatedDate
		{
			get { return createdDate ?? DateTime.UtcNow; }
			set { createdDate = value; }
		}

		[DataType(DataType.DateTime)]
		public DateTime? ModifiedDate { get; set; }

		public string CreatedBy { get; set; }

		public string ModifiedBy { get; set; }

		[Timestamp]
		public byte[] Version { get; set; }
	}
}
