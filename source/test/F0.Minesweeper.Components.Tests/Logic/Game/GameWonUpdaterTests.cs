using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Events;
using F0.Minesweeper.Components.Logic.Game;
using F0.Minesweeper.Logic.Abstractions;
using Prism.Events;

namespace F0.Minesweeper.Components.Tests.Logic.Game
{
	public class GameWonUpdaterTests
	{
		[Fact]
		public async Task OnUpdateAsync_UncoversCells()
		{
			// Arrange
			Mock<IEventAggregator> eventAggregator = new(MockBehavior.Strict);
			Mock<ICell> uiCellClicked = new(MockBehavior.Strict);
			Mock<ICell> uiCellAutomatic = new(MockBehavior.Strict);

			UncoverableCell[] uncoverableCells = new[]
			{
				new UncoverableCell(uiCellClicked.Object, false, 0),
				new UncoverableCell(uiCellAutomatic.Object, false, 1)
			};

			uiCellClicked
				.Setup(cell => cell.Location)
				.Returns(new Location(1, 1));

			uiCellClicked
				.Setup(cell => cell.SetUncoveredStatus(CellInteractionType.GameWon, false, 0));

			uiCellClicked.Setup(cell => cell.DisableClick());

			uiCellAutomatic
				.Setup(cell => cell.Location)
				.Returns(new Location(2, 1));

			uiCellAutomatic
				.Setup(cell => cell.SetUncoveredStatus(CellInteractionType.GameWon, false, 1));

			uiCellAutomatic.Setup(cell => cell.DisableClick());

			eventAggregator
				.Setup(agg => agg.GetEvent<GameFinishedEvent>().Publish(It.IsAny<string>()));

			GameWonUpdaterForTests instanceUnderTest = new(eventAggregator.Object);

			// Act && Assert
			await instanceUnderTest.OnUpdateAsyncExposed(uncoverableCells, new Minesweeper.Logic.Abstractions.Location(1, 1));
		}

		private class GameWonUpdaterForTests : GameWonUpdater
		{
			public GameWonUpdaterForTests(IEventAggregator eventAggregator) : base(eventAggregator)
			{
			}

			internal Task OnUpdateAsyncExposed(IEnumerable<UncoverableCell> uncoverableCells, Minesweeper.Logic.Abstractions.Location clickedLocation)
				=> OnUpdateAsync(uncoverableCells, clickedLocation);
		}
	}
}
