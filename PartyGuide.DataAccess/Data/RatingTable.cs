using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PartyGuide.DataAccess.Data
{
	public class RatingTable
	{
		public int Id { get; set; }

		public int ServiceId { get; set; }

		public string UserId { get; set; }

		public int Rating { get; set; }

		public string Comment { get; set; }

		public DateTime Date { get; set; }

		public ServiceTable Service { get; set; } = null!; // Required reference navigation to principal
	}
}
