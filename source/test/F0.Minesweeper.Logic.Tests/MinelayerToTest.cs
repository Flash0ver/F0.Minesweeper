using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;

namespace F0.Minesweeper.Logic.Tests
{
	public class MinelayerToTest : IMinelayer
	{
		private readonly IReadOnlyCollection<Location> locationsToPutMines;

		public MinelayerToTest(params Location[] locationsToPutMines)
			=> this.locationsToPutMines = locationsToPutMines;

		IReadOnlyCollection<Location> IMinelayer.PlaceMines(IEnumerable<Location> possibleLocations, uint mineCount, Location clickedLocation)
			=> locationsToPutMines;
	}
}
