using Microsoft.AspNetCore.Components;
using NuGet.Versioning;

namespace F0.Minesweeper.Components.Layout
{
	public partial class Footer
	{
		private static MarkupString Format(SemanticVersion version)
		{
			string preRelease = version.IsPrerelease ? $"-{version.Release}" : String.Empty;
			string buildMetadata = version.HasMetadata ? $"+{Commit(version.Metadata)}" : String.Empty;

			return (MarkupString)$"v{version.Major}.{version.Minor}.{version.Patch}{preRelease}{buildMetadata}";
		}

		private static string Commit(string commit) => $@"<a href=""https://github.com/Flash0ver/F0.Minesweeper/commit/{commit}"" target=""_blank"">{commit}</a>";
	}
}
