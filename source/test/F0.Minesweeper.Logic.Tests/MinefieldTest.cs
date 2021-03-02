using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;
using FluentAssertions;
using Moq;
using Xunit;

namespace F0.Minesweeper.Logic.Tests
{
	public class MinefieldTest
	{
		[Fact]
		public void MinefieldUncover_Should_ReturnAtLeastTheClickedLocation()
		{
			Minefield minefieldUnderTest = new(5, 7, 11, Abstractions.MinefieldFirstUncoverBehavior.MayYieldMine);
			Location locationTestValue = new(3, 4);

			IGameUpdateReport result = minefieldUnderTest.Uncover(locationTestValue);

			result.Cells.Should().NotBeEmpty()
				.And.ContainSingle(cell => cell.Location == locationTestValue);
		}

		// 2x1 mine uncovered + Game Over
		[Fact]
		public void Uncover_OnCellWithMine_ReturnsMineAndGameLost()
		{
			Location mineLocation = new(0, 0);
			Minefield minefieldUnderTest = new(2, 1, 1, new MinelayerToTest(mineLocation));

			IGameUpdateReport result = minefieldUnderTest.Uncover(0, 0);

			result.Cells.Should().NotBeEmpty()
				.And.ContainSingle(cell => cell.IsMine);
			result.Status.Should().Be(GameStatus.IsLost);
		}

		// 2x1 empty uncovered + Win
		[Fact]
		public void Uncover_OnLastCellWithoutMine_ReturnsNoMineAndGameWon()
		{
			Location mineLocation = new(1, 0);
			Minefield minefieldUnderTest = new(2, 1, 1, new MinelayerToTest(mineLocation));

			IGameUpdateReport result = minefieldUnderTest.Uncover(0, 0);

			result.Cells.Should().NotBeEmpty()
				.And.ContainSingle(cell => !cell.IsMine);
			result.Status.Should().Be(GameStatus.IsWon);
		}

		// 3x1 Game still in progress
		[Fact]
		public void Uncover_OnCellWithoutMine_GameStillInProgress()
		{
			Location mineLocation = new(1, 0);
			Minefield minefieldUnderTest = new(3, 1, 1, new MinelayerToTest(mineLocation));

			IGameUpdateReport result = minefieldUnderTest.Uncover(0, 0);

			result.Cells.Should().NotBeEmpty()
				.And.Contain(cell => !cell.IsMine);
			result.Status.Should().Be(GameStatus.InProgress);
		}

		//3x3 all numbers
		[Theory]
		[MemberData(nameof(MinefieldTestData.TestData), MemberType = typeof(MinefieldTestData))]
		public void Uncover_OnNoMine_ReturnsRightAmountOfAdjacentMines(MinefieldTestData testdata)
		{
			Minefield minefieldUnderTest = new(3, 3, 0, new MinelayerToTest(testdata.MineLocations));

			IGameUpdateReport result = minefieldUnderTest.Uncover(1, 1);

			result.Cells.Should().NotBeEmpty()
				.And.ContainSingle(cell => cell.AdjacentMineCount == testdata.MineLocations.Length);
		}

		//5x5 Empty cell propagation + win
		// [C][ ][ ][ ][ ]
		// [ ][ ][ ][ ][ ]
		// [M][M][M][ ][ ]
		// [ ][ ][ ][ ][ ]
		// [ ][ ][ ][ ][ ]
		[Fact]
		public void Uncover_OnEmptyArea_PropagatedThroughWholeMinefield()
		{
			Location[] mineLocations = { new(0, 2), new(1, 2), new(2, 2) };
			Minefield minefieldUnderTest = new(5, 5, 3, new MinelayerToTest(mineLocations));

			IGameUpdateReport result = minefieldUnderTest.Uncover(0, 0);

			result.Cells.Should().NotBeEmpty()
				.And.HaveCount(22)
				.And.Contain(cell => !cell.IsMine);
			result.Status.Should().Be(GameStatus.IsWon);
		}

