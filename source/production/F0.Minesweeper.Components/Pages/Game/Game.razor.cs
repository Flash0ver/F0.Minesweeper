using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Components.Events;
using F0.Minesweeper.Components.Pages.Game.Modules;
using F0.Minesweeper.Logic.Abstractions;
using Microsoft.AspNetCore.Components;
using Prism.Events;

namespace F0.Minesweeper.Components.Pages.Game
{
	public partial class Game
	{
		[Inject]
		internal IEventAggregator? EventAggregator { get; set; }

		public Minefield? Minefield { get; set; }

		public MinefieldOptions MinefieldOptions = new(10, 10, 10, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);

		protected override void OnParametersSet()
		{
			EventAggregator?.GetEvent<DifficultyLevelChangedEvent>().Subscribe(OnDifficultyChanged);
		}

		private void OnDifficultyChanged(DifficultyLevel selectedDifficulty)
		{
			MinefieldOptions = Minefield?.Options;
			//Minefield = new Minefield();
			//switch (selectedDifficulty)
			//{
			//	case DifficultyLevel.Easy:
			//		MinefieldOptions = new(5, 5, 5, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);
			//		break;
			//	case DifficultyLevel.Medium:
			//		MinefieldOptions = new(10, 10, 10, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);
			//		break;
			//	case DifficultyLevel.Hard:
			//		MinefieldOptions = new(15, 15, 15, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);
			//		break;
			//}

			// TODO : still need something like reset selection
			//IGameUpdateReport report = new F0.Minesweeper.Logic.Abstractions.GameUpdateReport(GameStatus.DifficultyChanged, allUncoveredCells.ToArray());
			//Minefield.GameUpdateFactory?.On(GameStatus.DifficultyChanged).WithReport(report).UpdateAsync(cells, null);

			StateHasChanged();
		}
	}
}
