using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Components.Pages.Game
{
	public partial class Game
	{
		public MinefieldOptions MinefieldOptions = new(10, 10, 10, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.GuidLocationShuffler);
	}
}
