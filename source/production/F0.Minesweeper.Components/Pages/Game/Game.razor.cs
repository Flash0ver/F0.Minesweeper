using System.ComponentModel;
using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Events;
using F0.Minesweeper.Logic.Abstractions;
using Microsoft.AspNetCore.Components;
using Prism.Events;

namespace F0.Minesweeper.Components.Pages.Game
{
	public partial class Game : IDisposable
	{
		[Inject]
		internal IEventAggregator? EventAggregator { get; set; }

		private MinefieldOptions options = CreateOptions(DifficultyLevel.Medium);

		protected override void OnParametersSet()
		{
			EventAggregator?.GetEvent<DifficultyLevelChangedEvent>().Subscribe(OnDifficultyChanged);
		}

		void IDisposable.Dispose()
		{
			EventAggregator?.GetEvent<DifficultyLevelChangedEvent>().Unsubscribe(OnDifficultyChanged);
		}

		private void OnDifficultyChanged(DifficultyLevel selectedDifficulty)
		{
			options = CreateOptions(selectedDifficulty);
			StateHasChanged();
		}

		private static MinefieldOptions CreateOptions(DifficultyLevel difficultyLevel)
		{
			return difficultyLevel switch
			{
				DifficultyLevel.Easy => MinefieldOptions.Easy,
				DifficultyLevel.Medium => MinefieldOptions.Medium,
				DifficultyLevel.Hard => MinefieldOptions.Hard,
				_ => throw new InvalidEnumArgumentException(nameof(difficultyLevel), (int)difficultyLevel, typeof(DifficultyLevel)),
			};
		}
	}
}
