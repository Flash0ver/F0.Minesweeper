using System;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class CellStatusTransition : IEquatable<CellStatusTransition>
	{
		private readonly CellStatusType processState;
		private readonly MouseButtonType command;

		public CellStatusTransition(CellStatusType processState, MouseButtonType command) 
		{
			this.processState = processState;
			this.command = command;
		}

		public static bool operator ==(CellStatusTransition left, CellStatusTransition right)
		{
			if (left is null)
			{
				return right is null;
			}
			return left.Equals(right);
		}

		public static bool operator !=(CellStatusTransition left, CellStatusTransition right)
		{
			return !(left == right);
		}

		public override int GetHashCode() => HashCode.Combine(processState, command);

		public override bool Equals(object? obj)
		{
			return obj is CellStatusTransition
				&& Equals(obj as CellStatusTransition);
		}

		public bool Equals(CellStatusTransition? other)
		{
			return other is not null
				&& processState == other.processState
				&& command == other.command;
		}
	}
}
