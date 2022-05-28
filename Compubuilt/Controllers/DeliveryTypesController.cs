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
    public class DeliveryTypesController : Controller
    {
        private readonly compubuiltContext _context;

        public DeliveryTypesController(compubuiltContext context)
        {
            _context = context;
        }

        // GET: DeliveryTypes
        public async Task<IActionResult> Index()
        {
              return _context.DeliveryTypes != null ? 
                          View(await _context.DeliveryTypes.ToListAsync()) :
                          Problem("Entity set 'compubuiltContext.DeliveryTypes'  is null.");
        }

        // GET: DeliveryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryTypes == null)
            {
                return NotFound();
            }

            var deliveryType = await _context.DeliveryTypes
                .FirstOrDefaultAsync(m => m.DeliveryTypeId == id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            return View(deliveryType);
        }

        // GET: DeliveryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryTypeId,DeliveryTypeName")] DeliveryType deliveryType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryType);
        }

        // GET: DeliveryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DeliveryTypes == null)
            {
                return NotFound();
            }

            var deliveryType = await _context.DeliveryTypes.FindAsync(id);
            if (deliveryType == null)
            {
                return NotFound();
            }
            return View(deliveryType);
        }

        // POST: DeliveryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryTypeId,DeliveryTypeName")] DeliveryType deliveryType)
        {
            if (id != deliveryType.DeliveryTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryTypeExists(deliveryType.DeliveryTypeId))
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
            return View(deliveryType);
        }

        // GET: DeliveryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeliveryTypes == null)
            {
                return NotFound();
            }

            var deliveryType = await _context.DeliveryTypes
                .FirstOrDefaultAsync(m => m.DeliveryTypeId == id);
            if (deliveryType == null)
            {
                return NotFound();
            }

            return View(deliveryType);
        }

        // POST: DeliveryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeliveryTypes == null)
            {
                return Problem("Entity set 'compubuiltContext.DeliveryTypes'  is null.");
            }
            var deliveryType = await _context.DeliveryTypes.FindAsync(id);
            if (deliveryType != null)
            {
                _context.DeliveryTypes.Remove(deliveryType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryTypeExists(int id)
        {
          return (_context.DeliveryTypes?.Any(e => e.DeliveryTypeId == id)).GetValueOrDefault();
        }
    }
}
