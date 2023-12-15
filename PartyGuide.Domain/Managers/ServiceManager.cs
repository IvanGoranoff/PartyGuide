using PartyGuide.DataAccess.Data;
using PartyGuide.DataAccess.Interfaces;
using PartyGuide.Domain.Adapters;
using PartyGuide.Domain.Interfaces;
using PartyGuide.Domain.Models;

namespace PartyGuide.Domain.Managers
{
	public class ServiceManager : IServiceManager
	{
		private readonly IServiceDbManager serviceDbManager;
		AdapterDomain adapter;

		public ServiceManager(IServiceDbManager serviceDbManager)
		{
			this.serviceDbManager = serviceDbManager;
			adapter = new AdapterDomain();
		}

		public async Task AddNewService(ServiceModel serviceModel)
		{
			var table = adapter.TransformModelToTable(serviceModel);

			await serviceDbManager.AddNewService(table);
		}

		public async Task DeleteService(int? id)
		{
			await serviceDbManager.DeleteService(id);
		}

		public async Task<List<ServiceModel>> GetAllServicesAsync()
		{
			var tables = await serviceDbManager.GetAllServicesAsync();

			return adapter.TransformTablesToModelsList(tables);
		}

		public async Task<List<ServiceModel>> GetAllServicesByUserAsync(string? currentUser)
		{
			var tables = await serviceDbManager.GetAllServicesByUserAsync(currentUser);

			return adapter.TransformTablesToModelsList(tables);
		}

		public async Task<ServiceModel> GetServiceByIdAsync(int? id)
		{
			var table = await serviceDbManager.GetServiceByIdAsync(id);

			return adapter.TransformTableToModel(table);
		}

		public async Task<List<ServiceModel>> GetServiceModelsFilterAsync(SearchModel model)
		{
			var tables = await serviceDbManager.GetServiceTablesFilterAsync(model.Category, model.Title, model.StartPrice, model.EndPrice, model.Location);

			return adapter.TransformTablesToModelsList(tables);
		}
	}
}
