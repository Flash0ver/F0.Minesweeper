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

		public GameUpdater On(IGameUpdateReport report)
		{
			if (gameUpdater.TryGetValue(report.Status, out GameUpdater updater))
			{
				return updater.WithReport(report);
			}

			throw new ArgumentOutOfRangeException(nameof(report), $"The {typeof(GameStatus)} '{report.Status}' has no associated {typeof(GameUpdater)} implementation.");
		}
	}
}