using System;
using System.Reflection;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Extensions;
using F0.Minesweeper.Components.Services;
using F0.Minesweeper.Logic;
using F0.Minesweeper.Logic.Abstractions;
using Microsoft.AspNetCore.Components.Web;
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
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddMinesweeperComponentsServices();
			builder.Services.AddSingleton<IMinefieldFactory, MinefieldFactory>();
			builder.Services.AddSingleton(CreateVersionInfo);

			await builder.Build().RunAsync();
		}

		private static VersionInfo CreateVersionInfo(IServiceProvider serviceProvider)
		{
			var assembly = Assembly.GetExecutingAssembly();

			return new VersionInfo(assembly);
		}
	}
}
