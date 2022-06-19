using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Compubuilt.Models;

namespace Compubuilt.Controllers
{
    public class PromotionalCodesController : Controller
    {
        private readonly compubuiltContext _context;

        public PromotionalCodesController(compubuiltContext context)
        {
            _context = context;
        }

        // GET: PromotionalCodes
        public async Task<IActionResult> Index()
        {
              return _context.PromotionalCodes != null ? 
                          View(await _context.PromotionalCodes.ToListAsync()) :
                          Problem("Entity set 'compubuiltContext.PromotionalCodes'  is null.");
        }

        // GET: PromotionalCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PromotionalCodes == null)
            {
                return NotFound();
            }

            var promotionalCode = await _context.PromotionalCodes
                .FirstOrDefaultAsync(m => m.PromotionalCodeId == id);
            if (promotionalCode == null)
            {
                return NotFound();
            }

            return View(promotionalCode);
        }

        // GET: PromotionalCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PromotionalCodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PromotionalCodeId,Name,Code,ValidFrom,ValidTo,UseLimitPerUser,DiscountValue")] PromotionalCode promotionalCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(promotionalCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(promotionalCode);
        }

        // GET: PromotionalCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PromotionalCodes == null)
            {
                return NotFound();
            }

            var promotionalCode = await _context.PromotionalCodes.FindAsync(id);
            if (promotionalCode == null)
            {
                return NotFound();
            }
            return View(promotionalCode);
        }

        // POST: PromotionalCodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PromotionalCodeId,Name,Code,ValidFrom,ValidTo,UseLimitPerUser,DiscountValue")] PromotionalCode promotionalCode)
        {
            if (id != promotionalCode.PromotionalCodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promotionalCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotionalCodeExists(promotionalCode.PromotionalCodeId))
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
            return View(promotionalCode);
        }

        // GET: PromotionalCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PromotionalCodes == null)
            {
                return NotFound();
            }

            var promotionalCode = await _context.PromotionalCodes
                .FirstOrDefaultAsync(m => m.PromotionalCodeId == id);
            if (promotionalCode == null)
            {
                return NotFound();
            }

            return View(promotionalCode);
        }

        // POST: PromotionalCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PromotionalCodes == null)
            {
                return Problem("Entity set 'compubuiltContext.PromotionalCodes'  is null.");
            }
            var promotionalCode = await _context.PromotionalCodes.FindAsync(id);
            if (promotionalCode != null)
            {
                _context.PromotionalCodes.Remove(promotionalCode);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PromotionalCodeExists(int id)
        {
          return (_context.PromotionalCodes?.Any(e => e.PromotionalCodeId == id)).GetValueOrDefault();
        }
    }
}
