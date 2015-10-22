using System;
using bybus.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace bybus.Services
{
	public interface IRouteRepository
	{
		Task<IList<Route>> SearchByStopName (string search);

		Task<IList<Stop>> SearchStopsByRouteId (int routeId);

		Task<IList<Departure>> SearchDeparturesByRouteId (int routeId);
	}

	public class RouteRepository : RestBaseRepository, IRouteRepository
	{
		public const string FindRoutesByStopName = "findRoutesByStopName/run";
		public const string FindStopsByRouteId = "findStopsByRouteId/run";
		public const string FindDeparturesByRouteId = "findDeparturesByRouteId/run";

		protected override string ServiceBaseUrl 
		{
			get 
			{
				return "https://api.appglu.com/v1/queries/{0}";
			}
		}

		public RouteRepository ()			
		{
			_client = new HttpClient ();

			Initialize ();
		}

		public RouteRepository (HttpClient client)			
		{
			_client = client;

			Initialize ();
		}

		private void Initialize()
		{
			Authenticate();

			SetHeaders();
		}

		public Task<IList<Route>> SearchByStopName(string search)
		{
			var searchQuery = new { @params = new { stopName = search } };

			return Task.Run<IList<Route>>(() => Query<Route>(FindRoutesByStopName, searchQuery));
		}

		public Task<IList<Stop>> SearchStopsByRouteId(int routeId)
		{
			var searchQuery = new { @params = new { routeId = routeId.ToString() } };

			return Task.Run<IList<Stop>>(() => base.Query<Stop>(FindStopsByRouteId, searchQuery));
		}

		public Task<IList<Departure>> SearchDeparturesByRouteId(int routeId)
		{
			var searchQuery = new { @params = new { routeId = routeId.ToString() } };

			return Task.Run<IList<Departure>>(() => base.Query<Departure>(FindDeparturesByRouteId, searchQuery));
		}
	}
}