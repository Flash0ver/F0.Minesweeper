using System.Diagnostics;
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
		[EditorRequired]
		public Location? Location { get; set; }

		public Func<Location, Task>? UncoveredAsync { get; set; }

		[Inject]
		private ICellStatusManager StatusManager { get; set; }

		[Inject]
		private ICellVisualizationManager? VisualizationManager { get; set; }

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
			StatusManager = DefaultCellStatusManager.Instance;
			statusText = CellVisualization.Default.Content;
			cssClass = CellVisualization.Default.CssClass;
		}

		protected override void OnParametersSet()
			=> ArgumentNullException.ThrowIfNull(Location);

		public void SetUncoveredStatus(CellInteractionType cellInteraction, bool isMine, byte adjacentMineCount)
		{
			if (!TryUpdateStatus(cellInteraction, isMine, adjacentMineCount))
			{
				if (cellInteraction != CellInteractionType.Automatic)
				{
					throw new InvalidOperationException($"Uncover is not allowed from the status '{StatusManager.CurrentStatus}'.");
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
			if (UncoveredAsync != null && Location is not null && StatusManager.CanMoveNext(CellInteractionType.LeftClick, null))
			{
				await UncoveredAsync(Location);
			}
		}

		private void OnRightClick()
			=> _ = TryUpdateStatus(CellInteractionType.RightClick);

		private bool TryUpdateStatus(CellInteractionType inputCommand, bool? isMine = null, byte? adjacentMineCount = null)
		{
			if (!StatusManager.CanMoveNext(inputCommand, isMine))
			{
				return false;
			}

			if (adjacentMineCount is not null and > 8)
			{
				return false;
			}

			CellStatusType newStatus = StatusManager.MoveNext(inputCommand, isMine);

			Debug.Assert(VisualizationManager is not null, $"The '{nameof(VisualizationManager)}' is injected on '{nameof(Cell)}' generation.");
			CellVisualization visualization = VisualizationManager.GetVisualization(newStatus, adjacentMineCount);
			StatusText = visualization.Content;
			CssClass = visualization.CssClass;
			StateHasChanged();

			return true;
		}
	}
}
