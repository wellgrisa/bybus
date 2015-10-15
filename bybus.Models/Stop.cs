using System;
using Newtonsoft.Json;

namespace bybus.Models
{
	public class Stop : Entity
	{
		public string Name { get; set; }
		public string Sequence { get; set; }
		[JsonProperty("route_id")]
		public int RouteId { get; set; }
	}
}

