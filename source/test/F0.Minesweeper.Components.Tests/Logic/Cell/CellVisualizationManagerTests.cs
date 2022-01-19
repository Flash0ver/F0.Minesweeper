using System;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Logic.Cell;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Components.Tests.Logic.Cell
{
	public class CellVisualizationManagerTests
	{
		[Fact]
		public void GetVisualization_UncoveredWithoutAdjacentMineCount_Throws()
		{
			// Arrange
			CellVisualizationManager instanceUnderTest = new();

			// Act && Assert
			Func<CellVisualization> methodUnderTest = () => instanceUnderTest.GetVisualization(CellStatusType.Uncovered);
			methodUnderTest.Should().ThrowExactly<ArgumentNullException>();
		}

		[Theory]
		[MemberData(nameof(TestData), MemberType = typeof(CellVisualizationManagerTests))]
		public void GetVisualization_CorrectParameters_ReturnsCorrectVisualization(VisualizationData visualizationData)
		{
			// Arrange
			CellVisualizationManager instanceUnderTest = new();

			// Act 
			CellVisualization visualization = instanceUnderTest.GetVisualization(visualizationData.CellStatusType, visualizationData.AdjacentMineCount);

			// Assert
			visualization.Content.Should().Be(visualizationData.ExpectedContent);
			visualization.CssClass.Should().NotBeNull();
		}

		public static TheoryData<VisualizationData> TestData => GenerateTestData();

		private static TheoryData<VisualizationData> GenerateTestData()
		{
			TheoryData<VisualizationData> resultData = new();

			resultData.Add(new(CellStatusType.Uncovered, 0, ' '));
			resultData.Add(new(CellStatusType.Uncovered, 1, '1'));
			resultData.Add(new(CellStatusType.Uncovered, 2, '2'));
			resultData.Add(new(CellStatusType.Uncovered, 3, '3'));
			resultData.Add(new(CellStatusType.Uncovered, 4, '4'));
			resultData.Add(new(CellStatusType.Uncovered, 5, '5'));
			resultData.Add(new(CellStatusType.Uncovered, 6, '6'));
			resultData.Add(new(CellStatusType.Uncovered, 7, '7'));
			resultData.Add(new(CellStatusType.Uncovered, 8, '8'));
			resultData.Add(new(CellStatusType.Covered, null, ' '));
			resultData.Add(new(CellStatusType.Flagged, null, '⚐'));
			resultData.Add(new(CellStatusType.FlaggedWrong, null, '⚐'));
			resultData.Add(new(CellStatusType.Unsure, null, '?'));
			resultData.Add(new(CellStatusType.Mine, null, '☢'));
			resultData.Add(new(CellStatusType.MineExploded, null, '☢'));
			return resultData;
		}
	}

	public record VisualizationData(CellStatusType CellStatusType, byte? AdjacentMineCount, char ExpectedContent);
}
