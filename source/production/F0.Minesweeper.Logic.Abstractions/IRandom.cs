namespace F0.Minesweeper.Logic.Abstractions
{
	public interface IRandom
	{
		public int NextNumber();
		public int NextNumber(int maxValue);
	}
}
