using Bunit;
using F0.Minesweeper.Components.Abstractions;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Components.Tests
{
	public class MinefieldTests : TestContext
	{
		[Fact]
		public void Rendering_NoSizeProvided_ShowsErrorLabel()
		{
			// Arrange
			const string expectedLabelText = "Minesweeper is played on a Minefield and not within a black hole! Provide a valid size!";
			string expectedMarkup = $"<h3>Minefield</h3><label>{expectedLabelText}</label>";

			// Act
			var componentUnderTest = RenderComponent<Minefield>();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		public void Rendering_UnsupportedSizeProvided_ShowsErrorLabel(uint height, uint width)
		{
			// Arrange
			const string expectedLabelText = "Minesweeper is played on a Minefield and not within a black hole! Provide a valid size!";
			string expectedMarkup = $"<h3>Minefield</h3><label>{expectedLabelText}</label>";

			var parameter = ComponentParameterFactory.Parameter(nameof(Minefield.Size), new MinefieldSize(height, width));

			// Act
			var componentUnderTest = RenderComponent<Minefield>(parameter);

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(1, 2)]
		[InlineData(2, 1)]
		[InlineData(10, 20)]
		public void Rendering_SupportedSizeProvided_ShowsCorrectAmountOfCells(uint height, uint width)
		{
			// Arrange
			int expectedCellAmount = (int)(height * width);
			var parameter = ComponentParameterFactory.Parameter(nameof(Minefield.Size), new MinefieldSize(height, width));
			
			// Act
			var componentUnderTest = RenderComponent<Minefield>(parameter);

			// Assert
			//componentUnderTest.Fin
			componentUnderTest.FindComponents<Cell>().Count.Should().Be(expectedCellAmount);
		}

		[Fact]
		public void Rendering_OneCellShown_MarkupIsCorrect()
		{
			// Arrange
			const string expectedMarkup = "<h3>Minefield</h3><table><tr><td><button>Covered</button></td></tr></table>";

			var parameter = ComponentParameterFactory.Parameter(nameof(Minefield.Size), new MinefieldSize(1, 1));

			// Act
			var componentUnderTest = RenderComponent<Minefield>(parameter);

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}
	}
}
