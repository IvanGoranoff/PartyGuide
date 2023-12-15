using PartyGuide.DataAccess.Data;

namespace PartyGuide.DataAccess.Interfaces
{
    public interface IServiceDbManager
    {
        Task AddNewService(ServiceTable serviceTable);
		Task DeleteService(int? id);
		Task<List<ServiceTable>> GetAllServicesAsync();
		Task<List<ServiceTable>> GetAllServicesByUserAsync(string currentUser);
		Task<ServiceTable> GetServiceByIdAsync(int? id);
        Task<List<ServiceTable>> GetServiceTablesFilterAsync(string category, string title, int? startPriceRange, int? endPriceRange, string location);
	}
}