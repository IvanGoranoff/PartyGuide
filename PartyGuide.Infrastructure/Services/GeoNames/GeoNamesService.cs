using PartyGuide.Infrastructure.Models.GeoNames;
using RestSharp;

namespace PartyGuide.Infrastructure.Services.GeoNames
{
	public class GeoNamesService : IGeoNamesService
	{
		private const string GeoNamesApiBaseUrl = "http://api.geonames.org";

		public List<City> GetCitiesInBulgaria()
		{
			var client = new RestClient(GeoNamesApiBaseUrl);

			var request = new RestRequest("childrenJSON", Method.Get);
			request.AddParameter("geonameId", "732800"); // GeonameId for Bulgaria
			request.AddParameter("username", "vivanovBG");

			var response = client.Execute<GeoNamesResponse>(request);

			if (response.IsSuccessful)
			{
				return response.Data?.Geonames;
			}

			return null;
		}
	}
}
