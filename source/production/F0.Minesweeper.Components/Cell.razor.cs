using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Logic.Abstractions;
using Microsoft.AspNetCore.Components;

namespace F0.Minesweeper.Components
{
	public partial class Cell
	{
		[Parameter]
		public Location? Location { get; set; }
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

		private CellStatus status { get; set; }
		private string? statusText;

		public Cell()
		{
			UpdateStatus(CellStatus.Covered);
		}

		protected override void OnParametersSet()
		{
			if (Location is null)
			{
				throw new ArgumentNullException(nameof(Location));
			}
		}

		private Task OnClickAsync()
		{
			UpdateStatus(CellStatus.Uncovered);
			return Task.CompletedTask;
		}

		private void OnRightClick()
		{
			CellStatus newStatus = CellStatusManager.OnRightMouseClick(status);
			UpdateStatus(newStatus);
		}

		private void UpdateStatus(CellStatus covered)
		{
			status = covered;
			StatusText = CellStatusManager.MapToText(covered);
		}

		private static class CellStatusManager
		{
			private static Lazy<Dictionary<CellStatus, string>> translations = new Lazy<Dictionary<CellStatus, string>>(InitializeTranslations);

			internal static string MapToText(CellStatus status)
			{
				if (!translations.Value.TryGetValue(status, out var translation))
				{
					return $"!!{status}!!";
				}

				return translation;
			}

			internal static CellStatus OnRightMouseClick(CellStatus status)
			{
				if (status == CellStatus.Unsure)
				{
					return CellStatus.Covered;
				}

				return (CellStatus)((int)status + 1);
			}

			private static Dictionary<CellStatus, string> InitializeTranslations()
			{
				return new Dictionary<CellStatus, string>
				{
					{ CellStatus.Covered, "Covered" },
					{ CellStatus.Flagged, "Flagged" },
					{ CellStatus.Uncovered, "Uncovered" },
					{ CellStatus.Unsure, "?" },
				};
			}
		}
	}
}
