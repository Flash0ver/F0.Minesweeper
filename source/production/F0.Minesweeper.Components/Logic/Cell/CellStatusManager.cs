using System;
using System.Collections.Generic;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Components.Logic.Cell
{
	internal class CellStatusManager : ICellStatusManager
	{
		private readonly Dictionary<CellStatusTransition, CellStatusType> transitions;
		private CellStatusType currentStatus;

		internal CellStatusManager()
		{
			currentStatus = CellStatusType.Covered;
			transitions = new Dictionary<CellStatusTransition, CellStatusType>
			{
				{ new CellStatusTransition(CellStatusType.Covered, MouseButtonType.Left), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Covered, MouseButtonType.Right), CellStatusType.Flagged },
				{ new CellStatusTransition(CellStatusType.Flagged, MouseButtonType.Right), CellStatusType.Unsure },
				{ new CellStatusTransition(CellStatusType.Unsure, MouseButtonType.Left), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Unsure, MouseButtonType.Right), CellStatusType.Covered }
			};
		}

		public bool CanMoveNext(MouseButtonType command)
		{
			return GetNext(command).CanMoveNext;
		}

		public CellStatusType MoveNext(MouseButtonType command)
		{
			var getNextResult = GetNext(command);

			if(!getNextResult.CanMoveNext)
			{
				throw new InvalidOperationException($"No transition from cell status '{currentStatus}' registered for command '{command}'.");
			}

			currentStatus = getNextResult.NextStatus;
			return currentStatus;
		}

		private (bool CanMoveNext, CellStatusType NextStatus) GetNext(MouseButtonType command)
		{
			var transition = new CellStatusTransition(currentStatus, command);

			if (transitions.TryGetValue(transition, out CellStatusType nextStatus))
			{
				return (true, nextStatus);
			}
			return (false, CellStatusType.Undefined);
		}
	}
}
