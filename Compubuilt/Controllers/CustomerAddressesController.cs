using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Compubuilt.Models;
using Compubuilt.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Compubuilt.Controllers
{
    [Authorize]
    public class CustomerAddressesController : Controller
    {
        private readonly compubuiltContext _context;

        public CustomerAddressesController(compubuiltContext context)
        {
            _context = context;
        }

        // GET: CustomerAddresses
        public async Task<IActionResult> Index()
        {
            var compubuiltContext = _context.CustomerAddresses.Include(c => c.Customer);
            return View(await compubuiltContext.ToListAsync());
        }

        // GET: CustomerAddresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CustomerAddresses == null)
            {
                return NotFound();
            }

            var customerAddress = await _context.CustomerAddresses
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CustomerAddressId == id);
            if (customerAddress == null)
            {
                return NotFound();
            }

            return View(customerAddress);
        }

        // GET: CustomerAddresses/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return View();
        }

        // POST: CustomerAddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerAddressId,CustomerId,StreetName,StreetNumber,CityName,PostalCode")] CustomerAddress customerAddress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerAddress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerAddress.CustomerId);
            return View(customerAddress);
        }

        // GET: CustomerAddresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CustomerAddresses == null)
            {
                return NotFound();
            }

            var customerAddress = await _context.CustomerAddresses.FindAsync(id);
            if (customerAddress == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerAddress.CustomerId);
            return View(customerAddress);
        }

        // POST: CustomerAddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerAddressId,CustomerId,StreetName,StreetNumber,CityName,PostalCode")] CustomerAddress customerAddress)
        {
            if (id != customerAddress.CustomerAddressId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerAddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerAddressExists(customerAddress.CustomerAddressId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerAddress.CustomerId);
            return View(customerAddress);
        }

        // GET: CustomerAddresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CustomerAddresses == null)
            {
                return NotFound();
            }

            var customerAddress = await _context.CustomerAddresses
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CustomerAddressId == id);
            if (customerAddress == null)
            {
                return NotFound();
            }

            return View(customerAddress);
        }

        // POST: CustomerAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CustomerAddresses == null)
            {
                return Problem("Entity set 'compubuiltContext.CustomerAddresses'  is null.");
            }
            var customerAddress = await _context.CustomerAddresses.FindAsync(id);
            if (customerAddress != null)
            {
                _context.CustomerAddresses.Remove(customerAddress);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerAddressExists(int id)
        {
          return (_context.CustomerAddresses?.Any(e => e.CustomerAddressId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> SaveAddressChanges(CustomerAddressEditViewModel updatedCustomerAddress)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.EmailAddress == User.Identity.Name);

            var customerAddress = await _context.CustomerAddresses
                //.Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CustomerId == customer.CustomerId);

            if (customerAddress != null)
            {
                customerAddress.StreetName = updatedCustomerAddress.StreetName;
                customerAddress.StreetNumber = updatedCustomerAddress.StreetNumber;
                customerAddress.CityName = updatedCustomerAddress.CityName;
                customerAddress.PostalCode = updatedCustomerAddress.PostalCode;
                _context.Update(customerAddress);
            }
            else
            {
                var newCustomerAddress = new CustomerAddress
                {
                    StreetName = updatedCustomerAddress.StreetName,
                    StreetNumber = updatedCustomerAddress.StreetNumber,
                    CityName = updatedCustomerAddress.CityName,
                    PostalCode = updatedCustomerAddress.PostalCode,
                    CustomerId = customer.CustomerId
                };
                _context.Add(newCustomerAddress);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction(updatedCustomerAddress.Action, updatedCustomerAddress.Controller);
        }
    }
}
