using System.ComponentModel;
using System.Text.Json;
using Microsoft.JSInterop;

namespace BlitBot.Data;

public class ChartService : IDisposable
{
    readonly IJSRuntime js;
    public readonly ChartConfig config;

    public readonly IEnumerable<string> symbolTypes = new[] { "all", "stock", "futures", "forex", "crypto", "index", "bond", "economic" };

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
        config.PropertyChanged += DrawChartSync;
    }

    private void DrawChartSync(object? sender, PropertyChangedEventArgs e)
    {
        DrawChart();
    }

    public async Task DrawChart() => 
        await js.InvokeVoidAsync("charting.show", config.Symbol.Id, config.Locale, config.TimeZone, config.Interval);

    const string technicalAnalysisJsUrl = "https://s3.tradingview.com/external-embedding/embed-widget-technical-analysis.js";
    public async Task LoadTechnicalAnalysis(string containerElement) =>
        await js.InvokeVoidAsync("scriptLoader", technicalAnalysisJsUrl, containerElement, JsonSerializer.Serialize(new
        {
            interval = "1m",
            width = "100%",
            height = "100%",
            isTransparent = false,
            symbol = config.Symbol.Symbol,
            showIntervalTabs = false,
            locale = config.Locale,
            colorTheme = "dark",
        }));

    public async Task<IEnumerable<string>> SearchTimeZones(string searchText) =>
        TimeZoneConverter.TZConvert
            .KnownIanaTimeZoneNames
            .Where(tz => tz.ToLower().Contains(searchText.ToLower()))
            .OrderBy(tz => tz);

    public void Dispose()
    {
        config.PropertyChanged -= DrawChartSync;
    }
}