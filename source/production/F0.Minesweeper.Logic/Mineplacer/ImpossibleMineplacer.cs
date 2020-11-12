using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Mineplacer
{
	internal class ImpossibleMineplacer : IMineplacer
	{
		IEnumerable<Location> IMineplacer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> new Location[] { clickedLocation };
	}
}
