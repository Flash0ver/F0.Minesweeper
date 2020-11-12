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
			var minefieldUnderTest = new Minefield(5, 7, 11, Abstractions.MinefieldGenerationOptions.Random);
			var locationTestValue = new Location(3, 4);

			var result = minefieldUnderTest.Uncover(locationTestValue);

			result.Cells.Should().NotBeEmpty()
				.And.ContainSingle(cell => cell.Location == locationTestValue);
		}
	}
}
