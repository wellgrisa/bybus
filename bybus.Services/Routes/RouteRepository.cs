using System;
using bybus.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace bybus.Services
{
	public class RouteRepository : RestBaseRepository
	{
		private const string FindRoutesByStopName = "findRoutesByStopName/run";
		private const string FindStopsByRouteId = "findStopsByRouteId/run";
		private const string FindDeparturesByRouteId = "findDeparturesByRouteId/run";

		public RouteRepository (string url)
			: base(url)
		{
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