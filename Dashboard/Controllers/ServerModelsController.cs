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
    public class ServerModelsController : Controller
    {
        private readonly IDMDBContext _context;

        public ServerModelsController(IDMDBContext context)
        {
            _context = context;
        }

        // GET: ServerModels
        public async Task<IActionResult> Index()
        {
            var iDMDBContext = _context.ServerModels.Include(s => s.ServerGroup);
            return View(await iDMDBContext.ToListAsync());
        }

        // GET: ServerModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serverModel = await _context.ServerModels
                .Include(s => s.ServerGroup)
                .FirstOrDefaultAsync(m => m.ServerId == id);
            if (serverModel == null)
            {
                return NotFound();
            }

            return View(serverModel);
        }

        // GET: ServerModels/Create
        
        public IActionResult Create()
        {
            ViewData["ServerGroupId"] = new SelectList(_context.ServerGroupModels, "ServerGroupId", "ServerGroupId");
            return View();
        }

        // POST: ServerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServerId,ServerName,ServerGroupId")] ServerModel serverModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serverModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServerGroupId"] = new SelectList(_context.ServerGroupModels, "ServerGroupId", "ServerGroupId", serverModel.ServerGroupId);
            return View(serverModel);
        }

        // GET: ServerModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serverModel = await _context.ServerModels.FindAsync(id);
            if (serverModel == null)
            {
                return NotFound();
            }
            ViewData["ServerGroupId"] = new SelectList(_context.ServerGroupModels, "ServerGroupId", "ServerGroupId", serverModel.ServerGroupId);
            return View(serverModel);
        }

        // POST: ServerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServerId,ServerName,ServerGroupId")] ServerModel serverModel)
        {
            if (id != serverModel.ServerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serverModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServerModelExists(serverModel.ServerId))
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
            ViewData["ServerGroupId"] = new SelectList(_context.ServerGroupModels, "ServerGroupId", "ServerGroupId", serverModel.ServerGroupId);
            return View(serverModel);
        }

        // GET: ServerModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serverModel = await _context.ServerModels
                .Include(s => s.ServerGroup)
                .FirstOrDefaultAsync(m => m.ServerId == id);
            if (serverModel == null)
            {
                return NotFound();
            }

            return View(serverModel);
        }

        // POST: ServerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serverModel = await _context.ServerModels.FindAsync(id);
            _context.ServerModels.Remove(serverModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        

        private bool ServerModelExists(int id)
        {
            return _context.ServerModels.Any(e => e.ServerId == id);
        }
    }
}
