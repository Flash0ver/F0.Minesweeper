using System;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Tests
{
	internal class RandomToTest : IRandom
	{
		private int count;
		private readonly int[] nextNumbers;
		private readonly bool shouldRepeat;

		public RandomToTest(int[] nextNumbers, bool shouldRepeat = false)
		{
			this.nextNumbers = nextNumbers;
			this.shouldRepeat = shouldRepeat;
		}

		public int Next() => Next(int.MaxValue);

		public int Next(int maxValue)
		{
			int nextToReturn;
			if (shouldRepeat)
			{
				nextToReturn = nextNumbers[count % nextNumbers.Length];
			}
			else
			{
				if (count >= nextNumbers.Length)
				{
					throw new InvalidOperationException($"Tried to get more {nameof(Next)} numbers than this instance is initialized with.");
				}

				nextToReturn = nextNumbers[count];
			}

			if (nextToReturn >= maxValue)
			{
				throw new InvalidOperationException($"{nameof(Next)} number is bigger then the maximum allowed.");
			}

			count++;
			return nextToReturn;
		}
	}
}
