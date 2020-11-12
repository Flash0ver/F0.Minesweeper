using System;
using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Mineplacer;

namespace F0.Minesweeper.Logic
{
	internal sealed class Minefield : IMinefield
	{
		private readonly uint width, height, mineCount;
		private IMineplacer mineplacer;
		private bool isFirstUncover = true;
		private bool[,] minefield;
		private string DebugVisualization
		{
			get
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				for (int j = 0; j < height; j++)
				{
					for (int i = 0; i < width; i++)
					{
						sb.Append(minefield[i, j] ? "☒" : "☐");
					}
					sb.AppendLine();
				}
				return sb.ToString();
			}
		}

		internal Minefield(uint width, uint height, uint mineCount, MinefieldGenerationOptions generationOptions)
		{
			this.width = width;
			this.height = height;
			this.mineCount = mineCount;
			switch (generationOptions)
			{
				case MinefieldGenerationOptions.Random:
					mineplacer = new RandomMineplacer();
					break;
				case MinefieldGenerationOptions.Safe:
					mineplacer = new SafeMineplacer();
					break;
				case MinefieldGenerationOptions.FirstEmpty:
					mineplacer = new FirstEmptyMineplacer();
					break;
				case MinefieldGenerationOptions.Impossible:
					mineplacer = new ImpossibleMineplacer();
					break;
				default:
					throw new NotImplementedException($"Enumeration {generationOptions.GetType().Name}.{generationOptions} not implemented.");
			}
			minefield = new bool[width, height];
		}

		internal Minefield(MinefieldOptions minefieldOptions) : this(
			minefieldOptions.Difficulty.Width,
			minefieldOptions.Difficulty.Height,
			minefieldOptions.Difficulty.MineCount,
			minefieldOptions.GenerationOption)
		{ }

		public IGameUpdateReport Uncover(uint x, uint y) => Uncover(new Location(x, y));
		public IGameUpdateReport Uncover(Location location)
		{
			if (isFirstUncover)
			{
				GenerateMinefield((Location)location);
				isFirstUncover = false;
			}

			// TODO: implement real logic
			return new GameUpdateReport(GameStatus.InProgress,
				new IUncoveredCell[] {
					new UncoveredCell(new Location(location.X, location.Y), false, 1)
				});
		}

		private void GenerateMinefield(Location clickedLocation)
		{
			var allLocations = new List<Location>();
			for (uint x = 0; x < width; x++)
			{
				for (uint y = 0; y < height; y++)
				{
					allLocations.Add(new Location(x, y));
				}
			}

			var mineLocations = mineplacer.PlaceMines(allLocations, mineCount, clickedLocation);

			foreach (var mineLocation in mineLocations)
			{
				minefield[mineLocation.X, mineLocation.Y] = true;
			}
		}
	}
}
