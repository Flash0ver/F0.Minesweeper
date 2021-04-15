using System;
using System.Collections.Generic;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Components.Logic.Game
{
	internal class GameUpdateFactory : IGameUpdateFactory
	{
		private readonly Dictionary<GameStatus, GameUpdater> gameUpdater;

		internal GameUpdateFactory()
		{
			gameUpdater = new()
			{
				{ GameStatus.InProgress, new GameInProgressUpdater() },
				{ GameStatus.IsWon, new GameWonUpdater() },
				{ GameStatus.IsLost, new GameLostUpdater() }
			};
		}

		public GameUpdater On(GameStatus gameStatus)
		{
			if (gameUpdater.TryGetValue(gameStatus, out GameUpdater updater))
			{
				return updater;
			}

			throw new ArgumentOutOfRangeException(nameof(gameStatus), $"The {typeof(GameStatus)} '{gameStatus}' has no associated {typeof(GameUpdater)} implementation.");
		}
	}
}