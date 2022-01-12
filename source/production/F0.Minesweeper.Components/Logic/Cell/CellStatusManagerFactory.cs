using F0.Minesweeper.Components.Abstractions;

namespace F0.Minesweeper.Components.Logic.Cell
{
	public static class CellStatusManagerFactory
	{
		public static ICellStatusManager GetManager(IServiceProvider _)
		{
			return new CellStatusManager();
		}
	}
}
