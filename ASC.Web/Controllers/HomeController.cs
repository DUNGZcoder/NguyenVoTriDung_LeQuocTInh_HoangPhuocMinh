using ASC.Web.Configuration;
using ASC.Web.Models;
using ASC.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace ASC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<ApplicationSettings> _settings;
        private readonly ISession _session;

        public HomeController(ILogger<HomeController> logger, IOptions<ApplicationSettings> settings, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _settings = settings;
            _session = httpContextAccessor.HttpContext?.Session;
        }

        public IActionResult Index()
        {
            ViewBag.Title = _settings.Value.ApplicationTitle;
            
            // Store ApplicationTitle in session
            if (_session != null)
            {
                Microsoft.AspNetCore.Http.SessionExtensions.SetString(_session, "ApplicationTitle", _settings.Value.ApplicationTitle);
            }
            
            return View();
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
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
