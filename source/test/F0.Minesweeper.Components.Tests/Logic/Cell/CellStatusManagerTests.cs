using System;
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
		[InlineData(CellStatusType.Covered, MouseButtonType.Middle, null)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Middle, true)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Middle, false)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Right, true)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Right, false)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Left, null)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Left, true)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Left, false)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Middle, null)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Middle, true)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Middle, false)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Right, true)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Right, false)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Middle, null)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Middle, true)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Middle, false)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Right, true)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Right, false)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Left, null)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Left, true)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Left, false)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Middle, null)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Middle, true)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Middle, false)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Right, null)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Right, true)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Right, false)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Left, null)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Left, true)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Left, false)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Middle, null)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Middle, true)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Middle, false)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Right, null)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Right, true)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Right, false)]
		public void CanNext_UndefinedTransition_ReturnsFalse(CellStatusType currentStatus, MouseButtonType command, bool? isMine)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act
			bool result = componentUnderTest.CanMoveNext(command, isMine);

			// Assert
			result.Should().BeFalse();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, MouseButtonType.Left, null)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Left, true)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Left, false)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Right, null)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Right, null)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Left, null)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Left, true)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Left, false)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Right, null)]
		public void CanNext_DefinedTransition_ReturnsTrue(CellStatusType currentStatus, MouseButtonType command, bool? isMine)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act
			bool result = componentUnderTest.CanMoveNext(command, isMine);

			// Assert
			result.Should().BeTrue();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, MouseButtonType.Middle, null)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Middle, true)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Middle, false)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Right, true)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Right, false)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Left, null)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Left, true)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Left, false)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Middle, null)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Middle, true)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Middle, false)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Right, true)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Right, false)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Middle, null)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Middle, true)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Middle, false)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Right, true)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Right, false)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Left, null)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Left, true)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Left, false)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Middle, null)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Middle, true)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Middle, false)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Right, null)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Right, true)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Right, false)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Left, null)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Left, true)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Left, false)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Middle, null)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Middle, true)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Middle, false)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Right, null)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Right, true)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Right, false)]
		public void MoveNext_CanNotMoveNext_Throws(CellStatusType currentStatus, MouseButtonType command, bool? isMine)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act && Assert
			Action testAction = () => componentUnderTest.MoveNext(command, isMine);
			testAction.Should().Throw<InvalidOperationException>();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, MouseButtonType.Left, null)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Left, null)]
		public void MoveNext_MoveToUndefinedStatus_Throws(CellStatusType currentStatus, MouseButtonType command, bool? isMine)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act && Assert
			Action testAction = () => componentUnderTest.MoveNext(command, isMine);
			testAction.Should().Throw<InvalidOperationException>();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, MouseButtonType.Left, false, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Left, true, CellStatusType.Mine)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Right, null, CellStatusType.Flagged)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Right, null, CellStatusType.Unsure)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Left, false, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Left, true, CellStatusType.Mine)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Right, null, CellStatusType.Covered)]
		public void MoveNext_DefinedTransition_ReturnsNextStatus(CellStatusType currentStatus, MouseButtonType command, bool? isMine, CellStatusType expectedNextStatus)
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
			var componentForTest = new CellStatusManager();

			PropertyInfo? currentStatusProperty = typeof(CellStatusManager).GetProperty(nameof(CellStatusManager.CurrentStatus));
			currentStatusProperty?.SetValue(componentForTest, currentStatus, BindingFlags.NonPublic | BindingFlags.Instance, null, null, null);

			return componentForTest;
		}
	}
}
