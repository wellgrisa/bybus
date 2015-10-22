using System;
using System.Linq;
using bybus.Models;
using Xamarin.Forms;
using Views.bybus;
using bybus.Shared.Visitor;
using System.Collections.ObjectModel;
using bybus.Services;
using PropertyChanged;

namespace bybus.ViewModels 
{
	[ImplementPropertyChanged]
	public class RouteDetailsViewModel : BaseViewModel
	{
		private readonly RouteRepository _routeRepository;

		public bool IsLoading { get; set; }

		public Route Route { get; set; }

		public ObservableCollection<Stop> Stops { get; set; }

		public ObservableCollection<Departure> WeekdayDepartures { get; set; }

		public ObservableCollection<Departure> WeekendDepartures { get; set; }

		public RouteDetailsViewModel (RouteRepository routeRepository, Route route)
		{
			_routeRepository = routeRepository;

			Route = route;

			LoadStops ();

			LoadDepartures ();
		}

		private async void LoadStops()
		{
			IsLoading = true;

			var result = await _routeRepository.SearchStopsByRouteId(Route.Id);

			Stops = new ObservableCollection<Stop> (result);

			IsLoading = false;
		}

		private async void LoadDepartures()
		{
			IsLoading = true;

			var result = await _routeRepository.SearchDeparturesByRouteId(Route.Id);

			var departures = new ObservableCollection<Departure> (result);

			WeekdayDepartures = new ObservableCollection<Departure>(departures
				.Where(x => x.Calendar == Departure.Weekday));

			WeekendDepartures = new ObservableCollection<Departure>(departures
				.Where(x => x.Calendar != Departure.Weekday));

			IsLoading = false;
		}

		public override void Accept(IViewModelVisitor viewModelVisitor)
		{
			viewModelVisitor.Visit(this);
		}
	}
}

