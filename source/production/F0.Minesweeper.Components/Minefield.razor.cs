using F0.Minesweeper.Components.Abstractions;
using F0.Minesweeper.Components.Abstractions.Enums;
using Microsoft.AspNetCore.Components;

namespace F0.Minesweeper.Components
{
	public partial class Minefield
	{
		[Parameter]
		public MinefieldSize Size { get; set; }

		public DifficultyLevel Difficulty { get; set; }

		private bool isValidSize;

		public Minefield()
		{
			Size = new MinefieldSize(0, 0);
		}

		protected override void OnParametersSet()
		{
			isValidSize = Size.Height > 0 && Size.Width > 0;
		}
	}
}
