using CurrencyService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;

namespace CurrencyService.Data
{
	public class CurrentCurrencyService : ICurrentCurrencyService
	{
		private readonly IHttpClientFactory _factory;

		public CurrentCurrencyService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

		private async Task<IEnumerable<Currency>> GetCurrenciesFromServer()
		{
			var client = _factory.CreateClient("currencyClient");
			var responseContent = await client.GetAsync("daily_json.js");
			var contentToString = await responseContent.Content.ReadAsStringAsync();
			var stringAsJson = JObject.Parse(contentToString)["Valute"]?.ToString();

			//HACK: Десериализацию, возможно, поменять на другую

			var stringToListOfCurrency = JsonConvert.DeserializeObject<Dictionary<string, Currency>>(stringAsJson)?.Select(c => c.Value);

			return stringToListOfCurrency;
		}

		public async Task<IEnumerable<Currency>> GetCurrencies()
		{
			var currencies = await GetCurrenciesFromServer();

			return currencies;
		}

		public async Task<Currency> GetCurrencyById(string currencyId)
		{
			var currencies = await GetCurrenciesFromServer();

			//HACK: Возможно, стоит поменять реализацию сравнения

			return currencies.FirstOrDefault(c => c.Id.ToLower() == currencyId.ToLower());
		}
	}
}