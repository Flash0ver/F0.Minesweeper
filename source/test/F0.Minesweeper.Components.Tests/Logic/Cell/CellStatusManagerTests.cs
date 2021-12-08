using System.Reflection;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Logic.Cell;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Components.Tests.Logic.Cell
{
	public class CellStatusManagerTests
	{
		[Theory]
		[InlineData(CellStatusType.Covered, CellInteractionType.Automatic, null)]
		[InlineData(CellStatusType.Covered, CellInteractionType.RightClick, true)]
		[InlineData(CellStatusType.Covered, CellInteractionType.RightClick, false)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameWon, null)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameLost, null)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.LeftClick, null)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.LeftClick, true)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.LeftClick, false)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.Automatic, null)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.Automatic, true)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.RightClick, true)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.RightClick, false)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameWon, null)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameLost, null)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.Automatic, null)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.Automatic, true)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.RightClick, true)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.RightClick, false)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameWon, null)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameLost, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.LeftClick, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.LeftClick, true)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.LeftClick, false)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.Automatic, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.Automatic, true)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.Automatic, false)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.RightClick, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.RightClick, true)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.RightClick, false)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.GameWon, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.GameWon, true)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.GameLost, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.GameLost, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.LeftClick, null)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.LeftClick, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.LeftClick, false)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.Automatic, null)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.Automatic, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.Automatic, false)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.RightClick, null)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.RightClick, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.RightClick, false)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameWon, null)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameWon, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameWon, false)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameLost, null)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameLost, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameLost, false)]
		public void CanMoveNext_UndefinedTransition_ReturnsFalse(CellStatusType currentStatus, CellInteractionType command, bool? isMine)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act
			bool result = componentUnderTest.CanMoveNext(command, isMine);

			// Assert
			result.Should().BeFalse();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, CellInteractionType.LeftClick, null)]
		[InlineData(CellStatusType.Covered, CellInteractionType.LeftClick, true)]
		[InlineData(CellStatusType.Covered, CellInteractionType.LeftClick, false)]
		[InlineData(CellStatusType.Covered, CellInteractionType.Automatic, false)]
		[InlineData(CellStatusType.Covered, CellInteractionType.RightClick, null)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameWon, false)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameWon, true)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameLost, false)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameLost, true)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.RightClick, null)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.Automatic, false)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameWon, true)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameWon, false)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameLost, true)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameLost, false)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.LeftClick, null)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.LeftClick, true)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.LeftClick, false)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.RightClick, null)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameWon, true)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameWon, false)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameLost, true)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameLost, false)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.Automatic, false)]
		public void CanMoveNext_DefinedTransition_ReturnsTrue(CellStatusType currentStatus, CellInteractionType command, bool? isMine)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act
			bool result = componentUnderTest.CanMoveNext(command, isMine);

			// Assert
			result.Should().BeTrue();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, CellInteractionType.Automatic, null)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameWon, null)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameLost, null)]
		[InlineData(CellStatusType.Covered, CellInteractionType.RightClick, true)]
		[InlineData(CellStatusType.Covered, CellInteractionType.RightClick, false)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.LeftClick, null)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.LeftClick, true)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.LeftClick, false)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.Automatic, null)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.Automatic, true)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.RightClick, true)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.RightClick, false)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameWon, null)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameLost, null)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.Automatic, null)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.Automatic, true)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.RightClick, true)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.RightClick, false)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameWon, null)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameLost, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.LeftClick, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.LeftClick, true)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.LeftClick, false)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.Automatic, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.Automatic, true)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.Automatic, false)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.RightClick, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.RightClick, true)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.RightClick, false)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.GameWon, true)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.GameWon, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.GameLost, null)]
		[InlineData(CellStatusType.Uncovered, CellInteractionType.GameLost, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.LeftClick, null)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.LeftClick, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.LeftClick, false)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.Automatic, null)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.Automatic, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.Automatic, false)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.RightClick, null)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.RightClick, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.RightClick, false)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameWon, null)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameWon, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameWon, false)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameLost, null)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameLost, true)]
		[InlineData(CellStatusType.Undefined, CellInteractionType.GameLost, false)]
		public void MoveNext_CanNotMoveNext_Throws(CellStatusType currentStatus, CellInteractionType command, bool? isMine)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act && Assert
			Action testAction = () => componentUnderTest.MoveNext(command, isMine);
			testAction.Should().Throw<InvalidOperationException>();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, CellInteractionType.LeftClick, null)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.LeftClick, null)]
		public void MoveNext_MoveToUndefinedStatus_Throws(CellStatusType currentStatus, CellInteractionType command, bool? isMine)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act && Assert
			Action testAction = () => componentUnderTest.MoveNext(command, isMine);
			testAction.Should().Throw<InvalidOperationException>();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, CellInteractionType.LeftClick, false, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Covered, CellInteractionType.LeftClick, true, CellStatusType.MineExploded)]
		[InlineData(CellStatusType.Covered, CellInteractionType.Automatic, false, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Covered, CellInteractionType.RightClick, null, CellStatusType.Flagged)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.RightClick, null, CellStatusType.Unsure)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.LeftClick, false, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.LeftClick, true, CellStatusType.MineExploded)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.RightClick, null, CellStatusType.Covered)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.Automatic, false, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameWon, false, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameWon, true, CellStatusType.Flagged)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameLost, false, CellStatusType.Covered)]
		[InlineData(CellStatusType.Covered, CellInteractionType.GameLost, true, CellStatusType.Mine)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.Automatic, false, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameWon, true, CellStatusType.Flagged)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameWon, false, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameLost, true, CellStatusType.Flagged)]
		[InlineData(CellStatusType.Flagged, CellInteractionType.GameLost, false, CellStatusType.FlaggedWrong)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameWon, true, CellStatusType.Flagged)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameWon, false, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameLost, true, CellStatusType.Mine)]
		[InlineData(CellStatusType.Unsure, CellInteractionType.GameLost, false, CellStatusType.Unsure)]
		public void MoveNext_DefinedTransition_ReturnsNextStatus(CellStatusType currentStatus, CellInteractionType command, bool? isMine, CellStatusType expectedNextStatus)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act
			CellStatusType result = componentUnderTest.MoveNext(command, isMine);

			// Assert
			result.Should().Be(expectedNextStatus);
		}

		private static CellStatusManager CreateCellStatusManager(CellStatusType currentStatus)
		{
			CellStatusManager componentForTest = new();

			PropertyInfo? currentStatusProperty = typeof(CellStatusManager).GetProperty(nameof(CellStatusManager.CurrentStatus));
			currentStatusProperty!.SetValue(componentForTest, currentStatus, BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);

			return componentForTest;
		}
	}
}
