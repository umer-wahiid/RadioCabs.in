using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RadioCabs.Models;

namespace RadioCabs.Controllers
{
    public class FeedBacksController : Controller
    {
        private readonly RCDbContext _context;

        public FeedBacksController(RCDbContext context)
        {
            _context = context;
        }

        // GET: FeedBacks
        public async Task<IActionResult> Index()
        {
              return _context.FeedBacks != null ? 
                          View(await _context.FeedBacks.ToListAsync()) :
                          Problem("Entity set 'RCDbContext.FeedBacks'  is null.");
        }

        // GET: FeedBacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FeedBacks == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBacks
                .FirstOrDefaultAsync(m => m.FeedId == id);
            if (feedBack == null)
            {
                return NotFound();
            }

            return View(feedBack);
        }

        // GET: FeedBacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FeedBacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeedBack feedBack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feedBack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: FeedBacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FeedBacks == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBacks.FindAsync(id);
            if (feedBack == null)
            {
                return NotFound();
            }
            return View(feedBack);
        }

        // POST: FeedBacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FeedId,Name,MobileNo,Email,City,Type,Description")] FeedBack feedBack)
        {
            if (id != feedBack.FeedId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feedBack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedBackExists(feedBack.FeedId))
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
            return View(feedBack);
        }

        // GET: FeedBacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FeedBacks == null)
            {
                return NotFound();
            }

            var feedBack = await _context.FeedBacks
                .FirstOrDefaultAsync(m => m.FeedId == id);
            if (feedBack == null)
            {
                return NotFound();
            }

            return View(feedBack);
        }

        // POST: FeedBacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FeedBacks == null)
            {
                return Problem("Entity set 'RCDbContext.FeedBacks'  is null.");
            }
            var feedBack = await _context.FeedBacks.FindAsync(id);
            if (feedBack != null)
            {
                _context.FeedBacks.Remove(feedBack);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedBackExists(int id)
        {
          return (_context.FeedBacks?.Any(e => e.FeedId == id)).GetValueOrDefault();
        }
    }
}
