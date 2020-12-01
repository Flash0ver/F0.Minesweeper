using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	static class Utilities
	{
		internal static IEnumerable<Location> GetLocationsAreaAroundLocation(IEnumerable<Location> allLocations, Location location, bool exludeGivenLocation)
		{
			var returnLocations = new List<Location>();
			uint x = location.X;
			uint y = location.Y;

			for (int xi = -1; xi <= 1; xi++)
			{
				for (int yi = -1; yi <= 1; yi++)
				{
					if (exludeGivenLocation
						&& xi == 0 && yi == 0)
					{
						continue;
					}

					var locationToLookFor = new Location(
						(uint)Math.Max(x + xi, 0),
						(uint)Math.Max(y + yi, 0));

					var locationInArea = allLocations.SingleOrDefault(l => l == locationToLookFor);

					if (locationInArea != null)
					{
						returnLocations.Add(locationInArea);
					}
				}
			}

			return returnLocations.Distinct();
		}
	}
}
