using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Mineplacer
{
	internal class FirstEmptyMineplacer : IMineplacer
	{
		IEnumerable<ILocation> IMineplacer.PlaceMines(IEnumerable<ILocation> possibleLocations, uint mineCount, ILocation clickedLocation)
			=> RemoveLocationsAroundClickedLocation(possibleLocations.ToList(), clickedLocation)
				.OrderBy(_ => Guid.NewGuid())
				.Take((int)mineCount);

		private static IEnumerable<ILocation> RemoveLocationsAroundClickedLocation(IList<ILocation> locations, ILocation clickLocation)
		{
			List<ILocation> locationsToDelete = GetLocationsAreaAroundLocation(locations, clickLocation);
			foreach (var locationToDelete in locationsToDelete)
			{
				locations.Remove(locationToDelete);
			}
			return locations;
		}

		private static List<ILocation> GetLocationsAreaAroundLocation(IList<ILocation> allLocations, ILocation location)
		{
			var returnLocations = new List<ILocation>();
			uint x = location.X;
			uint y = location.Y;

			for (int xi = -1; xi <= 1; xi++)
			{
				for (int yi = -1; yi <= 1; yi++)
				{
					Location locationToLookFor = new Location(
						(uint)Math.Max(x + xi, 0),
						(uint)Math.Max(y + yi, 0));

					var locationInArea = allLocations.SingleOrDefault(l => (Location)l == locationToLookFor);

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
