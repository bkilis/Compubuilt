using Compubuilt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Compubuilt.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly compubuiltContext _context;

        public HomeController(ILogger<HomeController> logger, compubuiltContext context)
        {
            _logger = logger;
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            GetOrCreateCustomer();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetLoggedUserEmail() => User.Identity.Name;

        private async Task GetOrCreateCustomer()
        {
            var azureUserClaims = User.Claims.ToList();

            var email = User.Identity.Name;
            var name = azureUserClaims
                ?.FirstOrDefault(auc => auc.Type.Equals("name", StringComparison.OrdinalIgnoreCase))?.Value;
            var azureObjectId = azureUserClaims
                ?.FirstOrDefault(auc => auc.Type.Equals("http://schemas.microsoft.com/identity/claims/objectidentifier", StringComparison.OrdinalIgnoreCase))?.Value;

            await GetOrCreateCustomerFromDb(email, name, azureObjectId);
        }

        private async Task GetOrCreateCustomerFromDb(string email, string name, string azureObjectId)
        {
            var existingUser = _context.Customers.FirstOrDefaultAsync(c => c.AzureAdsid == Encoding.ASCII.GetBytes(azureObjectId));

            if (existingUser.Result != null) 
                return;

            var newCustomer = new Customer()
            {
                FirstName = name,
                EmailAddress = email,
                AzureAdsid = Encoding.ASCII.GetBytes(azureObjectId)
            };

             await _context.AddAsync(newCustomer);
             await _context.SaveChangesAsync();
        }
    }
}