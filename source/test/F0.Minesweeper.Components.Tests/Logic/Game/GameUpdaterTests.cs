using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

using GameCell = F0.Minesweeper.Components.Pages.Game.Modules.Cell;

namespace F0.Minesweeper.Components.Logic.Game
{
	public class GameUpdaterTests
	{
		[Fact]
		public void WithReport_SetsReport()
		{
			// Arrange
			Mock<IGameUpdateReport> gameReport = new(MockBehavior.Strict);
			gameReport.Setup(report => report.Cells).Throws<NotImplementedException>();
			GameUpdaterForTests instanceUnderTest = new();

			// Act
			instanceUnderTest.WithReport(gameReport.Object);

			// Assert
			Action actionUnderAssertion = () => { _ = instanceUnderTest.ExposedReport?.Cells; };
			actionUnderAssertion.Should().Throw<NotImplementedException>();
		}

		[Fact]
		public void UpdateAsync_NoReportSet_Throws()
		{
			// Arrange
			GameUpdaterForTests instanceUnderTest = new();

			// Act & Assert
			Func<Task> actionUnderTest = async () => await instanceUnderTest.UpdateAsync(Enumerable.Empty<Pages.Game.Modules.Cell>(), new Location(1, 1));
			actionUnderTest.Should().Throw<InvalidOperationException>();
		}

		[Fact]
		public async Task UpdateAsync_WithReport_CallsOnUpdateWithUncoverableCells()
		{
			// Arrange

			// Game Field 	=> x = 5, y = 3
			// Clicked 		=> x = 2, y = 1
			// Report (x = covered, number = uncovered cell):
			// x x x x x
			// 1 2 1 x x
			// 0 0 2 x x
			IEnumerable<Pages.Game.Modules.Cell> gameCells = GetCells(5, 3);
			Mock<IGameUpdateReport> gameReport = new(MockBehavior.Strict);
			gameReport.Setup(report => report.Cells).Returns(GetUncoveredCells());
			GameUpdaterForTests instanceUnderTest = new();
			instanceUnderTest.WithReport(gameReport.Object);

			// Act
			await instanceUnderTest.UpdateAsync(gameCells, new Location(2, 1));

			// Assert
			instanceUnderTest.UncoverableCells.Should()
				.NotBeNullOrEmpty()
				.And
				.HaveCount(6);

			IEnumerable<GameCell> GetCells(uint x, uint y)
			{
				using TestContext testContext = new();
				testContext.Services.AddSingleton<ICellStatusManager>(new Mock<ICellStatusManager>(MockBehavior.Strict).Object);
				testContext.Services.AddSingleton<ICellVisualisationManager>(new Mock<ICellVisualisationManager>(MockBehavior.Strict).Object);

				for (uint i = 0; i < x; i++)
				{
					for (uint j = 0; j < y; j++)
					{
						Location location = new(i, j);
						ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(GameCell.Location), location);
						GameCell cellComponent = testContext.RenderComponent<GameCell>(parameter).Instance;
						yield return cellComponent;
					}
				}
			}

			IUncoveredCell[] GetUncoveredCells()
			{
				return new[]{
					new UncoveredCellForTests(new Location(1, 0), false, 1),
					new UncoveredCellForTests(new Location(1, 1), false, 2),
					new UncoveredCellForTests(new Location(1, 2), false, 1),
					new UncoveredCellForTests(new Location(2, 0), false, 0),
					new UncoveredCellForTests(new Location(2, 1), false, 0),
					new UncoveredCellForTests(new Location(2, 2), false, 2),
				};
			}
		}

		private class GameUpdaterForTests : GameUpdater
		{
			internal IGameUpdateReport? ExposedReport => Report;
			internal IEnumerable<UncoverableCell>? UncoverableCells;

			protected override Task OnUpdateAsync(IEnumerable<UncoverableCell> uncoverableCells, Location clickedLocation)
			{
				UncoverableCells = uncoverableCells;
				return Task.CompletedTask;
			}
		}

		private record UncoveredCellForTests(Location Location, bool IsMine, byte AdjacentMineCount) : IUncoveredCell;
	}
}
