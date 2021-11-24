using System;
using System.Diagnostics.CodeAnalysis;

namespace F0.Minesweeper.Logic.Abstractions
{
	[SuppressMessage("Security", "CA5394:Do not use insecure randomness", Justification = "This Random object is not being used for cryptography.")]
	public sealed class DefaultRandom : IRandom
	{
		private readonly Random random;
		public DefaultRandom() => random = new Random();

		public int Next() => random.Next();
		public int Next(int maxValue) => random.Next(maxValue);
	}
}
