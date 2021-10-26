using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.LocationShuffler
{
	internal class FisherYatesLocationShuffler : ILocationShuffler
	{
		private readonly IRandom randomNumberGenerator;

		internal FisherYatesLocationShuffler(IRandom? randomNumberGenerator = null)
			=> this.randomNumberGenerator = randomNumberGenerator ?? new DefaultRandom();

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
