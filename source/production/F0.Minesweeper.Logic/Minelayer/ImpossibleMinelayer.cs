using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Minelayer
{
	internal class ImpossibleMinelayer : IMinelayer
	{
		public Dictionary<Location, Cell> PlaceMinesAlternate(uint width, uint height, uint mineCount, Location clickedLocation) => throw new NotImplementedException();
		IReadOnlyCollection<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> new Location[] { clickedLocation };
	}
}
