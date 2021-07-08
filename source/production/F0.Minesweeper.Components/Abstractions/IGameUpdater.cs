using F0.Minesweeper.Components.Logic.Game;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Components.Abstractions
{
	internal interface IGameUpdateFactory
	{
		GameUpdater On(GameStatus gameStatus);
	}
}