using System;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	public class GameUpdateReport : IGameUpdateReport
	{
		public GameStatus Status { get; private set; }

		public IUncoveredCell[] Cells { get; private set; }

		public GameUpdateReport(GameStatus status, IUncoveredCell[] cells)
		{
			Status = status;
			Cells = cells ?? throw new ArgumentNullException(nameof(cells));
		}
	}
}
