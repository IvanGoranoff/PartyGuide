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
					citiesSelectList.Add(new SelectListItem { Text = city.Name, Value = city.Name });
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

		public static List<SelectListItem> CreateCategoriesList()
		{
			var categoriesSelectList = new List<SelectListItem>()
	{
		new SelectListItem { Value = "Birthday", Text = "Birthday" },
		new SelectListItem { Value = "Anniversary", Text = "Anniversary" },
		new SelectListItem { Value = "Wedding", Text = "Wedding" },
		new SelectListItem { Value = "PROM", Text = "PROM" },
		new SelectListItem { Value = "Company", Text = "Company" },
		new SelectListItem { Value = "Baptism", Text = "Baptism" },
		new SelectListItem { Value = "Retirement", Text = "Retirement" },
		new SelectListItem { Value = "Graduation", Text = "Graduation" },
	};

			return categoriesSelectList;
		}

	}
}
