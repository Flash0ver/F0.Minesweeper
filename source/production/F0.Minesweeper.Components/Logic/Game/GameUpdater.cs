using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Logic.Abstractions;
using GameModule = F0.Minesweeper.Components.Pages.Game.Modules;

namespace F0.Minesweeper.Components.Logic.Game
{
	internal abstract class GameUpdater
	{
		protected IGameUpdateReport? report;

		internal async Task UpdateAsync(IEnumerable<GameModule.Cell> cells, Location clickedLocation)
		{
			if (report is null)
			{
				throw new InvalidOperationException($"'{nameof(WithReport)}' on game updater '{GetType()}' has not been called before '{nameof(UpdateAsync)}'.");
			}

			IEnumerable<UncoverableCell> uncoverableCells = GetUncoverableCells(cells);

			await OnUpdateAsync(uncoverableCells, clickedLocation);
		}

		internal GameUpdater WithReport(IGameUpdateReport report)
		{
			this.report = report;
			return this;
		}

		protected abstract Task OnUpdateAsync(IEnumerable<UncoverableCell> cells, Location clickedLocation);

		private IEnumerable<UncoverableCell> GetUncoverableCells(IEnumerable<GameModule.Cell> cells)
		{
			Debug.Assert(report is not null, $"'{nameof(WithReport)}' on game updater '{GetType()}' has not been called before '{nameof(GetUncoverableCells)}'.");

			return
				(
					from cell in cells
					join reportCell in report.Cells on cell.Location equals reportCell.Location
					select new UncoverableCell(cell, reportCell.IsMine, reportCell.AdjacentMineCount)
				);
		}
	}
}
