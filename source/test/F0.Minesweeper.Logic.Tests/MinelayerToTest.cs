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

		Dictionary<Location, Cell> IMinelayer.PlaceMinesAlternate(uint width, uint height, uint mineCount, Location clickedLocation) => throw new NotImplementedException();
		IReadOnlyCollection<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> locationsToPutMines;
	}
}
