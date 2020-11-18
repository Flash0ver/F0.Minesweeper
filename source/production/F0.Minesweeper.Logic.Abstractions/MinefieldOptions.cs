namespace F0.Minesweeper.Logic.Abstractions
{
	public sealed record MinefieldOptions(uint Width, uint Height, uint MineCount, MinefieldFirstUncoverBehavior GenerationOption)
	{
		public static MinefieldOptions Easy
			=> new(10, 8, 10, MinefieldFirstUncoverBehavior.WithoutAdjacentMines);

		public static MinefieldOptions Medium
			=> new(18, 14, 40, MinefieldFirstUncoverBehavior.CannotYieldMine);

		public static MinefieldOptions Hard
			=> new(24, 20, 99, MinefieldFirstUncoverBehavior.MayYieldMine);
	}
}
