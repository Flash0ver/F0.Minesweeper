using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Abstractions
{
	public interface ICellStatusManager
	{
		bool CanMoveNext(CellInteractionType command, bool? isMine);
		CellStatusType MoveNext(CellInteractionType command, bool? isMine);
		CellStatusType CurrentStatus { get; }
	}
}
