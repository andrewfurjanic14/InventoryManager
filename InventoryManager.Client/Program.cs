using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace InventoryManager.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Configure HttpClient to call the API. ApiBaseUrl can be overridden in wwwroot/appsettings.json
            var apiBase = builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBase) });

            // Register typed services for business logic
            builder.Services.AddScoped<Services.OilService>();
            builder.Services.AddScoped<Services.ProviderService>();
            builder.Services.AddScoped<Services.OilBatchService>();

            await builder.Build().RunAsync();
        }
    }
}
