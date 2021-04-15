using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Components.Logic.Game
{
	internal abstract class GameUpdater
	{
		protected IGameUpdateReport? report;

		internal abstract void UpdateAsync(List<Components.Cell> cells, Location clickedLocation);
		internal GameUpdater WithReport(IGameUpdateReport report)
        {
            this.report = report;
            return this;
        }
	}
}