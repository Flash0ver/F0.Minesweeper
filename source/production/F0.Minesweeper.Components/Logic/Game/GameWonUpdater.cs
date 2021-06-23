using System.Collections.Generic;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Events;
using Prism.Events;

namespace F0.Minesweeper.Components.Logic.Game
{
	internal class GameWonUpdater : GameUpdater
	{
		private readonly IEventAggregator eventAggregator;

		public GameWonUpdater(IEventAggregator eventAggregator)
		{
			this.eventAggregator = eventAggregator;
		}

		protected override Task OnUpdateAsync(IEnumerable<UncoverableCell> uncoverableCells, Minesweeper.Logic.Abstractions.Location clickedLocation)
		{
			foreach (var uncoverableCell in uncoverableCells)
			{
				uncoverableCell.Cell.SetUncoveredStatus(CellInteractionType.GameWon, uncoverableCell.IsMine, uncoverableCell.AdjacentMineCount);
				uncoverableCell.Cell.DisableClick();
			}

			this.eventAggregator.GetEvent<GameFinishedEvent>().Publish("Congratz! You've won the game!");
			return Task.CompletedTask;
		}
	}
}