using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Compubuilt.Models;
using Compubuilt.ApiModels;

namespace Compubuilt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsApiController : ControllerBase
    {
        private readonly compubuiltContext _context;

        public ProductsApiController(compubuiltContext context)
        {
            _context = context;
        }

        // GET: api/ProductsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
          return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// Returns products and their stock availability
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProductsStock/")]
        public async Task<ActionResult<GetProductsStockResponse>> GetProductsStock()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.ToListAsync();

            var response = new GetProductsStockResponse();
            foreach (var product in products)
            {
                response.Stocks.Add(new ProductStock
                {
                    Name = product.Name,
                    ProductId = product.ProductId,
                    Quantity = product.Quantity
                });
            }

            return response;
        }

        // GET: api/ProductsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
          var product = await _context.Products.FindAsync(id);

          if (product == null)
          {
              return NotFound();
          }

          return product;
        }

        // GET: api/ProductsApi/5
        [HttpGet("GetProductNameAndQuantity/{id}")]
        public async Task<ActionResult<GetProductNameAndQuantityResponse>> GetProductNameAndQuantity(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var response = new GetProductNameAndQuantityResponse
            {
                Name = product.Name,
                Quantity = product.Quantity
            };

            return response;
        }

        // PUT: api/ProductsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'compubuiltContext.Products'  is null.");
          }
          _context.Products.Add(product);
          await _context.SaveChangesAsync();

          return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/ProductsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

    }
}
