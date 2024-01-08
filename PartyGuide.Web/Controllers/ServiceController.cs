using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PartyGuide.DataAccess.Interfaces;
using PartyGuide.Domain.Interfaces;
using PartyGuide.Domain.Models;
using PartyGuide.Infrastructure.Models.GeoNames;
using PartyGuide.Infrastructure.Services.GeoNames;
using PartyGuide.Web.Helpers;
using PartyGuide.Web.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PartyGuide.Web.Controllers
{
	public class ServiceController : Controller
	{
		private readonly ILogger<ServiceController> _logger;
		private readonly IServiceManager serviceManager;
		private readonly IRatingManager ratingManager;
		private readonly IGeoNamesService geoNamesService;

		public ServiceController(ILogger<ServiceController> logger,
							     IServiceManager serviceManager,
							     IRatingManager ratingManager,
							     IGeoNamesService geoNamesService)
		{
			_logger = logger;
			this.serviceManager = serviceManager;
			this.ratingManager = ratingManager;
			this.geoNamesService = geoNamesService;
		}

		public async Task<IActionResult> IndexHome()
		{
			return View();
		}

		public async Task<IActionResult> Index()
		{
			var model = new SearchModel();

			List<City> cities = geoNamesService.GetCitiesInBulgaria();
			ViewBag.Cities = SelectListItemHelper.CreateAllCitiesSelectList(cities);

			model.Services = await serviceManager.GetAllServicesAsync();

			ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Index(SearchModel model)
		{
			List<City> cities = geoNamesService.GetCitiesInBulgaria();
			ViewBag.Cities = SelectListItemHelper.CreateAllCitiesSelectList(cities);

			model.Services = await serviceManager.GetServiceModelsFilterAsync(model);

			ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;

			return View(model);
		}

		public async Task<IActionResult> ServiceDetails(int? id)
		{
			var model = await serviceManager.GetServiceByIdAsync(id);

			return View(model);
		}

		[Authorize]
		[HttpGet]
		public IActionResult AddNewService()
		{
			List<City> cities = geoNamesService.GetCitiesInBulgaria();
			ViewBag.Cities = SelectListItemHelper.CreateOnlyCitiesSelectList(cities);
			ViewBag.Categories = SelectListItemHelper.CreateCategoriesList();

			return View();
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> AddNewService(ServiceModel model, IFormFile imageFile)
		{
			if (!User.Identity.IsAuthenticated)
			{
				return View(model);
			}

			ViewBag.Categories = SelectListItemHelper.CreateCategoriesList();
			List<City> cities = geoNamesService.GetCitiesInBulgaria();
			ViewBag.Cities = SelectListItemHelper.CreateOnlyCitiesSelectList(cities);

			model.CreatedBy = User.FindFirst(ClaimTypes.Email)?.Value;

			// Exclude Image property from validation
			ModelState.Remove("imageFile");

			if (!ModelState.IsValid)
			{
				return View();
			}
			try
			{
				if (imageFile != null && imageFile.Length > 0)
				{
					using (var stream = new MemoryStream())
					{
						imageFile.CopyTo(stream);
						model.Image = stream.ToArray();
					}
				}
				else
				{
					string defaultImagePath = Path.Combine("wwwroot", "images", "banner.png");
					byte[] defaultImageBytes = System.IO.File.ReadAllBytes(defaultImagePath);
					model.Image = defaultImageBytes;
				}

				await serviceManager.AddNewService(model);

				TempData["SuccessMessage"] = "Service added successfully.";

				return RedirectToAction("Index");

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> ManageServices()
		{
			string currentUser = User.FindFirst(ClaimTypes.Email)?.Value;

			var serviceModels = await serviceManager.GetAllServicesByUserAsync(currentUser);

			ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;

			return View(serviceModels);
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> DeleteService(int? id)
		{
			// If the user is Admin - do not check if the current service is created by him
			//var userIsAdmin = User.Claims.Select(role => role.Value).Contains("Admin");
			var userIsAdmin = User.IsInRole("Admin");

			if (userIsAdmin)
			{
				await serviceManager.DeleteService(id);

				return Json(new { success = true });
			}

			// Check if the service is created by the user
			string currentUser = User.FindFirst(ClaimTypes.Email)?.Value;

			var serviceModels = await serviceManager.GetAllServicesByUserAsync(currentUser);

			if (serviceModels.Exists(s => s.Id == id))
			{
				await serviceManager.DeleteService(id);

				return Json(new { success = true });
			}
			else
			{
				return Json(new { success = false });
			}

		}

		public async Task<List<ServiceModel>> GetAllServices()
		{
			var models = await serviceManager.GetAllServicesAsync();
			return models;
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}