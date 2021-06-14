using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Helpers
{
	public class PagedList <TElement> : List<TElement>
	{
		private readonly IQueryable<TElement> m_elements;

		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalSize { get; set; }
		public bool HasPrevious { get => this.CurrentPage > 1; }
		public bool HasNext { get => this.CurrentPage < TotalPages; }

		public PagedList(IEnumerable<TElement> elements, int totalSize, int currentPage, int pageSize)
		{
			this.TotalSize = totalSize;
			this.TotalPages = (int)Math.Ceiling(totalSize / (float)pageSize);
			this.CurrentPage = currentPage;

			AddRange(elements);
		}

		public static PagedList<TElement> ToPagedList(IEnumerable<TElement> source, int pageIndex, int pageSize)
		{
			var totalSize = source.Count();
			var elements = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

			return new PagedList<TElement>(elements, totalSize, pageIndex, pageSize);
		}

	}
}
