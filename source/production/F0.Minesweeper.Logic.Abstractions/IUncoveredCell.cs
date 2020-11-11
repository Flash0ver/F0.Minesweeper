namespace F0.Minesweeper.Logic.Abstractions
{
	public interface IUncoveredCell
	{
		Location Location { get; }
		bool IsMine { get; }
		byte AdjacentMineCount { get; }
	}
}
