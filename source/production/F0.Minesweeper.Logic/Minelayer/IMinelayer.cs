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
		/// <returns>A collection of locations where mines are.</returns>
		internal IReadOnlyCollection<Location> PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation);
		Dictionary<Location, Cell> PlaceMinesAlternate(Dictionary<Location, Cell> allLocations, Location clickedLocation, uint mineCount, uint width, uint height);
	}
}
