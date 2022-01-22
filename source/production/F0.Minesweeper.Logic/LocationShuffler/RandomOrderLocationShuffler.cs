using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.LocationShuffler
{
	internal class RandomOrderLocationShuffler : ILocationShuffler
	{
		private readonly IRandom randomNumberGenerator;

		internal RandomOrderLocationShuffler(IRandom? randomNumberGenerator = null)
			=> this.randomNumberGenerator = randomNumberGenerator ?? new DefaultRandom();

		//public Dictionary<Location, Cell> ShuffleAndTakeAlternate(IDictionary<Location, Cell> allLocations, int count)
		//{
		//	ArgumentNullException.ThrowIfNull(allLocations);
			
		//	if (count > allLocations.Count)
		//	{
		//		throw new ArgumentOutOfRangeException(nameof(count), count, $"The take count should not be greater than the number of elements in {nameof(allLocations)}. Element count: {allLocations.Count}");
		//	}

		//	IOrderedEnumerable<Location> orderedLocations = allLocations.OrderBy(_ => randomNumberGenerator.Next());

		//	Dictionary<Location, Cell> toReturn = new(count);

		//	for (int i = 0; i < count; i++)
		//	{
		//		Location location = orderedLocations.ElementAt(i);
		//		toReturn[location] = new Cell(location, true, 0, false);
		//	}

		//	return toReturn;
		//}

		IReadOnlyCollection<Location> ILocationShuffler.ShuffleAndTake(IEnumerable<Location> allLocations, int count)
		{
			_ = allLocations ?? throw new ArgumentNullException(nameof(allLocations));

			Location[] shuffledLocations = allLocations
				  .OrderBy(_ => randomNumberGenerator.Next())
				  .Take(count)
				  .ToArray();

			if (count > shuffledLocations.Length)
			{
				throw new ArgumentOutOfRangeException(nameof(count), count, $"The take count should not be greater than the number of elements in {nameof(allLocations)}. Element count: {shuffledLocations.Length}");
			}

			return shuffledLocations;
		}
	}
}
