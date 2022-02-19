using System.Text.Json;
using Microsoft.JSInterop;

namespace BlitBot.Data;

public class ChartService 
{
    readonly IJSRuntime js;
    public readonly ChartConfig config;

    public readonly IEnumerable<string> symbolTypes = new[] { "all", "stock", "futures", "forex", "bitcoin%2Ccrypto", "index", "bond", "economic" };

    public IEnumerable<(string, string)> Intervals = new[]
    {
        ("1", "1m"),
        ("3", "3m"),
        ("5", "5m"),
        ("15", "15m"),
        ("30", "30m"),
        ("60", "1h"),
        ("120", "2h"),
        ("180", "3h"),
        ("240", "4h"),
        ("D", "1d"),
        ("W", "1w"),
    };

    public ChartService(IJSRuntime js, ChartConfig config)
    {
        this.js = js;
        this.config = config;
        config.PropertyChanged += (s, r) => DrawChart();
    }

    public async Task DrawChart() => 
        await js.InvokeVoidAsync("charting.show", config.Symbol.Symbol, config.Locale, config.TimeZone, config.Interval);

    const string targetUrl = "https://s3.tradingview.com/external-embedding/embed-widget-technical-analysis.js";
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

    public async Task<IEnumerable<string>> SearchTimeZones(string searchText) =>
        TimeZoneConverter.TZConvert
            .KnownIanaTimeZoneNames
            .Where(tz => tz.ToLower().Contains(searchText.ToLower()));
}