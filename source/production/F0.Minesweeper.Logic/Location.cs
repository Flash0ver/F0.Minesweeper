using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	public record Location : ILocation
	{
		public uint X { get; init; }
		public uint Y { get; init; }

		internal Location(uint x, uint y)
		{
			X = x;
			Y = y;
		}

		internal Location() : this(0u, 0u) { }
	}
}
