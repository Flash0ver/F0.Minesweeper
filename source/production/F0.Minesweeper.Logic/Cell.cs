using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	internal class Cell : IUncoveredCell
	{
		public Location Location { get; init; }
		public bool IsMine { get; internal set; }
		public byte AdjacentMineCount { get; internal set; }
		public bool Uncovered { get; set; }

		internal Cell(Location location, bool isMine, byte adjacentMineCount, bool uncovered)
		{
			Location = location;
			IsMine = isMine;
			AdjacentMineCount = adjacentMineCount;
			Uncovered = uncovered;
		}
	}
}
