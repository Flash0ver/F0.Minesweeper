using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace F0.Minesweeper.Server
{
	internal static class Program
	{
		private static void Main(string[] args)
			=> CreateHostBuilder(args).Build().Run();

		private static IHostBuilder CreateHostBuilder(string[] args)
			=> Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
