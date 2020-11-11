using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class CellStatusTransition
	{
		private readonly CellStatusType processState;
		private readonly MouseButtonType command;

		public CellStatusTransition(CellStatusType processState, MouseButtonType command) 
		{
			this.processState = processState;
			this.command = command;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return 23 + 19 * processState.GetHashCode() + 19 * processState.GetHashCode();
			}
		}

		public override bool Equals(object? obj)
		{
			var other = obj as CellStatusTransition;

			return other != null
				&& processState == other.processState
				&& command == other.command;
		}
	}
}
