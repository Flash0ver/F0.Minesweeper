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
			_ = serviceCollection.AddSingleton<IEventAggregator, EventAggregator>();
			_ = serviceCollection.AddTransient(CellStatusManagerFactory.GetManager);
			_ = serviceCollection.AddSingleton<IGameUpdateFactory>((serviceProvider) => new GameUpdateFactory(serviceProvider.GetRequiredService<IEventAggregator>()));
			_ = serviceCollection.AddSingleton<ICellVisualizationManager, CellVisualizationManager>();
			return serviceCollection;
		}
	}
}
