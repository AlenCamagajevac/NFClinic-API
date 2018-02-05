using System;
using System.Collections.Generic;
using System.Text;

namespace NFClinic.Core.DomainModels.Pagination
{
    public class PaginatedListDTO<T>
    {
		public int PageIndex { get; private set; }

		public int TotalPages { get; private set; }

		public bool HasPreviousPage { get; set; }

		public bool HasNextPage { get; set; }

		public List<T> Items { get; set; }
	}
}
