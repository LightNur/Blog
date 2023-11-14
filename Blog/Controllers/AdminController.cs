using Blog.Models;
using Blog.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AnimeClothes.Controllers
{
    public class AdminController : Controller
	{
		private readonly DatabaseContext _context;

		public AdminController(DatabaseContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return _context.Post != null ?
				View(await _context.Post.ToListAsync()) :
				Problem("Entity set 'MovieContext.Movie' is null");

		}



		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> Create([Bind("Id,Title,LongDesc, ShortDesc, Author")] Post post)
		{
			if (ModelState.IsValid)
			{
				_context.Add(post);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View();
		}
		public async Task<IActionResult> Edit(int id)
		{
			if (id == null || _context.Post == null)
			{
				return NotFound();
			}
			var post = await _context.Post.FindAsync(id);
			if (post == null)
			{
				return NotFound();
			}
			return View(post);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,LongDesc, ShortDesc, Author")] Post post)
		{


			if (id != post.Id)
			{
				return NotFound();
			}
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(post);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!PostExist(post.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));

			}
			return View(post);

		}




		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Post == null) { return NotFound(); }
			var post = await _context.Post
				.FirstOrDefaultAsync(m => m.Id == id);
			if (post == null)
			{
				return NotFound();
			}
			return View(post);

		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Post == null)
			{
				return NotFound();
			}
			var post = await _context.Post.FindAsync(id);
			if (post != null)
			{
				_context.Post.Remove(post);
			}
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool PostExist(int id)
		{
			return (_context.Post?.Any(e => e.Id == id)).GetValueOrDefault();
		}

	}
}

