using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyGuide.Domain.Models
{
	public class SearchModel
	{
		public int? StartPrice { get; set; }

		public int? EndPrice { get; set; }

		public string Category { get; set; }

		public string Title { get; set; }

		public string Location { get; set; }

		public List<ServiceModel> Services { get; set; }
    }
}
