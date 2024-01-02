using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;
using PartyGuide.Domain.Interfaces;
using PartyGuide.Domain.Models;
using PartyGuide.Infrastructure.Models.GeoNames;
using PartyGuide.Infrastructure.Services.GeoNames;
using PartyGuide.Web.Controllers;
using System.Security.Claims;

namespace PartyGuide.UnitTest
{
	[TestClass]
	public class ServiceControllerTests
	{
		/// <summary>
		/// Important INFORMATION
		/// In order for the mock tests to work you need to comment out the authentication from every controller
		/// --- AUTHENTICATION ---
		///   if (!User.Identity.IsAuthenticated)
		///   {
		///   	 return View(model);
		///   }
		/// </summary>
		/// <returns></returns>

	[TestMethod]
		public async Task AddNewService_AuthenticatedUser_ReturnsRedirectToActionResult()
		{
			// Arrange
			var loggerMock = new Mock<ILogger<ServiceController>>();
			var serviceManagerMock = new Mock<IServiceManager>();
			var ratingManagerMock = new Mock<IRatingManager>();

			// Mock IGeoNamesService
			var geoNamesServiceMock = new Mock<IGeoNamesService>();
			geoNamesServiceMock.Setup(service => service.GetCitiesInBulgaria())
							   .Returns(new List<City>
							   {
									new City { Name = "Sofia", PostalCode = "1700" }
							   });

			var controller = new ServiceController(loggerMock.Object, serviceManagerMock.Object, ratingManagerMock.Object, geoNamesServiceMock.Object);

			// Simulate an authenticated user
			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Email, "testuser@example.com")
					}))
				}
			};

			// Simulate TempData setup
			controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());


			var model = new ServiceModel
			{
				Category = "Birthday",
				CreatedBy = "testuser@example.com",
				Description = "Project Description that is more than 100 symbols testing the project. Description of a service model.",
				EndPriceRange = 1000,
				StartPriceRange = 100,
				ExtendedDescription = "Project Description that is more than 100 symbols testing the project.Description of a service model. testtttttttttttttttttttttttttttttt",
				Image = null,
				Location = "Sofia",
				PhoneNumber = "+359988993274",
				Ratings = null,
				Id = 1,
				Title = "Test title"
			};

			// Act
			var result = await controller.AddNewService(model, null) as RedirectToActionResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Index", result.ActionName);
		}

		[TestMethod]
		public async Task RateService_ValidInput_ReturnsJsonResult()
		{
			// Arrange
			var loggerMock = new Mock<ILogger<ServiceController>>();
			var serviceManagerMock = new Mock<IServiceManager>();
			var ratingManagerMock = new Mock<IRatingManager>();
			var geoNamesServiceMock = new Mock<IGeoNamesService>();

			var controller = new ServiceController(loggerMock.Object, serviceManagerMock.Object, ratingManagerMock.Object, geoNamesServiceMock.Object);
			controller.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
					{
						new Claim(ClaimTypes.Email, "testuser@example.com")
					}))
				}
			};

			// Act
			var result = await controller.RateService(1, 5, "I LOVE IT") as JsonResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsTrue((bool)result.Value.GetType().GetProperty("success").GetValue(result.Value));
		}
	}
}