using System.Diagnostics;
using System.Security.Cryptography;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Benchmarks
{
	public partial class UtilitiesBenchmarks
	{
		private IEnumerable<Location> minefield = null!;
		private Dictionary<Location, Cell> minefieldDict = null!;

		[ParamsSource(nameof(Parameters))]
		public Param Parameter { get; set; } = null!;

		[GlobalSetup]
		public void GlobalSetup()
		{
			uint size = Parameter.Width * Parameter.Height;
			List<Location> locations = new((int)size);
			minefieldDict = new((int)size);

			for (uint width = 0; width < Parameter.Width; width++)
			{
				for (uint height = 0; height < Parameter.Height; height++)
				{
					Location location = new(width, height);
					locations.Add(location);
					minefieldDict[location] = new Cell(location, false, 0, false);
				}
			}

			Debug.Assert(locations.Count == size);
			minefield = locations;
		}

		[Benchmark]
		public IList<Location> GetLocationsAreaAroundLocation_ExcludeGivenLocation()
		{
			return Utilities.GetLocationsAreaAroundLocation(minefield, Parameter.ClickedLocation, true).ToList();
		}

		[Benchmark]
		public IList<Location> GetLocationsAreaAroundLocation_DoNotExcludeGivenLocation()
		{
			return Utilities.GetLocationsAreaAroundLocation(minefield, Parameter.ClickedLocation, false).ToList();
		}

		[Benchmark]
		public IList<IUncoveredCell> GetLocationsAreaAroundLocationOptimised_ExcludeGivenLocation()
		{
			return Utilities.GetLocationsAreaAroundLocation(minefieldDict, Parameter.ClickedLocation, true).ToList<IUncoveredCell>();
		}

		[Benchmark]
		public IList<IUncoveredCell> GetLocationsAreaAroundLocationOptimised_DoNotExcludeGivenLocation()
		{
			return Utilities.GetLocationsAreaAroundLocation(minefieldDict, Parameter.ClickedLocation, false).ToList<IUncoveredCell>();
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
				uint clickedX = (uint)RandomNumberGenerator.GetInt32((int)item.Width);
				uint clickedY = (uint)RandomNumberGenerator.GetInt32((int)item.Height);

				yield return new Param(item.Width, item.Height, item.MineCount, new(clickedX, clickedY));
			}
		}

		public record Param(uint Width, uint Height, uint MineCount, Location ClickedLocation);
	}
}
