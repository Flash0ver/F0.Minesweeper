using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class DefaultCellStatusManager : ICellStatusManager
	{
		private static readonly Lazy<DefaultCellStatusManager> lazyInstance = new(() => new DefaultCellStatusManager());

		private DefaultCellStatusManager()
		{
		}

		internal static DefaultCellStatusManager Instance => lazyInstance.Value;
		public CellStatusType CurrentStatus => throw new InvalidOperationException();
		public bool CanMoveNext(CellInteractionType command, bool? isMine) => throw new InvalidOperationException();
		public CellStatusType MoveNext(CellInteractionType command, bool? isMine) => throw new InvalidOperationException();
	}
}
