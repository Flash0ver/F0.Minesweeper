using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Tests
{
	public class LocationTest
	{
		[Fact]
		public void NewLocation_XParameterEqualsX()
		{
			uint testValue = 5;
			Location locationUnderTest = new(testValue, 7);

			locationUnderTest.X.Should().Be(testValue);
		}

		[Fact]
		public void NewLocation_YParameterEqualsY()
		{
			uint testValue = 7;
			Location locationUnderTest = new(5, testValue);

			locationUnderTest.Y.Should().Be(testValue);
		}

		[Fact]
		public void NewLocation_MaxIntParameterOnX()
		{
			uint testValue = Int32.MaxValue;
			Location locationUnderTest = new(testValue, 7);

			locationUnderTest.X.Should().Be(testValue);
		}

		[Fact]
		public void NewLocation_MaxIntParameterOnY()
		{
			uint testValue = Int32.MaxValue;
			Location locationUnderTest = new(5, testValue);

			locationUnderTest.Y.Should().Be(testValue);
		}

		[Fact]
		public void NewLocationEmptyConstructor_CreatesZeroLocation()
		{
			Location locationUnderTest = new();

			locationUnderTest.X.Should().Be(0);
			locationUnderTest.Y.Should().Be(0);
		}
	}
}
