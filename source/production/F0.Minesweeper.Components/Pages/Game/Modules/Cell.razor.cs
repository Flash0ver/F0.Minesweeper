using System;
using System.Diagnostics;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Logic.Cell;
using F0.Minesweeper.Logic.Abstractions;
using Microsoft.AspNetCore.Components;

namespace F0.Minesweeper.Components.Pages.Game.Modules
{
	public partial class Cell : ICell
	{
		[Parameter]
		public Location? Location { get; set; }

		public Func<Location, Task>? UncoveredAsync { get; set; }

		[Inject]
		private ICellStatusManager statusManager { get; set; }

		[Inject]
		private ICellVisualisationManager? visualisationManager { get; set; }

		private char StatusText
		{
			get => statusText;
			set
			{
				if (statusText != value)
				{
					statusText = value;
				}
			}
		}

		private string CssClass
		{
			get => cssClass;
			set
			{
				if (cssClass != value)
				{
					cssClass = value;
				}
			}
		}

		private bool IsDisabled { get; set; }

		private char statusText;
		private string cssClass;

		public Cell()
		{
			statusManager = DefaultCellStatusManager.Instance;
			statusText = CellVisualisation.Default.Content;
			cssClass = CellVisualisation.Default.CssClass;
		}

		protected override void OnParametersSet()
		{
			if (Location is null)
			{
				throw new ArgumentNullException(nameof(Location));
			}
		}

		public void SetUncoveredStatus(CellInteractionType cellInteraction, bool isMine, byte adjacentMineCount)
		{
			if (!TryUpdateStatus(cellInteraction, isMine, adjacentMineCount))
			{
				if (cellInteraction != CellInteractionType.Automatic)
				{
					throw new InvalidOperationException($"Uncover is not allowed from the status '{statusManager.CurrentStatus}'.");
				}
			}
		}

		public void DisableClick()
		{
			IsDisabled = true;
			StateHasChanged();
		}

		private async Task OnClickAsync()
		{
			if (UncoveredAsync != null && Location is not null && statusManager.CanMoveNext(CellInteractionType.LeftClick, null))
			{
				await UncoveredAsync(Location);
			}
		}

		private void OnRightClick()
		{
			TryUpdateStatus(CellInteractionType.RightClick);
		}

		private bool TryUpdateStatus(CellInteractionType inputCommand, bool? isMine = null, byte? adjacentMineCount = null)
		{
			if (!statusManager.CanMoveNext(inputCommand, isMine))
			{
				return false;
			}

			if (adjacentMineCount is not null && adjacentMineCount > 8)
			{
				return false;
			}

			CellStatusType newStatus = statusManager.MoveNext(inputCommand, isMine);

			Debug.Assert(visualisationManager is not null, $"The '{nameof(visualisationManager)}' is injected on '{nameof(Cell)}' generation.");
			CellVisualisation visualisation = visualisationManager.GetVisualisation(newStatus, adjacentMineCount);
			StatusText = visualisation.Content;
			CssClass = visualisation.CssClass;
			StateHasChanged();

			return true;
		}
	}
}
