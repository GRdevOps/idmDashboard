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
    public class ServerGroupModelsController : Controller
    {
        private readonly IDMDBContext _context;

        public ServerGroupModelsController(IDMDBContext context)
        {
            _context = context;
        }

        // GET: ServerGroupModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServerGroupModels.ToListAsync());
        }

        // GET: ServerGroupModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serverGroupModel = await _context.ServerGroupModels
                .FirstOrDefaultAsync(m => m.ServerGroupId == id);
            if (serverGroupModel == null)
            {
                return NotFound();
            }

            return View(serverGroupModel);
        }

        // GET: ServerGroupModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServerGroupModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServerGroupId,ServerGroupName")] ServerGroupModel serverGroupModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serverGroupModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serverGroupModel);
        }

        // GET: ServerGroupModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serverGroupModel = await _context.ServerGroupModels.FindAsync(id);
            if (serverGroupModel == null)
            {
                return NotFound();
            }
            return View(serverGroupModel);
        }

        // POST: ServerGroupModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServerGroupId,ServerGroupName")] ServerGroupModel serverGroupModel)
        {
            if (id != serverGroupModel.ServerGroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serverGroupModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServerGroupModelExists(serverGroupModel.ServerGroupId))
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
            return View(serverGroupModel);
        }

        // GET: ServerGroupModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serverGroupModel = await _context.ServerGroupModels
                .FirstOrDefaultAsync(m => m.ServerGroupId == id);
            if (serverGroupModel == null)
            {
                return NotFound();
            }

            return View(serverGroupModel);
        }

        // POST: ServerGroupModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serverGroupModel = await _context.ServerGroupModels.FindAsync(id);
            _context.ServerGroupModels.Remove(serverGroupModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServerGroupModelExists(int id)
        {
            return _context.ServerGroupModels.Any(e => e.ServerGroupId == id);
        }
    */
    }
}
