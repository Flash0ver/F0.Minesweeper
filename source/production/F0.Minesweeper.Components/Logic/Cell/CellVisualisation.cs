namespace F0.Minesweeper.Components.Logic.Cell
{
	internal record CellVisualisation(char Content, string CssClass)
	{
		internal static CellVisualisation Default { get; } = new CellVisualisation(' ', "f0-cell f0-cell-covered");
	}
}
