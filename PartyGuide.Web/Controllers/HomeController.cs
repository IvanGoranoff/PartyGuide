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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddNewService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewService(ServiceModel model)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await serviceManager.AddNewService(model);

                return Ok();

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