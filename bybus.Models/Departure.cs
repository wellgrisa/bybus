using System;
using Newtonsoft.Json;

namespace bybus.Models
{
	public class Departure : Entity
	{	
		public string Calendar { get; set; }
		public string Time { get; set; }
	}
}

