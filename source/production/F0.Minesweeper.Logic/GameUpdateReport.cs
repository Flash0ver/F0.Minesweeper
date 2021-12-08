using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	internal class GameUpdateReport : IGameUpdateReport
	{
		public GameStatus Status { get; }

		public IReadOnlyCollection<IUncoveredCell> Cells { get; }

		internal GameUpdateReport(GameStatus status, IReadOnlyCollection<IUncoveredCell> cells)
		{
			Status = status;
			Cells = cells;
		}
	}
}
