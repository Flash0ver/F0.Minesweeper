using System;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Events;
using Microsoft.AspNetCore.Components;
using Prism.Events;

namespace F0.Minesweeper.Components.Pages.Game.Modules
{
	public partial class GameEndStatus : IDisposable
	{
		[Inject]
		public IEventAggregator? EventAggregator { get; set; }

		private string GameEndMessage { get; set; }

		private string GameEndTextCssClass { get; set; }

		public GameEndStatus()
		{
			GameEndMessage = String.Empty;
			GameEndTextCssClass = "f0-end-text-invisible";
		}

		protected override void OnParametersSet()
		{
			EventAggregator?.GetEvent<DifficultyLevelChangedEvent>().Subscribe(OnDifficultyLevelChanged);
			EventAggregator?.GetEvent<GameFinishedEvent>().Subscribe(OnGameFinished);
		}

		void IDisposable.Dispose()
		{
			EventAggregator?.GetEvent<GameFinishedEvent>().Unsubscribe(OnGameFinished);
			EventAggregator?.GetEvent<DifficultyLevelChangedEvent>().Unsubscribe(OnDifficultyLevelChanged);
		}

		private void OnDifficultyLevelChanged(DifficultyLevel difficultyLevel)
		{
			GameEndMessage = String.Empty;
			GameEndTextCssClass = "f0-end-text-invisible";
			StateHasChanged();
		}

		private void OnGameFinished(string finishedMessage)
		{
			GameEndMessage = finishedMessage;
			GameEndTextCssClass = "f0-end-text-visible";
			StateHasChanged();
		}
	}
}
