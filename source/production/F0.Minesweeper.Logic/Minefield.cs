using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;

namespace F0.Minesweeper.Logic
{
	internal sealed class Minefield : IMinefield
	{
		private readonly uint width, height, mineCount;
		private readonly IMinelayer minelayer;
		private bool isFirstUncover = true;
		private Dictionary<Location, Cell> minefield;
		private IEnumerable<Location> GetAllLocations() => minefield.Keys;

		internal Minefield(uint width, uint height, uint mineCount, IMinelayer minelayer)
		{
			if (mineCount > Int32.MaxValue)
			{
				throw new ArgumentOutOfRangeException(nameof(mineCount), mineCount, "Minecount should not be greater than int.MaxValue.");
			}

			this.width = width;
			this.height = height;
			this.mineCount = mineCount;
			this.minelayer = minelayer;
			minefield = new Dictionary<Location, Cell>();
		}

		public IGameUpdateReport Uncover(uint x, uint y) => Uncover(new Location(x, y));
		public IGameUpdateReport Uncover(Location location)
		{
			if (isFirstUncover)
			{
				GenerateMinefieldAlternate(location);
				isFirstUncover = false;
			}

			if (!GetAllLocations().Contains(location))
			{
				throw new ArgumentException("Invalid Location for minefield.");
			}

			List<Cell> allUncoveredCells = UncoverCells(location);

			GameStatus gameStatus = GetGameStatus();

			if (gameStatus != GameStatus.InProgress)
			{
				return new GameUpdateReport(gameStatus, minefield.Values.ToArray());
			}

			return new GameUpdateReport(gameStatus, allUncoveredCells);
		}

		private GameStatus GetGameStatus()
		{
			if (minefield.Any(cell => cell.Value.IsMine && cell.Value.Uncovered))
			{
				return GameStatus.IsLost;
			}

			if (minefield
				.Where(cell => !cell.Value.IsMine)
				.All(nonMineCell => nonMineCell.Value.Uncovered))
			{
				return GameStatus.IsWon;
			}

			return GameStatus.InProgress;
		}

		private List<Cell> UncoverCells(Location location)
		{
			List<Cell> returnCellList = new();
			Cell uncoveredCell = minefield[location];

			if (uncoveredCell.Uncovered)
			{
				return returnCellList;
			}

			uncoveredCell.Uncovered = true;
			returnCellList.Add(uncoveredCell);

			if (uncoveredCell.IsMine || uncoveredCell.AdjacentMineCount > 0)
			{
				return returnCellList;
			}

			IEnumerable<Location> locationsAroundCell = Utilities.GetLocationsAreaAroundLocation(GetAllLocations(), location, true);

			foreach (Location l in locationsAroundCell)
			{
				returnCellList.AddRange(UncoverCells(l));
			}

			return returnCellList;
		}

		private void GenerateMinefield(Location clickedLocation)
		{
			List<Location> allLocations = new();
			for (uint x = 0; x < width; x++)
			{
				for (uint y = 0; y < height; y++)
				{
					allLocations.Add(new Location(x, y));
					minefield.Add(new Location(x, y), new Cell(new Location(x, y), false, 0, false));
				}
			}

			IReadOnlyCollection<Location> mineLocations = minelayer.PlaceMines(allLocations, mineCount, clickedLocation);

			foreach (Cell minefieldCell in minefield.Values)
			{
				if (mineLocations.Contains(minefieldCell.Location))
				{
					minefieldCell.IsMine = true;
					continue;
				}

				IEnumerable<Location> locationsArea = Utilities.GetLocationsAreaAroundLocation(allLocations, minefieldCell.Location, true);

				byte countAdjacentMines = (byte)mineLocations
					.Intersect(locationsArea)
					.Count();

				minefieldCell.AdjacentMineCount = countAdjacentMines;
			}
		}

		private void GenerateMinefieldAlternate(Location clickedLocation)
		{
			Dictionary<Location, Cell> allLocations = minelayer.PlaceMinesAlternate(width, height, mineCount, clickedLocation);

			for (uint x = 0; x < width; x++)
			{
				for (uint y = 0; y < height; y++)
				{
					Location location = new(x, y);

					if(!allLocations.ContainsKey(location))
					{
						IEnumerable<Cell> locationsArea = Utilities.GetLocationsAreaAroundLocation(allLocations, location, true);

						byte countAdjacentMines = (byte)locationsArea.Count(cell => cell.IsMine);

						allLocations[location] = new Cell(location, false, countAdjacentMines, false);
					}
				}
			}

			minefield = allLocations;
		}
	}
}
