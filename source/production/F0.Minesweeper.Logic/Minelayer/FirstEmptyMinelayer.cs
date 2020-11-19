using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal class FirstEmptyMinelayer : IMinelayer
	{
		IEnumerable<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> RemoveLocationsAroundClickedLocation(possibleLocations.ToList(), clickedLocation)
				.OrderBy(_ => Guid.NewGuid())
				.Take((int)mineCount);

		private static IEnumerable<Location> RemoveLocationsAroundClickedLocation(IList<Location> locations, Location clickLocation)
		{
			List<Location> locationsToDelete = GetLocationsAreaAroundLocation(locations, clickLocation);
			foreach (var locationToDelete in locationsToDelete)
			{
				locations.Remove(locationToDelete);
			}
			return locations;
		}

		private static List<Location> GetLocationsAreaAroundLocation(IList<Location> allLocations, Location location)
		{
			var returnLocations = new List<Location>();
			uint x = location.X;
			uint y = location.Y;

			for (int xi = -1; xi <= 1; xi++)
			{
				for (int yi = -1; yi <= 1; yi++)
				{
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

			return returnLocations;
		}
	}
}
