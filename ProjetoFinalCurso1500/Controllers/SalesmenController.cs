using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCurso1500.Data;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Controllers
{
    public class SalesmenController : Controller
    {
        private readonly ProjetoFinalCurso1500Context _context;
        private readonly IMapper _mapper;

        public SalesmenController(ProjetoFinalCurso1500Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Salesmen
        public async Task<IActionResult> Index()
        {
            var projetoFinalCurso1500Context = _context.Salesman.Include(s => s.Concessionaire).Include(s => s.User);
            return View(await projetoFinalCurso1500Context.ToListAsync());
        }

        // GET: Salesmen/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Salesman == null)
            {
                return NotFound();
            }

            var salesman = await _context.Salesman
                .Include(s => s.Concessionaire)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesman == null)
            {
                return NotFound();
            }

            return View(salesman);
        }

        // GET: Salesmen/Create
        public IActionResult Create()
        {
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users.Where(c=>c.Salesman==null), "Id", "Name");
            return View();
        }

        // POST: Salesmen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Salarie,IdConcessionaire")] SalesmanDTO salesmanDTO)
        {
            if (ModelState.IsValid)
            {
                var salesman = _mapper.Map<Salesman>(salesmanDTO);
                salesman.Id = Guid.NewGuid().ToString();
                _context.Add(salesman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name", salesmanDTO.IdConcessionaire);
            ViewData["UserId"] = new SelectList(_context.Users.Where(c => c.Salesman == null), "Id", "Name", salesmanDTO.UserId);
            return View(salesmanDTO);
        }

        // GET: Salesmen/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Salesman == null)
            {
                return NotFound();
            }

            var salesman = await _context.Salesman.FindAsync(id);
            if (salesman == null)
            {
                return NotFound();
            }
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name", salesman.IdConcessionaire);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Name", salesman.UserId);
            return View(_mapper.Map<SalesmanDTO>(salesman));
        }

        // POST: Salesmen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,Salarie,IdConcessionaire")] SalesmanDTO salesmanDTO)
        {
            var salesman = _context.Salesman.Find(id);

            if (salesman==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _mapper.Map<SalesmanDTO, Salesman>(salesmanDTO, salesman);
                    _context.Update(salesman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesmanExists(id))
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
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Id", salesmanDTO.IdConcessionaire);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", salesmanDTO.UserId);
            return View(salesmanDTO);
        }

        // GET: Salesmen/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Salesman == null)
            {
                return NotFound();
            }

            var salesman = await _context.Salesman
                .Include(s => s.Concessionaire)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (salesman == null)
            {
                return NotFound();
            }

            return View(salesman);
        }

        // POST: Salesmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Salesman == null)
            {
                return Problem("Entity set 'ProjetoFinalCurso1500Context.Salesman'  is null.");
            }
            var salesman = await _context.Salesman.FindAsync(id);
            if (salesman != null)
            {
                _context.Salesman.Remove(salesman);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesmanExists(string id)
        {
          return (_context.Salesman?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
