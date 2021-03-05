using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Bunit;
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
			string expectedMarkup = "<div><button>C</button></div>";

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(x, y));

			// Act
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
			componentUnderTest.Instance.Location.Should().BeEquivalentTo(new Location(x, y));
		}

		[Fact]
		public void OnClick_StatusManagerCanMoveNext_CallsUncoveredAsync()
		{
			// Arrange
			Location expectedLocation = new (1, 1);

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Left, null)).Returns(true);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), expectedLocation);
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			Location? uncoveredLocation = null;
			componentUnderTest.Instance.UncoveredAsync += (location) => { uncoveredLocation = location; return Task.CompletedTask; };

			// Act
			componentUnderTest.Find("button").Click();

			// Assert
			uncoveredLocation.Should().NotBeNull().And.Be(expectedLocation);
		}

		[Fact]
		public void OnClick_StatusManagerCanNotMoveNext_UncoveredAsyncIsNotCalled()
		{
			// Arrange
			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Left, null)).Returns(false);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			Location? uncoveredLocation = null;
			componentUnderTest.Instance.UncoveredAsync += (location) => { uncoveredLocation = location; return Task.CompletedTask; };

			// Act
			componentUnderTest.Find("button").Click();

			// Assert
			uncoveredLocation.Should().BeNull();
		}

		[Fact]
		[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "Setting it for unit tests is nice and keeps tests simple and developers happy.")]
		public void OnClick_LocationIsNull_UncoveredAsyncIsNotCalled()
		{
			// Arrange
			Location expectedLocation = new (1, 1);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), expectedLocation);
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			Location? uncoveredLocation = null;
			componentUnderTest.Instance.UncoveredAsync += (location) => { uncoveredLocation = location; return Task.CompletedTask; };

			componentUnderTest.Instance.Location = null;

			// Act
			componentUnderTest.Find("button").Click();

			// Assert
			uncoveredLocation.Should().BeNull();
		}

		[Fact]
		public void OnClick_UncoveredAsyncIsNull_UncoveredAsyncIsNotCalled()
		{
			// Arrange
			Location expectedLocation = new (1, 1);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), expectedLocation);
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act && Assert (via not called canmovenext on strict mock)
			componentUnderTest.Find("button").Click();
		}

		[Fact]
		public void OnRightClick_StatusManagerReturnsFlagged_ChangesTextToFlagged()
		{
			// Arrange
			string expectedMarkup = "<div><button>⚐</button></div>";

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Right, null)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Right, null)).Returns(CellStatusType.Flagged);

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
			string expectedMarkup = "<div><button>?</button></div>";

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Right, null)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Right, null)).Returns(CellStatusType.Unsure);

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
			string expectedMarkup = "<div><button>!</button></div>";

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Right, null)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Right, null)).Returns(CellStatusType.Undefined);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act
			componentUnderTest.Find("button").ContextMenu();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Theory]
		[InlineData(true, CellStatusType.Mine, 3, '☢')]
		[InlineData(false, CellStatusType.Uncovered, 0, '0')]
		[InlineData(false, CellStatusType.Uncovered, 1, '1')]
		[InlineData(false, CellStatusType.Uncovered, 2, '2')]
		[InlineData(false, CellStatusType.Uncovered, 3, '3')]
		[InlineData(false, CellStatusType.Uncovered, 4, '4')]
		[InlineData(false, CellStatusType.Uncovered, 5, '5')]
		[InlineData(false, CellStatusType.Uncovered, 6, '6')]
		[InlineData(false, CellStatusType.Uncovered, 7, '7')]
		[InlineData(false, CellStatusType.Uncovered, 8, '8')]
		public void SetUncoveredStatus_StatusManagerCanMoveNext_ChangesTextToCorrectTranslation(bool isMine, CellStatusType newStatus, byte adjacentMineCount, char expectedStatusChar)
		{
			// Arrange
			string expectedMarkup = $"<div><button>{expectedStatusChar}</button></div>";

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Left, isMine)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Left, isMine)).Returns(newStatus);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act
			componentUnderTest.Instance.SetUncoveredStatus(isMine, adjacentMineCount);
			componentUnderTest.Render();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Theory]
		[InlineData(9)]
		[InlineData(10)]
		[InlineData(100)]
		public void SetUncoveredStatus_StatusManagerCanMoveNextWithUnsupportedAdjacentMineCount_Throws(byte adjacentMineCount)
		{
			// Arrange
			bool isMine = true;

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Left, isMine)).Returns(false);
			cellStatusManagerMock.Setup((manager) => manager.CurrentStatus).Returns(CellStatusType.Mine);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act && Assert
			Action methodUnderTest = () => componentUnderTest.Instance.SetUncoveredStatus(isMine, adjacentMineCount);
			methodUnderTest.Should().Throw<InvalidOperationException>();
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public void SetUncoveredStatus_StatusManagerCanNotMoveNext_Throws(bool isMine)
		{
			// Arrange
			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Left, isMine)).Returns(false);
			cellStatusManagerMock.Setup((manager) => manager.CurrentStatus).Returns(CellStatusType.Mine);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act && Assert
			Action methodUnderTest = ()=> componentUnderTest.Instance.SetUncoveredStatus(isMine, 2);
			methodUnderTest.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public void OnClick_HasAdjacentMinesToUncover_UncoversAllCells()
		{
			// Arrange
			string expectedMarkup = "<div><button>0</button></div>";
			Location expectedLocationClickedCell = new(1, 1);

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Left, null)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Left, false)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Left, false)).Returns(CellStatusType.Uncovered);

			ComponentParameter parameterClickedCell = ComponentParameterFactory.Parameter(nameof(Cell.Location), expectedLocationClickedCell);
			IRenderedComponent<Cell> clickedComponentUnderTest = RenderComponent<Cell>(parameterClickedCell);

			Location expectedLocationAutoUncoveredCell = new(1, 2);

			cellStatusManagerMock.Setup((manager) => manager.CanMoveNext(MouseButtonType.Left, false)).Returns(true);
			cellStatusManagerMock.Setup((manager) => manager.MoveNext(MouseButtonType.Left, false)).Returns(CellStatusType.Uncovered);

			ComponentParameter parameterAutoUncoveredCell = ComponentParameterFactory.Parameter(nameof(Cell.Location), expectedLocationAutoUncoveredCell);
			IRenderedComponent<Cell> autoUncoveredComponentUnderTest = RenderComponent<Cell>(parameterAutoUncoveredCell);

			Cell[] cells = { clickedComponentUnderTest.Instance, autoUncoveredComponentUnderTest.Instance };
			MinefieldForCellTests minefield = new(cells, null);
			minefield.SetupUncoveredAsync();

			// Act
			clickedComponentUnderTest.Find("button").Click();

			// Assert
			clickedComponentUnderTest.MarkupMatches(expectedMarkup, "Clicked cell does not match expected markup");
			autoUncoveredComponentUnderTest.MarkupMatches(expectedMarkup, "Automatically uncovered cell does not match expected markup");
		}

		private class MinefieldForCellTests
		{
			private readonly IEnumerable<Cell> cells;
			private readonly bool expectsMine;
			private readonly byte expectedAdjacentMineCount;

			public MinefieldForCellTests(IEnumerable<Cell> cells, byte? expectedAdjacentMineCount = null)
			{
				if(expectedAdjacentMineCount <= 0)
				{
					throw new ArgumentException("Test only accepts mine count values > 0.");
				}

				this.cells = cells;
				expectsMine = expectedAdjacentMineCount is not null;
				this.expectedAdjacentMineCount = expectedAdjacentMineCount ?? 0;
			}

			internal void SetupUncoveredAsync()
			{
				foreach (Cell cell in cells)
				{
					cell.UncoveredAsync += OnCellUncoveredAsync;
				}
			}

			private Task OnCellUncoveredAsync(Location location)
			{
				foreach(Cell cell in cells)
				{
					cell.SetUncoveredStatus(expectsMine, expectedAdjacentMineCount);
				}

				return Task.CompletedTask;
			}
		}
	}
}
