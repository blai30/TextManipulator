using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.AntDesign;
using Blazorise.Icons.FontAwesome;

namespace TextManipulator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Add Blazorise.
            builder.Services
                .AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddAntDesignProviders()
                .AddFontAwesomeIcons();

            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(_ => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            var host = builder.Build();

            // Use Blazorise.
            host.Services
                .UseAntDesignProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
