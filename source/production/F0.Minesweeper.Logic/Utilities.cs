using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	internal static class Utilities
	{
		internal static IEnumerable<Location> GetLocationsAreaAroundLocation(IEnumerable<Location> allLocations, Location location, bool exludeGivenLocation)
		{
			var locationArea = new HashSet<Location>();
			uint x = location.X;
			uint y = location.Y;

			for (int xi = -1; xi <= 1; xi++)
			{
				for (int yi = -1; yi <= 1; yi++)
				{
					Location locationToLookFor = new(
						(uint)Math.Max(x + xi, 0),
						(uint)Math.Max(y + yi, 0)
					);

					if (exludeGivenLocation
						&& locationToLookFor == location)
					{
						continue;
					}

					Location? locationInArea = allLocations.SingleOrDefault(l => l == locationToLookFor);

					if (locationInArea is not null)
					{
						locationArea.Add(locationInArea);
					}
				}
			}

			return locationArea;
		}

		internal static IEnumerable<Cell> GetLocationsAreaAroundLocation(Dictionary<Location, Cell> allLocations, Location location, bool exludeGivenLocation, uint width, uint height)
		{
			var locationArea = new List<Cell>();
			uint x = location.X;
			uint y = location.Y;

			for (uint xi = (uint)Math.Max((int)x - 1, 0); xi <= (uint)Math.Min((int)x + 1, width - 1); xi++)
			{
				for (uint yi = (uint)Math.Max((int)y - 1, 0); yi <= (uint)Math.Min((int)y + 1, height - 1); yi++)
				{
					Location locationToLookFor = new((uint)xi, (uint)yi);

					if (exludeGivenLocation
						&& locationToLookFor == location)
					{
						continue;
					}

					locationArea.Add(allLocations[locationToLookFor]);
				}
			}

			return locationArea;
		}

		internal static IEnumerable<Location> GetLocationsAreaAroundLocationOptimised(IEnumerable<Location> allLocations, Location location, bool exludeGivenLocation)
		{
			var locationArea = new HashSet<Location>();
			uint x = location.X;
			uint y = location.Y;

			for (int xi = -1; xi <= 1; xi++)
			{
				for (int yi = -1; yi <= 1; yi++)
				{
					Location locationToLookFor = new(
						(uint)Math.Max(x + xi, 0),
						(uint)Math.Max(y + yi, 0)
					);

					if (exludeGivenLocation
						&& locationToLookFor == location)
					{
						continue;
					}

					Location? locationInArea = allLocations.SingleOrDefault(l => l == locationToLookFor);

					if (locationInArea is not null)
					{
						locationArea.Add(locationInArea);
					}
				}
			}

			return locationArea;
		}
	}
}
