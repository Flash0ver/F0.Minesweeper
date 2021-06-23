using System.Collections.Generic;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Logic.Game;
using F0.Minesweeper.Logic.Abstractions;
using Moq;
using Xunit;

namespace F0.Minesweeper.Components.Tests.Logic.Game
{
    public class GameInProgressUpdaterTests 
    {
		[Fact]
		public async Task OnUpdateAsync_UncoverableCellAndClickedLocationMatch_UncoversCellWithLeftClick()
		{
			// Arrange
			GameInProgressUpdaterForTests instanceUnderTest = new();
			Mock<ICell> uiCell = new(MockBehavior.Strict);
			const uint expectedX = 1;
			const uint expectedY = 3;
			const bool expectedIsMine = true;
			const byte expectedAdjacentMineCount = 0;

			UncoverableCell[] uncoverableCells = new []
			{
				new UncoverableCell(uiCell.Object, expectedIsMine, expectedAdjacentMineCount)
            };

			uiCell
				.Setup(cell => cell.Location)
				.Returns(new Location(expectedX, expectedY));

			uiCell
				.Setup(cell => cell.SetUncoveredStatus(CellInteractionType.LeftClick, expectedIsMine, expectedAdjacentMineCount));
			
			// Act && Assert
			await instanceUnderTest.OnUpdateAsyncExposed(uncoverableCells, new Minesweeper.Logic.Abstractions.Location(expectedX, expectedY));
		}

		[Fact]
		public async Task OnUpdateAsync_UncoverableCellAndClickedLocationDoNotMatch_UncoversCellWithAutomatic()
		{
			// Arrange
			GameInProgressUpdaterForTests instanceUnderTest = new();
			Mock<ICell> uiCell = new(MockBehavior.Strict);
			const bool expectedIsMine = false;
			const byte expectedAdjacentMineCount = 1;

			UncoverableCell[] uncoverableCells = new []
			{
				new UncoverableCell(uiCell.Object, expectedIsMine, expectedAdjacentMineCount)
            };

			uiCell
				.Setup(cell => cell.Location)
				.Returns(new Location(1, 1));

			uiCell
				.Setup(cell => cell.SetUncoveredStatus(CellInteractionType.Automatic, expectedIsMine, expectedAdjacentMineCount));
			
			// Act && Assert
			await instanceUnderTest.OnUpdateAsyncExposed(uncoverableCells, new Minesweeper.Logic.Abstractions.Location(2, 2));
		}

		[Fact]
		public async Task OnUpdateAsync_MultipleCells_UncoversCells()
		{
			// Arrange
			GameInProgressUpdaterForTests instanceUnderTest = new();
			Mock<ICell> uiCellClicked = new(MockBehavior.Strict);
			Mock<ICell> uiCellAutomatic = new(MockBehavior.Strict);

			UncoverableCell[] uncoverableCells = new []
			{
				new UncoverableCell(uiCellClicked.Object, false, 0),
				new UncoverableCell(uiCellAutomatic.Object, false, 1)
            };

			uiCellClicked
				.Setup(cell => cell.Location)
				.Returns(new Location(1, 1));

			uiCellClicked
				.Setup(cell => cell.SetUncoveredStatus(CellInteractionType.LeftClick, false, 0));

			uiCellAutomatic
				.Setup(cell => cell.Location)
				.Returns(new Location(2, 1));

			uiCellAutomatic
				.Setup(cell => cell.SetUncoveredStatus(CellInteractionType.Automatic, false, 1));
			
			// Act && Assert
			await instanceUnderTest.OnUpdateAsyncExposed(uncoverableCells, new Minesweeper.Logic.Abstractions.Location(1, 1));
		}


		private class GameInProgressUpdaterForTests : GameInProgressUpdater
		{
			public GameInProgressUpdaterForTests()
			{
			}

            internal Task OnUpdateAsyncExposed(IEnumerable<UncoverableCell> uncoverableCells, Minesweeper.Logic.Abstractions.Location clickedLocation) 
                => this.OnUpdateAsync(uncoverableCells, clickedLocation);
		}
	}
}