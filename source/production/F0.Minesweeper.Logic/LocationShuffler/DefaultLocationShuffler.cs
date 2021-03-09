using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.LocationShuffler
{
	internal class DefaultLocationShuffler : ILocationShuffler
	{
		IReadOnlyCollection<Location> ILocationShuffler.ShuffleAndTake(IEnumerable<Location> allLocations, int count) =>
			allLocations
				.OrderBy(_ => Guid.NewGuid())
				.Take(count)
				.ToArray();
	}
}
