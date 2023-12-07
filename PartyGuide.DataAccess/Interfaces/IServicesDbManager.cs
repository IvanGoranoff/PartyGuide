using PartyGuide.DataAccess.Data;

namespace PartyGuide.DataAccess.Interfaces
{
    public interface IServiceDbManager
    {
        Task AddNewService(ServiceTable serviceTable);
        Task<List<ServiceTable>> GetAllServicesAsync();
        Task<ServiceTable> GetServiceByIdAsync(int? id);
        Task<List<ServiceTable>> GetServiceTablesFilterAsync(string category, string title, string startPriceRange, string endPriceRange, string location);
    }
}