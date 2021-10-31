using System;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class CellStatusTranslation
	{
		private readonly char? activeTranslation;
		private string cssClass;

		internal CellStatusTranslation(string cssClass)
		{
			this.cssClass = cssClass;
		}

		internal CellStatusTranslation(char translation, string cssClass) : this(cssClass)
		{
			activeTranslation = translation;
		}

		internal char GetDisplayValue(byte? adjacentMineCount)
		{
			if (activeTranslation is null && adjacentMineCount is null)
			{
				throw new ArgumentNullException(nameof(adjacentMineCount));
			}

			if (activeTranslation.HasValue)
			{
				return activeTranslation.Value;
			}

			if (adjacentMineCount.HasValue)
			{
				return adjacentMineCount.Value switch
				{
					0 => ' ',
					_ => adjacentMineCount.Value.ToString()[0]
				};
			}

			throw new InvalidOperationException($"Can not attain the display value with {nameof(activeTranslation)} value '{activeTranslation}' and {nameof(adjacentMineCount)} value '{adjacentMineCount}'");
		}

		internal string GetCssClass(byte? adjacentMineCount)
		{
			return string.Format(cssClass, adjacentMineCount);
		}
	}
}
