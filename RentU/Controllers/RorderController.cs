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
    public class RorderController : Controller
    {
        private readonly ModelContext _context;

        public RorderController(ModelContext context)
        {
            _context = context;
        }

        // GET: Rorder
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Rorders.Include(o => o.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Rorder/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            if (id == null)
            {
                return NotFound();
            }

            var Rorder = await _context.Rorders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (Rorder == null)
            {
                return NotFound();
            }

            return View(Rorder);
        }

        // GET: Rorder/Create
        public IActionResult Create()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid");
            return View();
        }

        // POST: Rorder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orderid,Userid,Orderdate, Status")] Rorder Rorder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Rorder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", Rorder.Userid);
            return View(Rorder);
        }

        // GET: Rorder/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Rorder = await _context.Rorders.FindAsync(id);
            if (Rorder == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", Rorder.Userid);
            return View(Rorder);
        }

        // POST: Rorder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Orderid,Userid,Orderdate,Status")] Rorder Rorder)
        {
            if (id != Rorder.Orderid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Rorder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Order1Exists(Rorder.Orderid))
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
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", Rorder.Userid);
            return View(Rorder);
        }

        // GET: Rorder/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Rorder = await _context.Rorders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (Rorder == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(Rorder);
        }

        // POST: Rorder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var Rorder = await _context.Rorders.FindAsync(id);
            _context.Rorders.Remove(Rorder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Order1Exists(decimal id)
        {
            return _context.Rorders.Any(e => e.Orderid == id);
        }

    }
}
