using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.LocationShuffler
{
	internal interface ILocationShuffler
	{
		internal IEnumerable<Location> ShuffleAndTake(IEnumerable<Location> allLocations, int count);
	}
}
