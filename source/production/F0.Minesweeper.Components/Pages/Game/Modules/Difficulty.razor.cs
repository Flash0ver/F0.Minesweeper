using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Events;
using Microsoft.AspNetCore.Components;
using Prism.Events;

namespace F0.Minesweeper.Components.Pages.Game.Modules
{
	public partial class Difficulty
	{
		private DifficultyLevel selected;

		[Inject]
		internal IEventAggregator? EventAggregator { get; set; }

		public Difficulty() => selected = DifficultyLevel.Medium;

		public DifficultyLevel Selected
		{
			get => selected;
			set
			{
				if (selected == value)
				{
					return;
				}

				selected = value;
				EventAggregator?.GetEvent<DifficultyLevelChangedEvent>().Publish(value);
			}
		}
	}
}
