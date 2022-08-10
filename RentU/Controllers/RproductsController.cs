using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentU.Models;

namespace RentU.Controllers
{
    public class RproductsController : Controller
    {


        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public RproductsController(ModelContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            //assign initial value variable
            webhostEnvironment = _webHostEnvironment;
        }
        // GET: Product1
        public async Task<IActionResult> Index()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var Ruseraccount = _context.Ruseraccounts.ToList();
            var product = _context.Rproducts.ToList();

            var join = from p in product
                       join u in Ruseraccount
                       on p.Userid equals u.Userid
                       select new JoinUserOrder { useraccount = u, product = p };

            return View(join);
          
        }
        [HttpPost]
        public async Task<IActionResult> Index(string? product)
        {
            var modelContext = _context.Rproducts;
            if (product != null)
            {
                var result = await modelContext.Where(x => x.Productname.ToUpper().Contains(product.ToUpper())).ToListAsync();
                return View(result);
            }
            return View(modelContext);
        }
        public IActionResult JoinCategoryProduct(int id)
        {

            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var product = _context.Rproducts.Where(x => x.Status == "paid");
            var category = _context.Rcategories.Where(x => x.Categoryid == id);
            var user = _context.Ruseraccounts.ToList();

            var join = from u in user 
                       join p in product
                       on u.Userid equals p.Userid
                       join c in category
                       on p.Categoryid equals c.Categoryid
                       select new JoinCategoryProduct { product = p, category = c, user=u };
            return View(join);
        }

