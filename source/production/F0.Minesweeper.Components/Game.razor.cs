using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Components
{
	public partial class Game
	{
		public MinefieldOptions MinefieldOptions = new MinefieldOptions(10, 10, 2, MinefieldFirstUncoverBehavior.MayYieldMine);
	}
}
