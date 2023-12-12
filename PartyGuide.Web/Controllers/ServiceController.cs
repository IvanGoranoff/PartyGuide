using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyGuide.Domain.Interfaces;
using PartyGuide.Domain.Models;
using PartyGuide.Web.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PartyGuide.Web.Controllers
{
	public class ServiceController : Controller
	{
		private readonly ILogger<ServiceController> _logger;
		private readonly IServiceManager serviceManager;

		public ServiceController(ILogger<ServiceController> logger, IServiceManager serviceManager)
		{
			_logger = logger;
			this.serviceManager = serviceManager;
		}

		public async Task<IActionResult> Index()
		{
			var models = await serviceManager.GetAllServicesAsync();

			ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;

			return View(models);
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
			model.CreatedBy = User.FindFirst(ClaimTypes.Email)?.Value;

			// Exclude Image property from validation
			ModelState.Remove("imageFile");

			//if (!ModelState.IsValid)
			//         {
			//             return View();
			//         }
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
					// No image uploaded, set the default image
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
			string currentUser = User.FindFirst(ClaimTypes.Email)?.Value;

			var serviceModels = await serviceManager.GetAllServicesByUserAsync(currentUser);

			if (serviceModels.Exists(s => s.Id == id))
			{
				await serviceManager.DeleteService(id);

				TempData["SuccessMessage"] = "Service deleted successfully.";
			}
			else
			{
				TempData["SuccessMessage"] = "Service not found.";
			}

			return RedirectToAction("ManageServices");
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