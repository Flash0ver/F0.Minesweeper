using System;
using System.Collections.Generic;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class CellStatusManager : ICellStatusManager
	{
		public CellStatusType CurrentStatus { get; private set; }

		private readonly Dictionary<CellStatusTransition, CellStatusType> transitions;

		internal CellStatusManager()
		{
			CurrentStatus = CellStatusType.Covered;
			transitions = new Dictionary<CellStatusTransition, CellStatusType>
			{
				{ new CellStatusTransition(CellStatusType.Covered, new (MouseButtonType.Left, false)), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Covered, new (MouseButtonType.Left, true)), CellStatusType.Mine },
				{ new CellStatusTransition(CellStatusType.Covered, new (MouseButtonType.Left)), CellStatusType.Undefined },
				{ new CellStatusTransition(CellStatusType.Covered, new (MouseButtonType.Right)), CellStatusType.Flagged },
				{ new CellStatusTransition(CellStatusType.Flagged, new (MouseButtonType.Right)), CellStatusType.Unsure },
				{ new CellStatusTransition(CellStatusType.Unsure, new (MouseButtonType.Left, false)), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Unsure, new (MouseButtonType.Left, true)), CellStatusType.Mine },
				{ new CellStatusTransition(CellStatusType.Unsure, new (MouseButtonType.Left)), CellStatusType.Undefined },
				{ new CellStatusTransition(CellStatusType.Unsure, new (MouseButtonType.Right)), CellStatusType.Covered }
			};
		}

		public bool CanMoveNext(MouseButtonType command, bool? isMine)
		{
			return GetNext(command, isMine).CanMoveNext;
		}

		public CellStatusType MoveNext(MouseButtonType command, bool? isMine)
		{
			var getNextResult = GetNext(command, isMine);

			if(!getNextResult.CanMoveNext)
			{
				throw new InvalidOperationException($"No transition from cell status '{CurrentStatus}' registered for command '{command}'.");
			}

			if(getNextResult.NextStatus == CellStatusType.Undefined)
			{
				throw new InvalidOperationException($"The transition from cell status '{CurrentStatus}' is not allowed for {nameof(isMine)} value '{isMine}'.");
			}

			CurrentStatus = getNextResult.NextStatus;
			return CurrentStatus;
		}

		private (bool CanMoveNext, CellStatusType NextStatus) GetNext(MouseButtonType mouseInput, bool? isMine)
		{
			var command = new CellStatusTransitionCommand(mouseInput, isMine);
			var transition = new CellStatusTransition(CurrentStatus, command);

			if (transitions.TryGetValue(transition, out CellStatusType nextStatus))
			{
				return (true, nextStatus);
			}
			return (false, CellStatusType.Undefined);
		}
	}
}
