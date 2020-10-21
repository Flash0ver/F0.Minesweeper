using System;

namespace F0.Minesweeper.Logic.Abstractions
{
	public sealed record MinefieldOptions(uint Width, uint Height, uint MineCount)
	{
		public static MinefieldOptions Easy()
			=> throw new NotImplementedException();

		public static MinefieldOptions Medium()
			=> throw new NotImplementedException();

		public static MinefieldOptions Hard()
			=> throw new NotImplementedException();
	}
}
