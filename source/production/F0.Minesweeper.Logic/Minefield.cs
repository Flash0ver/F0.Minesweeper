using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.Minelayer;

namespace F0.Minesweeper.Logic
{
	[DebuggerDisplay("{" + nameof(DebugVisualization) + ", nq}")]
	internal sealed class Minefield : IMinefield
	{
		private readonly uint width, height, mineCount;
		private readonly IMinelayer mineplacer;
		private bool isFirstUncover = true;
		private readonly bool[,] minefield;
		private string DebugVisualization
		{
			get
			{
				var sb = new StringBuilder();
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
			minefield = new bool[width, height];
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
