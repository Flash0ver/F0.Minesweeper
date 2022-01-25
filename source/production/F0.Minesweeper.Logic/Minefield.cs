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

			// todo: add new valid location check
			//if (!GetAllLocations().Contains(location))
			//{
			//	throw new ArgumentException("Invalid Location for minefield.");
			//}

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

			if(!minefield.TryGetValue(location, out Cell? uncoveredCell))
			{
				uncoveredCell = new Cell(location, false, 0, false);
				minefield.Add(location, uncoveredCell);
			}

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

			IEnumerable<Cell> locationsAroundCell = Utilities.GetLocationsAreaAroundLocation(minefield, location, true, width, height);
			//IEnumerable<Location> locationsAroundCell = Utilities.GetLocationsAreaAroundLocation(GetAllLocations(), location, true);

			// todo: potential improvement by making adjacentminecount nullable
			// todo: and only set it when we really know what the value is.
			// todo: so in case the count is 0 we won't have to recalculate adjacent mine count
			uncoveredCell.AdjacentMineCount = (byte)locationsAroundCell.Where(s => s.IsMine).Count();

			if (uncoveredCell.AdjacentMineCount > 0)
			{
				return returnCellList;
			}

			foreach (Cell l in locationsAroundCell)
			{
				returnCellList.AddRange(UncoverCells(l.Location));
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
			minefield = minelayer.PlaceMinesAlternate(width, height, mineCount, clickedLocation);
		}
	}
}
