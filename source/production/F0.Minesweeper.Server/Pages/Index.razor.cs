using F0.Minesweeper.Components;
using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;

namespace F0.Minesweeper.Server.Pages
{
	public partial class Index
	{
		public MinefieldSize MinefieldSize = new MinefieldSize(10, 10);

		public Minefield? Minefield1 { get; set; }

		private void OnDifficultyChange(DifficultyLevel selectedDifficulty)
		{
			if (Minefield1 is null)
			{
				return;
			}

			Minefield1.Difficulty = selectedDifficulty;
		}
	}
}
