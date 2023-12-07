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

        public async Task<List<ServiceModel>> GetAllServicesAsync()
        {
            var tables = await serviceDbManager.GetAllServicesAsync();

            return adapter.TransformTablesToModelsList(tables);
        }

        public async Task<ServiceModel> GetServiceByIdAsync(int? id)
        {
            var table = await serviceDbManager.GetServiceByIdAsync(id);

            return adapter.TransformTableToModel(table);
        }
        public async Task<List<ServiceModel>> GetServiceTablesFilterAsync(string category, string title, string startPriceRange, string endPriceRange, string location)
        {
            var tables = await serviceDbManager.GetServiceTablesFilterAsync(category, title, startPriceRange, endPriceRange, location);

            return adapter.TransformTablesToModelsList(tables);
        }
    }
}
