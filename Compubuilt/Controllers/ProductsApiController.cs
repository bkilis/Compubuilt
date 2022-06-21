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
        public async Task<ActionResult<IEnumerable<ProductApiModel>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }

          var response = new List<ProductApiModel>();
          foreach (var product in _context.Products)
          {
              response.Add(new ProductApiModel
              {
                  ProductId = product.ProductId,
                  Name = product.Name,
                  Description = product.Description,
                  Quantity = product.Quantity,
                  Price = product.Price,
                  ProductCategoryId = product.ProductCategoryId,
                  AverageRatingValue = product.AverageRatingValue
              });
          }

          return response;
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
        public async Task<ActionResult<ProductApiModel>> GetProduct(int id)
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

            var response = new ProductApiModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity,
                Price = product.Price,
                ProductCategoryId = product.ProductCategoryId,
                AverageRatingValue = product.AverageRatingValue
            };

            return response;
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
        public async Task<IActionResult> PutProduct(int id, ProductApiModel request)
        {
            if (id != request.ProductId)
            {
                return BadRequest("Passed Ids don't match.");
            }

            var product = _context.Products.Find(id);
            bool newEntry = false;

            var validationResult = ValidateProductApiModelRequestAsync(request);
            if(validationResult.Result != null)
                return BadRequest(validationResult.Result);

            if (product == null)
            {
                product = new Product()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Quantity = request.Quantity,
                    Price = request.Price,
                    ProductCategoryId = request.ProductCategoryId,
                    AverageRatingValue = request.AverageRatingValue
                };
                _context.Entry(product).State = EntityState.Added;
                newEntry = true;
            }
            else
            {
                product.Name = request.Name;
                product.Description = request.Description;
                product.Quantity = request.Quantity;
                product.Price = request.Price;
                product.ProductCategoryId = request.ProductCategoryId;
                product.AverageRatingValue = request.AverageRatingValue;
                _context.Entry(product).State = EntityState.Modified;
            }

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

            if (newEntry)
                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);

            return NoContent();
        }

        // POST: api/ProductsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductApiModel request)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'compubuiltContext.Products'  is null.");
            }

            var validationResult = ValidateProductApiModelRequestAsync(request);
            if (validationResult.Result != null)
                return BadRequest(validationResult.Result);

            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Quantity = request.Quantity,
                Price = request.Price,
                ProductCategoryId = request.ProductCategoryId,
                AverageRatingValue = request.AverageRatingValue
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            request.ProductId = product.ProductId;

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, request);
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

        private async Task<string?> ValidateProductApiModelRequestAsync(ProductApiModel request)
        {
            var validationResult = request.Validate();
            if (validationResult != null)
                return validationResult;

            var productCategory = await _context.ProductCategories.FindAsync(request.ProductCategoryId);

            if (productCategory == null)
                return "Invalid Product Category";

            return null;
        }

    }
}
