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
						_ = locationArea.Add(locationInArea);
					}
				}
			}

			return locationArea;
		}
	}
}
