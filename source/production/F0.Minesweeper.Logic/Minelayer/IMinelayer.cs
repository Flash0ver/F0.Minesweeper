using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal interface IMinelayer
	{
		/// <summary>
		/// A method that places mines in possible locations and
		/// that can take the first clicked location in account.
		/// </summary>
		/// <param name="possibleLocations">All possible locations where mines can be placed.</param>
		/// <param name="mineCount">The amount of mines to be placed.</param>
		/// <param name="clickedLocation">The location where the user clicked.</param>
		/// <returns>A List of locations where mines are.</returns>
		internal IEnumerable<Location> PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation);
	}
}
