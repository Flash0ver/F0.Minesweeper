using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Components.Abstractions
{
	public sealed record Location(uint X, uint Y) : ILocation
	{
	}
}
