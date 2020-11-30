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
			List<Location> locationsToDelete = Utilities.GetLocationsAreaAroundLocation(locations, clickLocation);
			foreach (var locationToDelete in locationsToDelete)
			{
				locations.Remove(locationToDelete);
			}
			return locations;
		}
	}
}
