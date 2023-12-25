using Microsoft.EntityFrameworkCore;
using PartyGuide.Common;
using PartyGuide.DataAccess.Data;
using PartyGuide.DataAccess.DbContext;
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
			return await dbContext.ServiceTables.Include(s => s.Ratings).Where(s => s.Id == id).FirstOrDefaultAsync();
		}

		public async Task<List<ServiceTable>> GetAllServicesAsync()
		{
			return await dbContext.ServiceTables.Include(s => s.Ratings).ToListAsync();

		}

		public async Task<List<ServiceTable>> GetAllServicesByUserAsync(string currentUser)
		{
			return await dbContext.ServiceTables.Include(s => s.Ratings).Where(x => x.CreatedBy == currentUser).ToListAsync();
		}

		public async Task<List<ServiceTable>> GetServiceTablesFilterAsync(string category,
																		  string title,
																		  int? startPriceRange,
																		  int? endPriceRange,
																		  string location)
		{
			string query = $"SELECT * from ServiceTable t WHERE ";

			query += $"t.START_PRICE_RANGE >= {startPriceRange.Value} ";

			query += $"AND t.END_PRICE_RANGE <= {endPriceRange.Value} ";

			if (!string.IsNullOrEmpty(category))
			{
				if (category != CommonNomenclature.All)
				{
					query += $"AND t.CATEGORY = '{category}' ";
				}
			}

			if (!string.IsNullOrEmpty(title))
			{
				query += $"AND t.TITLE LIKE '%{title}%' ";
			}

			if (!string.IsNullOrEmpty(location))
			{
				if (location != CommonNomenclature.All)
				{
					query += $"AND t.LOCATION = '{location}' ";
				}
			}

			return await dbContext.ServiceTables.FromSqlRaw(query).Include(s => s.Ratings).ToListAsync();
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
