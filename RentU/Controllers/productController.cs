using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentU.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace RentU.Controllers
{
    public class productController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webhostEnvironment;
        public productController(ModelContext context, IWebHostEnvironment _webHostEnvironment)
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
            return View(await _context.Rproducts.ToListAsync());
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
            var product = _context.Rproducts.ToList();
            var category = _context.Rcategories.Where(x => x.Categoryid == id);

            var join = from  p in product
                       join c in category                       
                       on p.Categoryid equals c.Categoryid
                       select new JoinCategoryProduct { product = p, category=c};
            return View(join);
        }
        //[HttpPost]
        //public async Task<IActionResult> JoinShopProduct(string? productt)
        //{
        //    ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
        //    ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
        //    ViewBag.Email = HttpContext.Session.GetString("Email");
        //    var product = _context.Rproducts.ToList();
        //    var Rorderproducts = _context.Rorderproducts.ToList();
        //    var productshop = _context.Productshops.ToList();
        //    var shop = _context.Shop1s.ToList();
        //    var join = from p in product
        //               join ps in productshop
        //               on p.Productid equals ps.Productid
        //               join s in shop
        //               on ps.Shopid equals s.Shopid
        //               select new JoinShopProduct { product = p, productshop = ps, shop = s };
        //    if (productt != null)
        //    {
        //        var result = join.Where(x => x.product.Productname.ToUpper().Contains(productt.ToUpper())).ToList();
        //        return View(result);
        //    }
        //    return View(join);
        //}
        public IActionResult Products()
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var product = _context.Rproducts.ToList();
            var category = _context.Rcategories.ToList();
            var join = from p in product
                       join c in category
                       on p.Categoryid equals c.Categoryid
                      
                       select new JoinCategoryProduct { product = p, category=c};
            return View(join);
        }
        [HttpPost]
        public async Task<IActionResult> Products(string? productt)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            var product = _context.Rproducts.ToList();
            var category = _context.Rcategories.ToList();
            var join = from p in product
                       join c in category
                       on p.Categoryid equals c.Categoryid

                       select new JoinCategoryProduct { product = p, category = c };
            if (productt != null)
            {
                var result = join.Where(x => x.product.Productname.ToUpper().Contains(productt.ToUpper())).ToList();
                return View(result);
            }
            return View(join);
        }
        //public IActionResult AddToCart(int id)
        //{
        //    ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
        //    ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
        //    ViewBag.Email = HttpContext.Session.GetString("Email");
        //    var product = _context.Rproducts.Where(x => x.Productid == id);

        //    return View();
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id, int Numberofpieces)
        {
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            ViewBag.shopid = HttpContext.Session.GetInt32("shopid");

            var product = _context.Rproducts.Where(x => x.Productid == id).FirstOrDefault();
            Rorder order = new Rorder();
            order.Userid = ViewBag.Userid;
            DateTime now = DateTime.Now;
            order.Orderdate = now;
            order.Status = "0";
            _context.Rorders.Add(order);
            await _context.SaveChangesAsync();
            Rorderproduct Rorderproduct = new Rorderproduct();
            Rorderproduct.Orderid = order.Orderid;
            Rorderproduct.Numberofpieces = Numberofpieces;
            Rorderproduct.Totalamount = Numberofpieces * product.Price;
            Rorderproduct.Status = "0";
            Rorderproduct.Productid = product.Productid;
            _context.Rorderproducts.Add(Rorderproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("JoinTable", "Rproduct");
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
            return View();
        }

        // POST: Product1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Productid,Productname,Price,Productvalue,Image,Shopid,Productsize,ImageFile")] Rproduct product1)
        {
            if (ModelState.IsValid)
            {
                if (product1.ImageFile != null)
                {
                    //1- get w3rootpath
                    string w3rootpath = webhostEnvironment.WebRootPath;
                    //Guid.NewGuid : generate unique string before image name ;
                    ////2- generate image name and add unique string
                    string fileName = Guid.NewGuid().ToString() + "_" + product1.ImageFile.FileName;
                    string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                    //4-create Image inside image file in w3root folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await product1.ImageFile.CopyToAsync(fileStream);
                    }

                    product1.Image = fileName;
                }
                _context.Add(product1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            ViewBag.Fullname = HttpContext.Session.GetString("Fullname");
            ViewBag.Userid = HttpContext.Session.GetInt32("Userid");
            ViewBag.Email = HttpContext.Session.GetString("Email");
            return View(product1);
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
        public async Task<IActionResult> Edit(decimal id, [Bind("Productid,Productname,Price,Productvalue,Image,Shopid,Productsize,ImageFile")] Rproduct product1)
        {
            if (id != product1.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product1.ImageFile != null)
                    {
                        //1- get w3rootpath
                        string w3rootpath = webhostEnvironment.WebRootPath;
                        //Guid.NewGuid : generate unique string before image name ;
                        ////2- generate image name and add unique string
                        string fileName = Guid.NewGuid().ToString() + "_" + product1.ImageFile.FileName;
                        string path = Path.Combine(w3rootpath + "/Image/" + fileName);
                        //4-create Image inside image file in w3root folder
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await product1.ImageFile.CopyToAsync(fileStream);
                        }

                        product1.Image = fileName;
                    }
                    _context.Update(product1);
                    await _context.SaveChangesAsync();
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product1Exists(product1.Productid))
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
            return View(product1);
        }

        // GET: Product1/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
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

        private bool Product1Exists(decimal id)
        {
            return _context.Rproducts.Any(e => e.Productid == id);
        }
    }
}
