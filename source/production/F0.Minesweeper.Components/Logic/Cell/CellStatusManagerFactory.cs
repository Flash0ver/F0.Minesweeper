using System;
using F0.Minesweeper.Components.Abstractions;

namespace F0.Minesweeper.Components.Logic.Cell
{
	public static class CellStatusManagerFactory
	{
		public static ICellStatusManager GetManager(IServiceProvider arg)
		{
			return new CellStatusManager();
		}
	}
}