		//5x5 Empty cell propagation + InProgress + last click win
		// [ ][ ][ ][ ][ ]
		// [ ][ ][C][ ][ ]
		// [ ][ ][ ][ ][ ]
		// [M][M][ ][ ][ ]
		// [C][M][ ][ ][ ]
		[Fact]
		public void Uncover_OnEmptyArea_PropagatedThroughWholeMinefieldAndThenWin()
		{
			Location[] mineLocations = { new(0, 3), new(1, 3), new(1, 4) };
			Minefield minefieldUnderTest = new(5, 5, 3, new MinelayerToTest(mineLocations));

			IGameUpdateReport result = minefieldUnderTest.Uncover(2, 1);

			result.Cells.Should().NotBeEmpty()
				.And.HaveCount(21)
				.And.Contain(cell => !cell.IsMine);
			result.Status.Should().Be(GameStatus.InProgress);

			result = minefieldUnderTest.Uncover(0, 4);
			result.Cells.Should().NotBeEmpty()
				.And.HaveCount(1)
				.And.Contain(cell => !cell.IsMine);
			result.Status.Should().Be(GameStatus.IsWon);
		}

		[Fact]
		public void Uncover_ForTheFirstTime_PlacesMinesOnlyOnce()
		{
			Mock<IMinelayer> minelayerMock = new Mock<IMinelayer>(MockBehavior.Strict);
			minelayerMock
				.Setup(ml => ml.PlaceMines(It.IsAny<IEnumerable<Location>>(), It.IsAny<uint>(), It.IsAny<Location>()))
				.Returns(new List<Location> { new(0, 0), new(1, 0) });

			Minefield minefieldUnderTest = new(3, 3, 2, minelayerMock.Object);

			minefieldUnderTest.Uncover(new Location(2, 2));

			minelayerMock
				.Verify(ml => ml.PlaceMines(It.IsAny<IEnumerable<Location>>(), It.IsAny<uint>(), It.IsAny<Location>()),
				 Times.Once);
		}

		[Fact(Skip = "WIP - Can't figure out how to mock/test the behaviour.")]
		public void Uncover_ForTheFirstTime_ShufflesMinesOnlyOnce()
		{
			Mock<IEnumerable<Location>> shuffledMineLocations = new(MockBehavior.Strict);
			shuffledMineLocations
				.As<IEnumerable<Location>>()
				.Setup(sml => sml.GetEnumerator()).Returns(ShuffledMines().GetEnumerator());

			Mock<IMinelayer> minelayerMock = new(MockBehavior.Strict);
			minelayerMock
				.Setup(ml => ml.PlaceMines(It.IsAny<IEnumerable<Location>>(), It.IsAny<uint>(), It.IsAny<Location>()))
				.Returns(shuffledMineLocations.Object);

			Minefield minefieldUnderTest = new(3, 3, 2, new RandomMinelayer());

			minefieldUnderTest.Uncover(new Location(2, 2));

			shuffledMineLocations
				.Verify(sml => sml.GetEnumerator(),
				 Times.Once);
		}

		private static IEnumerable<Location> ShuffledMines()
		{
			yield return new Location(0, 0);
			yield return new Location(1, 0);
		}
	}

	public class MinefieldTestData
	{
		public Location[] MineLocations { get; init; }

		public MinefieldTestData(Location[] mineLocations) => MineLocations = mineLocations;

		public static TheoryData<MinefieldTestData> TestData => new()
		{
			new MinefieldTestData(new Location[] {
				new(0, 0)
			}),
			new MinefieldTestData(new Location[] {
				new(0, 0), new(1, 0)
			}),
			new MinefieldTestData(new Location[] {
				new(0, 0), new(1, 0), new(2, 0)
			}),
			new MinefieldTestData(new Location[] {
				new(0, 0), new(1, 0), new(2, 0),
				new(0, 1)
			}),
			new MinefieldTestData(new Location[] {
				new(0, 0), new(1, 0), new(2, 0),
				new(0, 1), new(2, 1)
			}),
			new MinefieldTestData(new Location[] {
				new(0, 0), new(1, 0), new(2, 0),
				new(0, 1), new(2, 1),
				new(0, 2)
			}),
			new MinefieldTestData(new Location[] {
				new(0, 0), new(1, 0), new(2, 0),
				new(0, 1), new(2, 1),
				new(0, 2), new(1, 2)
			}),
			new MinefieldTestData(new Location[] {
				new(0, 0), new(1, 0), new(2, 0),
				new(0, 1), new(2, 1),
				new(0, 2), new(1, 2), new(2, 2)
			})
		};
	}
}
