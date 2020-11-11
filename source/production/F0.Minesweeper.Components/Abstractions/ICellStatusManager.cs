using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Abstractions
{
	public interface ICellStatusManager
	{
		bool CanNext(MouseButtonType command);
		CellStatusType MoveNext(MouseButtonType command);
	}
}
