namespace F0.Minesweeper.Logic.Abstractions
{
	public sealed record MinefieldDifficulty(uint Width, uint Height, uint MineCount)
	{
		public static MinefieldDifficulty Easy
			=> new MinefieldDifficulty(10, 8, 10);

		public static MinefieldDifficulty Medium
			=> new MinefieldDifficulty(18, 14, 40);

		public static MinefieldDifficulty Hard
			=> new MinefieldDifficulty(24, 20, 99);
	}
}
