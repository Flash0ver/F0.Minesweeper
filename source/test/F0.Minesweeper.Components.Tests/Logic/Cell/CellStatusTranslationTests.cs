using System;
using F0.Minesweeper.Components.Logic.Cell;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Components.Tests.Logic.Cell
{
    public class CellStatusTranslationTests 
    {
        [Fact]
        public void Constructor_WithoutTranslation_SetsCssClass()
        {
            // Arrange & Act
			const string expectedCssClass = "my-class";
			var instanceUnderTest = new CellStatusTranslation(expectedCssClass);

			// Assert
			instanceUnderTest.CssClass.Should().Be(expectedCssClass);
		}

        [Fact]
        public void Constructor_WithTranslation_SetsCssClass()
        {
            // Arrange & Act
			const string expectedCssClass = "my-class";
			var instanceUnderTest = new CellStatusTranslation('1', expectedCssClass);

			// Assert
			instanceUnderTest.CssClass.Should().Be(expectedCssClass);
		}

        [Fact]
        public void GetDisplayValue_NoActiveTranslationSet_NoAdjacentMines_Throws()
        {
			// Arrange
			var instanceUnderTest = new CellStatusTranslation("css-class");

			// Act && Assert
			Action actionToTest = () => instanceUnderTest.GetDisplayValue(null);
			actionToTest.Should().Throw<ArgumentNullException>();
		}

        [Theory]
        [InlineData(null)]
        [InlineData(4)]
        public void GetDisplayValue_ActiveTranslationSet_ReturnsActiveTranslation(int? adjacentMineCount)
        {
			// Arrange
			const char expectedTranslation = 't';
			var instanceUnderTest = new CellStatusTranslation(expectedTranslation, "css-class");

			// Act
			char displayValue = instanceUnderTest.GetDisplayValue((byte?)adjacentMineCount);

			// Assert
			displayValue.Should().Be(expectedTranslation);
		}

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void GetDisplayValue_NoActiveTranslationSet_HasAdjacentMines_ReturnsCharRepresentationOfAdjacentMines(int adjacentMineCount)
        {
            // Arrange
			var instanceUnderTest = new CellStatusTranslation("css-class");

			// Act
			char displayValue = instanceUnderTest.GetDisplayValue((byte?)adjacentMineCount);

			// Assert
			displayValue.Should().Be(adjacentMineCount.ToString()[0]);
        }
    }
}