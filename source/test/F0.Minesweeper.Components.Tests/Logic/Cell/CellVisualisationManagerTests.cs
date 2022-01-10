using System;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Logic.Cell;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Components.Tests.Logic.Cell
{
	public class CellVisualisationManagerTests
	{
		[Fact]
		public void GetVisualisation_UncoveredWithoutAdjacentMineCount_Throws()
		{
			// Arrange
			CellVisualisationManager instanceUnderTest = new();

			// Act && Assert
			Func<CellVisualisation> methodUnderTest = () => instanceUnderTest.GetVisualisation(CellStatusType.Uncovered);
			methodUnderTest.Should().ThrowExactly<ArgumentNullException>();
		}

		[Theory]
		[MemberData(nameof(TestData), MemberType = typeof(CellVisualisationManagerTests))]
		public void GetVisualisation_CorrectParameters_ReturnsCorrectVisualisation(VisualisationData visualisationData)
		{
			// Arrange
			CellVisualisationManager instanceUnderTest = new();

			// Act 
			CellVisualisation visualisation = instanceUnderTest.GetVisualisation(visualisationData.CellStatusType, visualisationData.AdjacentMineCount);

			// Assert
			visualisation.Content.Should().Be(visualisationData.ExpectedContent);
			visualisation.CssClass.Should().NotBeNull();
		}

		public static TheoryData<VisualisationData> TestData => GenerateTestData();

		private static TheoryData<VisualisationData> GenerateTestData()
		{
			TheoryData<VisualisationData> resultData = new();

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

	public record VisualisationData(CellStatusType CellStatusType, byte? AdjacentMineCount, char ExpectedContent);
}
