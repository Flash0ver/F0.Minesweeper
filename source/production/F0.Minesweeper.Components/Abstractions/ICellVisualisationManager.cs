using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Logic.Cell;

namespace F0.Minesweeper.Components.Abstractions
{
	internal interface ICellVisualisationManager
	{
		CellVisualisation GetVisualisation(CellStatusType cellStatus);
		CellVisualisation GetVisualisation(CellStatusType cellStatus, byte? adjacentMineCount);
	}
}
