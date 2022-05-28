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
    public class OrdersController : Controller
    {
        private readonly compubuiltContext _context;

        public OrdersController(compubuiltContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var compubuiltContext = _context.Orders.Include(o => o.Customer).Include(o => o.Delivery).Include(o => o.OrderStatusType).Include(o => o.Payment).Include(o => o.PromotionalCode);
            return View(await compubuiltContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Delivery)
                .Include(o => o.OrderStatusType)
                .Include(o => o.Payment)
                .Include(o => o.PromotionalCode)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "DeliveryId", "DeliveryId");
            ViewData["OrderStatusTypeId"] = new SelectList(_context.OrderStatusTypes, "OrderStatusTypeId", "OrderStatusTypeId");
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId");
            ViewData["PromotionalCodeId"] = new SelectList(_context.PromotionalCodeTypes, "PromotionalCodeTypeId", "PromotionalCodeTypeId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderNumber,PlacementDate,CompletionDate,IsDraft,OrderStatusTypeId,PaymentId,CustomerId,PromotionalCodeId,DeliveryId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "DeliveryId", "DeliveryId", order.DeliveryId);
            ViewData["OrderStatusTypeId"] = new SelectList(_context.OrderStatusTypes, "OrderStatusTypeId", "OrderStatusTypeId", order.OrderStatusTypeId);
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", order.PaymentId);
            ViewData["PromotionalCodeId"] = new SelectList(_context.PromotionalCodeTypes, "PromotionalCodeTypeId", "PromotionalCodeTypeId", order.PromotionalCodeId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "DeliveryId", "DeliveryId", order.DeliveryId);
            ViewData["OrderStatusTypeId"] = new SelectList(_context.OrderStatusTypes, "OrderStatusTypeId", "OrderStatusTypeId", order.OrderStatusTypeId);
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", order.PaymentId);
            ViewData["PromotionalCodeId"] = new SelectList(_context.PromotionalCodeTypes, "PromotionalCodeTypeId", "PromotionalCodeTypeId", order.PromotionalCodeId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderNumber,PlacementDate,CompletionDate,IsDraft,OrderStatusTypeId,PaymentId,CustomerId,PromotionalCodeId,DeliveryId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", order.CustomerId);
            ViewData["DeliveryId"] = new SelectList(_context.Deliveries, "DeliveryId", "DeliveryId", order.DeliveryId);
            ViewData["OrderStatusTypeId"] = new SelectList(_context.OrderStatusTypes, "OrderStatusTypeId", "OrderStatusTypeId", order.OrderStatusTypeId);
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", order.PaymentId);
            ViewData["PromotionalCodeId"] = new SelectList(_context.PromotionalCodeTypes, "PromotionalCodeTypeId", "PromotionalCodeTypeId", order.PromotionalCodeId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Delivery)
                .Include(o => o.OrderStatusType)
                .Include(o => o.Payment)
                .Include(o => o.PromotionalCode)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'compubuiltContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
