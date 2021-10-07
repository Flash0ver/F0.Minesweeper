using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.LocationShuffler
{
	internal class FisherYatesLocationShuffler : ILocationShuffler
	{
		private static Random randomNumberGenerator = new();
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
				Location buffer = shuffledLocations[k];
				shuffledLocations[k] = shuffledLocations[n];
				shuffledLocations[n] = buffer;
			}

			return shuffledLocations
				.Take(count)
				.ToArray();
		}
	}
}
