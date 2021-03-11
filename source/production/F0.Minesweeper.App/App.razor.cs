using System.Collections.Generic;
using System.Reflection;
using F0.Minesweeper.Components;

namespace F0.Minesweeper.App
{
	public partial class App
	{
		private static IEnumerable<Assembly> GetAdditionalAssemblies()
		{
			yield return typeof(Game).Assembly;
		}
	}
}
