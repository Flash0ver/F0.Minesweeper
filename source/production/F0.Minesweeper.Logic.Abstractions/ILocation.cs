namespace F0.Minesweeper.Logic.Abstractions
{
	// Discuss if this really should be an interface and not just a concrete class/record
	public interface ILocation
	{
		uint X { get; }
		uint Y { get; }
	}
}
