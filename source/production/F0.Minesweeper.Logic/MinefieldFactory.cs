using System.ComponentModel;
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
			ArgumentNullException.ThrowIfNull(options);

			ILocationShuffler locationShuffler;

			if (LocationShufflers.TryGetValue(options.LocationShuffler, out ILocationShuffler? shuffler))
			{
				locationShuffler = shuffler;
			}
			else
			{
				locationShuffler = options.LocationShuffler switch
				{
					Abstractions.LocationShuffler.GloballyUniqueIdentifier => new GuidLocationShuffler(),
					Abstractions.LocationShuffler.RandomOrder => new RandomOrderLocationShuffler(),
					Abstractions.LocationShuffler.FisherYates => new FisherYatesLocationShuffler(),
					_ => throw new InvalidEnumArgumentException(nameof(options.LocationShuffler), (int)options.LocationShuffler, typeof(Abstractions.LocationShuffler)),
				};

				LocationShufflers.Add(options.LocationShuffler, locationShuffler);
			}

			IMinelayer minelayer = options.GenerationOption switch
			{
				MinefieldFirstUncoverBehavior.MayYieldMine => new RandomMinelayer(locationShuffler),
				MinefieldFirstUncoverBehavior.CannotYieldMine => new SafeMinelayer(locationShuffler),
				MinefieldFirstUncoverBehavior.WithoutAdjacentMines => new FirstEmptyMinelayer(locationShuffler),
				MinefieldFirstUncoverBehavior.AlwaysYieldsMine => new ImpossibleMinelayer(),
				_ => throw new InvalidEnumArgumentException(nameof(options.GenerationOption), (int)options.GenerationOption, typeof(MinefieldFirstUncoverBehavior)),
			};

			return new Minefield(options.Width, options.Height, options.MineCount, minelayer);
		}
	}
}
