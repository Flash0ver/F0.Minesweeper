namespace F0.Minesweeper.Logic.Abstractions
{
	public interface IRandom
	{
		public int Next();
		public int Next(int maxValue);
	}
}
