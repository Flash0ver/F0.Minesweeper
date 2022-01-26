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

		[ParamsSource(nameof(Parameters))]
		public Param Parameter { get; set; } = null!;

		[GlobalSetup]
		public void GlobalSetup()
		{
			uint size = Parameter.Width * Parameter.Height;
			List<Location> locations = new((int)size);

			for (uint width = 0; width < Parameter.Width; width++)
			{
				for (uint height = 0; height < Parameter.Height; height++)
				{
					locations.Add(new Location(width, height));
				}
			}

			Debug.Assert(locations.Count == size);
			minefield = locations;
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

		public static IEnumerable<Param> Parameters()
		{
			MinefieldOptions[] collection =
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
