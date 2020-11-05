using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	internal sealed class Minefield : IMinefield
	{
		private readonly uint width, height, mineCount;

		internal Minefield(uint width, uint height, uint mineCount)
		{
			this.width = width;
			this.height = height;
			this.mineCount = mineCount;
		}

		internal Minefield(MinefieldOptions minefieldOptions)
		{
			width = minefieldOptions.Width;
			height = minefieldOptions.Height;
			mineCount = minefieldOptions.MineCount;
		}

		public IGameUpdateReport Uncover(uint x, uint y) => Uncover(new Location(x, y));
		public IGameUpdateReport Uncover(ILocation location)
		{
			// TODO: implement real logic
			return new GameUpdateReport(GameStatus.InProgress,
				new IUncoveredCell[] {
					new UncoveredCell(new Location(location.X, location.Y), false, 1)
				});
		}
	}
}
