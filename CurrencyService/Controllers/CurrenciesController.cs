using AutoMapper;
using CurrencyService.Data;
using CurrencyService.Dtos;
using CurrencyService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyService.Controllers
{
	[Route("api/[action]")]
	[ApiController]
	public class CurrenciesController : Controller
	{
		private readonly ICurrentCurrencyService _service;
		private readonly IMapper _mapper;

		public CurrenciesController(ICurrentCurrencyService service, IMapper mapper)
        {
            _service = service;
			_mapper = mapper;
        }

		[HttpGet]
		[ActionName("currencies")]
		public async Task<ActionResult<IEnumerable<CurrencyReadDto>>> GetCurrencies([FromQuery] PagingInfo pagingInfo)
		{
			var currencies = await _service.GetCurrencies(pagingInfo);

			if (currencies == null)
				return NotFound();

			return Ok(_mapper.Map<IEnumerable<CurrencyReadDto>>(currencies));
		}

		[HttpGet("{currencyId}")]
		[ActionName("currency")]
		public async Task<ActionResult<CurrencyReadDto>> GetCurrency(string currencyId)
		{
			var currency = await _service.GetCurrencyById(currencyId);

			if (currency == null) 
				return NotFound();

			return Ok(_mapper.Map<CurrencyReadDto>(currency));
		}
    }
}