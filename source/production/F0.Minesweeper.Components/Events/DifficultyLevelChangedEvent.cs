using F0.Minesweeper.Components.Abstractions.Enums;
using Prism.Events;

namespace F0.Minesweeper.Components.Events
{
	internal class DifficultyLevelChangedEvent : PubSubEvent<DifficultyLevel> { }
}
