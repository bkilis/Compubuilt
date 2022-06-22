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
    public class ProductImagesController : Controller
    {
        private readonly compubuiltContext _context;

        public ProductImagesController(compubuiltContext context)
        {
            _context = context;
        }

        // GET: ProductImages
        public async Task<IActionResult> Index()
        {
            var compubuiltContext = _context.ProductImages.Include(p => p.Product).Include(p => p.ProductImageType);
            return View(await compubuiltContext.ToListAsync());
        }

        // GET: ProductImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductImages == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .Include(p => p.ProductImageType)
                .FirstOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // GET: ProductImages/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name");
            ViewData["ProductImageTypeId"] = new SelectList(_context.ProductImageTypes, "ProductImageTypeId", "Name");
            return View();
        }

        // POST: ProductImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductImageId,ProductId,ImageName,Url,ProductImageTypeId,IsActive,CreatedDate,CreatedBy,LastModifiedDate,LastModifiedBy")] ProductImage productImage)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(productImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productImage.ProductId);
            ViewData["ProductImageTypeId"] = new SelectList(_context.ProductImageTypes, "ProductImageTypeId", "Name", productImage.ProductImageTypeId);
            return View(productImage);
        }

        // GET: ProductImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductImages == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productImage.ProductId);
            ViewData["ProductImageTypeId"] = new SelectList(_context.ProductImageTypes, "ProductImageTypeId", "Name", productImage.ProductImageTypeId);
            return View(productImage);
        }

        // POST: ProductImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductImageId,ProductId,ImageName,Url,ProductImageTypeId,IsActive,CreatedDate,CreatedBy,LastModifiedDate,LastModifiedBy")] ProductImage productImage)
        {
            if (id != productImage.ProductImageId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(productImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImageExists(productImage.ProductImageId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "Name", productImage.ProductId);
            ViewData["ProductImageTypeId"] = new SelectList(_context.ProductImageTypes, "ProductImageTypeId", "Name", productImage.ProductImageTypeId);
            return View(productImage);
        }

        // GET: ProductImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductImages == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImages
                .Include(p => p.Product)
                .Include(p => p.ProductImageType)
                .FirstOrDefaultAsync(m => m.ProductImageId == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: ProductImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductImages == null)
            {
                return Problem("Entity set 'compubuiltContext.ProductImages'  is null.");
            }
            var productImage = await _context.ProductImages.FindAsync(id);
            if (productImage != null)
            {
                _context.ProductImages.Remove(productImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductImageExists(int id)
        {
          return (_context.ProductImages?.Any(e => e.ProductImageId == id)).GetValueOrDefault();
        }
    }
}
