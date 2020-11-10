using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Mineplacer
{
	internal class ImpossibleMineplacer : IMineplacer
	{
		IEnumerable<ILocation> IMineplacer.PlaceMines(IEnumerable<ILocation> possibleLocations, uint mineCount, ILocation clickedLocation)
			=> new ILocation[] { clickedLocation };
	}
}
