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
using UsingSearch.Models.ViewModels;

namespace Project.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private readonly ApplicationDataContext _context;

        public ItemsController(ApplicationDataContext context)
        {
            _context = context;
        }
        [Route("Yazan/osama/Majd/Ahmad")]
        public async Task<IActionResult> Index(string sortBy, string sortOrder)
        {
            // Fetch items from the database
            var items = _context.Item.AsQueryable();

            // Apply sorting based on the selected criteria
            if (sortBy == "Price")
            {
                items = sortOrder == "ASC" ? items.OrderBy(i => i.Price) : items.OrderByDescending(i => i.Price);
            }
            else if (sortBy == "ItamName")
            {
                items = sortOrder == "ASC" ? items.OrderBy(i => i.ItamName) : items.OrderByDescending(i => i.ItamName);
            }
            else if (sortBy == "Rate")
            {
                items = sortOrder == "ASC" ? items.OrderBy(i => i.Rate) : items.OrderByDescending(i => i.Rate);
            }

            // Return the sorted items
            return View(await items.ToListAsync());
        }

        public IActionResult Find()
        {
            return View(new Finditem());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Find([Bind("ItamName,Price,Rate")] Finditem finditem)
        {
            IQueryable<Item> query = _context.Item.AsQueryable();

            // البحث بالاسم إذا كان محددًا
            if (!string.IsNullOrEmpty(finditem?.ItamName))
            {
                query = query.Where(b => b.ItamName.Contains(finditem.ItamName));
            }

            // البحث بالسعر إذا كان محددًا
            if (finditem?.Price > 0)
            {
                query = query.Where(b => b.Price == finditem.Price);
            }

            // البحث بالتقييم إذا كان محددًا
            if (finditem?.Rate > 0)
            {
                query = query.Where(b => b.Rate == finditem.Rate);
            }

            // تنفيذ الاستعلام وترتيب النتائج
          

            finditem!.items = query;

            return View(finditem);
        }








        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItamName,Price,Rate,Status")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItamName,Price,Rate,Status")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            if (item != null)
            {
                _context.Item.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.ItemId == id);
        }

    }
}
