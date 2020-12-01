using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;

namespace F0.Minesweeper.Logic
{
	internal sealed class Minefield : IMinefield
	{
		private readonly uint width, height, mineCount;
		private readonly IMinelayer mineplacer;
		private bool isFirstUncover = true;
		private readonly List<Cell> minefield;
		private IEnumerable<Location> AllLocations => minefield.Select(m => m.Location);

		internal Minefield(uint width, uint height, uint mineCount, MinefieldFirstUncoverBehavior generationOptions)
		{
			this.width = width;
			this.height = height;
			this.mineCount = mineCount;
			mineplacer = generationOptions switch
			{
				MinefieldFirstUncoverBehavior.MayYieldMine => new RandomMinelayer(),
				MinefieldFirstUncoverBehavior.CannotYieldMine => new SafeMinelayer(),
				MinefieldFirstUncoverBehavior.WithoutAdjacentMines => new FirstEmptyMinelayer(),
				MinefieldFirstUncoverBehavior.AlwaysYieldsMine => new ImpossibleMinelayer(),
				_ => throw new NotImplementedException($"Enumeration {generationOptions.GetType().Name}.{generationOptions} not implemented."),
			};
			minefield = new List<Cell>();
		}

		internal Minefield(MinefieldOptions minefieldOptions) : this(
			minefieldOptions.Width,
			minefieldOptions.Height,
			minefieldOptions.MineCount,
			minefieldOptions.GenerationOption)
		{ }

		public IGameUpdateReport Uncover(uint x, uint y) => Uncover(new Location(x, y));
		public IGameUpdateReport Uncover(Location location)
		{
			if (isFirstUncover)
			{
				GenerateMinefield(location);
				isFirstUncover = false;
			}

			List<Cell> allUncoveredCells = UncoverCells(location);

			GameStatus gameStatus = GetGameStatus();

			return new GameUpdateReport(gameStatus, allUncoveredCells.ToArray());
		}

		private GameStatus GetGameStatus()
		{
			if (minefield.Any(cell => cell.IsMine && cell.Uncovered))
			{
				return GameStatus.IsLost;
			}
			if (minefield.All(cell => !cell.IsMine && cell.Uncovered))
			{
				return GameStatus.IsWon;
			}
			return GameStatus.InProgress;
		}

		private List<Cell> UncoverCells(Location location)
		{
			var returnCellList = new List<Cell>();
			var uncoveredCell = minefield.Single(cell => cell.Location == location);
			uncoveredCell.Uncovered = true;
			returnCellList.Add(uncoveredCell);

			if (uncoveredCell.IsMine || uncoveredCell.AdjacentMineCount > 0)
			{
				return returnCellList;
			}

			IEnumerable<Location> locationsAroundCell = Utilities.GetLocationsAreaAroundLocation(AllLocations, location, true);

			foreach (var l in locationsAroundCell)
			{
				returnCellList.AddRange(UncoverCells(l));
			}

			return returnCellList;
		}

		private void GenerateMinefield(Location clickedLocation)
		{
			var allLocations = new List<Location>();
			for (uint x = 0; x < width; x++)
			{
				for (uint y = 0; y < height; y++)
				{
					allLocations.Add(new Location(x, y));
					minefield.Add(new Cell(new Location(x, y), false, 0, false));
				}
			}

			var mineLocations = mineplacer.PlaceMines(allLocations, mineCount, clickedLocation);

			foreach (var minefieldCell in minefield)
			{
				if (mineLocations.Any(l => l == minefieldCell.Location))
				{
					minefieldCell.IsMine = true;
					continue;
				}

				var locationsArea = Utilities.GetLocationsAreaAroundLocation(allLocations, minefieldCell.Location, false);

				byte countAdjactentMines = (byte)mineLocations
					.Join(locationsArea,
						mineLoc => mineLoc,
						areaLoc => areaLoc,
						(mineLoc, areaLoc) => mineLoc)
					.Count();

				minefieldCell.AdjacentMineCount = countAdjactentMines;
			}
		}
	}
}
