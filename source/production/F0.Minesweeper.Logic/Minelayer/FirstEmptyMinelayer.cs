using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal class FirstEmptyMinelayer : IMinelayer
	{
		public ILocationShuffler LocationShuffler { get; }

		public FirstEmptyMinelayer(ILocationShuffler locationShuffler)
			=> LocationShuffler = locationShuffler;

		IReadOnlyCollection<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> LocationShuffler.ShuffleAndTake(
				RemoveClickedLocationArea(possibleLocations.ToList(), clickedLocation),
				(int)mineCount);

		public Dictionary<Location, Cell> PlaceMinesAlternate(uint width, uint height, uint mineCount, Location clickedLocation) => throw new NotImplementedException();

		private static IEnumerable<Location> RemoveClickedLocationArea(IList<Location> locations, Location clickLocation)
		{
			IEnumerable<Location> locationsToDelete = Utilities.GetLocationsAreaAroundLocation(locations, clickLocation, false);
			foreach (Location locationToDelete in locationsToDelete)
			{
				locations.Remove(locationToDelete);
			}
			return locations;
		}
	}
}
