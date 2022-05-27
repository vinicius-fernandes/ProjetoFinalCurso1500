using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCurso1500.Data;
using ProjetoFinalCurso1500.Models;
using System.Diagnostics;

namespace ProjetoFinalCurso1500.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProjetoFinalCurso1500Context _context;

        public HomeController(ILogger<HomeController> logger, ProjetoFinalCurso1500Context context)
        {
            _context = context;

            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var newsFeeds = await _context.NewsFeed.OrderBy(x => x.Data).Take(10).ToListAsync();
            ViewData["NewsFeeds"] = newsFeeds;
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
    }
}