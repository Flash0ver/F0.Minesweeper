using System;
using System.Collections.Generic;
using System.ComponentModel;
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

		private IEnumerable<Location> GetAllLocations() => minefield.Select(m => m.Location);

		internal Minefield(uint width, uint height, uint mineCount, MinefieldFirstUncoverBehavior generationOptions) : this(
			width,
			height,
			mineCount,
			generationOptions switch
			{
				MinefieldFirstUncoverBehavior.MayYieldMine => new RandomMinelayer(),
				MinefieldFirstUncoverBehavior.CannotYieldMine => new SafeMinelayer(),
				MinefieldFirstUncoverBehavior.WithoutAdjacentMines => new FirstEmptyMinelayer(),
				MinefieldFirstUncoverBehavior.AlwaysYieldsMine => new ImpossibleMinelayer(),
				_ => throw new InvalidEnumArgumentException($"Enumeration {generationOptions.GetType().Name}.{generationOptions} not implemented."),
			})
		{ }

		internal Minefield(MinefieldOptions minefieldOptions) : this(
			minefieldOptions.Width,
			minefieldOptions.Height,
			minefieldOptions.MineCount,
			minefieldOptions.GenerationOption)
		{ }

		internal Minefield(uint width, uint height, uint mineCount, IMinelayer mineplacer)
		{
			this.width = width;
			this.height = height;
			this.mineCount = mineCount;
			this.mineplacer = mineplacer;
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

			if(!GetAllLocations().Contains(location))
			{
				throw new ArgumentException("Invalid Location for minefield.");
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

			IReadOnlyCollection<Location> mineLocations = mineplacer.PlaceMines(allLocations, mineCount, clickedLocation);

			foreach (var minefieldCell in minefield)
			{
				if (mineLocations.Contains(minefieldCell.Location))
				{
					minefieldCell.IsMine = true;
					continue;
				}

				var locationsArea = Utilities.GetLocationsAreaAroundLocation(allLocations, minefieldCell.Location, true);

				byte countAdjactentMines = (byte)mineLocations
					.Intersect(locationsArea)
					.Count();

				minefieldCell.AdjacentMineCount = countAdjactentMines;
			}
		}
	}
}
