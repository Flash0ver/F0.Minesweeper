namespace F0.Minesweeper.Components.Abstractions
{
	internal record UncoverableCell(ICell Cell, bool IsMine, byte AdjacentMineCount);
}