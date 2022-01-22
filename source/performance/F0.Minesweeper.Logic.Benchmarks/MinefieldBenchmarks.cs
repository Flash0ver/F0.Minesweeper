using System.Diagnostics;
using System.Security.Cryptography;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;
using F0.Minesweeper.Logic.Minelayer;

namespace F0.Minesweeper.Logic.Benchmarks
{
	public partial class MinefieldBenchmarks
	{
		private Minefield minefield = null!;
		private Minefield uncoveredMinefield = null!;
		private Location secondUncover = null!;

		[ParamsSource(nameof(Parameters))]
		public Param Parameter { get; set; } = null!;

		[GlobalSetup]
		public void GlobalSetup()
		{
			IEnumerable<Location> cells = MinefieldTestUtilities.CreateMinefield(Parameter.Height, Parameter.Width);
			IMinelayer minelayer = new SafeMinelayer(new FisherYatesLocationShuffler());
			minefield = new(Parameter.Width, Parameter.Height, Parameter.MineCount, minelayer);
			uncoveredMinefield = new(Parameter.Width, Parameter.Height, Parameter.MineCount, minelayer);
			IGameUpdateReport report = uncoveredMinefield.Uncover(Parameter.FirstUncover);
			List<Location>  uncoverableCells = cells.Except(report.Cells.Select(cell => cell.Location)).ToList();
			secondUncover = uncoverableCells.ElementAt(RandomNumberGenerator.GetInt32(0, uncoverableCells.Count - 1));
		}

		[Benchmark]
		public IGameUpdateReport FirstUncover()
		{
			return minefield.Uncover(Parameter.FirstUncover);
		}

		[Benchmark]
		public IGameUpdateReport SecondUncover()
		{
			return uncoveredMinefield.Uncover(secondUncover);
		}

		public static IEnumerable<Param> Parameters()
		{
			MinefieldOptions[] collection = new[]
			{
				MinefieldOptions.Easy,
				MinefieldOptions.Medium,
				MinefieldOptions.Hard,
			};

			foreach (MinefieldOptions item in collection)
			{
				uint clickedX = (uint)RandomNumberGenerator.GetInt32(0, (int)item.Width);
				uint clickedY = (uint)RandomNumberGenerator.GetInt32(0, (int)item.Height);

				yield return new Param(item.Width, item.Height, item.MineCount, new(clickedX, clickedY));
			}
		}

		public record Param(uint Width, uint Height, uint MineCount, Location FirstUncover)
		{
			public override string ToString()
				=> $"X:{Width:D2}, Y:{Height:D2}, Mines:{MineCount:D2}";
		}
	}
}
