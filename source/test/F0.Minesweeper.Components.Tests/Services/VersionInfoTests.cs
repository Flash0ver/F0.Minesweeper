using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using F0.Minesweeper.Components.Services;
using FluentAssertions;
using NuGet.Versioning;
using Xunit;

[assembly: AssemblyInformationalVersion($"{Information.VersionPrefix}-{Information.VersionSuffix}+{Information.SourceRevisionId}")]

namespace F0.Minesweeper.Components.Tests.Services
{
	public class VersionInfoTests
	{
		[Fact]
		public void Create_WithAssembly()
		{
			// Arrange
			SemanticVersion expectedVersion = new(1, 2, 3, Information.VersionSuffix, Information.SourceRevisionId);

			// Act
			VersionInfo version = new(Assembly.GetExecutingAssembly());
			Debug.Assert(version.HasInformation);

			// Assert
			version.HasInformation.Should().BeTrue();

			version.ProductName.Should().Be(typeof(VersionInfoTests).Assembly.GetName().Name);
			version.ProductVersion.Equals(expectedVersion, VersionComparison.VersionReleaseMetadata).Should().BeTrue();
			version.FrameworkVersion.Should().Be(RuntimeInformation.FrameworkDescription);
			version.CopyrightNotice.Should().Be("Copyright © 2021");
		}

		[Fact]
		public void Create_WithoutAssembly()
		{
			// Arrange
			// Act
			VersionInfo version = new(null);

			// Assert
			version.HasInformation.Should().BeFalse();

			version.ProductName.Should().BeNull();
			version.ProductVersion.Should().BeNull();
			version.FrameworkVersion.Should().BeNull();
			version.CopyrightNotice.Should().BeNull();
		}
	}
}

internal static class Information
{
	internal const string VersionPrefix = "1.2.3";
	internal const string VersionSuffix = "alpha";
	internal const string SourceRevisionId = "920eee8770d5afa9d2aeb4571044111e96286c86";
}
