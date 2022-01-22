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
		public Dictionary<Location, Cell> PlaceMinesAlternate(uint width, uint height, uint mineCount, Location clickedLocation) => throw new NotImplementedException();
	}
}
