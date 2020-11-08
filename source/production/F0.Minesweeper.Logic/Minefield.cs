using System;
using System.Collections.Generic;
using System.Linq;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	internal sealed class Minefield : IMinefield
	{
		private readonly uint width, height, mineCount;
		private readonly MinefieldGenerationOptions generationOptions;
		private bool isFirstUncover = true;
		private bool[,] minefield;
		private string minefieldDebugVisualization
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
			this.generationOptions = generationOptions;
			minefield = new bool[width, height];
		}

		internal Minefield(MinefieldOptions minefieldOptions)
		{
			width = minefieldOptions.Difficulty.Width;
			height = minefieldOptions.Difficulty.Height;
			mineCount = minefieldOptions.Difficulty.MineCount;
			generationOptions = minefieldOptions.GenerationOption;
			minefield = new bool[width, height];
		}

		public IGameUpdateReport Uncover(uint x, uint y) => Uncover(new Location(x, y));
		public IGameUpdateReport Uncover(ILocation location)
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

			switch (generationOptions)
			{
				case MinefieldGenerationOptions.Random:
					break;
				case MinefieldGenerationOptions.Safe:
					allLocations.Remove(clickedLocation);
					break;
				case MinefieldGenerationOptions.FirstEmpty:
					allLocations = RemoveLocationsAroundClickedLocation(allLocations, clickedLocation);
					break;
				default:
					throw new NotImplementedException($"Enumeration {generationOptions.GetType().Name}.{generationOptions} not implemented.");
			}

			var mineLocations = allLocations
				.OrderBy(_ => Guid.NewGuid())
				.Take((int)mineCount);

			foreach (var mineLocation in mineLocations)
			{
				minefield[mineLocation.X, mineLocation.Y] = true;
			}
		}

		private List<Location> RemoveLocationsAroundClickedLocation(List<Location> locations, Location clickLocation)
		{
			List<Location> locationsToDelete = GetLocationsAreaAroundLocation(clickLocation);
			foreach (var locationToDelete in locationsToDelete)
			{
				locations.Remove(locationToDelete);
			}
			return locations;
		}

		private List<Location> GetLocationsAreaAroundLocation(Location location)
		{
			var returnLocations = new List<Location>();
			int x = (int)location.X;
			int y = (int)location.Y;

			for (int xi = -1; xi <= 1; xi++)
			{
				for (int yi = -1; yi <= 1; yi++)
				{
					returnLocations.Add(new Location(
						(uint)Math.Clamp(x + xi, 0, width - 1),
						(uint)Math.Clamp(y + yi, 0, height - 1)
						));
				}
			}

			// Have to do .Distinct() because we have duplicates if click is on a border cell
			return returnLocations.Distinct().ToList();
		}
	}
}
