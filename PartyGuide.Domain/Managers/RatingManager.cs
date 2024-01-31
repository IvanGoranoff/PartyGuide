using PartyGuide.DataAccess.Interfaces;
using PartyGuide.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PartyGuide.Domain.Managers
{
    public class RatingManager : IRatingManager
	{
		private readonly IRatingDbManager ratingDbManager;

		public RatingManager(IRatingDbManager ratingDbManager)
		{
			this.ratingDbManager = ratingDbManager;
		}

		public async Task<bool> CheckIfUserHasRatedServiceAsync(int serviceId, string userId)
		{
			return await ratingDbManager.CheckIfUserHasRatedServiceAsync(serviceId, userId);
		}

		public async Task AddNewRating(int serviceId, string userId, int rating, string comment)
		{
			await ratingDbManager.AddNewRating(serviceId, userId, rating, comment);
		}

		public async Task UpdateRating(int serviceId, int rating, string comment)
		{
			await ratingDbManager.UpdateRating(serviceId, rating, comment);
		}
	}
}
