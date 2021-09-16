using System;
using AngleSharp.Dom;
using Bunit;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Events;
using F0.Minesweeper.Components.Pages.Game.Modules;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Prism.Events;
using Xunit;

namespace F0.Minesweeper.Components.Tests.Pages.Game.Modules
{
	public class DifficultyTests : TestContext
	{
		private readonly Mock<IEventAggregator> eventAggregatorMock;

		public DifficultyTests()
		{
			eventAggregatorMock = new(MockBehavior.Strict);
			Services.AddSingleton(eventAggregatorMock.Object);
		}

		[Fact]
		public void Render_Default()
		{
			// Arrange
			string expectedMarkup = GetMarkup(DifficultyLevel.Medium);

			// Act
			IRenderedComponent<Difficulty> cut = RenderComponent<Difficulty>();

			// Assert
			cut.MarkupMatches(expectedMarkup);
		}

		[Theory]
		[MemberData(nameof(GetDifficultyLevels))]
		public void Render_Select_Option(DifficultyLevel difficultyLevel)
		{
			// Arrange
			string expectedMarkup = GetMarkup(difficultyLevel);

			DifficultyLevelChangedEvent @event = new();
			var selected = (DifficultyLevel)Int32.MinValue;

			eventAggregatorMock
				.Setup(agg => agg.GetEvent<DifficultyLevelChangedEvent>())
				.Returns(@event);
			@event.Subscribe(level => selected = level);

			// Act
			IRenderedComponent<Difficulty> cut = RenderComponent<Difficulty>();
			IElement select = cut.Find("select");
			select.Change(difficultyLevel.ToString());

			// Assert
			cut.MarkupMatches(expectedMarkup);
			if (difficultyLevel == DifficultyLevel.Medium)
			{
				selected.Should().Be(Int32.MinValue);
			}
			else
			{
				selected.Should().Be(difficultyLevel);
			}
		}

		private static string GetMarkup(DifficultyLevel difficultyLevel)
		{
			return $@"
<div>
	<label for='difficulty'>Difficulty</label>
	<select name='difficulty' value='{difficultyLevel}'>
		<option id='{nameof(DifficultyLevel.Easy)}'>{nameof(DifficultyLevel.Easy)}</option>
		<option id='{nameof(DifficultyLevel.Medium)}'>{nameof(DifficultyLevel.Medium)}</option>
		<option id='{nameof(DifficultyLevel.Hard)}'>{nameof(DifficultyLevel.Hard)}</option>
	</select>
</div>";
		}

		private static TheoryData<DifficultyLevel> GetDifficultyLevels()
		{
			var data = new TheoryData<DifficultyLevel>();

			foreach (DifficultyLevel difficultyLevel in Enum.GetValues<DifficultyLevel>())
			{
				data.Add(difficultyLevel);
			}

			return data;
		}
	}
}
