using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Events;
using F0.Minesweeper.Components.Logic.Game;
using F0.Minesweeper.Logic.Abstractions;
using Microsoft.AspNetCore.Components;
using Prism.Events;

namespace F0.Minesweeper.Components.Pages.Game.Modules
{
	public partial class Minefield
	{
		[Parameter]
		public MinefieldOptions Options { get; set; }

		[Inject]
		internal IEventAggregator? EventAggregator { get; set; }

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
			Options = new MinefieldOptions(0, 0, 0, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);
			cells = new List<Cell>();
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();
			EventAggregator?.GetEvent<DifficultyLevelChangedEvent>().Subscribe(OnDifficultyChanged);
		}

		protected override void OnParametersSet()
		{
			isValidSize = Options.Height > 0 && Options.Width > 0;

			if (isValidSize)
			{
				minefield = MinefieldFactory?.Create(Options);				
			} 			
		}

		private void OnDifficultyChanged(DifficultyLevel difficultyLevel)
		{
			switch (difficultyLevel)
			{
				case DifficultyLevel.Easy:
					Options = new(5, 5, 5, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);
					break;
				case DifficultyLevel.Medium:
					Options = new(10, 10, 10, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);
					break;
				case DifficultyLevel.Hard:
					Options = new(15, 15, 15, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);
					break;
			}

			minefield = MinefieldFactory?.Create(Options);

			// TODO : still need something like reset selection
			// IGameUpdateReport report = GameUpdateReport(GameStatus.DifficultyChanged, allUncoveredCells.ToArray());
			// GameUpdateFactory?.On(GameStatus.DifficultyChanged).WithReport(report).UpdateAsync(cells, null);
		}

		protected override void OnAfterRender(bool firstRender)
		{
			foreach (Cell cell in cells)
			{
				cell.UncoveredAsync += OnCellUncoveredAsync;
			}
		}

		private async Task OnCellUncoveredAsync(Location clickedLocation)
		{
			Debug.Assert(GameUpdateFactory != null, $"The '{nameof(GameUpdateFactory)}' is injected on minefield generation.");
			
			if(minefield == null)
			{
				throw new InvalidOperationException($"The '{nameof(minefield)}' has to be created before an uncover.");
			}

			IGameUpdateReport report = minefield.Uncover(clickedLocation);

			await GameUpdateFactory.On(report.Status).WithReport(report).UpdateAsync(cells, clickedLocation);
		}
	}
}
