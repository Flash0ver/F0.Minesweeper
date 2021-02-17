using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal class SafeMinelayer : IMinelayer
	{
		public ILocationShuffler LocationShuffler { get; }

		public SafeMinelayer(ILocationShuffler locationShuffler)
			=> LocationShuffler = locationShuffler;

		IEnumerable<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> LocationShuffler.ShuffleAndTake(
				possibleLocations.Where(l => l != clickedLocation),
				(int)mineCount);
			
	}
}
