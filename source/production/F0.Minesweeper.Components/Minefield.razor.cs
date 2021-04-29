using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Logic.Abstractions;
using Microsoft.AspNetCore.Components;

namespace F0.Minesweeper.Components
{
	public partial class Minefield
	{
		[Parameter]
		public MinefieldOptions Options { get; set; }

		[Inject]
		internal IMinefieldFactory? MinefieldFactory { get; set; }

		private readonly List<Cell> cells;

		private Cell Cell { set => cells.Add(value); }

		private IMinefield? minefield;

		private bool isValidSize;

		public Minefield()
		{
			Options = new MinefieldOptions(0, 0, 0, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);
			cells = new List<Cell>();
		}

		protected override void OnParametersSet()
		{
			isValidSize = Options.Height > 0 && Options.Width > 0;

			if (isValidSize)
			{
				minefield = MinefieldFactory?.Create(Options);
			}
		}

		protected override void OnAfterRender(bool firstRender)
		{
			foreach(Cell cell in cells)
			{
				cell.UncoveredAsync += OnCellUncoveredAsync;
			}
		}

		private Task OnCellUncoveredAsync(Location clickedLocation)
		{
			if(minefield == null)
			{
				throw new InvalidOperationException($"The '{nameof(minefield)}' has to be created before an uncover.");
			}

			IGameUpdateReport report = minefield.Uncover(clickedLocation);


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

			return Task.CompletedTask;
		}
	}
}
