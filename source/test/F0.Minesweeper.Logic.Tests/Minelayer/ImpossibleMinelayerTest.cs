using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests.Minelayer
{
	public class ImpossibleMinelayerTest
	{
		[Theory]
		[InlineData(0, 0)]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(3, 3)]
		[InlineData(4, 4)]
		[InlineData(2, 4)]
		[InlineData(4, 3)]
		[InlineData(3, 1)]
		[InlineData(4, 0)]
		[InlineData(2, 0)]
		[InlineData(0, 3)]
		public void PlaceMines_AlwaysResultsInOneMineAtClickedLocation(uint clickedLocationX, uint clickedLocationY)
		{
			Location clickedLocation = new(clickedLocationX, clickedLocationY);
			IMinelayer minelayerUnderTest = new ImpossibleMinelayer();
			IEnumerable<Location> placedMines = minelayerUnderTest.PlaceMines(field, 0, clickedLocation);

			placedMines.Should().HaveCount(1);
			placedMines.Should().BeEquivalentTo(clickedLocation);
		}

		private readonly Location[] field = {
			new(0, 0), new(1, 0), new(2, 0), new(3, 0), new(4, 0),
			new(0, 1), new(1, 1), new(2, 1), new(3, 1), new(4, 1),
			new(0, 2), new(1, 2), new(2, 2), new(3, 2), new(4, 2),
			new(0, 3), new(1, 3), new(2, 3), new(3, 3), new(4, 3),
			new(0, 4), new(1, 4), new(2, 4), new(3, 4), new(4, 4),
		};
	}
}
