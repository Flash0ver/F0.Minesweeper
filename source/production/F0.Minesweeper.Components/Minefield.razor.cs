using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
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

		[Inject]
		internal IGameUpdateFactory? GameUpdateFactory { get; set; }
		
		private readonly List<Cell> cells;

		private Cell Cell { set => cells.Add(value); }

		private IMinefield? minefield;

		private bool isValidSize;

		public Minefield()
		{
			Options = new MinefieldOptions(0, 0, 0, MinefieldFirstUncoverBehavior.MayYieldMine);
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
			Debug.Assert(minefield != null, $"The '{nameof(minefield)}' has to be created before an uncover.");
			Debug.Assert(GameUpdateFactory != null, $"The '{nameof(GameUpdateFactory)}' is injected on minefield generation.");

			if (minefield == null)
			{
				throw new InvalidOperationException();
			}

			IGameUpdateReport report = minefield.Uncover(clickedLocation);

			GameUpdateFactory.On(report).UpdateAsync(cells, clickedLocation);
			
			return Task.CompletedTask;
		}
	}
}
