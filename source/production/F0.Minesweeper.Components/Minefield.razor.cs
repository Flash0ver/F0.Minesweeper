using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using Microsoft.AspNetCore.Components;

namespace F0.Minesweeper.Components
{
	public partial class Minefield
	{
		[Parameter]
		public MinefieldSize Size { get; set; }

		private bool isValidSize;

		protected override void OnParametersSet()
		{
			isValidSize = Size != null && Size.Height > 0 && Size.Width > 0;
		}
	}
}
