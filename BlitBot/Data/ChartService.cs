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
        await js.InvokeVoidAsync("charting.show", config.Symbol);
}