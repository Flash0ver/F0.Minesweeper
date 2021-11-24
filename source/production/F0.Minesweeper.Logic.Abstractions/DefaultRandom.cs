using System;

namespace F0.Minesweeper.Logic.Abstractions
{
	public sealed class DefaultRandom : IRandom
	{
		private readonly Random random;
		public DefaultRandom() => random = new Random();
		public int Next() => random.Next();
		public int Next(int maxValue) => random.Next(maxValue);
	}
}
