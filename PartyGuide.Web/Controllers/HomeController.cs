using Microsoft.AspNetCore.Mvc;
using PartyGuide.Domain.Interfaces;
using PartyGuide.Domain.Models;
using PartyGuide.Web.Models;
using System.Diagnostics;

namespace PartyGuide.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceManager serviceManager;

        public HomeController(ILogger<HomeController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            this.serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            var models = await serviceManager.GetAllServicesAsync();

            // Get success message from TempData
            ViewBag.SuccessMessage = TempData["SuccessMessage"] as string;

            return View(models);
        }

        [HttpGet]
        public IActionResult AddNewService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewService(ServiceModel model, IFormFile imageFile)
        {
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

				await serviceManager.AddNewService(model);

                // Set success message in TempData
                TempData["SuccessMessage"] = "Service added successfully.";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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