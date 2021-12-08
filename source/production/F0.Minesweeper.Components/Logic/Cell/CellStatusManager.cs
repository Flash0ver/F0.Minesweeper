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
				{ new CellStatusTransition(CellStatusType.Covered, new (CellInteractionType.LeftClick, false)), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Covered, new (CellInteractionType.LeftClick, true)), CellStatusType.MineExploded },
				{ new CellStatusTransition(CellStatusType.Covered, new (CellInteractionType.LeftClick)), CellStatusType.Undefined },
				{ new CellStatusTransition(CellStatusType.Unsure, new (CellInteractionType.LeftClick, false)), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Unsure, new (CellInteractionType.LeftClick, true)), CellStatusType.MineExploded },
				{ new CellStatusTransition(CellStatusType.Unsure, new (CellInteractionType.LeftClick)), CellStatusType.Undefined },

				{ new CellStatusTransition(CellStatusType.Covered, new (CellInteractionType.Automatic, false)), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Flagged, new (CellInteractionType.Automatic, false)), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Unsure, new (CellInteractionType.Automatic, false)), CellStatusType.Uncovered },

				{ new CellStatusTransition(CellStatusType.Covered, new (CellInteractionType.RightClick)), CellStatusType.Flagged },
				{ new CellStatusTransition(CellStatusType.Flagged, new (CellInteractionType.RightClick)), CellStatusType.Unsure },
				{ new CellStatusTransition(CellStatusType.Unsure, new (CellInteractionType.RightClick)), CellStatusType.Covered },

				{ new CellStatusTransition(CellStatusType.Covered, new (CellInteractionType.GameWon, true)), CellStatusType.Flagged },
				{ new CellStatusTransition(CellStatusType.Covered, new (CellInteractionType.GameWon, false)), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Flagged, new (CellInteractionType.GameWon, true)), CellStatusType.Flagged },
				{ new CellStatusTransition(CellStatusType.Flagged, new (CellInteractionType.GameWon, false)), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Unsure, new (CellInteractionType.GameWon, true)), CellStatusType.Flagged },
				{ new CellStatusTransition(CellStatusType.Unsure, new (CellInteractionType.GameWon, false)), CellStatusType.Uncovered },
				{ new CellStatusTransition(CellStatusType.Uncovered, new (CellInteractionType.GameWon, false)), CellStatusType.Uncovered },

				{ new CellStatusTransition(CellStatusType.Covered, new (CellInteractionType.GameLost, false)), CellStatusType.Covered },
				{ new CellStatusTransition(CellStatusType.Covered, new (CellInteractionType.GameLost, true)), CellStatusType.Mine },
				{ new CellStatusTransition(CellStatusType.Flagged, new (CellInteractionType.GameLost, true)), CellStatusType.Flagged },
				{ new CellStatusTransition(CellStatusType.Flagged, new (CellInteractionType.GameLost, false)), CellStatusType.FlaggedWrong },
				{ new CellStatusTransition(CellStatusType.Unsure, new (CellInteractionType.GameLost, true)), CellStatusType.Mine },
				{ new CellStatusTransition(CellStatusType.Unsure, new (CellInteractionType.GameLost, false)), CellStatusType.Unsure },
				{ new CellStatusTransition(CellStatusType.Uncovered, new (CellInteractionType.GameLost, false)), CellStatusType.Uncovered },
			};
		}

		public bool CanMoveNext(CellInteractionType command, bool? isMine)
		{
			return GetNext(command, isMine).CanMoveNext;
		}

		public CellStatusType MoveNext(CellInteractionType command, bool? isMine)
		{
			var getNextResult = GetNext(command, isMine);

			if (!getNextResult.CanMoveNext)
			{
				throw new InvalidOperationException($"No transition from cell status '{CurrentStatus}' registered for command '{command}'.");
			}

			if (getNextResult.NextStatus == CellStatusType.Undefined)
			{
				throw new InvalidOperationException($"The transition from cell status '{CurrentStatus}' is not allowed for {nameof(isMine)} value '{isMine}'.");
			}

			CurrentStatus = getNextResult.NextStatus;
			return CurrentStatus;
		}

		private (bool CanMoveNext, CellStatusType NextStatus) GetNext(CellInteractionType mouseInput, bool? isMine)
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
