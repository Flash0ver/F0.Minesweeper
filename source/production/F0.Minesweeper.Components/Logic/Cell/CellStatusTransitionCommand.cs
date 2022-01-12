using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal record class CellStatusTransitionCommand
	{
		internal CellInteractionType MouseButtonType { get; }
		internal bool? IsMine { get; }

		public CellStatusTransitionCommand(CellInteractionType mouseButtonType, bool? isMine = null)
		{
			MouseButtonType = mouseButtonType;
			IsMine = isMine;
		}
	}
}
