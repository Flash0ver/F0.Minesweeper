using System.Diagnostics;
using F0.Minesweeper.Components.Events;
using Microsoft.AspNetCore.Components;
using Prism.Events;

namespace F0.Minesweeper.Components.Pages.Game.Modules
{
	public partial class RestartGame
	{
		[Inject]
		internal IEventAggregator? EventAggregator { get; set; }

		private void Restart()
		{
			Debug.Assert(EventAggregator != null, $"The '{nameof(IEventAggregator)}' is injected on component generation.");
			EventAggregator.GetEvent<RestartGameEvent>().Publish();
		}
	}
}
