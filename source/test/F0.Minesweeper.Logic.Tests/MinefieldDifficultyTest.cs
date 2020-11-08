using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests
{
	public class MinefieldDifficultyTest
	{
		private readonly MinefieldDifficulty easy = MinefieldDifficulty.Easy;
		private readonly MinefieldDifficulty medium = MinefieldDifficulty.Medium;
		private readonly MinefieldDifficulty hard = MinefieldDifficulty.Hard;

		[Fact]
		public void RisingDifficultyLevel_Should_IncreaseMinefieldHeightAndWidth()
		{
			medium.Height.Should().BeGreaterThan(easy.Height);
			hard.Height.Should().BeGreaterThan(medium.Height);

			medium.Width.Should().BeGreaterThan(easy.Width);
			hard.Width.Should().BeGreaterThan(medium.Width);
		}

		[Fact]
		public void RisingDifficultyLevel_Should_IncreaseMineCountInMinefield()
		{
			medium.MineCount.Should().BeGreaterThan(easy.MineCount);
			hard.MineCount.Should().BeGreaterThan(medium.MineCount);
		}
	}
}
