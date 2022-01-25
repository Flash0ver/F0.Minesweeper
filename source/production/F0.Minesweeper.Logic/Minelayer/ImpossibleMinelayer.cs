using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal class ImpossibleMinelayer : IMinelayer
	{
		public Dictionary<Location, Cell> PlaceMinesAlternate(Dictionary<Location, Cell> allLocations, Location clickedLocation, uint mineCount, uint width, uint height) => throw new NotImplementedException();
		IReadOnlyCollection<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> new Location[] { clickedLocation };
	}
}
