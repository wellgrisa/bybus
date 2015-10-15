using System;

namespace bybus.Models
{
	public class Route : Entity
	{
		public string ShortName { get; set; }
		public string LongName { get; set; }
		public DateTime LastModifiedDate { get; set; }
		public int AgencyId { get; set; }

		public Route ()
		{
		}
	}
}

