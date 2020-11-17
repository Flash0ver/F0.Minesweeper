using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Logic.Abstractions;
using Microsoft.AspNetCore.Components;

namespace F0.Minesweeper.Components
{
	public partial class Cell
	{
		[Parameter]
		public Location? Location { get; set; }

		[Inject]
		private ICellStatusManager? statusManager { get; set; }

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

		private static Dictionary<CellStatusType, char> translations = InitializeTranslations();

		private char statusText;

		public Cell()
		{
			statusText = MapToText(CellStatusType.Covered);
		}

		protected override void OnParametersSet()
		{
			if (Location is null)
			{
				throw new ArgumentNullException(nameof(Location));
			}
		}

		private static Dictionary<CellStatusType, char> InitializeTranslations()
		{
			return new Dictionary<CellStatusType, char>
				{
					{ CellStatusType.Covered, 'C' },
					{ CellStatusType.Flagged, '⚐' },
					{ CellStatusType.Uncovered, 'U' },
					{ CellStatusType.Unsure, '?' },
				};
		}

		private Task OnClickAsync()
		{
			TryUpdateStatus(MouseButtonType.Left);

			return Task.CompletedTask;
		}

		private void OnRightClick() => TryUpdateStatus(MouseButtonType.Right);

		private bool TryUpdateStatus(MouseButtonType inputCommand)
		{
			if (statusManager is null || !statusManager.CanMoveNext(inputCommand))
			{
				return false;
			}

			CellStatusType newStatus = statusManager.MoveNext(inputCommand);
			StatusText = MapToText(newStatus);
			return true;
		}

		private char MapToText(CellStatusType status)
			=> translations.TryGetValue(status, out char translation) ? translation : '!';
	}
}
