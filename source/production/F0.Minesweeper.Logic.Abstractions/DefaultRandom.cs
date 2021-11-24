using System;

namespace F0.Minesweeper.Logic.Abstractions
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "This Random object is not being used for cryptography.")]
	public sealed class DefaultRandom : IRandom
	{
		private readonly Random random;
		public DefaultRandom() => random = new Random();

		public int NextNumber() => random.Next();
		public int NextNumber(int maxValue) => random.Next(maxValue);
	}
}
