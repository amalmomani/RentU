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
    public class RpaymentsController : Controller
    {
        private readonly ModelContext _context;

        public RpaymentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Rpayments
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Rpayments.Include(p => p.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Rpayments/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Rpayments
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Payid == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Rpayments/Create
        public IActionResult Create()
        {
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid");
            return View();
        }

        // POST: Rpayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Payid,Cardnumber,Amount,Paydate,Userid")] Rpayment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", payment.Userid);
            return View(payment);
        }

        // GET: Rpayments/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Rpayments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", payment.Userid);
            return View(payment);
        }

        // POST: Rpayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Payid,Cardnumber,Amount,Paydate,Userid")] Rpayment payment)
        {
            if (id != payment.Payid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Payid))
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
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", payment.Userid);
            return View(payment);
        }

        // GET: Rpayments/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Rpayments
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Payid == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Rpayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var payment = await _context.Rpayments.FindAsync(id);
            _context.Rpayments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(decimal id)
        {
            return _context.Rpayments.Any(e => e.Payid == id);
        }
    }
}
