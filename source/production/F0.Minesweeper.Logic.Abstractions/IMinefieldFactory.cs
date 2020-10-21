namespace F0.Minesweeper.Logic.Abstractions
{
	public interface IMinefieldFactory
	{
		IMinefield Create(MinefieldOptions options);
	}
}
