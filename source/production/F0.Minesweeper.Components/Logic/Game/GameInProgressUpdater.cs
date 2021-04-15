using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Game
{
	internal class GameInProgressUpdater : GameUpdater
	{
		internal override void UpdateAsync(List<Components.Cell> cells, Minesweeper.Logic.Abstractions.Location clickedLocation) 
        {
            var uncoverableCells =
				(
					from cell in cells
					join reportCell in report.Cells on cell.Location equals reportCell.Location
					select new { Cell = cell, IsMine = reportCell.IsMine, AdjacentMineCount = reportCell.AdjacentMineCount }
				).ToList();

			foreach(var uncoverableCell in uncoverableCells)
			{
				CellInteractionType interactionType =
					uncoverableCell.Cell.Location == clickedLocation
					? CellInteractionType.LeftClick
					: CellInteractionType.Automatic;

				uncoverableCell.Cell.SetUncoveredStatus(interactionType, uncoverableCell.IsMine, uncoverableCell.AdjacentMineCount);
			}
        }
	}
}