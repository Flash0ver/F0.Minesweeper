using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal class FirstEmptyMinelayer : IMinelayer
	{
		IEnumerable<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> RemoveClickedLocationArea(possibleLocations.ToList(), clickedLocation)
				.OrderBy(_ => Guid.NewGuid())
				.Take((int)mineCount);

		private static IEnumerable<Location> RemoveClickedLocationArea(IList<Location> locations, Location clickLocation)
		{
			IEnumerable<Location> locationsToDelete = Utilities.GetLocationsAreaAroundLocation(locations, clickLocation, false);
			foreach (var locationToDelete in locationsToDelete)
			{
				locations.Remove(locationToDelete);
			}
			return locations;
		}
	}
}
