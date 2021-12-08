using F0.Minesweeper.Logic.Abstractions;

namespace F0.Minesweeper.Logic.Tests
{
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

	public class GetLocationsAreaAroundLocationTestData
	{
		public IReadOnlyCollection<Location> AllLocations { get; init; }
		public Location ClickedLocation { get; init; }
		public bool ExcludeSelf { get; init; }
		public IReadOnlyCollection<Location> ExpectedResult { get; init; }

		public GetLocationsAreaAroundLocationTestData()
		{
			AllLocations = Array.Empty<Location>();
			ClickedLocation = new Location();
			ExpectedResult = Array.Empty<Location>();
		}

		public static IReadOnlyCollection<Location> TestField
		{
			get
			{
				return new Location[] {
					new(0, 0), new(1, 0), new(2, 0),
					new(0, 1), new(1, 1), new(2, 1),
					new(0, 2), new(1, 2), new(2, 2)
				};
			}
		}

		public static TheoryData<GetLocationsAreaAroundLocationTestData> Data =>
		new TheoryData<GetLocationsAreaAroundLocationTestData>
		{
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(0, 0),
				ExcludeSelf = false,
				ExpectedResult = new Location[] {
					new(0, 0), new(1, 0),
					new(0, 1), new(1, 1),
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(1, 0),
				ExcludeSelf = false,
				ExpectedResult = new Location[] {
					new(0, 0), new(1, 0), new(2, 0),
					new(0, 1), new(1, 1), new(2, 1)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(2, 0),
				ExcludeSelf = false,
				ExpectedResult = new Location[] {
					new(1, 0), new(2, 0),
					new(1, 1), new(2, 1)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(0, 1),
				ExcludeSelf = false,
				ExpectedResult = new Location[] {
					new(0, 0), new(1, 0),
					new(0, 1), new(1, 1),
					new(0, 2), new(1, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(1, 1),
				ExcludeSelf = false,
				ExpectedResult = new Location[] {
					new(0, 0), new(1, 0), new(2, 0),
					new(0, 1), new(1, 1), new(2, 1),
					new(0, 2), new(1, 2), new(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(2, 1),
				ExcludeSelf = false,
				ExpectedResult = new Location[] {
					new(1, 0), new(2, 0),
					new(1, 1), new(2, 1),
					new(1, 2), new(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(0, 2),
				ExcludeSelf = false,
					ExpectedResult = new Location[] {
					new(0, 1), new(1, 1),
					new(0, 2), new(1, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(1, 2),
				ExcludeSelf = false,
				ExpectedResult = new Location[] {
					new(0, 1), new(1, 1), new(2, 1),
					new(0, 2), new(1, 2), new(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(2, 2),
				ExcludeSelf = false,
				ExpectedResult = new Location[] {
					new(1, 1), new(2, 1),
					new(1, 2), new(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(0, 0),
				ExcludeSelf = true,
				ExpectedResult = new Location[] {
					new(1, 0),
					new(0, 1), new(1, 1),
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(1, 0),
				ExcludeSelf = true,
				ExpectedResult = new Location[] {
					new(0, 0), new(2, 0),
					new(0, 1), new(1, 1), new(2, 1)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(2, 0),
				ExcludeSelf = true,
				ExpectedResult = new Location[] {
					new(1, 0),
					new(1, 1), new(2, 1)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(0, 1),
				ExcludeSelf = true,
				ExpectedResult = new Location[] {
					new(0, 0), new(1, 0),
					new(1, 1),
					new(0, 2), new(1, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(1, 1),
				ExcludeSelf = true,
				ExpectedResult = new Location[] {
					new(0, 0), new(1, 0), new(2, 0),
					new(0, 1), new(2, 1),
					new(0, 2), new(1, 2), new(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(2, 1),
				ExcludeSelf = true,
				ExpectedResult = new Location[] {
					new(1, 0), new(2, 0),
					new(1, 1),
					new(1, 2), new(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(0, 2),
				ExcludeSelf = true,
					ExpectedResult = new Location[] {
					new(0, 1), new(1, 1),
					new(1, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(1, 2),
				ExcludeSelf = true,
				ExpectedResult = new Location[] {
					new(0, 1), new(1, 1), new(2, 1),
					new(0, 2), new(2, 2)
				}
			},
			new GetLocationsAreaAroundLocationTestData {
				AllLocations = TestField,
				ClickedLocation = new Location(2, 2),
				ExcludeSelf = true,
				ExpectedResult = new Location[] {
					new(1, 1), new(2, 1),
					new(1, 2)
				}
			}
		};
	}
}
