namespace CurrencyService.Models
{
	/// <summary>
	/// Класс, отвечающий за пагинацию списка курса валют.
	/// </summary>
	public class PagingInfo
	{
		private const int DEFAULT_ITEMS_PER_PAGE = 5;
		private const int DEFAULT_CURRENT_PAGE = 1;
		
		private int itemsPerPage = DEFAULT_ITEMS_PER_PAGE;
		private int currentPage = DEFAULT_CURRENT_PAGE;

		public int ItemsPerPage
		{
			get => itemsPerPage;
			set => itemsPerPage = (value >= 1) ? value : DEFAULT_ITEMS_PER_PAGE;
		}
		public int CurrentPage
		{
			get => currentPage;
			set => currentPage = (value >= 1) ? value : DEFAULT_CURRENT_PAGE;
		}
	}
}