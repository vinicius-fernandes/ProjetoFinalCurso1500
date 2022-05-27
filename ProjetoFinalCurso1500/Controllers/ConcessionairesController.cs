using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCurso1500.Data;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Controllers
{
    [Authorize(Policy = "Admin")]
    public class ConcessionairesController : Controller
    {
        private readonly ProjetoFinalCurso1500Context _context;
        private readonly IMapper _mapper;

        public ConcessionairesController(ProjetoFinalCurso1500Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Concessionaires
        public async Task<IActionResult> Index()
        {
              return _context.Concessionaires != null ? 
                          View(await _context.Concessionaires.ToListAsync()) :
                          Problem("Entity set 'ProjetoFinalCurso1500Context.Concessionaires'  is null.");
        }

        // GET: Concessionaires/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Concessionaires == null)
            {
                return NotFound();
            }

            var concessionaire = await _context.Concessionaires
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concessionaire == null)
            {
                return NotFound();
            }

            return View(concessionaire);
        }

        // GET: Concessionaires/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Concessionaires/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] ConcessionaireDTO concessionaireDTO)
        {
            if (ModelState.IsValid)
            {
                var concessionaire =  _mapper.Map<Concessionaire>(concessionaireDTO);
                concessionaire.Id = Guid.NewGuid().ToString();
                _context.Add(concessionaire);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(concessionaireDTO);
        }

        // GET: Concessionaires/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Concessionaires == null)
            {
                return NotFound();
            }

            var concessionaire = await _context.Concessionaires.FindAsync(id);
            if (concessionaire == null)
            {
                return NotFound();
            }


            return View(_mapper.Map<ConcessionaireDTO>(concessionaire) );
        }

        // POST: Concessionaires/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name")] ConcessionaireDTO concessionaireDTO)
        {
            var concessionaire = _context.Concessionaires.Find(id);
            if (concessionaire==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _mapper.Map<ConcessionaireDTO, Concessionaire>(concessionaireDTO, concessionaire);
                    _context.Update(concessionaire);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConcessionaireExists(id))
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
            return View(concessionaire);
        }

        // GET: Concessionaires/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Concessionaires == null)
            {
                return NotFound();
            }

            var concessionaire = await _context.Concessionaires
                .FirstOrDefaultAsync(m => m.Id == id);
            if (concessionaire == null)
            {
                return NotFound();
            }

            return View(concessionaire);
        }

        // POST: Concessionaires/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Concessionaires == null)
            {
                return Problem("Entity set 'ProjetoFinalCurso1500Context.Concessionaires'  is null.");
            }
            var concessionaire = await _context.Concessionaires.FindAsync(id);
            if (concessionaire != null)
            {
                _context.Concessionaires.Remove(concessionaire);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConcessionaireExists(string id)
        {
          return (_context.Concessionaires?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
