using Blog.Models;
using Blog.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog.Controllers
{
    public class HomeController : Controller
	{
        private readonly DatabaseContext _context;

        // All data of database will be giving to _context
        public HomeController(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> More(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

		public async Task<IActionResult> Index()
		{
			return _context.Post != null ?
				View(await _context.Post.ToListAsync()) :
				Problem("Entity set 'MovieContext.Movie' is null");

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