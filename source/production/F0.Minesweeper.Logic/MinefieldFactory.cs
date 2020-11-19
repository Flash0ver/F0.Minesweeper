using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	public class MinefieldFactory : IMinefieldFactory
	{
		public IMinefield Create(MinefieldOptions options)
			=> new Minefield(options);
	}
}
