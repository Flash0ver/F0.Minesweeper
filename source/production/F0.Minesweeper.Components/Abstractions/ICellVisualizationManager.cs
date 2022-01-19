using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Logic.Cell;

namespace F0.Minesweeper.Components.Abstractions
{
	internal interface ICellVisualizationManager
	{
		CellVisualization GetVisualization(CellStatusType cellStatus);
		CellVisualization GetVisualization(CellStatusType cellStatus, byte? adjacentMineCount);
	}
}