        public IActionResult Products()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var product = _context.Rproducts.Where(x => x.Status == "paid");
            return View(product);
        }
        public IActionResult Published()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            int u = ViewBag.Userid;
            var product = _context.Rproducts.Where(x => x.Userid==u);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Products(string? productt)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var product = _context.Rproducts.Where(x => x.Status == "paid");

            if (productt != null)
            {
                var result = product.Where(x => x.Productname.ToUpper().Contains(productt.ToUpper())).ToList();
                return View( result);
            }
            return View(product);
        }
        public IActionResult AddToCart(int id)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var product = _context.Rproducts.Where(x => x.Productid == id);

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id,int number)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");

            var product = _context.Rproducts.Where(x => x.Productid == id).FirstOrDefault();
            Rorder order = new Rorder();
            order.Userid = ViewBag.Userid;
            DateTime now = DateTime.Now;
            order.Orderdate = now;
            order.Status = "0";
            
            _context.Rorders.Add(order);
            await _context.SaveChangesAsync();
            Rorderproduct orderproduct = new Rorderproduct();
            orderproduct.Orderid = order.Orderid;
            orderproduct.Numberofpieces = 1;
            orderproduct.Totalamount =  product.Price;
            orderproduct.Status = "0";
            orderproduct.Productid = product.Productid;
            _context.Rorderproducts.Add(orderproduct);
            await _context.SaveChangesAsync();




            //Rorderproduct Rorderproduct = new Rorderproduct();
            //Rorderproduct.Orderid = order.Orderid;
            //Rorderproduct.Totalamount = product.Price;
            //Rorderproduct.Status = "0";
            //Rorderproduct.Productid = product.Productid;
            //_context.Rorderproducts.Add(Rorderproduct);
            //await _context.SaveChangesAsync();
            return RedirectToAction("JoinTable", "Rproducts");
        }


        // GET: Product1/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Rproducts
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (product1 == null)
            {
                return NotFound();
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(product1);
        }

        // GET: Product1/Create
        public IActionResult Create()
        {
            ViewData["Categoryid"] = new SelectList(_context.Rcategories, "Categoryid", "Categoryname");
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View();
        }

        // POST: Product1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Productid,Productname,Price,Image,ImageFile,ImageFile2,Categoryid,Userid,Proof,Status,Costtopost")] Rproduct rproduct)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            if (ModelState.IsValid)
            {
                if (rproduct.ImageFile != null && rproduct.ImageFile2 != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = webhostEnvironment.WebRootPath;
                    //Guid.NewGuid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + rproduct.ImageFile.FileName;
                    string fileName2 = Guid.NewGuid().ToString() + "_" + rproduct.ImageFile2.FileName;

                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    string path2 = Path.Combine(w3rootpath + "/Image/" + fileName2);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await rproduct.ImageFile.CopyToAsync(fileStream);
                        rproduct.Image = fileName;

                    }
                    using (var fileStream = new FileStream(path2, FileMode.Create))
                    {
                        await rproduct.ImageFile2.CopyToAsync(fileStream);
                        rproduct.Proof = fileName2;
                    }
                    rproduct.Userid = ViewBag.Userid;
                    rproduct.Status = "Under Process";
                }
                rproduct.Userid = ViewBag.Userid;
                rproduct.Status = "Under Process";
                _context.Add(rproduct);
                await _context.SaveChangesAsync();
                return RedirectToAction("Published", "Rproducts");

            }
            ViewData["Categoryid"] = new SelectList(_context.Rcategories, "Categoryid", "Categoryname", rproduct.Categoryid);
         
            return View(rproduct);
        }

        // GET: Product1/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Rproducts.FindAsync(id);
            if (product1 == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.Rcategories, "Categoryid", "Categoryname", product1.Categoryid);
            ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", product1.Userid);
            return View(product1);
        }

        // POST: Product1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Productid,Productname,ImageFile,ImageFile2,Price,Image,Categoryid,Userid,Proof,Status,Costtopost")] Rproduct rproduct)
        {
           
            if (id != rproduct.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (rproduct.ImageFile != null && rproduct.ImageFile2 != null)
                    {
                        //1- get w3rootpath
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        //Guid.NewGuid : generate unique string before image name ;
                        ////2- generate image name and add unique string
                        string fileName = Guid.NewGuid().ToString() + "_" + rproduct.ImageFile.FileName;
                        string fileName2 = Guid.NewGuid().ToString() + "_" + rproduct.ImageFile2.FileName;

                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        string path2 = Path.Combine(w3rootpath + "/Image/" + fileName2);
                        //4-create Image inside image file in w3root folder
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await rproduct.ImageFile.CopyToAsync(fileStream);
                            rproduct.Image = fileName;

                        }
                        using (var fileStream = new FileStream(path2, FileMode.Create))
                        {
                            await rproduct.ImageFile2.CopyToAsync(fileStream);
                            rproduct.Proof = fileName2;
                        }
                    }
                    _context.Update(rproduct);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product1Exists(rproduct.Productid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["Categoryid"] = new SelectList(_context.Rcategories, "Categoryid", "Categoryname", rproduct.Categoryid);
                ViewData["Userid"] = new SelectList(_context.Ruseraccounts, "Userid", "Userid", rproduct.Userid); 
                return RedirectToAction(nameof(Index));
            }
            return View(rproduct);
        }



        // GET: Product1/Edit/5
        public async Task<IActionResult> UserEdit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product1 = await _context.Rproducts.FindAsync(id);
            if (product1 == null)
            {
                return NotFound();
            }
            ViewData["Categoryid"] = new SelectList(_context.Rcategories, "Categoryid", "Categoryname", product1.Categoryid);
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(product1);
        }

        // POST: Product1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(decimal id, [Bind("Productid,Productname,ImageFile,ImageFile2,Price,Image,Categoryid,Userid,Proof,Status,Costtopost")] Rproduct rproduct)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            if (id != rproduct.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (rproduct.ImageFile != null && rproduct.ImageFile2 != null)
                    {
                        //1- get w3rootpath
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        //Guid.NewGuid : generate unique string before image name ;
                        ////2- generate image name and add unique string
                        string fileName = Guid.NewGuid().ToString() + "_" + rproduct.ImageFile.FileName;
                        string fileName2 = Guid.NewGuid().ToString() + "_" + rproduct.ImageFile2.FileName;

                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        string path2 = Path.Combine(w3rootpath + "/Image/" + fileName2);
                        //4-create Image inside image file in w3root folder
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await rproduct.ImageFile.CopyToAsync(fileStream);
                            rproduct.Image = fileName;

                        }
                        using (var fileStream = new FileStream(path2, FileMode.Create))
                        {                          
                            await rproduct.ImageFile2.CopyToAsync(fileStream);
                            rproduct.Proof = fileName2;
                        }
                        rproduct.Status = "Under Process";
                    }
                    rproduct.Status = "Under Process";
                    _context.Update(rproduct);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product1Exists(rproduct.Productid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["Categoryid"] = new SelectList(_context.Rcategories, "Categoryid", "Categoryname", rproduct.Categoryid);
                return RedirectToAction("Published", "Rproducts");
            }
            return View(rproduct);
        }




        // GET: Product1/Delete/5
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

        // POST: Product1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var product1 = await _context.Rproducts.FindAsync(id);


            _context.Rproducts.Remove(product1);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult JoinTable()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var Ruseraccount = _context.Ruseraccounts.ToList();
            var product = _context.Rproducts.Where(x => x.Status == "paid");

            var join = from u in Ruseraccount
                       join p in product
                       on u.Userid equals p.Userid
                       select new JoinUserOrder { useraccount = u, product = p };
           
            return View(join);
        }



        public async Task<IActionResult> UserDelete(decimal? id)
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

        // POST: Product1/Delete/5
        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDeleteConfirmed(decimal id)
        {
            var product1 = await _context.Rproducts.FindAsync(id);


            _context.Rproducts.Remove(product1);
            await _context.SaveChangesAsync();

            return RedirectToAction("Published","Rproducts");
        }

        private bool Product1Exists(decimal id)
        {
            return _context.Rproducts.Any(e => e.Productid == id);
        }
    }
}
