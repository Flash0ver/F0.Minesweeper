using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.LocationShuffler
{
	internal interface ILocationShuffler
	{
		internal IReadOnlyCollection<Location> ShuffleAndTake(IEnumerable<Location> allLocations, int count);
		//Dictionary<Location, Cell> ShuffleAndTakeAlternate(IDictionary<Location, Cell> allLocations, int count);
	}
}
