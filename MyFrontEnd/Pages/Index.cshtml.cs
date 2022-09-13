using Dapr.Client;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFrontEnd.Pages;

public class IndexModel : PageModel
{
    private readonly DaprClient _daprClient;
    private readonly ILogger _logger;

    public IndexModel(DaprClient daprClient, ILogger<IndexModel> logger)
    {
        _daprClient = daprClient;
        _logger = logger;
    }

    public async Task OnGet()
    {
        _logger.LogInformation("Index page visited at {DT}", DateTime.UtcNow.ToLongTimeString());

        var forecasts = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(
            HttpMethod.Get,
            "mybackend",
            "weatherforecast");

        ViewData["WeatherForecastData"] = forecasts;
    }
}