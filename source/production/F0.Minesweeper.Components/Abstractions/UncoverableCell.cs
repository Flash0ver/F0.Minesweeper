namespace F0.Minesweeper.Components.Abstractions
{
	internal record class UncoverableCell(ICell Cell, bool IsMine, byte AdjacentMineCount);
}
