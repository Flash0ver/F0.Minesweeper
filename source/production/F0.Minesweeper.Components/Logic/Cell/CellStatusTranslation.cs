using System;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class CellStatusTranslation
	{
		private char? activeTranslation;

		internal CellStatusTranslation() { }

		internal CellStatusTranslation(char translation)
		{
			activeTranslation = translation;
		}

		internal char GetDisplayValue(byte? adjacentMineCount)
		{
			if (activeTranslation is null && adjacentMineCount is null)
			{
				throw new ArgumentNullException(nameof(adjacentMineCount));
			}

			return activeTranslation is not null ?
					activeTranslation.Value :
					adjacentMineCount.Value.ToString()[0];
		}
	}
}
