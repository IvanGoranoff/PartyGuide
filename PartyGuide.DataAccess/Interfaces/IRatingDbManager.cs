namespace PartyGuide.DataAccess.Interfaces
{
    public interface IRatingDbManager
    {
		Task AddNewRating(int serviceId, string userId, int rating, string comment);
		Task<bool> CheckIfUserHasRatedServiceAsync(int serviceId, string userId);
	}
}