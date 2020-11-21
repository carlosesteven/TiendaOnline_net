using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create(int? id)
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
        public async Task<IActionResult> AddToCart(int id, int amount)
        {
            if (ModelState.IsValid)
            {
                CartObj obj = new CartObj();

                var product = await _context.Product
                    .FirstOrDefaultAsync(m => m.Id == id);

                obj.Amount = amount;
                obj.Product = product;

                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                Cart cart = new Cart();

                cart.Products = new List<CartObj>
                {
                    obj
                };
                cart.User = userName;

                _context.Add(cart);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

    }
}
