using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests.Minelayer
{
	public class SafeMinelayerTest
	{
		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 1)]
		[InlineData(1, 3)]
		[InlineData(3, 1)]
		[InlineData(3, 4)]
		[InlineData(4, 3)]
		public void PlaceMines_WithBadLuck_ResultsInClickedLocationIsNoMine(uint clickedLocationX, uint clickedLocationY)
		{
			Location clickedLocation = new(clickedLocationX, clickedLocationY);
			LocationShufflerToTest locationShuffler = new(mineLocations);
			IMinelayer minelayerUnderTest = new SafeMinelayer(locationShuffler);
			IEnumerable<Location> placedMines = minelayerUnderTest.PlaceMines(field, (uint)mineLocations.Length, clickedLocation);

			placedMines.Should().NotContain(clickedLocation);
		}

		[Theory]
		[InlineData(0, 0, -1)]
		[InlineData(1, 1, -1)]
		[InlineData(2, 2, 0)]
		[InlineData(3, 3, 0)]
		[InlineData(4, 4, 0)]
		[InlineData(2, 4, 0)]
		[InlineData(4, 3, -1)]
		[InlineData(3, 1, -1)]
		[InlineData(4, 0, 0)]
		[InlineData(2, 0, 0)]
		[InlineData(0, 3, 0)]
		public void PlaceMines_RemovesClickedLocation(uint clickedLocationX, uint clickedLocationY, int mineCountChange)
		{
			Location clickedLocation = new(clickedLocationX, clickedLocationY);
			LocationShufflerToTest locationShuffler = new(mineLocations);
			IMinelayer minelayerUnderTest = new SafeMinelayer(locationShuffler);
			IEnumerable<Location> placedMines = minelayerUnderTest.PlaceMines(field, (uint)mineLocations.Length, clickedLocation);

			placedMines.Should().HaveCount(mineLocations.Length + mineCountChange);
			placedMines.Should().BeEquivalentTo(mineLocations.Except(new[] { clickedLocation }));
		}

		private readonly Location[] field = {
			new(0, 0), new(1, 0), new(2, 0), new(3, 0), new(4, 0),
			new(0, 1), new(1, 1), new(2, 1), new(3, 1), new(4, 1),
			new(0, 2), new(1, 2), new(2, 2), new(3, 2), new(4, 2),
			new(0, 3), new(1, 3), new(2, 3), new(3, 3), new(4, 3),
			new(0, 4), new(1, 4), new(2, 4), new(3, 4), new(4, 4),
		};

		/// <summary>
		/// Locations where Mines should be located/shuffled ('X' means "Mine"):
		/// X O O O O
		/// X X X X O
		/// O O O X O
		/// O X O O X
		/// O X O X O
		/// </summary>
		private readonly Location[] mineLocations = {
			new(0, 0),
			new(0, 1), new(1, 1), new(2, 1), new(3, 1),
			new(3, 2),
			new(1, 3), new(4, 3),
			new(1, 4), new(3, 4),
		};
	}
}
