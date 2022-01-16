using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;

namespace F0.Minesweeper.Logic
{
	internal sealed class Minefield : IMinefield
	{
		private readonly uint width, height, mineCount;
		private readonly IMinelayer minelayer;
		private bool isFirstUncover = true;
		private readonly List<Cell> minefield;
		private IEnumerable<Location> GetAllLocations() => minefield.Select(m => m.Location);

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
			minefield = new List<Cell>();
		}

		public IGameUpdateReport Uncover(uint x, uint y) => Uncover(new Location(x, y));
		public IGameUpdateReport Uncover(Location location)
		{
			if (isFirstUncover)
			{
				GenerateMinefield(location);
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
				return new GameUpdateReport(gameStatus, minefield.ToArray());
			}

			return new GameUpdateReport(gameStatus, allUncoveredCells);
		}

		private GameStatus GetGameStatus()
		{
			if (minefield.Any(cell => cell.IsMine && cell.Uncovered))
			{
				return GameStatus.IsLost;
			}

			if (minefield
				.Where(cell => !cell.IsMine)
				.All(nonMineCell => nonMineCell.Uncovered))
			{
				return GameStatus.IsWon;
			}

			return GameStatus.InProgress;
		}

		private List<Cell> UncoverCells(Location location)
		{
			List<Cell> returnCellList = new();
			Cell uncoveredCell = minefield.Single(cell => cell.Location == location);

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
					minefield.Add(new Cell(new Location(x, y), false, 0, false));
				}
			}

			IReadOnlyCollection<Location> mineLocations = minelayer.PlaceMines(allLocations, mineCount, clickedLocation);

			foreach (Cell minefieldCell in minefield)
			{
				if (mineLocations.Contains(minefieldCell.Location))
				{
					minefieldCell.IsMine = true;
					continue;
				}

				IEnumerable<Location> locationsArea = Utilities.GetLocationsAreaAroundLocation(allLocations, minefieldCell.Location, true);

				byte countAdjactentMines = (byte)mineLocations
					.Intersect(locationsArea)
					.Count();

				minefieldCell.AdjacentMineCount = countAdjactentMines;
			}
		}
	}
}
