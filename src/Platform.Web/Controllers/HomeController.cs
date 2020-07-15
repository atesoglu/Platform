using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Platform.Model.Runtime.Response;
using Platform.Model.View;
using System;
using System.Diagnostics;

namespace Platform.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMemoryCache memoryCache, ILogger<HomeController> logger)
        {
            this.memoryCache = memoryCache;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            throw new Exception("Catch me if you can.");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error([FromQuery] string uid)
        {
            var error = new ErrorViewModel { UId = uid, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            memoryCache.TryGetValue(uid, out ErrorResponseModel errorResponse);
            error.ErrorResponse = errorResponse ?? new ErrorResponseModel();

            return View(error);
        }
    }
}