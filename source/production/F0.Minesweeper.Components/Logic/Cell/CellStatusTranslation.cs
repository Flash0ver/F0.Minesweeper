using System;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class CellStatusTranslation
	{
		private readonly char? activeTranslation;

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

			if(activeTranslation.HasValue)
			{
				return activeTranslation.Value;
			}

			if (adjacentMineCount.HasValue)
			{
				return adjacentMineCount.Value.ToString()[0];
			}

			throw new InvalidOperationException($"Can not attain the display value with {nameof(activeTranslation)} value '{activeTranslation}' and {nameof(adjacentMineCount)} value '{adjacentMineCount}'");
		}
	}
}
