namespace F0.Minesweeper.Components.Logic.Cell
{
	internal record CellVisualization(char Content, string CssClass)
	{
		internal static CellVisualization Default { get; } = new CellVisualization(' ', "f0-cell f0-cell-covered");
	}
}
