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
		[InlineData(CellStatusType.Covered, MouseButtonType.Middle)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Left)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Middle)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Middle)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Left)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Middle)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Right)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Left)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Middle)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Right)]
		public void CanNext_UndefinedTransition_ReturnsFalse(CellStatusType currentStatus, MouseButtonType command)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act
			bool result = componentUnderTest.CanMoveNext(command);

			// Assert
			result.Should().BeFalse();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, MouseButtonType.Left)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Right)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Right)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Left)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Right)]
		public void CanNext_DefinedTransition_ReturnsTrue(CellStatusType currentStatus, MouseButtonType command)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act
			bool result = componentUnderTest.CanMoveNext(command);

			// Assert
			result.Should().BeTrue();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, MouseButtonType.Middle)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Left)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Middle)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Middle)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Left)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Middle)]
		[InlineData(CellStatusType.Uncovered, MouseButtonType.Right)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Left)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Middle)]
		[InlineData(CellStatusType.Undefined, MouseButtonType.Right)]
		public void MoveNext_UndefinedTransition_Throws(CellStatusType currentStatus, MouseButtonType command)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act && Assert
			Action testAction = () => componentUnderTest.MoveNext(command);
			testAction.Should().Throw<InvalidOperationException>();
		}

		[Theory]
		[InlineData(CellStatusType.Covered, MouseButtonType.Left, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Covered, MouseButtonType.Right, CellStatusType.Flagged)]
		[InlineData(CellStatusType.Flagged, MouseButtonType.Right, CellStatusType.Unsure)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Left, CellStatusType.Uncovered)]
		[InlineData(CellStatusType.Unsure, MouseButtonType.Right, CellStatusType.Covered)]
		public void MoveNext_DefinedTransition_ReturnsNextStatus(CellStatusType currentStatus, MouseButtonType command, CellStatusType expectedNextStatus)
		{
			// Arrange
			CellStatusManager componentUnderTest = CreateCellStatusManager(currentStatus);

			// Act
			CellStatusType result = componentUnderTest.MoveNext(command);

			// Assert
			result.Should().Be(expectedNextStatus);
		}

		private static CellStatusManager CreateCellStatusManager(CellStatusType currentStatus)
		{
			var componentForTest = new CellStatusManager();

			FieldInfo? currentStatusField = typeof(CellStatusManager).GetField("currentStatus", BindingFlags.NonPublic | BindingFlags.Instance);
			currentStatusField?.SetValue(componentForTest, currentStatus);

			return componentForTest;
		}
	}
}
