using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.LocationShuffler
{
	internal class GuidLocationShuffler : ILocationShuffler
	{
		private static ILocationShuffler? instance;

		private GuidLocationShuffler() { }

		internal static ILocationShuffler Instance
		{
			get
			{
				instance ??= new GuidLocationShuffler();
				return instance;
			}
		}

		IReadOnlyCollection<Location> ILocationShuffler.ShuffleAndTake(IEnumerable<Location> allLocations, int count)
		{
			_ = allLocations ?? throw new ArgumentNullException(nameof(allLocations));

			var allLocationsCount = allLocations.Count();
			if (count > allLocationsCount)
			{
				throw new ArgumentOutOfRangeException(nameof(count), count, $"The take count should not be greater than the number of elements in {nameof(allLocations)}. Element count: {allLocationsCount}");
			}

			return allLocations
				  .OrderBy(_ => Guid.NewGuid())
				  .Take(count)
				  .ToArray();
		}
	}
}
