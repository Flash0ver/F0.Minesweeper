using System;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class DefaultCellStatusManager : ICellStatusManager
	{
		public CellStatusType CurrentStatus => throw new InvalidOperationException();
		public bool CanMoveNext(MouseButtonType command, bool? isMine) => throw new InvalidOperationException();
		public CellStatusType MoveNext(MouseButtonType command, bool? isMine) => throw new InvalidOperationException();
	}
}
