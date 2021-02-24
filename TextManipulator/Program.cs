using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TextManipulator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Add AntDesign.
            builder.Services.AddAntDesign();

            builder.RootComponents.Add<App>("#app");
            builder.Services.AddScoped(_ => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            });

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}
