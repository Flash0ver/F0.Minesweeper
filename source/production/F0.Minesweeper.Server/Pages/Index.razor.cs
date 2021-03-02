using Microsoft.AspNetCore.Components;

namespace F0.Minesweeper.Server.Pages
{
	public partial class Index
	{
		[Inject]
		private NavigationManager? NavigationManager { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();

			NavigationManager?.NavigateTo("game");
		}
	}
}
