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

		protected override void OnParametersSet()
		{
			if (Location == null) 
			{
				throw new ArgumentNullException(nameof(Location));
			}
		}

		private Task OnClickAsync()
		{
			Console.WriteLine($"X: {Location.X} Y: {Location.Y}");
			return Task.CompletedTask;
		}
	}
}
