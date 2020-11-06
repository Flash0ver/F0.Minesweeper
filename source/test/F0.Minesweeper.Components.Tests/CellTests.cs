using System;
using AngleSharp.Dom;
using Bunit;
using Bunit.Rendering;
using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Components.Tests
{
	public class CellTests : TestContext
	{
		[Fact]
		public void Rendering_NoLocationProvided_Throws()
		{
			Action renderAction = () => RenderComponent<Cell>();
			renderAction.Should().Throw<ArgumentNullException>();
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(0, 1)]
		[InlineData(1, 0)]
		[InlineData(100, 100)]
		public void Rendering_LocationProvided_ShowsCell(uint x, uint y)
		{
			// Arrange
			string expectedMarkup = "<button>Covered</button>";

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(x, y));

			// Act
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
			componentUnderTest.Instance.Location.Should().BeEquivalentTo(new Location(x, y));
		}

		[Fact]
		public void OnClick_TextIsCovered_ChangesTextToUncovered()
		{
			// Arrange
			string expectedMarkup = "<button>Uncovered</button>";

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);

			// Act
			componentUnderTest.Find("button").Click();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Fact]
		public void OnClick_TextIsUncovered_TextStaysUncovered()
		{
			// Arrange
			string expectedMarkup = "<button>Uncovered</button>";

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Cell.Location), new Location(1, 1));
			IRenderedComponent<Cell> componentUnderTest = RenderComponent<Cell>(parameter);
			IElement buttonToClick = componentUnderTest.Find("button");
			buttonToClick.Click();

			// Act
			buttonToClick.Click();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}
	}
}
