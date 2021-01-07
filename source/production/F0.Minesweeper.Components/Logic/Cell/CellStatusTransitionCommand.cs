using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal record CellStatusTransitionCommand
	{
		internal MouseButtonType MouseButtonType { get; init; }
		internal bool? IsMine { get; init; }

		public CellStatusTransitionCommand(MouseButtonType mouseButtonType, bool? isMine = null)
		{
			MouseButtonType = mouseButtonType;
			IsMine = isMine;
		}
	}
}
