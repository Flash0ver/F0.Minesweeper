using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace F0.Minesweeper.Components.Services
{
	public sealed class VersionInfo
	{
		public VersionInfo(Assembly? assembly)
		{
			if (assembly is not null)
			{
				ProductName = assembly.GetCustomAttribute<AssemblyTitleAttribute>()?.Title;
				ProductVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
				FrameworkVersion = RuntimeInformation.FrameworkDescription;
				CopyrightNotice = "Copyright © 2021";
			}
		}

		[MemberNotNullWhen(true, nameof(ProductName))]
		[MemberNotNullWhen(true, nameof(ProductVersion))]
		[MemberNotNullWhen(true, nameof(FrameworkVersion))]
		[MemberNotNullWhen(true, nameof(CopyrightNotice))]
		public bool HasInformation => ProductName is not null && ProductVersion is not null;

		public string? ProductName { get; }
		public string? ProductVersion { get; }
		public string? FrameworkVersion { get; }
		public string? CopyrightNotice { get; }
	}
}
