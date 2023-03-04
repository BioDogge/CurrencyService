using CurrencyService.Data;

namespace CurrencyService
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			builder.Services.AddHttpClient("currencyClient", c =>
			{
				c.BaseAddress = new Uri("https://www.cbr-xml-daily.ru/daily_json.js");
			});

			builder.Services.AddTransient<ICurrentCurrencyService, CurrentCurrencyService>();
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			var app = builder.Build();

			app.UseRouting();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

			app.Run();
		}
	}
}