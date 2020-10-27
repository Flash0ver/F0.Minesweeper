using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	public class Location : ILocation
	{
		public uint X { get; private set; }
		public uint Y { get; private set; }

		internal Location(uint x, uint y)
		{
			X = x;
			Y = y;
		}
	}
}
