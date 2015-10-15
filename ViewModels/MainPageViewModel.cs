using System;
using bybus.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using bybus.Models;
using bybus.ViewModels;
using bybus.Shared.Visitor;
using PropertyChanged;
using bybus;

namespace Views.bybus
{
	[ImplementPropertyChanged]
	public class MainPageViewModel
	{
		private readonly RouteRepository _routeRepository;

		public ObservableCollection<Route> Routes { get; set; }

		public RouteDetailsViewModel SelectedRoute { get; set; }

		public Command GoToDetailsCommand { get; set; }

		public bool IsLoading { get; set; }

		public string SearchQuery { get; set; }

		public MainPageViewModel()
		{
			_routeRepository = new RouteRepository ("https://api.appglu.com/v1/queries/{0}");

			SearchQuery = "lauro linhares";

			GoToDetailsCommand = new Command(GoToDetails);

			LoadRoutes();
		}

		private void GoToDetails(object obj)
		{
			var routeDetailsViewModel = new RouteDetailsViewModel ( _routeRepository, obj as Route);

			routeDetailsViewModel.Accept(new NavigateVisitor());
		}

		private async void LoadRoutes ()
		{
			this.IsLoading = true;

			var result = await _routeRepository.SearchByStopName(string.Format("%{0}%", SearchQuery));

			Routes = new ObservableCollection<Route> (result);

			this.IsLoading = false;
		}
	}
}

