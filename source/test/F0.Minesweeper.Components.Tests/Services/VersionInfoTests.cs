using System;
using System.Reflection;
using System.Runtime.InteropServices;
using F0.Minesweeper.Components.Services;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Components.Tests.Services
{
	public class VersionInfoTests
	{
		[Fact]
		public void Create_WithAssembly()
		{
			// Arrange
			VersionInfo version = new(Assembly.GetExecutingAssembly());

			// Act
			// Assert
			version.HasInformation.Should().BeTrue();

			version.ProductName.Should().Be(typeof(VersionInfoTests).Assembly.GetName().Name);
			version.ProductVersion.Should().Be(new Version(1, 0, 0).ToString());
			version.FrameworkVersion.Should().Be(RuntimeInformation.FrameworkDescription);
			version.CopyrightNotice.Should().Be("Copyright © 2021");
		}

		[Fact]
		public void Create_WithoutAssembly()
		{
			// Arrange
			VersionInfo version = new(null);

			// Act
			// Assert
			version.HasInformation.Should().BeFalse();

			version.ProductName.Should().BeNull();
			version.ProductVersion.Should().BeNull();
			version.FrameworkVersion.Should().BeNull();
			version.CopyrightNotice.Should().BeNull();
		}
	}
}
