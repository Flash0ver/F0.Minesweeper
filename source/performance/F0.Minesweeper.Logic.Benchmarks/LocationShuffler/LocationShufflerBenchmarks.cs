using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;

namespace F0.Minesweeper.Logic.Benchmarks.LocationShuffler
{
	public class LocationShufflerBenchmarks
	{
		[ParamsSource(nameof(Parameters))]
		public MinefieldOptions parameters = null!;

		private readonly ILocationShuffler globallyUniqueIdentifier = new GuidLocationShuffler();
		private readonly ILocationShuffler randomOrder = new RandomOrderLocationShuffler();
		private readonly ILocationShuffler fisherYates = new FisherYatesLocationShuffler();

		private IEnumerable<Location> minefield = Array.Empty<Location>();

		[GlobalSetup]
		public void GlobalSetup()
		{
			List<Location> locations = new((int)parameters.Width * (int)parameters.Height);

			for (uint width = 0; width < parameters.Width; width++)
			{
				for (uint height = 0; height < parameters.Height; height++)
				{
					locations.Add(new Location(width, height));
				}
			}

			minefield = locations;
		}

		[Benchmark]
		public IReadOnlyCollection<Location> GloballyUniqueIdentifier()
			=> globallyUniqueIdentifier.ShuffleAndTake(minefield, (int)parameters.MineCount);

		[Benchmark]
		public IReadOnlyCollection<Location> RandomOrder()
			=> randomOrder.ShuffleAndTake(minefield, (int)parameters.MineCount);

		[Benchmark]
		public IReadOnlyCollection<Location> FisherYates()
			=> fisherYates.ShuffleAndTake(minefield, (int)parameters.MineCount);

		public static IEnumerable<MinefieldOptions> Parameters()
			=> new[]
			{
				MinefieldOptions.Easy,
				MinefieldOptions.Medium,
				MinefieldOptions.Hard,
			};
	}
}
