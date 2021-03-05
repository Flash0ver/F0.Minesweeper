using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Logic.Cell;
using F0.Minesweeper.Logic.Abstractions;
using Microsoft.AspNetCore.Components;

namespace F0.Minesweeper.Components
{
	public partial class Cell
	{
		[Parameter]
		public Location? Location { get; set; }

		public Func<Location, Task>? UncoveredAsync { get; set; }

		[Inject]
		private ICellStatusManager statusManager { get; set; }

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

		private static Dictionary<CellStatusType, CellStatusTranslation> translations = InitializeTranslations();
		private char statusText;

		public Cell()
		{
			statusText = MapToText(CellStatusType.Covered);
			statusManager = DefaultCellStatusManager.Instance;
		}

		protected override void OnParametersSet()
		{
			if (Location is null)
			{
				throw new ArgumentNullException(nameof(Location));
			}
		}

		internal void SetUncoveredStatus(bool isMine, byte adjacentMineCount)
		{
			if(!TryUpdateStatus(MouseButtonType.Left, isMine, adjacentMineCount))
			{
				throw new InvalidOperationException($"Uncover is not allowed from the status '{statusManager.CurrentStatus}'.");
			}

		}

		private static Dictionary<CellStatusType, CellStatusTranslation> InitializeTranslations()
		{
			return new Dictionary<CellStatusType, CellStatusTranslation>
				{
					{ CellStatusType.Covered, new ('C') },
					{ CellStatusType.Flagged, new ('⚐') },
					{ CellStatusType.Uncovered, new () },
					{ CellStatusType.Unsure, new ('?') },
					{ CellStatusType.Mine, new ('☢') },
				};
		}

		private async Task OnClickAsync()
		{
			if (UncoveredAsync != null && Location is not null && statusManager.CanMoveNext(MouseButtonType.Left, null))
			{
				await UncoveredAsync(Location);
			}
		}

		private void OnRightClick() => TryUpdateStatus(MouseButtonType.Right);

		private bool TryUpdateStatus(MouseButtonType inputCommand, bool? isMine = null, byte? adjacentMineCount = null)
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
			StatusText = MapToText(newStatus, adjacentMineCount);
			StateHasChanged();

			return true;
		}

		private static char MapToText(CellStatusType status, byte? adjacentMineCount = null)
		{
			return translations.TryGetValue(status, out CellStatusTranslation? translation)
				? translation.GetDisplayValue(adjacentMineCount)
				: '!';
		}
	}
}
