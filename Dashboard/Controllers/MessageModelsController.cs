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
    public class MessageModelsController : Controller
    {
        private readonly IDMDBContext _context;

        public MessageModelsController(IDMDBContext context)
        {
            _context = context;
        }

        // GET: MessageModels
        public async Task<IActionResult> Index()
        {
            var iDMDBContext = _context.MessageModels.Include(m => m.Driver).Include(m => m.Server);
            return View(await iDMDBContext.ToListAsync());
        }

        // GET: MessageModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageModel = await _context.MessageModels
                .Include(m => m.Driver)
                .Include(m => m.Server)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (messageModel == null)
            {
                return NotFound();
            }

            return View(messageModel);
        }

        // GET: MessageModels/Create
        public IActionResult Create()
        {
            ViewData["DriverId"] = new SelectList(_context.DriverModels, "DriverId", "DriverId");
            ViewData["ServerId"] = new SelectList(_context.ServerModels, "ServerId", "ServerId");
            return View();
        }

        // POST: MessageModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MessageId,MessageType,MessageDate,Message,ServerId,DriverId")] MessageModel messageModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messageModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverId"] = new SelectList(_context.DriverModels, "DriverId", "DriverId", messageModel.DriverId);
            ViewData["ServerId"] = new SelectList(_context.ServerModels, "ServerId", "ServerId", messageModel.ServerId);
            return View(messageModel);
        }

        // GET: MessageModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageModel = await _context.MessageModels.FindAsync(id);
            if (messageModel == null)
            {
                return NotFound();
            }
            ViewData["DriverId"] = new SelectList(_context.DriverModels, "DriverId", "DriverId", messageModel.DriverId);
            ViewData["ServerId"] = new SelectList(_context.ServerModels, "ServerId", "ServerId", messageModel.ServerId);
            return View(messageModel);
        }

        // POST: MessageModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MessageId,MessageType,MessageDate,Message,ServerId,DriverId")] MessageModel messageModel)
        {
            if (id != messageModel.MessageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageModelExists(messageModel.MessageId))
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
            ViewData["DriverId"] = new SelectList(_context.DriverModels, "DriverId", "DriverId", messageModel.DriverId);
            ViewData["ServerId"] = new SelectList(_context.ServerModels, "ServerId", "ServerId", messageModel.ServerId);
            return View(messageModel);
        }

        // GET: MessageModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messageModel = await _context.MessageModels
                .Include(m => m.Driver)
                .Include(m => m.Server)
                .FirstOrDefaultAsync(m => m.MessageId == id);
            if (messageModel == null)
            {
                return NotFound();
            }

            return View(messageModel);
        }

        // POST: MessageModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messageModel = await _context.MessageModels.FindAsync(id);
            _context.MessageModels.Remove(messageModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageModelExists(int id)
        {
            return _context.MessageModels.Any(e => e.MessageId == id);
        }
    }
}
