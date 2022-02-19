using System.Text.Json;
using Microsoft.JSInterop;

namespace BlitBot.Data;

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
        await js.InvokeVoidAsync("charting.show", config.Symbol, config.Locale, config.TimeZone, config.Interval);

    string targetUrl = "https://s3.tradingview.com/external-embedding/embed-widget-technical-analysis.js";
    public async Task LoadTechnicalAnalysis(string containerElement) =>
        await js.InvokeVoidAsync("scriptLoader", targetUrl, containerElement, JsonSerializer.Serialize(new
        {
            interval = "1m",
            width = "100%",
            isTransparent = false,
            height = "100%",
            symbol = config.Symbol,
            showIntervalTabs = true,
            locale = config.Locale,
            colorTheme = "dark",
        }));
}