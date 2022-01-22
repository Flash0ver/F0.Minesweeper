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
		public Dictionary<Location, Cell> PlaceMinesAlternate(uint width, uint height, uint mineCount, Location clickedLocation)
		{
			Dictionary<Location, Cell> minefield = new();

			//for (uint i = 0; i < width; i++)
			//{
			//	for (uint y = 0; y < height; y++)
			//	{
			//		Location location = new(i, y);
			//		if (location != clickedLocation)
			//		{
			//			locations.Add(location, new Cell(location, true, 0, false);
			//		}
			//	}
			//}

			//return LocationShuffler.ShuffleAndTakeAlternate(locations, (int)mineCount);

			//for (int i = 0; i < mineCount; i++)
			//{
			//	int index = RandomNumberGenerator.GetInt32(0, locations.Count - 1);
			//	var location = locations[index];
			//	locations.RemoveAt(index);
			//	minefield[location] = new Cell(location, true, 0, false);
			//}

			for (int i = 0; i < mineCount; i++)
			{
				Location location;
				do
				{
					uint x = (uint)RandomNumberGenerator.GetInt32(0, (int)width);
					uint y = (uint)RandomNumberGenerator.GetInt32(0, (int)height);

					location = new(x, y);
				} while (location == clickedLocation || minefield.ContainsKey(location));

				minefield[location] = new Cell(location, true, 0, false);
			}

			return minefield;
		}
	}
}
