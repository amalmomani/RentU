using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentU.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RentU.Controllers
{
    public class RuseraccountsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public RuseraccountsController(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            //assign initial value variable
            webhostEnvironment = _webHostEnvironment;
        }

        // GET: Ruseraccounts
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var modelContext = _context.Ruseraccounts.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string? user)
        {
            var modelContext = _context.Ruseraccounts;
            if (user != null)
            {
                var result = await modelContext.Where(x => x.Fullname.ToUpper().Contains(user.ToUpper())).ToListAsync();
                return View(result);
            }
            return View(modelContext);
        }
        // GET: Ruseraccounts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ruseraccount = await _context.Ruseraccounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (Ruseraccount == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(Ruseraccount);
        }

        // GET: Ruseraccounts/Create
        public IActionResult Create()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Roleid"] = new SelectList(_context.Rroles, "Roleid", "Roleid");
            return View();
        }

        // POST: Ruseraccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Fullname,Phonenumber,Image,Email,Password,Roleid,ImageFile")] Ruseraccount Ruseraccount)
        {
            if (ModelState.IsValid)
            {
                if (Ruseraccount.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = webhostEnvironment.WebRootPath;
                    //Guid.NewGuid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + Ruseraccount.ImageFile.FileName;
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await Ruseraccount.ImageFile.CopyToAsync(fileStream);
                    }

                    Ruseraccount.Image = fileName;
                    Ruseraccount.Roleid = 1;
                    _context.Add(Ruseraccount);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                _context.Add(Ruseraccount);
            }
            ViewData["Roleid"] = new SelectList(_context.Rroles, "Roleid", "Roleid", Ruseraccount.Roleid);
            return View(Ruseraccount);
        }

        // GET: Ruseraccounts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ruseraccount = await _context.Ruseraccounts.FindAsync(id);
            if (Ruseraccount == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Roleid"] = new SelectList(_context.Rroles, "Roleid", "Roleid", Ruseraccount.Roleid);
            return View(Ruseraccount);
        }

        // POST: Ruseraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fullname,Phonenumber,Image,Email,Password,Roleid,ImageFile")] Ruseraccount Ruseraccount)
        {
            if (id != Ruseraccount.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Ruseraccount.ImageFile != null)
                    {
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + Ruseraccount.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ruseraccount.ImageFile.CopyToAsync(fileStream);
                        }
                        Ruseraccount.Image = fileName;
                        await _context.SaveChangesAsync();
                    }
                    _context.Update(Ruseraccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccountExists(Ruseraccount.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Dashboard", "Dashboard");
            }
            ViewData["Roleid"] = new SelectList(_context.Rroles, "Roleid", "Roleid", Ruseraccount.Roleid);
            return View(Ruseraccount);
        }


        public async Task<IActionResult> EditAdmin(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ruseraccount = await _context.Ruseraccounts.FindAsync(id);
            if (Ruseraccount == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Roleid"] = new SelectList(_context.Rroles, "Roleid", "Roleid", Ruseraccount.Roleid);
            return View(Ruseraccount);
        }

        // POST: Ruseraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(decimal id, [Bind("Userid,Fullname,Phonenumber,Image,Email,Password,Roleid,ImageFile")] Ruseraccount Ruseraccount)
        {
            if (id != Ruseraccount.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Ruseraccount.ImageFile != null)
                    {
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + Ruseraccount.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ruseraccount.ImageFile.CopyToAsync(fileStream);
                        }
                        Ruseraccount.Image = fileName;
                        await _context.SaveChangesAsync();
                    }
                    _context.Update(Ruseraccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccountExists(Ruseraccount.Userid))
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
            ViewData["Roleid"] = new SelectList(_context.Rroles, "Roleid", "Roleid", Ruseraccount.Roleid);
            return View(Ruseraccount);
        }


        public async Task<IActionResult> UserEdit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ruseraccount = await _context.Ruseraccounts.FindAsync(id);
            if (Ruseraccount == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewData["Roleid"] = new SelectList(_context.Rroles, "Roleid", "Roleid", Ruseraccount.Roleid);
            return View(Ruseraccount);
        }

        // POST: Ruseraccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(decimal id, [Bind("Userid,Fullname,Phonenumber,Image,Email,Password,Roleid,ImageFile")] Ruseraccount Ruseraccount)
        {
            if (id != Ruseraccount.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Ruseraccount.ImageFile != null)
                    {
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + Ruseraccount.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ruseraccount.ImageFile.CopyToAsync(fileStream);
                        }
                        Ruseraccount.Image = fileName;
                        await _context.SaveChangesAsync();
                    }
                    _context.Update(Ruseraccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UseraccountExists(Ruseraccount.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Category", "Rcategory");
            }
            ViewData["Roleid"] = new SelectList(_context.Rroles, "Roleid", "Roleid", Ruseraccount.Roleid);
            return View(Ruseraccount);
        }

        // GET: Ruseraccounts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Ruseraccount = await _context.Ruseraccounts
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (Ruseraccount == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(Ruseraccount);
        }

        // POST: Ruseraccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var Ruseraccount = await _context.Ruseraccounts.FindAsync(id);
            _context.Ruseraccounts.Remove(Ruseraccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UseraccountExists(decimal id)
        {
            return _context.Ruseraccounts.Any(e => e.Userid == id);
        }

    }
}
