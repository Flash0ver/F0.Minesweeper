using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests
{
	public class MinefieldTest
	{
		[Fact]
		public void MinefieldUncover_Should_ReturnAtLeastTheClickedLocation()
		{
			var minefieldUnderTest = new Minefield(5, 7, 11, Abstractions.MinefieldFirstUncoverBehavior.MayYieldMine);
			var locationTestValue = new Location(3, 4);

			var result = minefieldUnderTest.Uncover(locationTestValue);

			result.Cells.Should().NotBeEmpty()
				.And.ContainSingle(cell => cell.Location == locationTestValue);
		}

		// 2x1 mine uncovered + Game Over
		[Fact]
		public void Uncover_OnCellWithMine_ReturnsMineAndGameLost()
		{
			var mineLocation = new Location(0, 0);
			var minefieldUnderTest = new Minefield(2, 1, 1, new TestMinelayer(new List<Location> { mineLocation } ));

			var result = minefieldUnderTest.Uncover(new Location(0,0));

			result.Cells.Should().NotBeEmpty()
				.And.ContainSingle(cell => cell.IsMine);
			result.Status.Should().Be(GameStatus.IsLost);
		}

		// 2x1 empty uncovered + Win
		[Fact]
		public void Uncover_OnLastCellWithoutMine_ReturnsNoMineAndGameWon()
		{
			var mineLocation = new Location(1, 0);
			var minefieldUnderTest = new Minefield(2, 1, 1, new TestMinelayer(new List<Location> { mineLocation }));

			var result = minefieldUnderTest.Uncover(new Location(0, 0));

			result.Cells.Should().NotBeEmpty()
				.And.ContainSingle(cell => !cell.IsMine);
			result.Status.Should().Be(GameStatus.IsWon);
		}

		// 3x1 Game still in progress
		[Fact]
		public void Uncover_OnCellWithoutMine_GameStillInProgress()
		{
			var mineLocation = new Location(1, 0);
			var minefieldUnderTest = new Minefield(3, 1, 1, new TestMinelayer(new List<Location> { mineLocation }));

			var result = minefieldUnderTest.Uncover(new Location(0, 0));

			result.Cells.Should().NotBeEmpty()
				.And.Contain(cell => !cell.IsMine);
			result.Status.Should().Be(GameStatus.InProgress);
		}

		//3x3 all numbers
		[Theory]
		[MemberData(nameof(MinefieldTestData.TestData), MemberType = typeof(MinefieldTestData))]
		public void Uncover_OnNoMine_ReturnsRightAmountOfAdjacentMines(MinefieldTestData testdata)
		{
			var minefieldUnderTest = new Minefield(3, 3, 0, new TestMinelayer(testdata.MineLocations));

			var result = minefieldUnderTest.Uncover(new Location(1, 1));

			result.Cells.Should().NotBeEmpty()
				.And.ContainSingle(cell => cell.AdjacentMineCount == testdata.MineLocations.Count);
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
			var mineLocations = new List<Location> { new Location(0, 2), new Location(1, 2), new Location(2, 2) };
			var minefieldUnderTest = new Minefield(5, 5, 3, new TestMinelayer(mineLocations));

			var result = minefieldUnderTest.Uncover(new Location(0, 0));

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
			var mineLocations = new List<Location> { new Location(0, 3), new Location(1, 3), new Location(1, 4) };
			var minefieldUnderTest = new Minefield(5, 5, 3, new TestMinelayer(mineLocations));

			var result = minefieldUnderTest.Uncover(new Location(2, 1));

			result.Cells.Should().NotBeEmpty()
				.And.HaveCount(21)
				.And.Contain(cell => !cell.IsMine);
			result.Status.Should().Be(GameStatus.InProgress);

			result = minefieldUnderTest.Uncover(new Location(0, 4));
			result.Cells.Should().NotBeEmpty()
				.And.HaveCount(1)
				.And.Contain(cell => !cell.IsMine);
			result.Status.Should().Be(GameStatus.IsWon);
		}
	}

	public class MinefieldTestData
	{
		public List<Location> MineLocations { get; set; }

		public MinefieldTestData()
		{
			MineLocations = new List<Location>();
		}

		public static TheoryData<MinefieldTestData> TestData =>
		new TheoryData<MinefieldTestData>
		{
			new MinefieldTestData{
				MineLocations = new List<Location>{
					new Location(0,0)
				}
			},
			new MinefieldTestData{
				MineLocations = new List<Location>{
					new Location(0,0),
					new Location(1,0)
				}
			},
			new MinefieldTestData{
				MineLocations = new List<Location>{
					new Location(0,0),
					new Location(1,0),
					new Location(2,0)
				}
			},
			new MinefieldTestData{
				MineLocations = new List<Location>{
					new Location(0,0),
					new Location(1,0),
					new Location(2,0),
					new Location(0,1)
				}
			},
			new MinefieldTestData{
				MineLocations = new List<Location>{
					new Location(0,0),
					new Location(1,0),
					new Location(2,0),
					new Location(0,1),
					new Location(2,1)
				}
			},
			new MinefieldTestData{
				MineLocations = new List<Location>{
					new Location(0,0),
					new Location(1,0),
					new Location(2,0),
					new Location(0,1),
					new Location(2,1),
					new Location(0,2)
				}
			},
			new MinefieldTestData{
				MineLocations = new List<Location>{
					new Location(0,0),
					new Location(1,0),
					new Location(2,0),
					new Location(0,1),
					new Location(2,1),
					new Location(0,2),
					new Location(1,2)
				}
			},
			new MinefieldTestData{
				MineLocations = new List<Location>{
					new Location(0,0),
					new Location(1,0),
					new Location(2,0),
					new Location(0,1),
					new Location(2,1),
					new Location(0,2),
					new Location(1,2),
					new Location(2,2)
				}
			}
		};
	}
}
