using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal class ImpossibleMinelayer : IMinelayer
	{
		IEnumerable<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> new Location[] { clickedLocation };
	}
}
