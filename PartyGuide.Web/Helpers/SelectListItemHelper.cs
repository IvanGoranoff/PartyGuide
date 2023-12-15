using Microsoft.AspNetCore.Mvc.Rendering;
using PartyGuide.Infrastructure.Models.GeoNames;

namespace PartyGuide.Web.Helpers
{
	public class SelectListItemHelper
	{
		public static List<SelectListItem> CreateAllCitiesSelectList(List<City> citiesList)
		{
			var citiesSelectList = new List<SelectListItem>();

			citiesSelectList.Add(new SelectListItem { Text = "All", Value = "All" });

			if (citiesList.Count != 0)
			{
				foreach (City city in citiesList)
				{
					citiesSelectList.Add(new SelectListItem { Text = city.Name, Value = city.Name});
				}
			}

			return citiesSelectList;
		}

		public static List<SelectListItem> CreateOnlyCitiesSelectList(List<City> citiesList)
		{
			var citiesSelectList = new List<SelectListItem>();

			if (citiesList.Count != 0)
			{
				foreach (City city in citiesList)
				{
					citiesSelectList.Add(new SelectListItem { Text = city.Name, Value = city.Name });
				}
			}

			return citiesSelectList;
		}
	}
}
