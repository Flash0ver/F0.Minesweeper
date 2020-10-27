using System;

namespace F0.Minesweeper.Logic.Abstractions
{
	public sealed record MinefieldOptions(uint Width, uint Height, uint MineCount)
	{
		public static MinefieldOptions Easy
			=> new MinefieldOptions(10, 8, 10);

		public static MinefieldOptions Medium
			=> new MinefieldOptions(18, 14, 40);

		public static MinefieldOptions Hard
			=> new MinefieldOptions(24, 20, 99);
	}
}
