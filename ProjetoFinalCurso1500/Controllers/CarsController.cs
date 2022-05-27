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
    [Authorize]
    public class CarsController : Controller
    {
        private readonly ProjetoFinalCurso1500Context _context;
        private readonly IMapper _mapper;
        public CarsController(ProjetoFinalCurso1500Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [Authorize(Policy = "SalesmanAllowed")]

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var projetoFinalCurso1500Context = _context.Car.Include(c => c.Concessionaire);
            return View(await projetoFinalCurso1500Context.ToListAsync());
        }
        [Authorize(Policy = "SalesmanAllowed")]

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.Concessionaire)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [Authorize]
        public List<SelectListItem> GetCarsForConcessionaire(string idConcessionaire)
        {
   
            return _context.Car.Where(c => c.IdConcessionaire == idConcessionaire).Select(c=>new SelectListItem {Value=c.Id,Text=c.Model}).ToList();
        }
        [Authorize(Policy = "SalesmanAllowed")]

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name");
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "SalesmanAllowed")]

        public async Task<IActionResult> Create([Bind("Model,Price,Amount,IdConcessionaire")] CarDTO carDTO)
        {
            if (ModelState.IsValid)
            {
                var car = _mapper.Map<Car>(carDTO);
                car.Id = Guid.NewGuid().ToString();
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name", carDTO.IdConcessionaire);
            return View(carDTO);
        }

        // GET: Cars/Edit/5
        [Authorize(Policy = "SalesmanAllowed")]

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name", car.IdConcessionaire);
            return View(_mapper.Map<CarDTO>(car));
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "SalesmanAllowed")]

        public async Task<IActionResult> Edit(string id, [Bind("Model,Price,Amount,IdConcessionaire")] CarDTO carDTO)
        {
            var car = _context.Car.Find(id);
            if (car ==null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _mapper.Map<CarDTO, Car>(carDTO, car);
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(id))
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
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name", carDTO.IdConcessionaire);
            return View(car);
        }
        [Authorize(Policy = "SalesmanAllowed")]
        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Car == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.Concessionaire)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "SalesmanAllowed")]

        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Car == null)
            {
                return Problem("Entity set 'ProjetoFinalCurso1500Context.Car'  is null.");
            }
            var car = await _context.Car.FindAsync(id);
            if (car != null)
            {
                _context.Car.Remove(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [NonAction]

        private bool CarExists(string id)
        {
          return (_context.Car?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
