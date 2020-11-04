using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	internal class GameUpdateReport : IGameUpdateReport
	{
		public GameStatus Status { get; private set; }

		public IUncoveredCell[] Cells { get; private set; }

		internal GameUpdateReport(GameStatus status, IUncoveredCell[] cells)
		{
			Status = status;
			Cells = cells;
		}
	}
}
