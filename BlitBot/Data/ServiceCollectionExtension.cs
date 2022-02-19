namespace BlitBot.Data;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddTradingView(this IServiceCollection services) => 
        services
            .AddSingleton<ChartConfig>()
            .AddTransient<SymbolSearchForm>()
            .AddScoped<ChartService>();
}