using System.Collections.Generic;

namespace F0.Minesweeper.Components.Logic.Game
{
	internal class GameLostUpdater : GameUpdater
	{
		internal override void UpdateAsync(List<Components.Cell> cells, Minesweeper.Logic.Abstractions.Location clickedLocation) => throw new System.NotImplementedException();
	}
}