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

		// 2x1 mine found
		//[Fact]
		//[MemberData(nameof(TestMineLocations), MemberType = typeof(List<Location>))]
		//public void Uncover_OnCell_ReturnsMine(List<Location> locationsToPutMines)
		//{
		//	var minefieldUnderTest = new Minefield(3, 3, 0, new TestMinelayer(locationsToPutMines));

		//	var result = minefieldUnderTest.Uncover(new Location(1, 1));

		//	result.Cells.Should().NotBeEmpty()
		//		.And.ContainSingle(cell => cell.AdjacentMineCount = locationsToPutMines.Count);
		//}


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
