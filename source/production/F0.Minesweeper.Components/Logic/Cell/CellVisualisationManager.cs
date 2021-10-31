using System.Collections.Generic;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class CellVisualisationManager : ICellVisualisationManager
	{
		private static readonly Dictionary<CellStatusType, CellStatusTranslation> translations = InitializeTranslations();
		public CellVisualisation GetVisualisation(CellStatusType cellStatus) => GetVisualisation(cellStatus, null);
		public CellVisualisation GetVisualisation(CellStatusType cellStatus, byte? adjacentMineCount)
		{
			CellStatusTranslation translation = GetTranslationOrDefault(cellStatus);

			char content = translation.GetDisplayValue(adjacentMineCount);
			string cssClass = translation.GetCssClass(adjacentMineCount);

			return new(content, cssClass);
		}

		private static CellStatusTranslation GetTranslationOrDefault(CellStatusType status)
		{
			if (translations.TryGetValue(status, out CellStatusTranslation? translation))
			{
				return translation;
			}

			return new CellStatusTranslation('!', "f0-cell f0-cell-undefinedstatus");
		}

		private static Dictionary<CellStatusType, CellStatusTranslation> InitializeTranslations()
		{
			const string cellLayout = "f0-cell";

			return new Dictionary<CellStatusType, CellStatusTranslation>
				{
					{ CellStatusType.Covered, new (' ', $"{cellLayout} f0-cell-covered") },
					{ CellStatusType.Flagged, new ('⚐', $"{cellLayout} f0-cell-covered") },
					{ CellStatusType.FlaggedWrong, new ('⚐', $"{cellLayout} f0-cell-flagged-wrong") },
					{ CellStatusType.Uncovered, new ($"{cellLayout} f0-cell-uncovered f0-cell-{{0}}-adjacent-mines") },
					{ CellStatusType.Unsure, new ('?', $"{cellLayout} f0-cell-covered") },
					{ CellStatusType.Mine, new ('☢', $"{cellLayout} f0-cell-uncovered") },
					{ CellStatusType.MineExploded, new ('☢', $"{cellLayout} f0-cell-mine-exploded") }
				};
		}
	}
}
