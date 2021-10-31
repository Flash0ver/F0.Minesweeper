using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Logic.Cell;
using F0.Minesweeper.Components.Logic.Game;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;

namespace F0.Minesweeper.Components.Extensions
{
	public static class IServiceCollectionExtensions
	{
		public static IServiceCollection AddMinesweeperComponentsServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IEventAggregator, EventAggregator>();
			serviceCollection.AddTransient(CellStatusManagerFactory.GetManager);
			serviceCollection.AddSingleton<IGameUpdateFactory>((serviceProvider) => new GameUpdateFactory(serviceProvider.GetRequiredService<IEventAggregator>()));
			serviceCollection.AddSingleton<ICellVisualisationManager, CellVisualisationManager>();
			return serviceCollection;
		}
	}
}
