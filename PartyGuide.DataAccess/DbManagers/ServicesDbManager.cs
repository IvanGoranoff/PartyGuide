using Microsoft.EntityFrameworkCore;
using PartyGuide.DataAccess.Data;
using PartyGuide.DataAccess.Interfaces;

namespace PartyGuide.DataAccess.DbManagers
{
    public class ServiceDbManager : IServiceDbManager
    {
        private readonly ApplicationDbContext dbContext;

        public ServiceDbManager(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ServiceTable> GetServiceByIdAsync(int? id)
        {
            return await dbContext.ServiceTables.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

		public async Task<List<ServiceTable>> GetAllServicesAsync()
		{
			return await dbContext.ServiceTables.ToListAsync();

		}

        public async Task<List<ServiceTable>> GetAllServicesByUserAsync(string currentUser)
        {
            return await dbContext.ServiceTables.Where(x => x.CreatedBy == currentUser).ToListAsync();
        }

        public async Task<List<ServiceTable>> GetServiceTablesFilterAsync(string category,
                                                                          string title,
                                                                          string startPriceRange,
                                                                          string endPriceRange,
                                                                          string location)
        {
            string query = $"SELECT * from ServiceTable t WHERE";

            if (!string.IsNullOrEmpty(category))
            {
                query += $"t.CATEGORY = '{category}'";
            }

            if (!string.IsNullOrEmpty(title))
            {
                query += $"t.TITLE = '{title}'";
            }

            if (!string.IsNullOrEmpty(startPriceRange))
            {
                query += $"t.STARTPRICERANGE = '{startPriceRange}'";
            }

            if (!string.IsNullOrEmpty(endPriceRange))
            {
                query += $"t.ENDPRICERANGE = '{endPriceRange}'";
            }

            if (!string.IsNullOrEmpty(location))
            {
                query += $"t.LOCATION = '{location}'";
            }

            return await dbContext.ServiceTables.FromSqlRaw(query).ToListAsync();
        }

        public async Task AddNewService(ServiceTable serviceTable)
        {
            await dbContext.ServiceTables.AddAsync(serviceTable);

            await dbContext.SaveChangesAsync();
        }

		public async Task DeleteService(int? id)
		{
			var table = await dbContext.ServiceTables.Where(s => s.Id == id).FirstOrDefaultAsync();

		    dbContext.ServiceTables.Remove(table);

			await dbContext.SaveChangesAsync();
		}
	}
}
