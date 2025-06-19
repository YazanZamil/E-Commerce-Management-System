using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    [Authorize]
    public class ShoppingBasketsController : Controller
    {
        private readonly ApplicationDataContext _context;

        public ShoppingBasketsController(ApplicationDataContext context)
        {
            _context = context;
        }
        
        // GET: ShoppingBaskets
        [Route("[controller]/[action]")] //token-based
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ShoppingBasket.Include(s => s.customer).Include(s => s.items);
            return View(await applicationDbContext.ToListAsync());
        }
        
        // GET: ShoppingBaskets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingBasket = await _context.ShoppingBasket
                .Include(s => s.customer)
                .Include(s => s.items)
                .FirstOrDefaultAsync(m => m.ShoppingBasketId == id);
            if (shoppingBasket == null)
            {
                return NotFound();
            }

            return View(shoppingBasket);
        }

        // GET: ShoppingBaskets/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName");
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItamName");
            return View();
        }

        // POST: ShoppingBaskets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShoppingBasketId,CustomerId,ItemId,quantity,ArrivedTime")] ShoppingBasket shoppingBasket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingBasket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", shoppingBasket.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItamName", shoppingBasket.ItemId);
            return View(shoppingBasket);
        }

        // GET: ShoppingBaskets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingBasket = await _context.ShoppingBasket.FindAsync(id);
            if (shoppingBasket == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", shoppingBasket.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItamName", shoppingBasket.ItemId);
            return View(shoppingBasket);
        }

        // POST: ShoppingBaskets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ShoppingBasketId,CustomerId,ItemId,quantity,ArrivedTime")] ShoppingBasket shoppingBasket)
        {
            if (id != shoppingBasket.ShoppingBasketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingBasket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingBasketExists(shoppingBasket.ShoppingBasketId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerName", shoppingBasket.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Item, "ItemId", "ItamName", shoppingBasket.ItemId);
            return View(shoppingBasket);
        }

        // GET: ShoppingBaskets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingBasket = await _context.ShoppingBasket
                .Include(s => s.customer)
                .Include(s => s.items)
                .FirstOrDefaultAsync(m => m.ShoppingBasketId == id);
            if (shoppingBasket == null)
            {
                return NotFound();
            }

            return View(shoppingBasket);
        }

        // POST: ShoppingBaskets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingBasket = await _context.ShoppingBasket.FindAsync(id);
            if (shoppingBasket != null)
            {
                _context.ShoppingBasket.Remove(shoppingBasket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingBasketExists(int id)
        {
            return _context.ShoppingBasket.Any(e => e.ShoppingBasketId == id);
        }
    }
}
