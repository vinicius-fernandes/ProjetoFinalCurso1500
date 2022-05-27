using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCurso1500.Data;
using ProjetoFinalCurso1500.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace ProjetoFinalCurso1500.Controllers
{
    public class NewsFeedsController : Controller
    {
        private readonly ProjetoFinalCurso1500Context _context;
        private readonly IMapper _mapper;

        public NewsFeedsController(ProjetoFinalCurso1500Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

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
        [Authorize(Policy = "SalesmanAllowed")]

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
        [Authorize(Policy = "SalesmanAllowed")]

        public async Task<IActionResult> Create([Bind("Title,Content,Image")] NewsFeedDTO newsFeedDTO)
        {
            if (ModelState.IsValid)
            {
                var newsFeed = _mapper.Map<NewsFeed>(newsFeedDTO);
                newsFeed.Id = Guid.NewGuid().ToString();
                newsFeed.Data = DateTime.Now;
                _context.Add(newsFeed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsFeedDTO);
        }

        // GET: NewsFeeds/Edit/5
        [Authorize(Policy = "SalesmanAllowed")]

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
            return View(_mapper.Map<NewsFeedDTO>(newsFeed));
        }

        // POST: NewsFeeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "SalesmanAllowed")]

        public async Task<IActionResult> Edit(string id, [Bind("Title,Content,Image")] NewsFeedDTO newsFeedDTO)
        {
            var newsFeed = await _context.NewsFeed.FindAsync(id);
            if (newsFeed==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _mapper.Map<NewsFeedDTO,NewsFeed>(newsFeedDTO, newsFeed);
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
        [Authorize(Policy = "SalesmanAllowed")]

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
        [Authorize(Policy = "SalesmanAllowed")]

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
        [NonAction]
        private bool NewsFeedExists(string id)
        {
          return (_context.NewsFeed?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
