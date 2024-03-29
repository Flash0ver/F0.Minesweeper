namespace F0.Minesweeper.Logic.Abstractions
{
	public sealed record class MinefieldOptions(uint Width, uint Height, uint MineCount, MinefieldFirstUncoverBehavior GenerationOption, LocationShuffler LocationShuffler)
	{
		public static MinefieldOptions Easy
			=> new(10, 8, 10, MinefieldFirstUncoverBehavior.WithoutAdjacentMines, LocationShuffler.FisherYates);

		public static MinefieldOptions Medium
			=> new(18, 14, 40, MinefieldFirstUncoverBehavior.CannotYieldMine, LocationShuffler.FisherYates);

		public static MinefieldOptions Hard
			=> new(24, 20, 99, MinefieldFirstUncoverBehavior.MayYieldMine, LocationShuffler.FisherYates);
	}
}
