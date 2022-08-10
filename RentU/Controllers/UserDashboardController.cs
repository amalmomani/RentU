using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using RentU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RentU.Controllers
{
    public class UserDashboardController : Controller
    {
        private readonly ModelContext _context;
        public UserDashboardController(ModelContext context)
        {
            _context = context;
            //assign initial value variable
        }
        public IActionResult SendEmail()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(string to, decimal amount)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            to = ViewBag.Email;
            MimeMessage obj = new MimeMessage();
            MailboxAddress emailfrom = new MailboxAddress("RentU", "shophope17@gmail.com");
            MailboxAddress emailto = new MailboxAddress(ViewBag.Fullname, to);
            obj.From.Add(emailfrom);
            obj.To.Add(emailto);
            obj.Subject = "Success Checkout! "+ ViewBag.Fullname;
            BodyBuilder msgbody = new BodyBuilder();
            // bb.TextBody = body;
            msgbody.HtmlBody = "<html>" + "<h1>" + " Greetings from RentU! " + ViewBag.Fullname + "</h1>" + "</br>" + " Your bill has been paid successfully with " + "</br>" + " Total : " + amount + "$" + "</br>" + "</html>";
            obj.Body = msgbody.ToMessageBody();
            MailKit.Net.Smtp.SmtpClient emailclient = new MailKit.Net.Smtp.SmtpClient();
            emailclient.Connect("smtp.gmail.com", 465, true);
            emailclient.Authenticate("shophope17@gmail.com", "icntjjvnvveooroy");
            emailclient.Send(obj);
            emailclient.Disconnect(true);
            emailclient.Dispose();
           
            return View();
        }


        public IActionResult JoinTable()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var Rorderproduct = _context.Rorderproducts.ToList();
            var Ruseraccount = _context.Ruseraccounts.ToList();
            var order = _context.Rorders.ToList();
            var product = _context.Rproducts.ToList();

            var join = from u in Ruseraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in Rorderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid                      
                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p};
            ViewBag.total = join.Where(x => x.orderproduct.Status == "0" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Totalamount);
            ViewBag.amount = join.Where(x => x.orderproduct.Status == "0" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Numberofpieces);

            return View(join);
        }
      
       
        public async Task<IActionResult> Delete(decimal id)
        {
            var orderproduct = await _context.Rorderproducts.FindAsync(id);
            _context.Rorderproducts.Remove(orderproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("JoinTable", "UserDashboard");
        }
        public IActionResult Paied(DateTime? startDate, DateTime? endDate)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var Rorderproduct = _context.Rorderproducts.ToList();
            var Ruseraccount = _context.Ruseraccounts.ToList();
            var order = _context.Rorders.ToList();
            var product = _context.Rproducts.ToList();

            var join = from u in Ruseraccount
                       join o in order
                       on u.Userid equals o.Userid
                       join op in Rorderproduct
                       on o.Orderid equals op.Orderid
                       join p in product
                       on op.Productid equals p.Productid
                       select new JoinUserOrder { useraccount = u, order = o, orderproduct = op, product = p };
            ViewBag.total = join.Where(x=>x.orderproduct.Status=="1" && x.order.Userid== ViewBag.Userid).Sum(x => x.orderproduct.Totalamount);
            ViewBag.amount = join.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Numberofpieces);

            if (startDate == null && endDate == null)
                return View(join);
            else if (startDate != null && endDate == null)
            {
                var result1 = join.Where(x => x.order.Orderdate.Value.Date >= startDate).ToList();
                ViewBag.total = result1.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result1.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Numberofpieces);
                return View(result1);
            }
            else if (startDate == null && endDate != null)
            {
                var result = join.Where(x => x.order.Orderdate.Value.Date <= endDate).ToList();
                ViewBag.total = result.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Numberofpieces);

                return View(join);
            }
            else
            {
                var result = join.Where(x => x.order.Orderdate.Value.Date <= endDate && x.order.Orderdate.Value.Date >= startDate).ToList();
                ViewBag.total = result.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Totalamount);
                ViewBag.amount = result.Where(x => x.orderproduct.Status == "1" && x.order.Userid == ViewBag.Userid).Sum(x => x.orderproduct.Numberofpieces);

                return View(result);
            }
        }
        public IActionResult Payment()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment([Bind()] Rbank bank, Rpayment payment)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.flag = 1;
            ViewBag.emptyorder = 0;
            ViewBag.amount = 1;
            var userid = ViewBag.Userid;
            var auth = _context.Rbanks.Where(data => data.Cardnumber == bank.Cardnumber && data.Cvv == bank.Cvv).SingleOrDefault();
            if (auth != null)
            {
                var product = _context.Rproducts.ToList();
                List<decimal> pay = new List<decimal>();
                decimal total = 0;
                foreach (var item in product)
                {
                    if (userid == item.Userid && item.Status == "Accept")
                    {
                        if (item.Costtopost != null)
                        {
                            total = (decimal)(total + item.Costtopost);
                        }
                        if (auth.Amount >= total)
                        {
                            item.Status = "paid";
                            _context.Rproducts.Update(item);
                            await _context.SaveChangesAsync();
                        }
                        else
                            ViewBag.amount = 0;
                    }
                }

                if (total == 0)
                {
                    ViewBag.emptyorder = 1;
                }
                else
                {
                    auth.Amount = auth.Amount - total;
                    _context.Rbanks.Update(auth);
                    await _context.SaveChangesAsync();

                    var admin = _context.Rbanks.Where(data => data.Cardnumber == 100 && data.Cvv == 100).SingleOrDefault();
                    admin.Amount = admin.Amount + total;
                    _context.Rbanks.Update(admin);
                    await _context.SaveChangesAsync();
                    SendEmail(ViewBag.Email, total);
                    return RedirectToAction("Payment", "UserDashboard");
                }
            }
            else if (auth == null)
            {
                ViewBag.flag = 0;
            }
            return View();
        }


        //public async Task<IActionResult> Payment([Bind()] Rbank bank, Rpayment payment)
        //{
        //    ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
        //    ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
        //    ViewBag.Email = HttpContext.Session.GetString("Email");
        //    ViewBag.flag = 1;
        //    ViewBag.emptyorder = 0;
        //    ViewBag.amount = 1;


        //    var userid = ViewBag.Userid;
        //    var auth = _context.Rbanks.Where(data => data.Cardnumber == bank.Cardnumber && data.Cvv == bank.Cvv).SingleOrDefault();
        //    if (auth != null)
        //    {
        //        var Order = _context.Rorders.ToList();
        //        var Rorderproduct = _context.Rorderproducts.ToList();
        //        List<decimal> orderid = new List<decimal>();
        //        decimal total = 0;
        //        //foreach (var item in Order)
        //        //{
        //        //    if (userid == item.Userid && item.Status == "0")
        //        //    {
        //        //        orderid.Add(item.Orderid);
        //        //        item.Status = "1";
        //        //        _context.Rorders.Update(item);
        //        //    }
        //        //}
        //        if (orderid.Count != 0)
        //        {
        //            List<Rorderproduct> orderproduct = new List<Rorderproduct>();
        //            foreach (var item in _context.Rorderproducts)
        //            {
        //                foreach (var o in orderid)
        //                {
        //                    if (item.Orderid == o)
        //                    {
        //                        var order = item;
        //                        Rorderproduct.Add(order);
        //                        item.Status = "1";
        //                        _context.Update(item);
        //                    }
        //                }
        //            }
        //            foreach (var p in Rorderproduct)
        //            {
        //                total = (decimal)(total + p.Totalamount);
        //                _context.Rorderproducts.Update(p);
        //            }


        //            if (auth.Amount >= total)
        //            {
        //                auth.Amount = auth.Amount - total;

        //                DateTime now = DateTime.Now;
        //                payment.Paydate = now;
        //                payment.Cardnumber = auth.Cardnumber;
        //                payment.Amount = total;
        //                payment.Userid = userid;

        //                _context.Rpayments.Add(payment);
        //                await _context.SaveChangesAsync();
        //                SendEmail(ViewBag.Email, total);
        //                return RedirectToAction("Paied", "UserDashboard");
        //            }
        //            else
        //                ViewBag.amount = 0;
        //        }
        //        else
        //        {
        //            ViewBag.emptyorder = 1;
        //        }
        //    }
        //    else if (auth == null)
        //    {
        //        ViewBag.flag = 0;
        //    }
        //    return View();

        //}

    }
}
