using System;
using System.Linq;
using System.Threading.Tasks;
using crm.DataAccess.Data;
using crm.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crm.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrenciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string currencySearch = null, string currencySort = null, bool descendingSort = false)
        {
            ViewData["DescendingSort"] = descendingSort;
            ViewData["SearchCurrency"] = currencySearch;
            ViewData["SortCurrency"] = currencySort;

            var currencies = _context.Currencies.Select(currency => currency);

            if (!string.IsNullOrEmpty(currencySearch))
            {
                currencies = currencies.Where(currency => 
                    currency.Name.ToLower().Contains(currencySearch.ToLower()) ||
                    currency.Symbol.ToLower().Contains(currencySearch.ToLower()));
            }

            if (!string.IsNullOrEmpty(currencySort))
            {
                currencies = descendingSort 
                    ? currencies.OrderByDescending(currency => EF.Property<object>(currency, currencySort)) 
                    : currencies.OrderBy(currency => EF.Property<object>(currency, currencySort));
            }

            return View(await currencies.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Symbol,Name,IsSync")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                currency.CreatedAt = DateTime.Now;
                currency.UpdatedAt = DateTime.Now;
                _context.Add(currency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Symbol,Name,IsSync")] Currency currency)
        {
            if (id != currency.Id)
            {
                return NotFound();
            }

            var currencyFromDb = await _context.Currencies.FindAsync(id);
            if (currencyFromDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    currencyFromDb.Name = currency.Name;
                    currencyFromDb.IsSync = currency.IsSync;
                    currencyFromDb.UpdatedAt = DateTime.Now;
                    _context.Update(currencyFromDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currency.Id))
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
            return View(currency);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            
            if (currency == null)
            {
                return NotFound();
            }

            try
            {
                currency.UpdatedAt = DateTime.Now;
                currency.Ghosted = true;
                _context.Update(currency);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(currency.Id))
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

        public async Task<IActionResult> SymbolIsUnique(string symbol, int id)
        {
            bool currencyExists = await _context.Currencies.AnyAsync(currency => currency.Id == id);

            if (currencyExists)
            {
                return Content(true.ToString().ToLower());
            }

            bool symbolExists = await _context.Currencies.AnyAsync(currency => currency.Symbol.ToLower() == symbol.ToLower());
            bool symbolIsUnique = !symbolExists;
            
            return Content(symbolIsUnique.ToString().ToLower());
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currencies.Any(e => e.Id == id);
        }
    }
}
