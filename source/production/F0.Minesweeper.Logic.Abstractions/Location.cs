namespace F0.Minesweeper.Logic.Abstractions
{
	public record Location
	{
		public uint X { get; init; }
		public uint Y { get; init; }

		public Location(uint x, uint y)
		{
			X = x;
			Y = y;
		}

		public Location() : this(0u, 0u) { }
	}
}
