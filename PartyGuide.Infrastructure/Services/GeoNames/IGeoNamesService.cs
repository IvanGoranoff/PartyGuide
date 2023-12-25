using PartyGuide.Infrastructure.Models.GeoNames;

namespace PartyGuide.Infrastructure.Services.GeoNames
{
	public interface IGeoNamesService
	{
		List<City> GetCitiesInBulgaria();
	}
}