using System;
using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;
using F0.Minesweeper.Logic.LocationShuffler;
using F0.Minesweeper.Logic.Minelayer;

namespace F0.Minesweeper.Logic
{
	public class MinefieldFactory : IMinefieldFactory
	{
		private Dictionary<Abstractions.LocationShuffler, ILocationShuffler> LocationShufflers { get; }
			= new Dictionary<Abstractions.LocationShuffler, ILocationShuffler>();

		public IMinefield Create(MinefieldOptions options)
		{
			ILocationShuffler locationShuffler;

			if (LocationShufflers.ContainsKey(options.LocationShuffler))
			{
				locationShuffler = LocationShufflers[options.LocationShuffler];
			}
			else
			{
				locationShuffler = options.LocationShuffler switch
				{
					Abstractions.LocationShuffler.GuidLocationShuffler => new GuidLocationShuffler(),
					_ => throw new ArgumentOutOfRangeException(nameof(options.LocationShuffler), $"Not expected LocationShuffler value: {options.LocationShuffler}"),
				};

				LocationShufflers.Add(options.LocationShuffler, locationShuffler);
			}

			IMinelayer minelayer = options.GenerationOption switch
			{
				MinefieldFirstUncoverBehavior.MayYieldMine => new RandomMinelayer(locationShuffler),
				MinefieldFirstUncoverBehavior.CannotYieldMine => new SafeMinelayer(locationShuffler),
				MinefieldFirstUncoverBehavior.WithoutAdjacentMines => new FirstEmptyMinelayer(locationShuffler),
				MinefieldFirstUncoverBehavior.AlwaysYieldsMine => new ImpossibleMinelayer(),
				_ => throw new ArgumentOutOfRangeException(nameof(options.GenerationOption), $"Not expected GenerationOption value: {options.GenerationOption}"),
			};

			return new Minefield(options.Width, options.Height, options.MineCount, minelayer);
		}
	}
}
