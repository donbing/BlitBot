using BlitBot.Data;

public static class WebAppExtensions 
{
    public static void UseErrorHandlerForLiveEnvs(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddTradingView();
        services.AddHttpClient();
    }
}