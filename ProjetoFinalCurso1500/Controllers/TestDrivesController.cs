using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCurso1500.Data;
using ProjetoFinalCurso1500.Models;

namespace ProjetoFinalCurso1500.Controllers
{
    [Authorize]
    public class TestDrivesController : Controller
    {
        private readonly ProjetoFinalCurso1500Context _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public TestDrivesController(ProjetoFinalCurso1500Context context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;

        }

        // GET: TestDrives
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var userClaims = await _userManager.GetClaimsAsync(user);


            var testDrive = _context.TestDrive.
                                              Include(t => t.Car)
                                              .Include(t => t.Client).ThenInclude(t => t.User)
                                              .Include(t => t.Concessionaire)
                                              .Include(t => t.Salesman).ThenInclude(t => t.User);
            if (userClaims.Any(c => c.Type == "userType" && c.Value == "Client")) {


                var client = HttpContext.Session.GetString("ClientId");
                if (client != null)
                    testDrive.Where(c => c.IdClient == client);
            }

            return View(await testDrive.ToListAsync());
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
                .Include(t => t.Client).ThenInclude(t=>t.User)
                .Include(t => t.Concessionaire)
                .Include(t => t.Salesman).ThenInclude(t=>t.User)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (testDrive == null)
            {
                return NotFound();
            }

            return View(testDrive);
        }

        // GET: TestDrives/Create
        public async Task<IActionResult> Create()
        {

            var clients = await ClientsBasedOnPermission();
            ViewData["IdClient"] = new SelectList(clients, "Id", "User.Name");
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name");
            return View();
        }
        [NonAction]
        public async Task<IEnumerable<Client>> ClientsBasedOnPermission()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var userClaims = await _userManager.GetClaimsAsync(user);
            var clients = _context.Client.Include(c => c.User);
            if (userClaims.Any(c => c.Type == "userType" && c.Value == "Client"))
            {
                var client = HttpContext.Session.GetString("ClientId");

                clients.Where(c => c.Id == client);
            }
            return clients;
        }

        // POST: TestDrives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Date,IdSalesman,IdCar,IdConcessionaire,IdClient")] TestDriveDTO testDriveDTO)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var userClaims = await _userManager.GetClaimsAsync(user);
            var clientId = HttpContext.Session.GetString("ClientId");
            if (userClaims.Any(c => c.Type == "userType" && c.Value == "Client") && testDriveDTO.IdClient != clientId)
            {
                ModelState.AddModelError("Client invalid", "Não foi possível validar o cliente selecionado!");
            }

            if (ModelState.IsValid)
            {
                if (await _context.TestDrive.AnyAsync(c => c.Date >= testDriveDTO.Date.AddHours(-1) && c.Date<= testDriveDTO.Date.AddHours(1) && c.IdSalesman == testDriveDTO.IdSalesman))
                {
                    ModelState.AddModelError("Date invalid", $"O vendedor selecionado já possui testdrives agendados entre os horários {testDriveDTO.Date} e {testDriveDTO.Date.AddHours(1)} por favor selecione outros horários ou vendedor");
                    goto done;
                }



                var testDrive = _mapper.Map<TestDrive>(testDriveDTO);
                testDrive.Id = Guid.NewGuid().ToString();
                var concessionaire = _context.Concessionaires.Include(c=>c.Salesmans).Include(c=>c.Clients).FirstOrDefault(c=>c.Id==testDriveDTO.IdConcessionaire);
                var client = _context.Client.Include(c=>c.Concessionairies).FirstOrDefault(c=>c.Id==testDriveDTO.IdClient);

                if (client == null || concessionaire==null )
                {
                    goto done;
                }

       
                
                if ( !concessionaire.Clients.Any(c=>c == client))
                {
                    concessionaire.Clients.Add(client);
                    client.Concessionairies.Add(concessionaire);
                }

                testDrive.Client = client;
                testDrive.Concessionaire = concessionaire;
  
                _context.Add(testDrive);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            done:
  
            ViewData["IdClient"] = new SelectList(await ClientsBasedOnPermission(), "Id", "User.Name", testDriveDTO.IdClient);
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name", testDriveDTO.IdConcessionaire);
            return View(testDriveDTO);
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
            ViewData["IdClient"] = new SelectList(await ClientsBasedOnPermission(), "Id", "User.Name", testDrive.IdClient);
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name", testDrive.IdConcessionaire);
            return View(_mapper.Map<TestDriveDTO>(testDrive));
        }

        // POST: TestDrives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Date,IdSalesman,IdCar,IdConcessionaire,IdClient,Completed")] TestDriveDTO testDriveDTO)
        {
            var testDrive = await _context.TestDrive.FindAsync(id);
            if (testDrive==null)
            {
                return NotFound();
            }




            if (ModelState.IsValid)
            {
                try
                {
                    if (await _context.TestDrive.AnyAsync(c => c.Date >= testDriveDTO.Date.AddHours(-1) && c.Date <= testDriveDTO.Date.AddHours(1) && c.IdSalesman == testDriveDTO.IdSalesman && c.Id !=id))
                    {
                        ModelState.AddModelError("Date invalid", $"O vendedor selecionado já possui testdrives agendados entre os horários {testDriveDTO.Date} e {testDriveDTO.Date.AddHours(1)} por favor selecione outros horários ou vendedor");
                        goto done;
                    }


                    _mapper.Map<TestDriveDTO, TestDrive>(testDriveDTO,testDrive );

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
            done:
            ViewData["IdCar"] = new SelectList(_context.Car, "Id", "Model", testDrive.IdCar);
            ViewData["IdClient"] = new SelectList(await ClientsBasedOnPermission(), "Id", "User.Name", testDrive.IdClient);
            ViewData["IdConcessionaire"] = new SelectList(_context.Concessionaires, "Id", "Name", testDrive.IdConcessionaire);
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
