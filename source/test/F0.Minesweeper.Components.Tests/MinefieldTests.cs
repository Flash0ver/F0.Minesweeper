using System;
using Bunit;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Logic.Cell;
using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace F0.Minesweeper.Components.Tests
{
	public class MinefieldTests : TestContext
	{
		private readonly Mock<IMinefieldFactory> minefieldFactoryMock;
		private readonly Mock<IMinefield> minefieldMock;

		public MinefieldTests()
		{
			minefieldFactoryMock = new Mock<IMinefieldFactory>(MockBehavior.Strict);
			minefieldMock = new Mock<IMinefield>(MockBehavior.Strict);
			Services.AddSingleton(minefieldFactoryMock.Object);
			Services.AddSingleton<ICellStatusManager>(new CellStatusManager());
		}

		protected override void Dispose(bool disposing)
		{
			Mock.VerifyAll(minefieldFactoryMock);
			base.Dispose(disposing);
		}

		[Fact]
		public void Rendering_NoSizeProvided_ShowsErrorLabel()
		{
			// Arrange
			string expectedMarkup = $"<div id='f0-minefield'><label diff:ignore /></div>";

			// Act
			IRenderedComponent<Minefield> componentUnderTest = RenderComponent<Minefield>();

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
			string expectedMarkup = $"<div id='f0-minefield'><label diff:ignore /></div>";

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Minefield.Options), new MinefieldOptions(width, height, 2, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler));

			// Act
			IRenderedComponent<Minefield> componentUnderTest = RenderComponent<Minefield>(parameter);

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Theory]
		[InlineData(1, 1)]
		[InlineData(2, 2)]
		[InlineData(1, 2)]
		[InlineData(2, 1)]
		[InlineData(10, 20)]
		public void Rendering_SupportedSizeProvided_ShowsCorrectAmountOfCells(uint width, uint height)
		{
			// Arrange
			var options = new MinefieldOptions(width, height, 2, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);
			int expectedCellAmount = (int)(width * height);

			minefieldFactoryMock.Setup(factory => factory.Create(options)).Returns(minefieldMock.Object);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Minefield.Options), options);

			// Act
			IRenderedComponent<Minefield> componentUnderTest = RenderComponent<Minefield>(parameter);

			// Assert
			componentUnderTest.FindComponents<Cell>().Count.Should().Be(expectedCellAmount);
		}

		[Fact]
		public void Rendering_OneCellShown_MarkupIsCorrect()
		{
			// Arrange
			const string expectedMarkup = "<div id='f0-minefield'><table><tr><td><div diff:ignore /></td></tr></table></div>";

			var options = new MinefieldOptions(1, 1, 2, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);

			minefieldFactoryMock.Setup(factory => factory.Create(options)).Returns(minefieldMock.Object);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Minefield.Options), options);

			// Act
			IRenderedComponent<Minefield> componentUnderTest = RenderComponent<Minefield>(parameter);

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Fact]
		public void OnCellUncoveredAsync_MinefieldWasNotCreated_Throws()
		{
			// Arrange
			var options = new MinefieldOptions(1, 1, 2, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);

			minefieldFactoryMock.Setup(factory => factory.Create(options)).Returns<IMinefield>(null);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Minefield.Options), options);

			IRenderedComponent<Minefield> componentUnderTest = RenderComponent<Minefield>(parameter);

			// Act && Assert
			Action actionToTest = () => componentUnderTest.Find("button").Click();
			actionToTest.Should().Throw<InvalidOperationException>();
		}

		[Theory]
		[MemberData(nameof(GetReportVariations))]
		private void OnCellUncoveredAsync_ReportVariations_DoesNotThrow(GameUpdateReportForTests report)
		{
			// Arrange
			var options = new MinefieldOptions(1, 1, 2, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);

			minefieldFactoryMock.Setup(factory => factory.Create(options)).Returns(minefieldMock.Object);
			minefieldMock.Setup(field => field.Uncover(It.Is<Location>(s => s == new Location(0, 0)))).Returns(report);

			ComponentParameter parameter = ComponentParameterFactory.Parameter(nameof(Minefield.Options), options);

			IRenderedComponent<Minefield> componentUnderTest = RenderComponent<Minefield>(parameter);

			// Act && Assert
			Action actionToTest = () => componentUnderTest.Find("button").Click();
			actionToTest.Should().NotThrow();
		}

		private static TheoryData<GameUpdateReportForTests> GetReportVariations() =>
			new()
			{
				// with no cells
				new GameUpdateReportForTests(GameStatus.InProgress, new UncoveredCellForTests[0]),
				new GameUpdateReportForTests(GameStatus.IsLost, new UncoveredCellForTests[0]),
				new GameUpdateReportForTests(GameStatus.IsWon, new UncoveredCellForTests[0]),

				// with one valid mine cell 
				new GameUpdateReportForTests(GameStatus.InProgress, new []
				{
					new UncoveredCellForTests(new (0,0),true,0)
				}),
				new GameUpdateReportForTests(GameStatus.IsLost, new []
				{
					new UncoveredCellForTests(new (0,0),true,0)
				}),
				new GameUpdateReportForTests(GameStatus.IsWon, new []
				{
					new UncoveredCellForTests(new (0,0),true,0)
				}),

				// with one mine cell outside of field
				new GameUpdateReportForTests(GameStatus.InProgress, new []
				{
					new UncoveredCellForTests(new (500,400),true,0)
				}),
				new GameUpdateReportForTests(GameStatus.IsLost, new []
				{
					new UncoveredCellForTests(new (500,400),true,0)
				}),
				new GameUpdateReportForTests(GameStatus.IsWon, new []
				{
					new UncoveredCellForTests(new (500,400),true,0)
				}),

				// with one valid none mine cell
				new GameUpdateReportForTests(GameStatus.InProgress, new []
				{
					new UncoveredCellForTests(new (0,0),false,1)
				}),
				new GameUpdateReportForTests(GameStatus.IsLost, new []
				{
					new UncoveredCellForTests(new (0,0),false,1)
				}),
				new GameUpdateReportForTests(GameStatus.IsWon, new []
				{
					new UncoveredCellForTests(new (0,0),false,1)
				}),

					// with one non mine cell outside of field
				new GameUpdateReportForTests(GameStatus.InProgress, new []
				{
					new UncoveredCellForTests(new (500,400),false,3)
				}),
				new GameUpdateReportForTests(GameStatus.IsLost, new []
				{
					new UncoveredCellForTests(new (500,400),false,3)
				}),
				new GameUpdateReportForTests(GameStatus.IsWon, new []
				{
					new UncoveredCellForTests(new (500,400),false,3)
				})
			};

		private class GameUpdateReportForTests : IGameUpdateReport
		{
			public GameUpdateReportForTests(GameStatus status, IUncoveredCell[] cells)
			{
				Status = status;
				Cells = cells;
			}

			public GameStatus Status { get; init; }

			public IUncoveredCell[] Cells { get; init; }
		}

		private class UncoveredCellForTests : IUncoveredCell
		{
			public UncoveredCellForTests(Location location, bool isMine, byte adjacentMineCount)
			{
				Location = location;
				IsMine = isMine;
				AdjacentMineCount = adjacentMineCount;
			}

			public Location Location { get; init; }

			public bool IsMine { get; init; }

			public byte AdjacentMineCount { get; init; }
		}
	}
}
