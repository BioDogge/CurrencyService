using CurrencyService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CurrencyService.Data
{
	public class CurrentCurrencyService : ICurrentCurrencyService
	{
		private readonly IHttpClientFactory _factory;

		public CurrentCurrencyService(IHttpClientFactory factory)
		{
			_factory = factory;
		}

		//Метод, отвечающий за получение списка валют с заданного URL-адреса

		private async Task<IEnumerable<Currency>> GetCurrenciesFromServer()
		{
			var client = _factory.CreateClient("currencyClient");
			var responseContent = await client.GetAsync("daily_json.js");

			if (responseContent.IsSuccessStatusCode)
			{
				string contentToString = await responseContent.Content.ReadAsStringAsync();
				string stringAsJson = JObject.Parse(contentToString)["Valute"]?.ToString();

				var stringToListOfCurrency = JsonConvert.DeserializeObject<Dictionary<string, Currency>>(stringAsJson)?.Select(c => c.Value);

				return stringToListOfCurrency;
			}
			else
				throw new InvalidOperationException("Ошибка при получении курса валют.");
		}

		public async Task<IEnumerable<Currency>> GetCurrencies(PagingInfo pagingInfo)
		{
			var currencies = (await GetCurrenciesFromServer())
							.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.ItemsPerPage)
							.Take(pagingInfo.ItemsPerPage);

			return currencies;
		}

		public async Task<Currency> GetCurrencyById(string currencyId)
		{
			var currencies = (await GetCurrenciesFromServer())
							.FirstOrDefault(c => c.Id.ToLower() == currencyId.ToLower());

			return currencies;
		}
	}
}