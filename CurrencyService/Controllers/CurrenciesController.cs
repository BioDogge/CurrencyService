using CurrencyService.Data;
using CurrencyService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyService.Controllers
{
	[Route("api/[action]")]
	[ApiController]
	public class CurrenciesController : Controller
	{
		private readonly ICurrentCurrencyService _service;
		

		public CurrenciesController(ICurrentCurrencyService service)
        {
            _service = service;
        }

		[HttpGet]
		[ActionName("currencies")]
		public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies([FromQuery] PagingInfo pagingInfo)
		{
			var currencies = (await _service.GetCurrencies())
				.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.ItemsPerPage)
				.Take(pagingInfo.ItemsPerPage);

			if (currencies == null)
				return NotFound();

			return Ok(currencies);
		}

		[HttpGet]
		[ActionName("currency")]
		public async Task<ActionResult<Currency>> GetCurrency(string currencyId)
		{
			var currency = await _service.GetCurrencyById(currencyId);

			if (currency == null) 
				return NotFound();

			return Ok(currency);
		}
    }
}