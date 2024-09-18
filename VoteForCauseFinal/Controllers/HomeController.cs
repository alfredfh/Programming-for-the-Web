using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VoteForCauseFinal.Models;
using VoteForCauseFinal.Repositories;

namespace VoteForCauseFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICausePostRepository causePostRepository;

        public HomeController(ILogger<HomeController> logger, 
            ICausePostRepository causePostRepository)
        {
            _logger = logger;
            this.causePostRepository = causePostRepository;
        }

        public async Task<IActionResult> Index()
        {
            var causePosts = await causePostRepository.GetAllAsync();

            return View(causePosts);
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