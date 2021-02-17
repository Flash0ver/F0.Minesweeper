	using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests
{
	public class MinefieldOptionsTest
	{
		private readonly MinefieldOptions easy = MinefieldOptions.Easy;
		private readonly MinefieldOptions medium = MinefieldOptions.Medium;
		private readonly MinefieldOptions hard = MinefieldOptions.Hard;

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

		[Fact]
		public void EasyDifficultyLevel_Should_HaveTheCorrectMinefieldFirstUncoverBehavior()
		{
			MinefieldOptions.Easy.GenerationOption.Should().Be(MinefieldFirstUncoverBehavior.WithoutAdjacentMines);
		}
		[Fact]
		public void MediumDifficultyLevel_Should_HaveTheCorrectMinefieldFirstUncoverBehavior()
		{
			MinefieldOptions.Medium.GenerationOption.Should().Be(MinefieldFirstUncoverBehavior.CannotYieldMine);
		}
		[Fact]
		public void HardDifficultyLevel_Should_HaveTheCorrectMinefieldFirstUncoverBehavior()
		{
			MinefieldOptions.Hard.GenerationOption.Should().Be(MinefieldFirstUncoverBehavior.MayYieldMine);
		}
	}
}
