using System;
using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests.LocationShuffler
{
	public class RandomOrderLocationShufflerTest
	{
		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		[InlineData(4)]
		[InlineData(5)]
		[InlineData(10)]
		[InlineData(13)]
		[InlineData(25)]
		public void ShuffleAndTake_WithCount_DoesReturnCorrectCount(int count)
		{
			ILocationShuffler locationShufflerUnderTest = new RandomOrderLocationShuffler();
			IReadOnlyCollection<Location> resultingLocations = locationShufflerUnderTest.ShuffleAndTake(field, count);

			resultingLocations.Should().HaveCount(count);
		}

		[Fact]
		public void ShuffleAndTake_WithCountGreaterThanField_ThrowsArgumentOutOfRangeException()
		{
			ILocationShuffler locationShufflerUnderTest = new RandomOrderLocationShuffler();
			Action shuffleAction = () => locationShufflerUnderTest.ShuffleAndTake(field, field.Length + 1);

			shuffleAction.Should().Throw<ArgumentOutOfRangeException>();
		}

		[Theory]
		[MemberData(nameof(TestData))]
		public void ShuffleAndTake_WithPredefinedRandom_ShufflesSpecificWay((int[] randomNextValues, Location[] field, Location[] result) testDataTuple)
		{
			IRandom random = new RandomToTest(testDataTuple.randomNextValues);
			ILocationShuffler locationShufflerUnderTest = new RandomOrderLocationShuffler(random);
			IReadOnlyCollection<Location> resultingLocations = locationShufflerUnderTest.ShuffleAndTake(testDataTuple.field, testDataTuple.field.Length);

			resultingLocations
				.Should()
				.Equal(testDataTuple.result);
		}

		private readonly Location[] field = {
			new(0, 0), new(1, 0), new(2, 0), new(3, 0), new(4, 0),
			new(0, 1), new(1, 1), new(2, 1), new(3, 1), new(4, 1),
			new(0, 2), new(1, 2), new(2, 2), new(3, 2), new(4, 2),
			new(0, 3), new(1, 3), new(2, 3), new(3, 3), new(4, 3),
			new(0, 4), new(1, 4), new(2, 4), new(3, 4), new(4, 4),
		};

		private static readonly Location[] randomSeedField = {
			new(0, 0),
			new(1, 0),
			new(2, 0),
			new(3, 0),
			new(4, 0),
			new(5, 0),
			new(6, 0),
			new(7, 0),
			new(8, 0),
			new(9, 0),
		};

		private static TheoryData<(int[] randomNextValues, Location[] field, Location[] result)> TestData()
		{
			TheoryData<(int[] randomNextValues, Location[] field, Location[] result)> resultData = new();
			resultData.Add((
				randomNextValues: new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
				field: randomSeedField,
				result: new Location[] {
					new(0, 0),
					new(1, 0),
					new(2, 0),
					new(3, 0),
					new(4, 0),
					new(5, 0),
					new(6, 0),
					new(7, 0),
					new(8, 0),
					new(9, 0),
				}
			));
			resultData.Add((
				randomNextValues: new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 },
				field: randomSeedField,
				result: new Location[] {
					new(9, 0),
					new(8, 0),
					new(7, 0),
					new(6, 0),
					new(5, 0),
					new(4, 0),
					new(3, 0),
					new(2, 0),
					new(1, 0),
					new(0, 0),
				}
			));
			resultData.Add((
				randomNextValues: new[] { 4, 3, 2, 1, 0, 9, 8, 7, 6, 5 },
				field: randomSeedField,
				result: new Location[] {
					new(4, 0),
					new(3, 0),
					new(2, 0),
					new(1, 0),
					new(0, 0),
					new(9, 0),
					new(8, 0),
					new(7, 0),
					new(6, 0),
					new(5, 0),
				}
			));
			resultData.Add((
				randomNextValues: new[] { 7, 6, 3, 9, 4, 2, 0, 8, 1, 5 },
				field: randomSeedField,
				result: new Location[] {
					new(6, 0),
					new(8, 0),
					new(5, 0),
					new(2, 0),
					new(4, 0),
					new(9, 0),
					new(1, 0),
					new(0, 0),
					new(7, 0),
					new(3, 0),
				}
			));

			return resultData;
		}
	}
}
