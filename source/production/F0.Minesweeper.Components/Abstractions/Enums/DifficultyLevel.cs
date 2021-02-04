using System.ComponentModel.DataAnnotations;

namespace F0.Minesweeper.Components.Abstractions.Enums
{
	public enum DifficultyLevel
	{
		[Display(Name = "Easy*")]
		Easy,
		[Display(Name = "Medium*")]
		Medium,
		[Display(Name = "Hard*")]
		Hard
	}
}
