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
			EventAggregator?.GetEvent<GameFinishedEvent>().Subscribe(OnGameFinished);
			EventAggregator?.GetEvent<DifficultyLevelChangedEvent>().Subscribe(OnDifficultyLevelChanged);
			EventAggregator?.GetEvent<RestartGameEvent>().Subscribe(OnRestartGame);
		}

		void IDisposable.Dispose()
		{
			EventAggregator?.GetEvent<GameFinishedEvent>().Unsubscribe(OnGameFinished);
			EventAggregator?.GetEvent<DifficultyLevelChangedEvent>().Unsubscribe(OnDifficultyLevelChanged);
			EventAggregator?.GetEvent<RestartGameEvent>().Subscribe(OnRestartGame);
		}

		private void OnDifficultyLevelChanged(DifficultyLevel difficultyLevel) => ResetGameEndStatus();

		private void OnRestartGame() => ResetGameEndStatus();

		private void ResetGameEndStatus()
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
