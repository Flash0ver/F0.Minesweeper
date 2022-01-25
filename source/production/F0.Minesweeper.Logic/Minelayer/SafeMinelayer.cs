using System.Security.Cryptography;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal class SafeMinelayer : IMinelayer
	{
		public ILocationShuffler LocationShuffler { get; }

		public SafeMinelayer(ILocationShuffler locationShuffler)
			=> LocationShuffler = locationShuffler;

		IReadOnlyCollection<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> LocationShuffler.ShuffleAndTake(
				possibleLocations.Where(l => l != clickedLocation),
				(int)mineCount);
		public Dictionary<Location, Cell> PlaceMinesAlternate(Dictionary<Location, Cell> allLocations, Location clickedLocation, uint mineCount, uint width, uint height)
		{
			IOrderedEnumerable<Location> shuffledLocations = LocationShuffler.Shuffle(allLocations);

			for(int i = 0; i < allLocations.Count; i++)
			{
				Location location = shuffledLocations.ElementAt(i);

				if(location == clickedLocation)
				{
					continue;
				}

				Cell current = allLocations[location];
				current.IsMine = true;

				mineCount--;

				if(mineCount == 0)
				{
					break;
				}
			}

			return allLocations;
		}
	}
}
