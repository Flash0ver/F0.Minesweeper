using System.Collections.Generic;
using F0.Minesweeper.Logic.Abstractions;
using FluentAssertions;
using Xunit;

namespace F0.Minesweeper.Logic.Tests
{
	public class GetLocationsAreaAroundLocationTestData
	{
		public List<Location> AllLocations { get; set; }
		public Location ClickedLocation { get; set; }
		public bool ExcludeSelf { get; set; }
		public List<Location> ExpectedResult { get; set; }

		public GetLocationsAreaAroundLocationTestData()
		{
			AllLocations = new List<Location>();
			ClickedLocation = new Location();
			ExpectedResult = new List<Location>();
		}

		public static List<Location> TestField
		{
			get
			{
				return new List<Location>{
					new Location(0, 0), new Location(1, 0), new Location(2, 0),
					new Location(0, 1), new Location(1, 1), new Location(2, 1),
					new Location(0, 2), new Location(1, 2), new Location(2, 2)
				};
			}
		}

		public static TheoryData<GetLocationsAreaAroundLocationTestData> Data =>
		new TheoryData<GetLocationsAreaAroundLocationTestData>
		{
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(0, 0),
				ExcludeSelf = false,
				ExpectedResult = new List<Location>{
					new Location(0, 0), new Location(1, 0),
					new Location(0, 1), new Location(1, 1),
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(1, 0),
				ExcludeSelf = false,
				ExpectedResult = new List<Location>{
					new Location(0, 0), new Location(1, 0), new Location(2, 0),
					new Location(0, 1), new Location(1, 1), new Location(2, 1)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(2, 0),
				ExcludeSelf = false,
				ExpectedResult = new List<Location>{
					new Location(1, 0), new Location(2, 0),
					new Location(1, 1), new Location(2, 1)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(0, 1),
				ExcludeSelf = false,
				ExpectedResult = new List<Location>{
					new Location(0, 0), new Location(1, 0),
					new Location(0, 1), new Location(1, 1),
					new Location(0, 2), new Location(1, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(1, 1),
				ExcludeSelf = false,
				ExpectedResult = new List<Location>{
					new Location(0, 0), new Location(1, 0), new Location(2, 0),
					new Location(0, 1), new Location(1, 1), new Location(2, 1),
					new Location(0, 2), new Location(1, 2), new Location(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(2, 1),
				ExcludeSelf = false,
				ExpectedResult = new List<Location>{
					new Location(1, 0), new Location(2, 0),
					new Location(1, 1), new Location(2, 1),
					new Location(1, 2), new Location(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(0, 2),
				ExcludeSelf = false,
					ExpectedResult = new List<Location>{
					new Location(0, 1), new Location(1, 1),
					new Location(0, 2), new Location(1, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(1, 2),
				ExcludeSelf = false,
				ExpectedResult = new List<Location>{
					new Location(0, 1), new Location(1, 1), new Location(2, 1),
					new Location(0, 2), new Location(1, 2), new Location(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(2, 2),
				ExcludeSelf = false,
				ExpectedResult = new List<Location>{
					new Location(1, 1), new Location(2, 1),
					new Location(1, 2), new Location(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(0, 0),
				ExcludeSelf = true,
				ExpectedResult = new List<Location>{
					new Location(1, 0),
					new Location(0, 1), new Location(1, 1),
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(1, 0),
				ExcludeSelf = true,
				ExpectedResult = new List<Location>{
					new Location(0, 0), new Location(2, 0),
					new Location(0, 1), new Location(1, 1), new Location(2, 1)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(2, 0),
				ExcludeSelf = true,
				ExpectedResult = new List<Location>{
					new Location(1, 0),
					new Location(1, 1), new Location(2, 1)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(0, 1),
				ExcludeSelf = true,
				ExpectedResult = new List<Location>{
					new Location(0, 0), new Location(1, 0),
					new Location(1, 1),
					new Location(0, 2), new Location(1, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(1, 1),
				ExcludeSelf = true,
				ExpectedResult = new List<Location>{
					new Location(0, 0), new Location(1, 0), new Location(2, 0),
					new Location(0, 1), new Location(2, 1),
					new Location(0, 2), new Location(1, 2), new Location(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(2, 1),
				ExcludeSelf = true,
				ExpectedResult = new List<Location>{
					new Location(1, 0), new Location(2, 0),
					new Location(1, 1),
					new Location(1, 2), new Location(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(0, 2),
				ExcludeSelf = true,
					ExpectedResult = new List<Location>{
					new Location(0, 1), new Location(1, 1),
					new Location(1, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(1, 2),
				ExcludeSelf = true,
				ExpectedResult = new List<Location>{
					new Location(0, 1), new Location(1, 1), new Location(2, 1),
					new Location(0, 2), new Location(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData{
				AllLocations = TestField,
				ClickedLocation = new Location(2, 2),
				ExcludeSelf = true,
				ExpectedResult = new List<Location>{
					new Location(1, 1), new Location(2, 1),
					new Location(1, 2)
				}
			}

		};
	}

	public class UtilitiesTest
	{
		[Theory]
		[MemberData(nameof(GetLocationsAreaAroundLocationTestData.Data), MemberType = typeof(GetLocationsAreaAroundLocationTestData))]
		public void GetLocationsAreaAroundLocation_LocationProvided_ReturnsArea(GetLocationsAreaAroundLocationTestData testData)
		{
			IEnumerable<Location> result = Utilities.GetLocationsAreaAroundLocation(testData.AllLocations, testData.ClickedLocation, testData.ExcludeSelf);

			result.Should().BeEquivalentTo(testData.ExpectedResult);
		}
	}
}
