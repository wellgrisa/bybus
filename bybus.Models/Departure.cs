using System;
using Newtonsoft.Json;

namespace bybus.Models
{
	public class Departure : Entity
	{	
		public const string Saturday = "SATURDAY";
		public const string Sunday = "SUNDAY";
		public const string Weekday = "WEEKDAY";

		public string Calendar { get; set; }
		public string Time { get; set; }
		public string DayDescription 
		{ 
			get 
			{
				var dayDescription = string.Empty;

				switch (Calendar) {
				case Saturday:
					dayDescription = "Sábado";
					break;
				case Sunday:
					dayDescription = "Domingo";
					break;
				default:
					dayDescription = "Dia de semana";
					break;
				}

				return dayDescription;
			}
		}
	}
}

