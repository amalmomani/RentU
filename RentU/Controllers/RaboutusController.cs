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
    public class RaboutusController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public RaboutusController(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            //assign initial value variable
            webhostEnvironment = _webHostEnvironment;
        }


        // GET: Aboutus
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(await _context.Raboutus.ToListAsync());
        }

        // GET: Aboutus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Raboutus.FindAsync(id);
            if (aboutu == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(aboutu);
        }

        // POST: Aboutus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Aboutid,Text1,Text2,Text3,Text4,Image,ImageFile")] Raboutu aboutu)
        {
            if (id != aboutu.Aboutid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutu.ImageFile != null)
                    {
                        //1- get w3rootpath
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        //Guid.NewGuid : generate unique string before image name ;
                        ////2- generate image name and add unique string
                        string fileName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        //4-create Image inside image file in w3root folder
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await aboutu.ImageFile.CopyToAsync(fileStream);
                        }

                        aboutu.Image = fileName;
                    }
                    _context.Update(aboutu);
                    await _context.SaveChangesAsync();                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutuExists(aboutu.Aboutid))
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
            return View(aboutu);
        }

        // GET: Aboutus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Raboutus
                .FirstOrDefaultAsync(m => m.Aboutid == id);
            if (aboutu == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(aboutu);
        }

        // POST: Aboutus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var aboutu = await _context.Raboutus.FindAsync(id);
            _context.Raboutus.Remove(aboutu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutuExists(decimal id)
        {
            return _context.Raboutus.Any(e => e.Aboutid == id);
        }

    }
}
