using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic
{
	internal sealed class Minefield : IMinefield
	{
		private readonly uint width, height, mineCount;
		private readonly MinefieldGenerationOptions generationOptions;

		internal Minefield(uint width, uint height, uint mineCount, MinefieldGenerationOptions generationOptions)
		{
			this.width = width;
			this.height = height;
			this.mineCount = mineCount;
			this.generationOptions = generationOptions;
		}

		internal Minefield(MinefieldOptions minefieldOptions)
		{
			width = minefieldOptions.Difficulty.Width;
			height = minefieldOptions.Difficulty.Height;
			mineCount = minefieldOptions.Difficulty.MineCount;
			generationOptions = minefieldOptions.GenerationOption;
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
