namespace F0.Minesweeper.Logic.Abstractions
{
	public interface IMinefield
	{
		IGameUpdateReport Uncover(uint x, uint y);
		IGameUpdateReport Uncover(ILocation location);
	}
}
