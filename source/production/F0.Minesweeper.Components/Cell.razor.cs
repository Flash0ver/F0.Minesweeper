using System;
using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions;
using Microsoft.AspNetCore.Components;

namespace F0.Minesweeper.Components
{
	public partial class Cell
	{
		[Parameter]
		public Location Location { get; set; }
		private string StatusText {
			get => statusText;
			set
			{
				if(statusText == value)
				{
					return;
				}

				statusText = value;
			}
		}

		private string statusText;

		public Cell()
		{
			StatusText = "Covered";
		}

		protected override void OnParametersSet()
		{
			if (Location == null) 
			{
				throw new ArgumentNullException(nameof(Location));
			}
		}

		private Task OnClickAsync()
		{
			StatusText = "Uncovered";
			return Task.CompletedTask;
		}
	}
}
