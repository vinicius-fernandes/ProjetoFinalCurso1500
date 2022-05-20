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
    public class TestDrivesController : Controller
    {
        private readonly ProjetoFinalCurso1500Context _context;
        private readonly IMapper _mapper;
        public TestDrivesController(ProjetoFinalCurso1500Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: TestDrives
        public async Task<IActionResult> Index()
        {
            var projetoFinalCurso1500Context = _context.TestDrive.Include(t => t.Car).Include(t => t.Client).Include(t => t.Concessionaire).Include(t => t.Salesman);
            return View(await projetoFinalCurso1500Context.ToListAsync());
        }

        // GET: TestDrives/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TestDrive == null)
            {
                return NotFound();
            }

            var testDrive = await _context.TestDrive
                .Include(t => t.Car)
                .Include(t => t.Client)
                .Include(t => t.Concessionaire)
                .Include(t => t.Salesman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testDrive == null)
            {
                return NotFound();
            }

            return View(testDrive);
        }

        // GET: TestDrives/Create
        public IActionResult Create()
        {
            ViewData["IdCar"] = new SelectList(_context.Car, "Id", "Name");
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Name");
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name");
            ViewData["IdSalesman"] = new SelectList(_context.Salesman, "Id", "Name");
            return View();
        }

        // POST: TestDrives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,IdSalesman,IdCar,IdConcessionaire,IdClient,Completed")] TestDrive testDrive)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testDrive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCar"] = new SelectList(_context.Car, "Id", "Id", testDrive.IdCar);
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Id", testDrive.IdClient);
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Id", testDrive.IdConcessionaire);
            ViewData["IdSalesman"] = new SelectList(_context.Salesman, "Id", "Id", testDrive.IdSalesman);
            return View(testDrive);
        }

        // GET: TestDrives/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TestDrive == null)
            {
                return NotFound();
            }

            var testDrive = await _context.TestDrive.FindAsync(id);
            if (testDrive == null)
            {
                return NotFound();
            }
            ViewData["IdCar"] = new SelectList(_context.Car, "Id", "Id", testDrive.IdCar);
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Id", testDrive.IdClient);
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Id", testDrive.IdConcessionaire);
            ViewData["IdSalesman"] = new SelectList(_context.Salesman, "Id", "Id", testDrive.IdSalesman);
            return View(testDrive);
        }

        // POST: TestDrives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Date,IdSalesman,IdCar,IdConcessionaire,IdClient,Completed")] TestDrive testDrive)
        {
            if (id != testDrive.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testDrive);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestDriveExists(testDrive.Id))
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
            ViewData["IdCar"] = new SelectList(_context.Car, "Id", "Id", testDrive.IdCar);
            ViewData["IdClient"] = new SelectList(_context.Client, "Id", "Id", testDrive.IdClient);
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Id", testDrive.IdConcessionaire);
            ViewData["IdSalesman"] = new SelectList(_context.Salesman, "Id", "Id", testDrive.IdSalesman);
            return View(testDrive);
        }

        // GET: TestDrives/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TestDrive == null)
            {
                return NotFound();
            }

            var testDrive = await _context.TestDrive
                .Include(t => t.Car)
                .Include(t => t.Client)
                .Include(t => t.Concessionaire)
                .Include(t => t.Salesman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testDrive == null)
            {
                return NotFound();
            }

            return View(testDrive);
        }

        // POST: TestDrives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TestDrive == null)
            {
                return Problem("Entity set 'ProjetoFinalCurso1500Context.TestDrive'  is null.");
            }
            var testDrive = await _context.TestDrive.FindAsync(id);
            if (testDrive != null)
            {
                _context.TestDrive.Remove(testDrive);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestDriveExists(string id)
        {
          return (_context.TestDrive?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
