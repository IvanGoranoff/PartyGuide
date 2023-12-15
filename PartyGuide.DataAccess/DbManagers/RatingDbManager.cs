using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PartyGuide.DataAccess.Data;
using PartyGuide.DataAccess.DbContext;
using PartyGuide.DataAccess.Interfaces;

namespace PartyGuide.DataAccess.DbManagers
{
    public class RatingDbManager : IRatingDbManager
	{
		private readonly ApplicationDbContext dbContext;

		public RatingDbManager(ApplicationDbContext dbContext)
        {
			this.dbContext = dbContext;
		}

		public async Task AddNewRating(int serviceId, string userId, int rating, string comment)
		{
			var table = new RatingTable()
			{
				ServiceId = serviceId,
				UserId = userId,
				Rating = rating,
				Comment = comment,
				Date = DateTime.Now,
			};

			await dbContext.RatingTables.AddAsync(table);

			await dbContext.SaveChangesAsync();
		}

		public async Task<bool> CheckIfUserHasRatedServiceAsync(int serviceId, string userId)
		{
			var table = await dbContext.RatingTables.FirstOrDefaultAsync(r => r.ServiceId == serviceId && r.UserId == userId);

			if (table == null)
			{
				return false;
			}

			return true;

		}
	}
}
