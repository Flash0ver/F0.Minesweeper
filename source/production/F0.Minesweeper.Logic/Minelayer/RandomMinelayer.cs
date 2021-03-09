using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal class RandomMinelayer : IMinelayer
	{
		public ILocationShuffler LocationShuffler { get; }

		public RandomMinelayer(ILocationShuffler locationShuffler)
			=> LocationShuffler = locationShuffler;

		IReadOnlyCollection<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> LocationShuffler.ShuffleAndTake(
				possibleLocations,
				(int)mineCount);
	}
}
