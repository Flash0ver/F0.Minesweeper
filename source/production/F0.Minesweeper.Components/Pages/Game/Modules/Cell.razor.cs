using System;
using System.Collections.Generic;
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

		private static readonly Dictionary<CellStatusType, CellStatusTranslation> translations = InitializeTranslations();
		private char statusText;
		private string cssClass;

		public Cell()
		{
			CellStatusTranslation translation = GetTranslationOrDefault(CellStatusType.Covered);
			statusText = translation.GetDisplayValue(null);
			cssClass = translation.CssClass;
			statusManager = DefaultCellStatusManager.Instance;
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

		private static Dictionary<CellStatusType, CellStatusTranslation> InitializeTranslations()
		{
			const string cellLayout = "f0-cell";

			return new Dictionary<CellStatusType, CellStatusTranslation>
				{
					{ CellStatusType.Covered, new (' ', $"{cellLayout} f0-cell-covered") },
					{ CellStatusType.Flagged, new ('⚐', $"{cellLayout} f0-cell-covered") },
					{ CellStatusType.FlaggedWrong, new ('⚐', $"{cellLayout} f0-cell-flagged-wrong") },
					{ CellStatusType.Uncovered, new ($"{cellLayout} f0-cell-uncovered") },
					{ CellStatusType.Unsure, new ('?', $"{cellLayout} f0-cell-covered") },
					{ CellStatusType.Mine, new ('☢', $"{cellLayout} f0-cell-uncovered") },
					{ CellStatusType.MineExploded, new ('☢', $"{cellLayout} f0-cell-mine-exploded") }
				};
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

			CellStatusTranslation translation = GetTranslationOrDefault(newStatus);
			StatusText = translation.GetDisplayValue(adjacentMineCount);
			CssClass = translation.CssClass.ToString();
			StateHasChanged();

			return true;
		}

		private static CellStatusTranslation GetTranslationOrDefault(CellStatusType status)
		{
			if (translations.TryGetValue(status, out CellStatusTranslation? translation))
			{
				return translation;
			}

			return new CellStatusTranslation('!', "f0-cell f0-cell-undefinedstatus");
		}
	}
}
