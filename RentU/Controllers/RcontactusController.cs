using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentU.Models;
using Microsoft.AspNetCore.Http;
using MimeKit;
using MailKit.Net.Smtp;

namespace RentU.Controllers
{
    public class RcontactusController : Controller
    {
        private readonly ModelContext _context;

        public RcontactusController(ModelContext context)
        {
            _context = context;
        }

        // GET: Contactus
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(await _context.Rcontactus.ToListAsync());
        }

        // GET: Contactus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactu = await _context.Rcontactus
                .FirstOrDefaultAsync(m => m.Contid == id);
            if (contactu == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(contactu);
        }

        // GET: Contactus/Create
        public IActionResult ContactUs()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }

        // POST: Contactus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs([Bind("Contid,Name,Email,Message")] Rcontactu contactu)
        {
            if (ModelState.IsValid)
            {               
                _context.Add(contactu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ContactUs));
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(contactu);
        }
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactu = await _context.Rcontactus
                .FirstOrDefaultAsync(m => m.Contid == id);
            if (contactu == null)
            {
                return NotFound();
            }

            return View(contactu);
        }

        // POST: Contactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var contactu = await _context.Rcontactus.FindAsync(id);
            _context.Rcontactus.Remove(contactu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactuExists(decimal id)
        {
            return _context.Rcontactus.Any(e => e.Contid == id);
        }
        public IActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(string to, string subject, string body)
        {
            to = ViewBag.Email;
            MimeMessage obj = new MimeMessage();
            MailboxAddress emailfrom = new MailboxAddress("Shoppers.com", "shoppersdelight789@gmail.com");
            MailboxAddress emailto = new MailboxAddress("Shoppers.com", to);
            obj.From.Add(emailfrom);
            obj.To.Add(emailto);
            obj.Subject = subject;
            BodyBuilder bb = new BodyBuilder();
            // bb.TextBody = body;
            bb.HtmlBody = "<html>" + "<h1>" + body + "</h1>" + "</html>";
            obj.Body = bb.ToMessageBody();
            SmtpClient emailclient = new SmtpClient();
            emailclient.Connect("smtp.gmail.com", 465, true);
            emailclient.Authenticate("shoppersdelight789@gmail.com", "shopper7899$");
            emailclient.Send(obj);
            emailclient.Disconnect(true);
            emailclient.Dispose();
            return View();
        }

    }
}
