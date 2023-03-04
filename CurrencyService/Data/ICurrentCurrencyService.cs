using CurrencyService.Models;

namespace CurrencyService.Data
{
	public interface ICurrentCurrencyService
	{
		Task<IEnumerable<Currency>> GetCurrencies(PagingInfo pagingInfo);
		Task<Currency> GetCurrencyById(string currencyId);
	}
}