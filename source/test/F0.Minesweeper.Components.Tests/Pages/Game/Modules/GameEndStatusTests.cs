using System;
using AutoFixture.Xunit2;
using Bunit;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Events;
using F0.Minesweeper.Components.Pages.Game.Modules;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Prism.Events;
using Xunit;

namespace F0.Minesweeper.Components.Tests.Pages.Game.Modules
{
	public class GameEndStatusTests : TestContext
	{
		private readonly Mock<IEventAggregator> eventAggregatorMock;

		public GameEndStatusTests()
		{
			eventAggregatorMock = new(MockBehavior.Strict);
			Services.AddSingleton(eventAggregatorMock.Object);
		}

		[Fact]
		public void Rendering_NoMessageReceived_GameEndTextIsHidden()
		{
			// Arrange
			string expectedMarkup = $"<div id='f0-gameendstatus'><p class='f0-end-text-invisible' /></div>";

			eventAggregatorMock
				.Setup(agg => agg.GetEvent<DifficultyLevelChangedEvent>())
				.Returns(new DifficultyLevelChangedEvent());
			eventAggregatorMock
				.Setup(agg => agg.GetEvent<GameFinishedEvent>())
				.Returns(new GameFinishedEvent());

			// Act
			IRenderedComponent<GameEndStatus> componentUnderTest = RenderComponent<GameEndStatus>();

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Fact]
		public void Rendering_GameOverMessageReceived_GameEndTextIsShown()
		{
			// Arrange
			string expectedText = "Random Text" + Guid.NewGuid();
			string expectedMarkup = $"<div id='f0-gameendstatus'><p class='f0-end-text-visible'>{expectedText}</p></div>";

			eventAggregatorMock
				.Setup(agg => agg.GetEvent<DifficultyLevelChangedEvent>())
				.Returns(new DifficultyLevelChangedEvent());
			eventAggregatorMock
				.Setup(agg => agg.GetEvent<GameFinishedEvent>())
				.Returns(new GameFinishedEvent());

			IRenderedComponent<GameEndStatus> componentUnderTest = RenderComponent<GameEndStatus>();

			// Act
			componentUnderTest.InvokeAsync(() => eventAggregatorMock.Object.GetEvent<GameFinishedEvent>().Publish(expectedText));

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}

		[Theory, AutoData]
		public void Rendering_GameRestartMessageReceived_GameEndTextIsHidden(DifficultyLevel difficultyLevel)
		{
			// Arrange
			string expectedMarkup = $"<div id='f0-gameendstatus'><p class='f0-end-text-invisible' /></div>";

			eventAggregatorMock
				.Setup(agg => agg.GetEvent<DifficultyLevelChangedEvent>())
				.Returns(new DifficultyLevelChangedEvent());
			eventAggregatorMock
				.Setup(agg => agg.GetEvent<GameFinishedEvent>())
				.Returns(new GameFinishedEvent());

			IRenderedComponent<GameEndStatus> componentUnderTest = RenderComponent<GameEndStatus>();

			// Act
			componentUnderTest.InvokeAsync(() => eventAggregatorMock.Object.GetEvent<GameFinishedEvent>().Publish("Game Over"));
			componentUnderTest.InvokeAsync(() => eventAggregatorMock.Object.GetEvent<DifficultyLevelChangedEvent>().Publish(difficultyLevel));

			// Assert
			componentUnderTest.MarkupMatches(expectedMarkup);
		}
	}
}
