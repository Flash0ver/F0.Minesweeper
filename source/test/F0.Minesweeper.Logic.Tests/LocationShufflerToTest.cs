using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;

namespace F0.Minesweeper.Logic.Tests
{
	internal class LocationShufflerToTest : ILocationShuffler
	{
		public IEnumerable<Location> Locations { get; }

		public LocationShufflerToTest(params (uint, uint)[] locations)
			=> Locations = locations.Select(l => new Location(l.Item1, l.Item2));

		public LocationShufflerToTest(Location[] locations)
			=> Locations = locations;

		IReadOnlyCollection<Location> ILocationShuffler.ShuffleAndTake(IEnumerable<Location> allLocations, int count)
			=> Locations
				.Intersect(allLocations)
				.Take(count)
				.ToArray();
	}
}
