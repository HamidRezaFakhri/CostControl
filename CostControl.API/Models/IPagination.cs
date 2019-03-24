namespace CostControl.API.Models
{
	/// <summary>
	/// Provides the interface(s) for paged list of any type.
	/// </summary>
	public interface IPagination
	{
		string CurrentSortOrder { get; }
		
		/// <summary>
		/// Gets the has next page.
		/// </summary>
		/// <value>The has next page.</value>
		bool HasNextPage { get; }

		/// <summary>
		/// Gets the has previous page.
		/// </summary>
		/// <value>The has previous page.</value>
		bool HasPreviousPage { get; }

		/// <summary>
		/// Gets the page index (current).
		/// </summary>
		int PageNumber { get; }

		/// <summary>
		/// Gets the page size.
		/// </summary>
		int PageSize { get; }

		/// <summary>
		/// Gets the total count of the list of type <typeparamref name="T"/>
		/// </summary>
		int TotalCount { get; }

		string SearchKey { get; }

		string SortDirection { get; }

		string SortOrder { get; }

		int TotalPages { get; }
	}
}