using Microsoft.AspNetCore.Mvc;
using PartyGuide.Domain.Interfaces;
using PartyGuide.Domain.Managers;
using System.Security.Claims;

namespace PartyGuide.Web.Controllers
{
	public class RatingController : Controller
	{
		private readonly IRatingManager ratingManager;

		public RatingController(IRatingManager ratingManager)
        {
			this.ratingManager = ratingManager;
		}

        [HttpPost]
		public async Task<ActionResult> RateService(int serviceId, int rating, string comment)
		{
			try
			{
				if (!User.Identity.IsAuthenticated)
				{
					return Json(new { success = false, errorMessage = "You have to be logged in to submit a review for this service." });
				}

				// Check if the user has already submitted a review
				var userId = User.FindFirst(ClaimTypes.Email)?.Value; // Adjust based on your authentication setup

				var existingReview = await ratingManager.CheckIfUserHasRatedServiceAsync(serviceId, userId);

				if (existingReview)
				{
					// User has already submitted a review
					// You can choose to update the existing review or reject the new submission
					return Json(new { success = false, errorMessage = "You have already submitted a review for this service." });
				}

				// Add the service rating
				await ratingManager.AddNewRating(serviceId, userId, rating, comment);

				// Return success message
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				// Return error message
				return Json(new { success = false, errorMessage = ex.Message });
			}
		}


		[HttpPost]
        [HttpPost]
        public async Task<ActionResult> UpdateRating(int ratingId, int newRating, string newComment, string userEmail)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, errorMessage = "You must be logged in." });
            }

            var currentUserEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (currentUserEmail != userEmail)
            {
                return Json(new { success = false, errorMessage = "Unauthorized edit attempt." });
            }

            // Add logic to get the review by ID and update it
            // Example: await ratingManager.UpdateRatingAsync(ratingId, newRating, newComment);

            return Json(new { success = true });
        }

    }
}
