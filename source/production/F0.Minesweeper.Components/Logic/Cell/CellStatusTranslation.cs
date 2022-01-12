using System.Globalization;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class CellStatusTranslation
	{
		private readonly char? activeTranslation;

		internal CellStatusTranslation(string cssClass)
		{
			CssClass = cssClass;
		}

		internal CellStatusTranslation(char translation, string cssClass) : this(cssClass)
		{
			activeTranslation = translation;
		}

		internal string CssClass { get; }

		internal char GetDisplayValue(byte? adjacentMineCount)
		{
			if (activeTranslation is null && adjacentMineCount is null)
			{
				ArgumentNullException.ThrowIfNull(adjacentMineCount);
			}

			if (activeTranslation.HasValue)
			{
				return activeTranslation.Value;
			}

			if (adjacentMineCount.HasValue)
			{
				return adjacentMineCount.Value.ToString(NumberFormatInfo.InvariantInfo)[0];
			}

			throw new InvalidOperationException($"Can not attain the display value with {nameof(activeTranslation)} value '{activeTranslation}' and {nameof(adjacentMineCount)} value '{adjacentMineCount}'");
		}
	}
}
