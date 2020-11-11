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
		private ICellStatusManager statusManager { get; set; }

		private string StatusText
		{
			get => statusText ?? string.Empty;
			set
			{
				if (statusText != value)
				{
					statusText = value;
				}
			}
		}

		private static Dictionary<CellStatusType, string> translations = InitializeTranslations();

		private string statusText;

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

		private static Dictionary<CellStatusType, string> InitializeTranslations()
		{
			return new Dictionary<CellStatusType, string>
				{
					{ CellStatusType.Covered, "Covered" },
					{ CellStatusType.Flagged, "Flagged" },
					{ CellStatusType.Uncovered, "Uncovered" },
					{ CellStatusType.Unsure, "?" },
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
			if (!statusManager.CanNext(inputCommand))
			{
				return false;
			}

			CellStatusType newStatus = statusManager.MoveNext(inputCommand);
			StatusText = MapToText(newStatus);
			return true;
		}

		private string MapToText(CellStatusType status)
		{
			if (!translations.TryGetValue(status, out var translation))
			{
				return $"!!{status}!!";
			}

			return translation;
		}
	}
}
