using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests
{
	public class LocationTest
	{
		[Fact]
		public void NewLocation_XParameterEqualsX()
		{
			uint testValue = 5;
			var locationUnderTest = new Location(testValue, 7);

			locationUnderTest.X.Should().Be(testValue);
		}

		[Fact]
		public void NewLocation_YParameterEqualsY()
		{
			uint testValue = 7;
			var locationUnderTest = new Location(5, testValue);

			locationUnderTest.Y.Should().Be(testValue);
		}

		[Fact]
		public void NewLocation_MaxIntParameterOnX()
		{
			uint testValue = int.MaxValue;
			var locationUnderTest = new Location(testValue, 7);

			locationUnderTest.X.Should().Be(testValue);
		}

		[Fact]
		public void NewLocation_MaxIntParameterOnY()
		{
			uint testValue = int.MaxValue;
			var locationUnderTest = new Location(5, testValue);

			locationUnderTest.Y.Should().Be(testValue);
		}

		[Fact]
		public void NewLocationEmptyConstructor_CreatesZeroLocation()
		{
			var locationUnderTest = new Location();

			locationUnderTest.X.Should().Be(0);
			locationUnderTest.Y.Should().Be(0);
		}
	}
}
