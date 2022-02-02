using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Events;
using Prism.Events;

namespace F0.Minesweeper.Components.Logic.Game
{
	internal class GameLostUpdater : GameUpdater
	{
		private readonly IEventAggregator eventAggregator;

		public GameLostUpdater(IEventAggregator eventAggregator)
			=> this.eventAggregator = eventAggregator;

		protected override Task OnUpdateAsync(IEnumerable<UncoverableCell> uncoverableCells, Minesweeper.Logic.Abstractions.Location clickedLocation)
		{
			foreach (UncoverableCell uncoverableCell in uncoverableCells)
			{
				CellInteractionType interaction = CellInteractionType.GameLost;

				if (uncoverableCell.Cell.Location == clickedLocation)
				{
					interaction = CellInteractionType.LeftClick;
				}

				uncoverableCell.Cell.SetUncoveredStatus(interaction, uncoverableCell.IsMine, uncoverableCell.AdjacentMineCount);
				uncoverableCell.Cell.DisableClick();
			}

			eventAggregator.GetEvent<GameFinishedEvent>().Publish("You lost the game!");
			return Task.CompletedTask;
		}
	}
}
