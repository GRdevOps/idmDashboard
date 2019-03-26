using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dashboard.Models;

namespace Dashboard.Controllers
{
    public class DriverModelsController : Controller
    {
        private readonly IDMDBContext _context;

        public DriverModelsController(IDMDBContext context)
        {
            _context = context;
        }


        // GET: DriverModels
        public async Task<IActionResult> Index()
        {
            var iDMDBContext = _context.DriverModels.Include(d => d.Server);
            return View(await iDMDBContext.ToListAsync());
        }

        // GET: DriverModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverModel = await _context.DriverModels
                .Include(d => d.Server)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driverModel == null)
            {
                return NotFound();
            }

            return View(driverModel);
        }

        // GET: DriverModels/Create
        public IActionResult Create()
        {
            ViewData["ServerId"] = new SelectList(_context.ServerModels, "ServerId", "ServerId");
            return View();
        }

        // POST: DriverModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriverId,DriverName,CurrentCache,PreviousCache,Status,ServerId")] DriverModel driverModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driverModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServerId"] = new SelectList(_context.ServerModels, "ServerId", "ServerId", driverModel.ServerId);
            return View(driverModel);
        }

        // GET: DriverModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverModel = await _context.DriverModels.FindAsync(id);
            if (driverModel == null)
            {
                return NotFound();
            }
            
            ViewData["ServerId"] = new SelectList(_context.ServerModels, "ServerId", "ServerId", driverModel.ServerId);
            return View(driverModel);
        }

        // POST: DriverModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DriverId,DriverName,CurrentCache,PreviousCache,Status,ServerId")] DriverModel driverModel)
        {
            if (id != driverModel.DriverId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldDriverModel = _context.DriverModels.Find(id);

                    oldDriverModel.PreviousCache = oldDriverModel.CurrentCache;
                    oldDriverModel.CurrentCache = driverModel.CurrentCache;

                    if (oldDriverModel.CurrentCache > oldDriverModel.PreviousCache)
                    {
                        oldDriverModel.Status = "Rising";
                    }
                    else if (oldDriverModel.CurrentCache < oldDriverModel.PreviousCache)
                    {
                        oldDriverModel.Status = "Lowering";
                    }
                    else
                    {
                        oldDriverModel.Status = "Unchanged";
                    }
                    
                    _context.Update(oldDriverModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverModelExists(driverModel.DriverId))
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
            ViewData["ServerId"] = new SelectList(_context.ServerModels, "ServerId", "ServerId", driverModel.ServerId);
            return View(driverModel);
        }

        // GET: DriverModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driverModel = await _context.DriverModels
                .Include(d => d.Server)
                .FirstOrDefaultAsync(m => m.DriverId == id);
            if (driverModel == null)
            {
                return NotFound();
            }

            return View(driverModel);
        }

        // POST: DriverModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driverModel = await _context.DriverModels.FindAsync(id);
            _context.DriverModels.Remove(driverModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
        private bool DriverModelExists(int id)
        {
            return _context.DriverModels.Any(e => e.DriverId == id);
        }

    }
}
