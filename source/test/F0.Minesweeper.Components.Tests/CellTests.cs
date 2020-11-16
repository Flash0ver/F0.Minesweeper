using System;
using Bunit;
using Bunit.Rendering;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace F0.Minesweeper.Components.Tests
{
	public class CellTests : TestContext
	{
		private readonly Mock<ICellStatusManager> cellStatusManagerMock;

		public CellTests()
		{
			// todo: Make strict
			cellStatusManagerMock = new Mock<ICellStatusManager>(MockBehavior.Strict);
			Services.AddSingleton(cellStatusManagerMock.Object);
		}

		public override void Dispose()
		{
			Mock.VerifyAll(cellStatusManagerMock);
			base.Dispose();
		}

		[Fact]
		public void Rendering_NoLocationProvided_Throws()
		{
			Action renderAction = () => RenderComponent<Cell>();
			renderAction.Should().Throw<ArgumentNullException>();
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(100, 100)]
		public void Rendering_LocationProvided_ShowsCell(uint x, uint y)
		{
			// Arrange
			string expectedMarkup = "<button>Covered</button>";

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(x, y));

			// Act
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
			componentUnderTest.Instance.Location.Should().BeEquivalentTo(new Location(x, y));
		}

		[Fact]
		public void OnClick_StatusManagerReturnsUncovered_ChangesTextToUncovered()
		{
			// Arrange
			string expectedMarkup = "<button>Uncovered</button>";

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Left)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Left)).Returns(CellStatusType.Uncovered);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act
			componentUnderTest.Find("button").Click();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Fact]
		public void OnClick_StatusManagerReturnsUndefined_ChangesTextToSpecialUndefinedValue()
		{
			// Arrange
			string expectedMarkup = "<button>!!Undefined!!</button>";

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Left)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Left)).Returns(CellStatusType.Undefined);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act
			componentUnderTest.Find("button").Click();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Fact]
		public void OnRightClick_StatusManagerReturnsFlagged_ChangesTextToFlagged()
		{
			// Arrange
			string expectedMarkup = "<button>Flagged</button>";

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Right)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Right)).Returns(CellStatusType.Flagged);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act
			componentUnderTest.Find("button").ContextMenu();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Fact]
		public void OnRightClick_StatusManagerReturnsUnsure_ChangesTextToQuestionMark()
		{
			// Arrange
			string expectedMarkup = "<button>?</button>";

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Right)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Right)).Returns(CellStatusType.Unsure);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act
			componentUnderTest.Find("button").ContextMenu();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Fact]
		public void OnRightClick_StatusManagerReturnsUndefined_ChangesTextToSpecialUndefinedValue()
		{
			// Arrange
			string expectedMarkup = "<button>!!Undefined!!</button>";

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Right)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Right)).Returns(CellStatusType.Undefined);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act
			componentUnderTest.Find("button").ContextMenu();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}
	}
}
