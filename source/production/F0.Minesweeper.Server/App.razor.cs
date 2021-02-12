using System.Collections.Generic;
using System.Reflection;
using F0.Minesweeper.Components;

namespace F0.Minesweeper.Server
{
	public partial class App
	{
		private static IEnumerable<Assembly> GetAdditionAssemblies()
		{
			yield return typeof(Game).Assembly;
		}
	}
}
