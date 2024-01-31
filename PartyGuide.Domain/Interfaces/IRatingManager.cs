namespace PartyGuide.Domain.Interfaces
{
    public interface IRatingManager
    {
        Task<bool> CheckIfUserHasRatedServiceAsync(int serviceId, string userId);

        Task AddNewRating(int serviceId, string userId, int rating, string comment);

        Task UpdateRating(int ratingId, int rating, string comment);

	}
}