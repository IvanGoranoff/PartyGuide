using PartyGuide.Domain.Models;

namespace PartyGuide.Domain.Interfaces
{
    public interface IServiceManager
    {
        Task AddNewService(ServiceModel serviceModel);
		Task DeleteService(int? id);
		Task<List<ServiceModel>> GetAllServicesAsync();
		Task<List<ServiceModel>> GetAllServicesByUserAsync(string? currentUser);
		Task<ServiceModel> GetServiceByIdAsync(int? id);
        Task<List<ServiceModel>> GetServiceModelsFilterAsync(SearchModel model);
	}
}