using System;
using System.Collections.Generic;
using System.Text;

namespace NFClinic.Core.DomainModels.Base
{
	public interface IEntity
	{
		object Id { get; }

		DateTime CreatedDate { get; set; }

		DateTime? ModifiedDate { get; set; }

		string CreatedBy { get; set; }

		string ModifiedBy { get; set; }

		byte[] Version { get; set; }
	}

	public interface IEntity<T> : IEntity
	{
		new T Id { get; set; }
	}
}
