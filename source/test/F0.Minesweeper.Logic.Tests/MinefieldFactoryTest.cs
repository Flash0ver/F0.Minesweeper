using System;
using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests
{
	public class MinefieldFactoryTest
	{
		[Theory]
		[MemberData(nameof(MinefieldFactoryTestData.TestData), MemberType = typeof(MinefieldFactoryTestData))]
		public void Create_WithAllPossibleEnumValues_GeneratesValidMinefields(MinefieldOptions options)
		{
			MinefieldFactory minefieldFactoryUnderTest = new MinefieldFactory();
			IMinefield? result = null;
			Action create = () => result = minefieldFactoryUnderTest.Create(options);
			create.Should().NotThrow();
			result.Should().NotBeNull();
		}

		public class MinefieldFactoryTestData
		{
			public static TheoryData<MinefieldOptions> TestData => GenerateTestData();
		}

		private static TheoryData<MinefieldOptions> GenerateTestData()
		{
			TheoryData<MinefieldOptions> resultData = new TheoryData<MinefieldOptions>();
			uint[] allWidthHeights = { 5, 7, 10 };
			uint[] allCountMines = { 15, 17, 20 };
			foreach (var widthHeight in allWidthHeights)
			{
				foreach (var countMines in allCountMines)
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
