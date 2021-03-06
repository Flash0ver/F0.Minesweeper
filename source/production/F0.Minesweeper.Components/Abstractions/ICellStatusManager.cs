using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Abstractions
{
	public interface ICellStatusManager
	{
		bool CanMoveNext(MouseButtonType command, bool? isMine);
		CellStatusType MoveNext(MouseButtonType command, bool? isMine);
		CellStatusType CurrentStatus { get; }
	}
}
