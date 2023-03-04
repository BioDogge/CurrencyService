﻿using CurrencyService.Models;

namespace CurrencyService.Data
{
	public interface ICurrentCurrencyService
	{
		Task<IEnumerable<Currency>> GetCurrencies();
		Task<Currency> GetCurrencyById(string currencyId);
	}
}