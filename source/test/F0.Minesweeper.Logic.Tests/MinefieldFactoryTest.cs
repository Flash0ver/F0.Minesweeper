using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Tests
{
	public class MinefieldFactoryTest
	{
		[Fact]
		public void Create_Null_Throws()
		{
			MinefieldFactory minefieldFactoryUnderTest = new();
			Func<IMinefield> create = () => minefieldFactoryUnderTest.Create(null!);
			create.Should().ThrowExactly<ArgumentNullException>();
		}

		[Theory]
		[MemberData(nameof(TestData))]
		public void Create_WithAllPossibleEnumValues_GeneratesValidMinefields(MinefieldOptions options)
		{
			MinefieldFactory minefieldFactoryUnderTest = new();
			IMinefield? result = null;
			Action create = () => result = minefieldFactoryUnderTest.Create(options);
			create.Should().NotThrow();
			result.Should().NotBeNull();
		}

		private static TheoryData<MinefieldOptions> TestData()
		{
			TheoryData<MinefieldOptions> resultData = new();
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
