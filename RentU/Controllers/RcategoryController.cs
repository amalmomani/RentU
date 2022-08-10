using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentU.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace RentU.Controllers
{
    public class RcategoryController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public RcategoryController(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            //assign initial value variable
            webhostEnvironment = _webHostEnvironment;
        }

        // GET: Rcategory
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(await _context.Rcategories.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string? category)
        {
            var modelContext = _context.Rcategories;
            if (category != null)
            {
                var result = await modelContext.Where(x => x.Categoryname.ToUpper().Contains(category.ToUpper())).ToListAsync();
                return View(result);
            }
            return View(modelContext);
        }
        public async Task<IActionResult> Category()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(await _context.Rcategories.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Category(string? category)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var cat = await _context.Rcategories.ToListAsync();
            if (category != null)
            {
                var result = cat.Where(x => x.Categoryname.ToUpper().Contains(category.ToUpper()));
                return View(result);
            }
            return View();
        }
        // GET: Rcategory/Create
        public IActionResult Create()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }

        // POST: Rcategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Categoryid,Categoryname,Image,ImageFile")] Rcategory Rcategory)
        {
            if (ModelState.IsValid)
            {
                if (Rcategory.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = webhostEnvironment.WebRootPath;
                    //Guid.NewGuid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + Rcategory.ImageFile.FileName;
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await Rcategory.ImageFile.CopyToAsync(fileStream);
                    }

                    Rcategory.Image = fileName;
                }
                _context.Add(Rcategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Rcategory);
        }

        // GET: Rcategory/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Rcategory = await _context.Rcategories.FindAsync(id);
            if (Rcategory == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(Rcategory);
        }

        // POST: Rcategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Categoryid,Categoryname,Image,ImageFile")] Rcategory Rcategory)
        {
            if (id != Rcategory.Categoryid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Rcategory.ImageFile != null)
                    {
                        //1- get w3rootpath
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        //Guid.NewGuid : generate unique string before image name ;
                        ////2- generate image name and add unique string
                        string fileName = Guid.NewGuid().ToString() + "_" + Rcategory.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        //4-create Image inside image file in w3root folder
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await Rcategory.ImageFile.CopyToAsync(fileStream);
                        }

                        Rcategory.Image = fileName;
                    }
                    _context.Update(Rcategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RcategoryExists(Rcategory.Categoryid))
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
            return View(Rcategory);
        }

        // GET: Rcategory/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Rcategory = await _context.Rcategories
                .FirstOrDefaultAsync(m => m.Categoryid == id);
            if (Rcategory == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(Rcategory);
        }

        // POST: Rcategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var Rcategory = await _context.Rcategories.FindAsync(id);
            _context.Rcategories.Remove(Rcategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RcategoryExists(decimal id)
        {
            return _context.Rcategories.Any(e => e.Categoryid == id);
        }
    }
}
