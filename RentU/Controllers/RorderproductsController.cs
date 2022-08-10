using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentU.Models;
using Microsoft.AspNetCore.Http;

namespace RentU.Controllers
{
    public class RorderproductsController : Controller
    {
        private readonly ModelContext _context;

        public RorderproductsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Rorderproducts
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var modelContext = _context.Rorderproducts.Include(o => o.Order).Include(o => o.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: Rorderproducts/Details/5s
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Rorderproduct = await _context.Rorderproducts
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Rorderproduct == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(Rorderproduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Rorderproducts/Create
       

        // POST: Rorderproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Orderid,Numberofpieces,Totalamount,Status,Productid")] Rorderproduct Rorderproduct)
        //{
        //    if (ModelState.IsValid)
        //    {
        //         Rorderproduct.Totalamount = Rorderproduct.Numberofpieces * Rorderproduct.Product.Price;
        //        _context.Add(Rorderproduct);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Orderid"] = new SelectList(_context.Rorders, "Orderid", "Orderid", Rorderproduct.Orderid);
        //    ViewData["Productid"] = new SelectList(_context.Rproducts, "Productid", "Productid", Rorderproduct.Productid);
        //    return View(Rorderproduct);
        //}

        // GET: Rorderproducts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Rorderproduct = await _context.Rorderproducts.FindAsync(id);
            if (Rorderproduct == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Orderid"] = new SelectList(_context.Rorders, "Orderid", "Orderid", Rorderproduct.Orderid);
            ViewData["Productid"] = new SelectList(_context.Rproducts, "Productid", "Productid", Rorderproduct.Productid);
            return View(Rorderproduct);
        }

        // POST: Rorderproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Orderid,Numberofpieces,Totalamount,Status,Productid")] Rorderproduct Rorderproduct)
        {
            if (id != Rorderproduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Rorderproduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderproductExists(Rorderproduct.Id))
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

            ViewData["Orderid"] = new SelectList(_context.Rorders, "Orderid", "Orderid", Rorderproduct.Orderid);
            ViewData["Productid"] = new SelectList(_context.Rproducts, "Productid", "Productid", Rorderproduct.Productid);
            return View(Rorderproduct);
        }

        // GET: Rorderproducts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Rorderproduct = await _context.Rorderproducts
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Rorderproduct == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(Rorderproduct);
        }

        // POST: Rorderproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var Rorderproduct = await _context.Rorderproducts.FindAsync(id);
            _context.Rorderproducts.Remove(Rorderproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderproductExists(decimal id)
        {
            return _context.Rorderproducts.Any(e => e.Id == id);
        }

    }
}
