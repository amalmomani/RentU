using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentU.Models;

namespace RentU.Controllers
{
    public class Rproducts1Controller : Controller
    {
        private readonly ModelContext _context;

        public Rproducts1Controller(ModelContext context)
        {
            _context = context;
        }

        // GET: Rproducts1
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Rproducts.Include(r => r.Category).Include(r => r.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Rproducts1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rproduct = await _context.Rproducts
                .Include(r => r.Category)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (rproduct == null)
            {
                return NotFound();
            }

            return View(rproduct);
        }

        // GET: Rproducts1/Create
        public IActionResult Create()
        {
            ViewData["Categoryid"] = new SelectList(_context.Rcategories, "Categoryid", "Categoryid");
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid");
            return View();
        }

        // POST: Rproducts1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Productid,Productname,Price,Productvalue,Image,Categoryid,Userid,Proof,Status,Costtopost")] Rproduct rproduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rproduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categoryid"] = new SelectList(_context.Rcategories, "Categoryid", "Categoryid", rproduct.Categoryid);
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", rproduct.Userid);
            return View(rproduct);
        }

        // GET: Rproducts1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rproduct = await _context.Rproducts.FindAsync(id);
            if (rproduct == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.Rcategories, "Categoryid", "Categoryid", rproduct.Categoryid);
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", rproduct.Userid);
            return View(rproduct);
        }

        // POST: Rproducts1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Productid,Productname,Price,Productvalue,Image,Categoryid,Userid,Proof,Status,Costtopost")] Rproduct rproduct)
        {
            if (id != rproduct.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rproduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RproductExists(rproduct.Productid))
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
            ViewData["Categoryid"] = new SelectList(_context.Rcategories, "Categoryid", "Categoryid", rproduct.Categoryid);
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", rproduct.Userid);
            return View(rproduct);
        }

        // GET: Rproducts1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rproduct = await _context.Rproducts
                .Include(r => r.Category)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (rproduct == null)
            {
                return NotFound();
            }

            return View(rproduct);
        }

        // POST: Rproducts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var rproduct = await _context.Rproducts.FindAsync(id);
            _context.Rproducts.Remove(rproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RproductExists(decimal id)
        {
            return _context.Rproducts.Any(e => e.Productid == id);
        }
    }
}
