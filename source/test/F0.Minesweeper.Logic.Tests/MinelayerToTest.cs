using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;

namespace F0.Minesweeper.Logic.Tests
{
	public class MinelayerToTest : IMinelayer
	{
		private readonly IReadOnlyCollection<Location> locationsToPutMines;

		public MinelayerToTest(IReadOnlyCollection<Location> locationsToPutMines)
			=> this.locationsToPutMines = locationsToPutMines;

		public MinelayerToTest(params Location[] locationsToPutMines)
			=> this.locationsToPutMines = locationsToPutMines;

		IReadOnlyCollection<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> locationsToPutMines;
		Dictionary<Location, Cell> IMinelayer.PlaceMinesAlternate(Dictionary<Location, Cell> allLocations, Location clickedLocation, uint mineCount, uint width, uint height) => throw new NotImplementedException();
	}
}
