using Microsoft.JSInterop;

namespace BlitBot.Data;

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}


public class ChartConfigChangedEventArgs
{
}

public class ChartConfig
{
    public string Symbol { get; set; }

    public event EventHandler OnChartChanged = 
        (sender, args) => { };

    public void Updated() => 
        OnChartChanged.Invoke(this, new EventArgs());
}

public class ChartService
{
    readonly IJSRuntime js;
    public readonly ChartConfig config;

    public ChartService(IJSRuntime js, ChartConfig config)
    {
        this.js = js;
        this.config = config;
    }

    public async Task DrawChart() => 
        await js.InvokeVoidAsync("charting.show", config.Symbol);
    
    public async Task UpdateChart()
    {
        config.Updated();
    }
}

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTradingView(this IServiceCollection services) => 
        services
            .AddSingleton<ChartConfig>()
            .AddScoped<ChartService>();
}