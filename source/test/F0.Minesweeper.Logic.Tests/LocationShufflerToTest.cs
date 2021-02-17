using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;

namespace F0.Minesweeper.Logic.Tests
{
	internal class LocationShufflerToTest : ILocationShuffler
	{
		public IEnumerable<Location> Locations { get; }

		public LocationShufflerToTest(params (uint, uint)[] locations)
			=> Locations = locations.Select(l => new Location(l.Item1, l.Item2));

		IEnumerable<Location> ILocationShuffler.ShuffleAndTake(IEnumerable<Location> allLocations, int count)
			=> Locations
				.Intersect(allLocations)
				.Take(count);
	}
}
