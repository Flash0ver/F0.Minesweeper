using System;
using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests
{
	public class MinefieldFactoryTest
	{
		[Fact]
		public void Create_Null_Throws()
		{
			var minefieldFactoryUnderTest = new MinefieldFactory();
			Func<IMinefield> create = () => minefieldFactoryUnderTest.Create(null!);
			create.Should().ThrowExactly<ArgumentNullException>();
		}

		[Theory]
		[MemberData(nameof(TestData))]
		public void Create_WithAllPossibleEnumValues_GeneratesValidMinefields(MinefieldOptions options)
		{
			var minefieldFactoryUnderTest = new MinefieldFactory();
			IMinefield? result = null;
			Action create = () => result = minefieldFactoryUnderTest.Create(options);
			create.Should().NotThrow();
			result.Should().NotBeNull();
		}

		private static TheoryData<MinefieldOptions> TestData()
		{
			var resultData = new TheoryData<MinefieldOptions>();
			uint[] allWidthHeights = { 5, 7, 10 };
			uint[] allCountMines = { 15, 17, 20 };
			foreach (uint widthHeight in allWidthHeights)
			{
				foreach (uint countMines in allCountMines)
				{
					foreach (MinefieldFirstUncoverBehavior firstUncoverBehaviour in Enum.GetValues(typeof(MinefieldFirstUncoverBehavior)))
					{
						foreach (Abstractions.LocationShuffler locationShuffler in Enum.GetValues(typeof(Abstractions.LocationShuffler)))
						{
							resultData.Add(new MinefieldOptions(widthHeight, widthHeight, countMines, firstUncoverBehaviour, locationShuffler));
						}
					}
				}
			}
			return resultData;
		}
	}
}
