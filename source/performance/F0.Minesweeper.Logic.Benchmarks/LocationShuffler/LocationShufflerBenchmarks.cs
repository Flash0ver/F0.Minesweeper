using System.Diagnostics;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;

namespace F0.Minesweeper.Logic.Benchmarks.LocationShuffler
{
	public class LocationShufflerBenchmarks
	{
		private readonly ILocationShuffler globallyUniqueIdentifier = new GuidLocationShuffler();
		private readonly ILocationShuffler randomOrder = new RandomOrderLocationShuffler();
		private readonly ILocationShuffler fisherYates = new FisherYatesLocationShuffler();

		private IEnumerable<Location> minefield = null!;
		private IReadOnlyList<Location> minefieldRO = null!;

		[ParamsSource(nameof(Parameters))]
		public Param Parameter { get; set; } = null!;

		[GlobalSetup]
		public void GlobalSetup()
		{
			minefield = MinefieldTestUtilities.CreateMinefield(Parameter.Height, Parameter.Width);
			minefieldRO = MinefieldTestUtilities.CreateMinefield(Parameter.Height, Parameter.Width).ToList();
		}

		[Benchmark]
		public IReadOnlyCollection<Location> GloballyUniqueIdentifier()
			=> globallyUniqueIdentifier.ShuffleAndTake(minefield, (int)Parameter.MineCount);

		[Benchmark]
		public IReadOnlyCollection<Location> RandomOrder()
			=> randomOrder.ShuffleAndTake(minefield, (int)Parameter.MineCount);

		[Benchmark]
		public IReadOnlyCollection<Location> FisherYates()
			=> fisherYates.ShuffleAndTake(minefield, (int)Parameter.MineCount);

		//[Benchmark]
		//public IReadOnlyCollection<Location> GloballyUniqueIdentifierAlt()
		//	=> globallyUniqueIdentifier.ShuffleAndTakeAlternate(minefieldRO, (int)Parameter.MineCount).Keys;

		//[Benchmark]
		//public IReadOnlyCollection<Location> RandomOrderAlt()
		//	=> randomOrder.ShuffleAndTakeAlternate(minefieldRO, (int)Parameter.MineCount).Keys;

		//[Benchmark]
		//public IReadOnlyCollection<Location> FisherYatesAlt()
		//	=> fisherYates.ShuffleAndTakeAlternate(minefieldRO, (int)Parameter.MineCount).Keys;

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
				yield return new Param(item.Width, item.Height, item.MineCount);
			}
		}

		public record class Param(uint Width, uint Height, uint MineCount)
		{
			public override string ToString()
				=> $"X:{Width:D2}, Y:{Height:D2}, Mines:{MineCount:D2}";
		}
	}
}
