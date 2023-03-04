using AutoMapper;
using CurrencyService.Dtos;
using CurrencyService.Models;

namespace CurrencyService.Profiles
{
	public class CurrencyProfile : Profile
	{
        public CurrencyProfile()
        {
            CreateMap<Currency, CurrencyReadDto>();
        }
    }
}