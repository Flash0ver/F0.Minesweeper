using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.LocationShuffler
{
	internal class FisherYatesLocationShuffler : ILocationShuffler
	{
		private readonly IRandom randomNumberGenerator;

		internal FisherYatesLocationShuffler(IRandom? randomNumberGenerator = null)
			=> this.randomNumberGenerator = randomNumberGenerator ?? new DefaultRandom();

		public IOrderedEnumerable<Location> Shuffle(Dictionary<Location, Cell> allLocations)
		{
			_ = allLocations ?? throw new ArgumentNullException(nameof(allLocations));

			Location[] shuffledLocations = allLocations.Keys.ToArray();
			int n = shuffledLocations.Length;

			while (n > 1)
			{
				n--;
				int k = randomNumberGenerator.Next(n + 1);
				(shuffledLocations[n], shuffledLocations[k]) = (shuffledLocations[k], shuffledLocations[n]);
			}

			return shuffledLocations.OrderBy(_ => 1);
		}

		//public Dictionary<Location, Cell> ShuffleAndTakeAlternate(IDictionary<Location, Cell> allLocations, int count)
		//{
		//	ArgumentNullException.ThrowIfNull(allLocations);

		//	Location[] shuffledLocations = allLocations;
		//	int n = shuffledLocations.Length;

		//	if (count > n)
		//	{
		//		throw new ArgumentOutOfRangeException(nameof(count), count, $"The take count should not be greater than the number of elements in {nameof(allLocations)}. Element count: {shuffledLocations.Length}");
		//	}

		//	while (n > 1)
		//	{
		//		n--;
		//		int k = randomNumberGenerator.Next(n + 1);
		//		(shuffledLocations[n], shuffledLocations[k]) = (shuffledLocations[k], shuffledLocations[n]);
		//	}

		//	Dictionary<Location, Cell> toReturn = new(count);

		//	for(int i = 0; i < count; i++)
		//	{
		//		Location location = shuffledLocations[i];
		//		toReturn[location] = new Cell(location, true, 0, false);
		//	}

		//	return toReturn;
		//}

		IReadOnlyCollection<Location> ILocationShuffler.ShuffleAndTake(IEnumerable<Location> allLocations, int count)
		{
			_ = allLocations ?? throw new ArgumentNullException(nameof(allLocations));

			Location[] shuffledLocations = allLocations.ToArray();
			int n = shuffledLocations.Length;

			if (count > n)
			{
				throw new ArgumentOutOfRangeException(nameof(count), count, $"The take count should not be greater than the number of elements in {nameof(allLocations)}. Element count: {shuffledLocations.Length}");
			}

			while (n > 1)
			{
				n--;
				int k = randomNumberGenerator.Next(n + 1);
				(shuffledLocations[n], shuffledLocations[k]) = (shuffledLocations[k], shuffledLocations[n]);
			}

			return shuffledLocations
				.Take(count)
				.ToArray();
		}
	}
}
