using F0.Minesweeper.Components.Events;
using Microsoft.AspNetCore.Components;
using Prism.Events;

namespace F0.Minesweeper.Components.Pages.Game.Modules
{
	public partial class GameEndStatus
	{
		[Inject]
		public IEventAggregator? EventAggregator { get; set; }

		private string GameEndMessage { get; set; }

		private string GameEndTextCssClass { get; set; }

		public GameEndStatus()
		{
			GameEndMessage = string.Empty;
			GameEndTextCssClass = "f0-end-text-invisible";
		}

		protected override void OnParametersSet() 
		{
			EventAggregator?.GetEvent<GameFinishedEvent>().Subscribe(OnGameFinished);
		}

		private void OnGameFinished(string finishedMessage) 
		{
			GameEndMessage = finishedMessage;
			GameEndTextCssClass = "f0-end-text-visible";
			StateHasChanged();
		}
	}
}
