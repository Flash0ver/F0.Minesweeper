using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests.Minelayer
{
	public class SafeMinelayerTest
	{
		[Fact]
		public void PlaceMines_WithBadLuck_ResultsInClickedLocationIsNoMine()
		{
			LocationShufflerToTest locationShuffler = new((0, 0), (1, 1), (2, 2));
			SafeMinelayer minelayerUnderTest = new(locationShuffler);
			var placedMines = ((IMinelayer)minelayerUnderTest).PlaceMines(AllLocations, 2, new Location(1, 1));

			placedMines.Should().HaveCount(2);
			placedMines.Should().BeEquivalentTo(new Location[] { new(0, 0), new(2, 2) });
		}

		public Location[] AllLocations = {
				new(0, 0), new(1, 0), new(2, 0),
				new(0, 1), new(1, 1), new(2, 1),
				new(0, 2), new(1, 2), new(2, 2)
			};
	}
}
