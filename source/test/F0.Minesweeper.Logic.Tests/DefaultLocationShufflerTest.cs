using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests
{
	public class DefaultLocationShufflerTest
	{
		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(4)]
		[InlineData(5)]
		[InlineData(10)]
		[InlineData(13)]
		public void ShuffleAndTake_WithCount_DoesReturnCorrectCount(int count)
		{
			DefaultLocationShuffler locationShufflerUnderTest = new();
			IReadOnlyCollection<Location> resultingLocations = ((ILocationShuffler)locationShufflerUnderTest).ShuffleAndTake(field, count);

			resultingLocations.Should().HaveCount(count);
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
