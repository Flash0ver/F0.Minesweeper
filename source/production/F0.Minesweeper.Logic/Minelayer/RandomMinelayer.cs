using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal class RandomMinelayer : IMinelayer
	{
		IEnumerable<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> possibleLocations
				.OrderBy(_ => Guid.NewGuid())
				.Take((int)mineCount);
	}
}
