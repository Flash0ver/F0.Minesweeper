using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests.Minelayer
{
	public class RandomMinelayerTest
	{
		[Fact]
		public void PlaceMines_WithBadLuck_ResultsInClickedLocationIsMine()
		{
			LocationShufflerToTest locationShuffler = new((0, 0), (1, 1), (2, 2));
			RandomMinelayer minelayerUnderTest = new(locationShuffler);
			var placedMines = ((IMinelayer)minelayerUnderTest).PlaceMines(AllLocations, 3, new Location(1, 1));

			placedMines.Should().HaveCount(3);
			placedMines.Should().BeEquivalentTo(new Location[] { new(0, 0), new(1, 1), new(2, 2) });
		}

		public Location[] AllLocations = {
				new(0, 0), new(1, 0), new(2, 0),
				new(0, 1), new(1, 1), new(2, 1),
				new(0, 2), new(1, 2), new(2, 2)
			};
	}
}
