using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.model;

namespace web.Pages;

public class IndexModel : PageModel
{
    public WeatherForecast[] Forecasts { get; set; }

    private readonly ILogger<IndexModel> _logger; 

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task OnGet([FromServices] DaprClient dapr)
    {
        Forecasts = await DaprClient.CreateInvokeHttpClient("ticketing")
                                    .GetFromJsonAsync<WeatherForecast[]>("weatherforecast");
    }
}