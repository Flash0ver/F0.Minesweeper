using System.Collections.Generic;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Game
{
	internal class GameDifficultyChangedUpdater : GameUpdater
	{			
		public GameDifficultyChangedUpdater()
		{			
		}

		protected override Task OnUpdateAsync(IEnumerable<UncoverableCell> uncoverableCells, Minesweeper.Logic.Abstractions.Location clickedLocation)
		{
			// TODO : Investigate how to create a report for reseting previous selected cells 
			foreach (var uncoverableCell in uncoverableCells)
			{
				uncoverableCell.Cell.SetUncoveredStatus(CellInteractionType.Automatic, uncoverableCell.IsMine, uncoverableCell.AdjacentMineCount);				
			}
			return Task.CompletedTask;
		}
	}
}
