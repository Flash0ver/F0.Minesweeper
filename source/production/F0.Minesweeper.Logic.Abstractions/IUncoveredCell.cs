namespace F0.Minesweeper.Logic.Abstractions
{
	public interface IUncoveredCell
	{
		ILocation Location { get; }
		bool IsMine { get; }
		byte AdjacentMineCount { get; }
	}
}
