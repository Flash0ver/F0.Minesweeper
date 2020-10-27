using System;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	public class UncoveredCell : IUncoveredCell
	{
		public ILocation Location { get; private set; }
		public bool IsMine { get; private set; }
		public byte AdjacentMineCount { get; private set; }

		internal UncoveredCell(ILocation location, bool isMine, byte adjacentMineCount)
		{
			Location = location ?? throw new ArgumentNullException(nameof(location));
			IsMine = isMine;
			AdjacentMineCount = adjacentMineCount;
		}
	}
}
