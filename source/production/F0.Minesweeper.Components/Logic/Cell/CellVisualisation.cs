namespace F0.Minesweeper.Components.Logic.Cell
{
	internal record CellVisualisation(char Content, string CssClass);

	internal record DefaultCellVisualisation() : CellVisualisation(' ', "f0-cell f0-cell-covered");
}
