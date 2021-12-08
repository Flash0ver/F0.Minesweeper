using F0.Minesweeper.Components.Logic.Game;
using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Moq;
using Prism.Events;
using Xunit;

namespace F0.Minesweeper.Components.Tests.Logic.Game
{
	public class GameUpdateFactoryTests
	{
		[Theory]
		[InlineData(GameStatus.InProgress, typeof(GameInProgressUpdater))]
		[InlineData(GameStatus.IsLost, typeof(GameLostUpdater))]
		[InlineData(GameStatus.IsWon, typeof(GameWonUpdater))]
		public void On_KnownGameStatus_ReturnsCorrectUpdater(GameStatus gameStatus, Type expectedUpdater)
		{
			// Arrange
			Mock<IEventAggregator> eventAggregatorMock = new(MockBehavior.Strict);
			GameUpdateFactory instanceUnderTest = new(eventAggregatorMock.Object);

			// Act
			GameUpdater updater = instanceUnderTest.On(gameStatus);

			// Assert
			updater.Should().BeOfType(expectedUpdater);
		}

		[Fact]
		public void On_UnknownGameStatus_Throws()
		{
			// Arrange
			Mock<IEventAggregator> eventAggregatorMock = new(MockBehavior.Strict);
			GameUpdateFactory instanceUnderTest = new(eventAggregatorMock.Object);

			// Act
			Action actionUnderTest = () => instanceUnderTest.On((GameStatus)99);

			// Assert
			actionUnderTest.Should().Throw<ArgumentOutOfRangeException>();
		}
	}
}