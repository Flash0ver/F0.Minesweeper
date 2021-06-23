using F0.Minesweeper.Components.Abstractions.Enums;
using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Components.Abstractions
{
    public interface ICell 
    {
        Location? Location { get; set; }
		void SetUncoveredStatus(CellInteractionType cellInteraction, bool isMine, byte adjacentMineCount);
		void DisableClick();
	}
}