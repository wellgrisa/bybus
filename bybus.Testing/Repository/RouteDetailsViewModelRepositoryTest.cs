using System;
using System.Linq;
using NUnit.Framework;
using bybus.Models;
using Moq;
using bybus.Services;
using System.Collections.Generic;
using bybus.ViewModels;

namespace bybus.Testing
{
	[TestFixture]
	public class RouteDetailsViewModelRepositoryTest
	{
		private Route _route;
		private IList<Departure> _departures;
		private IList<Stop> _stops;
		private Mock<IRouteRepository> routeRepositoryMock = new Mock<IRouteRepository> ();
		private const int DeparturesQuantity = 5;
		private const int StopsQuantity = 5;

		[SetUp]
		public void Setup()
		{
			SetUpRoute();

			SetUpDepartures ();

			SetUpStops();
		}

		private void SetUpRoute()
		{
			_route = new Route 
			{
				AgencyId = 1,
				ShortName = "Short Name"
			};
		}

		private void SetUpDepartures ()
		{
			_departures = new List<Departure> ();
			for (int i = 0; i < DeparturesQuantity; i++) {
				_departures.Add (new Departure { Calendar = Departure.Weekday, Time = string.Format ("0{0}:00", i) });
				_departures.Add (new Departure { Calendar = Departure.Saturday,Time = string.Format ("0{0}:00", i) });
				_departures.Add (new Departure { Calendar = Departure.Sunday, Time = string.Format ("0{0}:00", i) });
			}

			routeRepositoryMock.Setup (x => x.SearchDeparturesByRouteId(It.IsAny<int> ())).ReturnsAsync (_departures);
		}

		private void SetUpStops()
		{
			_stops = new List<Stop> ();
			for (int i = 0; i < StopsQuantity; i++) {
				_stops.Add (new Stop { Name = string.Format("Stop {0}", i) });
			}

			routeRepositoryMock.Setup (x => x.SearchStopsByRouteId(It.IsAny<int> ())).ReturnsAsync (_stops);
		}

		[Test]
		public void GivenARouteForViewModelTitleShouldBeAgencyFollowedByShortName()
		{			
			var routeDetailsViewModel = new RouteDetailsViewModel (routeRepositoryMock.Object, _route);

			Assert.AreEqual (_route.ToString(), routeDetailsViewModel.Title);				
		}

		[Test]
		public void GivenADeparturesListShouldBeSlipBetweenWeekendAndWeekday()
		{
			var routeDetailsViewModel = new RouteDetailsViewModel (routeRepositoryMock.Object, _route);
			Assert.AreEqual (routeDetailsViewModel.WeekdayDepartures.Count, 
				_departures.Where(x => x.Calendar == Departure.Weekday).Count());
			Assert.AreEqual (routeDetailsViewModel.WeekendDepartures.Count, 
				_departures.Where(x => x.Calendar != Departure.Weekday).Count());
		}

		[Test]
		public void GivenAStopsListShouldBeShowingItsQuantity()
		{
			var routeDetailsViewModel = new RouteDetailsViewModel (routeRepositoryMock.Object, _route);
			Assert.AreEqual (routeDetailsViewModel.Stops.Count, StopsQuantity);
		}
	}
}

