using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Logic.Abstractions;
using Prism.Events;

namespace F0.Minesweeper.Components.Logic.Game
{
	internal class GameUpdateFactory : IGameUpdateFactory
	{
		private readonly Dictionary<GameStatus, GameUpdater> gameUpdater;

		internal GameUpdateFactory(IEventAggregator eventAggregator)
		{
			gameUpdater = new()
			{
				{ GameStatus.InProgress, new GameInProgressUpdater() },
				{ GameStatus.IsWon, new GameWonUpdater(eventAggregator) },
				{ GameStatus.IsLost, new GameLostUpdater(eventAggregator) },
			};
		}

		public GameUpdater On(GameStatus gameStatus)
		{
			if (gameUpdater.TryGetValue(gameStatus, out GameUpdater? updater))
			{
				return updater;
			}

			throw new ArgumentOutOfRangeException(nameof(gameStatus), $"The {typeof(GameStatus)} '{gameStatus}' has no associated {typeof(GameUpdater)} implementation.");
		}
	}
}
