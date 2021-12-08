namespace F0.Minesweeper.Logic.Abstractions
{
	public interface IGameUpdateReport
	{
		GameStatus Status { get; }
		IReadOnlyCollection<IUncoveredCell> Cells { get; }
	}
}
