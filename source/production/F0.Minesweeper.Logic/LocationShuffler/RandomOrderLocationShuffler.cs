using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.LocationShuffler
{
	internal class RandomOrderLocationShuffler : ILocationShuffler
	{
		private readonly IRandom randomNumberGenerator;

		internal RandomOrderLocationShuffler(IRandom? randomNumberGenerator = null)
			=> this.randomNumberGenerator = randomNumberGenerator ?? new DefaultRandom();

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
