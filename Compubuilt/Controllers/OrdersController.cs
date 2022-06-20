using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Compubuilt.Enums;
using Compubuilt.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Compubuilt.Models;
using Compubuilt.ViewModels;
using Microsoft.Identity.Client;

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
            var compubuiltContext = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Delivery)
                .Include(o => o.OrderStatusType)
                .Include(o => o.Payment)
                .Include(o => o.PromotionalCode);
            return View(await compubuiltContext.ToListAsync());
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public async Task<IActionResult> PlaceOrder(int SelectedOrderDeliveryTypeId, int SelectedPaymentTypeId)
        {
            //TODO: zaktualizować po podłączeniu do AAD
            var userId = 1;
            var userAddressId = 1;

            var shoppingCartSession = HttpContext.Session.Get<ShoppingCartSessionModel>("cartContents");
            if (shoppingCartSession == null)
                return NotFound();

            var products = _context.Products
                .Where(t => shoppingCartSession.Items.Select(i => i.ProductId).Contains(t.ProductId))
                .ToList();

            if (!products.Any())
                return NotFound();

            var selectedDeliveryType = _context.DeliveryTypes.FirstOrDefault(dt => dt.DeliveryTypeId == SelectedOrderDeliveryTypeId);
            var selectedPaymentType = _context.PaymentTypes.FirstOrDefault(dt => dt.PaymentTypeId == SelectedPaymentTypeId);

            var order = new Order();
            order.OrderNumber = DateTime.Now.ToString("dd-MM-yy") + "_" + Guid.NewGuid();
            order.PlacementDate = DateTime.Now;
            order.OrderStatusTypeId = (int)OrderStatusTypeEnum.New;
            order.CustomerId = userId;
            order.AddressId = userAddressId;

            if (!string.IsNullOrWhiteSpace(shoppingCartSession.AppliedPromotionalCode))
            {
                var promotionalCode = _context.PromotionalCodes.FirstOrDefault(pc =>
                    pc.Code == shoppingCartSession.AppliedPromotionalCode.ToLower() && pc.ValidFrom < DateTime.Now &&
                    pc.ValidTo > DateTime.Now);
                if (promotionalCode != null)
                {
                    order.PromotionalCodeId = promotionalCode.PromotionalCodeId;
                }
            }

            _context.Add(order);
            await _context.SaveChangesAsync();

            foreach (var product in products)
            {
                
            }


            return View();
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
