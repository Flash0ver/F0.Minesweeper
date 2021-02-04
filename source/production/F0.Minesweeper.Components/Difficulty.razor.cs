using System.Threading.Tasks;
using F0.Minesweeper.Components.Abstractions.Enums;
using Microsoft.AspNetCore.Components;

namespace F0.Minesweeper.Components
{
	public partial class Difficulty
	{
		private DifficultyLevel selected;

		[Parameter]
		public EventCallback<DifficultyLevel> OnChange { get; set; }

		public DifficultyLevel Selected
		{
			get => selected;
			set
			{
				if (selected != value)
				{
					_ = OnChange.InvokeAsync(value);
				}

				selected = value;
			}
		}

		public Difficulty()
		{
			selected = DifficultyLevel.Medium;
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);

			if (!firstRender)
			{
				return;
			}

			await OnChange.InvokeAsync(Selected);
		}
	}
}
