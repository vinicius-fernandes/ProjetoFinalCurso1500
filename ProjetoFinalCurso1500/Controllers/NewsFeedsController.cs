using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCurso1500.Data;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Controllers
{
    public class NewsFeedsController : Controller
    {
        private readonly ProjetoFinalCurso1500Context _context;

        public NewsFeedsController(ProjetoFinalCurso1500Context context)
        {
            _context = context;
        }

        // GET: NewsFeeds
        public async Task<IActionResult> Index()
        {
              return _context.NewsFeed != null ? 
                          View(await _context.NewsFeed.ToListAsync()) :
                          Problem("Entity set 'ProjetoFinalCurso1500Context.NewsFeed'  is null.");
        }

        // GET: NewsFeeds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NewsFeed == null)
            {
                return NotFound();
            }

            var newsFeed = await _context.NewsFeed
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsFeed == null)
            {
                return NotFound();
            }

            return View(newsFeed);
        }

        // GET: NewsFeeds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsFeeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Image")] NewsFeed newsFeed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsFeed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsFeed);
        }

        // GET: NewsFeeds/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.NewsFeed == null)
            {
                return NotFound();
            }

            var newsFeed = await _context.NewsFeed.FindAsync(id);
            if (newsFeed == null)
            {
                return NotFound();
            }
            return View(newsFeed);
        }

        // POST: NewsFeeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Title,Content,Image")] NewsFeed newsFeed)
        {
            if (id != newsFeed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsFeed);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsFeedExists(newsFeed.Id))
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
            return View(newsFeed);
        }

        // GET: NewsFeeds/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.NewsFeed == null)
            {
                return NotFound();
            }

            var newsFeed = await _context.NewsFeed
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsFeed == null)
            {
                return NotFound();
            }

            return View(newsFeed);
        }

        // POST: NewsFeeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.NewsFeed == null)
            {
                return Problem("Entity set 'ProjetoFinalCurso1500Context.NewsFeed'  is null.");
            }
            var newsFeed = await _context.NewsFeed.FindAsync(id);
            if (newsFeed != null)
            {
                _context.NewsFeed.Remove(newsFeed);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsFeedExists(string id)
        {
          return (_context.NewsFeed?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
