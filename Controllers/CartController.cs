using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaOnline.Data;
using TiendaOnline.Models;

namespace TiendaOnline.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.FindByEmailAsync(User.Identity.Name);        

            return View(await _context.Cart.Where(m => m.UserId == users.UserName).ToListAsync() );
        }

        public async Task<IActionResult> Add(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToCart(int id, string name, string picture, int price, int amount)
        {
            if (ModelState.IsValid)
            {                         
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);

                Cart cart = new Cart
                {
                    ProductId = id,
                    ProductName = name,
                    ProductPicture = picture, 
                    ProductPrice = price,
                    ProductTotal = price * amount,
                    ProductAmount = amount,
                    UserId = user.UserName
                };

                _context.Add(cart);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,ProductName,ProductPicture,ProductPrice,ProductAmount,ProductTotal,UserId")] Cart product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            else
            {
                product.ProductTotal = product.ProductPrice * product.ProductAmount;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(product.Id))
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
            return View(product);
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
