using System.Collections.Generic;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Game
{
	internal class GameInProgressUpdater : GameUpdater
	{
		protected override Task OnUpdateAsync(IEnumerable<UncoverableCell> uncoverableCells, Minesweeper.Logic.Abstractions.Location clickedLocation)
		{
			foreach (UncoverableCell uncoverableCell in uncoverableCells)
			{
				CellInteractionType interactionType =
					uncoverableCell.Cell.Location == clickedLocation
					? CellInteractionType.LeftClick
					: CellInteractionType.Automatic;

				uncoverableCell.Cell.SetUncoveredStatus(interactionType, uncoverableCell.IsMine, uncoverableCell.AdjacentMineCount);
			}

			return Task.CompletedTask;
		}
	}
}
