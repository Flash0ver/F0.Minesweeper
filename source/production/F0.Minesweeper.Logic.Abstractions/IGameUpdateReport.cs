namespace F0.Minesweeper.Logic.Abstractions
{
	public interface IGameUpdateReport
	{
		GameStatus Status { get; }
		IUncoveredCell[] Cells { get; }
	}
}
