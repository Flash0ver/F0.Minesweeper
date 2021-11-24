using System;
using System.Collections.Generic;
using System.Globalization;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class CellVisualisationManager : ICellVisualisationManager
	{
		private static readonly Dictionary<CellStatusType, (char? content, string cssClass)> visualisationTemplates = InitializeTranslations();
		public CellVisualisation GetVisualisation(CellStatusType cellStatus) => GetVisualisation(cellStatus, null);
		public CellVisualisation GetVisualisation(CellStatusType cellStatus, byte? adjacentMineCount)
		{
			(char? content, string cssClass) template = GetVisualisationTemplateOrDefault(cellStatus);

			string cssClass = string.Format(CultureInfo.InvariantCulture, template.cssClass, adjacentMineCount);

			if(template.content is null && adjacentMineCount is null)
			{
				throw new ArgumentNullException(nameof(adjacentMineCount));
			}

			char content = template.content ?? GetAdjacentMineCountContent(adjacentMineCount!.Value);

			return new(content, cssClass);
		}

		private static char GetAdjacentMineCountContent(byte adjacentMineCount)
		{
			return adjacentMineCount switch
			{
				0 => ' ',
				_ => adjacentMineCount.ToString(CultureInfo.InvariantCulture)[0]
			};
		}

		private static (char? content, string cssClass) GetVisualisationTemplateOrDefault(CellStatusType status)
		{
			if (visualisationTemplates.TryGetValue(status, out (char?, string) translation))
			{
				return translation;
			}

			return new ('!', "f0-cell f0-cell-undefinedstatus");
		}

		private static Dictionary<CellStatusType, (char?, string)> InitializeTranslations()
		{
			const string cellLayout = "f0-cell";

			return new Dictionary<CellStatusType, (char?, string)>
				{
					{ CellStatusType.Covered, new (' ', $"{cellLayout} f0-cell-covered") },
					{ CellStatusType.Flagged, new ('⚐', $"{cellLayout} f0-cell-covered") },
					{ CellStatusType.FlaggedWrong, new ('⚐', $"{cellLayout} f0-cell-flagged-wrong") },
					{ CellStatusType.Uncovered, new (null, $"{cellLayout} f0-cell-uncovered f0-cell-{{0}}-adjacent-mines") },
					{ CellStatusType.Unsure, new ('?', $"{cellLayout} f0-cell-covered") },
					{ CellStatusType.Mine, new ('☢', $"{cellLayout} f0-cell-uncovered") },
					{ CellStatusType.MineExploded, new ('☢', $"{cellLayout} f0-cell-mine-exploded") }
				};
		}
	}
}
