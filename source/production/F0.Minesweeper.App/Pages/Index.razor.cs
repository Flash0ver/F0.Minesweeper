using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.App.Pages
{
	public partial class Index
	{
		public MinefieldOptions MinefieldOptions = new MinefieldOptions(10, 10, 2, MinefieldFirstUncoverBehavior.MayYieldMine);
	}
}
