using System.Threading.Tasks;
using F0.Minesweeper.Components.Extensions;
using F0.Minesweeper.Logic;
using F0.Minesweeper.Logic.Abstractions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace F0.Minesweeper.App
{
	internal static class Program
	{
		private static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddMinesweeperComponentsServices();
			builder.Services.AddSingleton<IMinefieldFactory>((serviceProvider) => new MinefieldFactory());
			
			await builder.Build().RunAsync();
		}
	}
}
