using Compubuilt.Enums;
using Compubuilt.Extensions;
using Compubuilt.Models;
using Compubuilt.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Compubuilt.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly compubuiltContext _context;

        public ShoppingCartController(compubuiltContext context)
        {
            _context = context;
        }
        // GET: ShoppingCartController
        public IActionResult Index()
        {
            var shoppingCart = HttpContext.Session.Get<ShoppingCartSessionModel>("cartContents");

            if (shoppingCart == null || shoppingCart.Items.Count == 0)
                return View(new ShoppingCartViewModel());

            var products = _context.Products
                .Include(p => p.ProductImages)
                .Where(t => shoppingCart.Items.Select(i => i.ProductId).Contains(t.ProductId))
                .ToList();

            var shoppingCartViewModel = new ShoppingCartViewModel();
            //shoppingCartViewModel.AppliedPromotionalCode = shoppingCart.

            foreach (var product in products)
            {
                var quantity = shoppingCart.Items
                    .Where(i => i.ProductId == product.ProductId)
                    .Select(i => i.Quantity).First();

                shoppingCartViewModel.Items.Add(new ShoppingCartItem
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                    TotalPrice = product.Price * quantity,
                    RatingValue = product.AverageRatingValue,
                    ProductPhotoUrl = product.ProductImages
                        .Where(pi => pi.ProductImageTypeId == (int)ProductImageTypeEnum.BasketThumbnail)
                        .Select(pi => pi.Url).FirstOrDefault()
                });

                shoppingCartViewModel.TotalValue += product.Price;
            }

            return View(shoppingCartViewModel);
        }

        public IActionResult AddItemToCart(int? id, int? quantity)
        {
            if (id == null || quantity == null || quantity < 1)
                return NotFound();

            byte[] cartId;

            if (!HttpContext.Session.TryGetValue("cartId", out cartId))
            {
                HttpContext.Session.SetString("cartId", Guid.NewGuid().ToString());
                HttpContext.Session.Set("cartContents", new ShoppingCartSessionModel
                {
                    Items = new List<ShoppingCartSessionItem>
                    {
                        new()
                        {
                            ProductId = id.Value,
                            Quantity = quantity.Value
                        }
                    }
                });
            }
            else //if item alreay is in the cart quantity ++
            {
                var shoppingCart = HttpContext.Session.Get<ShoppingCartSessionModel>("cartContents");

                var item = shoppingCart?.Items.FirstOrDefault(i => i.ProductId == id.Value);

                if (item == null)
                {
                    shoppingCart?.Items.Add(new ShoppingCartSessionItem
                    {
                        ProductId = id.Value,
                        Quantity = quantity.Value
                    });
                }
                else
                {
                    item.Quantity += quantity.Value;
                }

                HttpContext.Session.Set("cartContents", shoppingCart);
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: ShoppingCartController/Delete/5
        public IActionResult Delete(int id)
        {
            byte[] cartId;

            if (!HttpContext.Session.TryGetValue("cartId", out cartId))
            {
                return NotFound();
            }
            else
            {
                var shoppingCart = HttpContext.Session.Get<ShoppingCartSessionModel>("cartContents");

                var item = shoppingCart?.Items.FirstOrDefault(i => i.ProductId == id);

                if (item != null)
                {
                    var itemIndex = shoppingCart.Items.FindIndex(i => i.ProductId == id);
                    shoppingCart.Items.RemoveAt(itemIndex);
                }

                HttpContext.Session.Set("cartContents", shoppingCart);
            }

            return RedirectToAction(nameof(Index));
        }

        //// GET: ShoppingCartController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ShoppingCartController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ShoppingCartController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ShoppingCartController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ShoppingCartController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}



        //// POST: ShoppingCartController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
