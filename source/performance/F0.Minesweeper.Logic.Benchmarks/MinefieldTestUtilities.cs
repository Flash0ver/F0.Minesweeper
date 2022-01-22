using System.Diagnostics;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Benchmarks
{
	internal static class MinefieldTestUtilities
	{
		internal static IEnumerable<Location> CreateMinefield(uint height, uint width)
		{
			uint size = width * height;
			List<Location> locations = new((int)size);

			for (uint currentWidth = 0; currentWidth < width; currentWidth++)
			{
				for (uint currentHeight = 0; currentHeight < height; currentHeight++)
				{
					locations.Add(new Location(currentWidth, currentHeight));
				}
			}

			Debug.Assert(locations.Count == size);
			return locations;
		}
	}
}
