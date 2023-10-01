using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoesShop.Models;

namespace ShoesShop.Areas.Admin.Controllers
{
    public class BangSizeController : BaseController
    {
        private DBContextModel db = new DBContextModel();

        // GET: Admin/BangSize
        public async Task<ActionResult> Index()
        {
            var bANGSIZEs = db.BANGSIZEs.Include(b => b.SANPHAM);
            return View(await bANGSIZEs.ToListAsync());
        }

        // GET: Admin/BangSize/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANGSIZE bANGSIZE = await db.BANGSIZEs.FindAsync(id);
            if (bANGSIZE == null)
            {
                return HttpNotFound();
            }
            return View(bANGSIZE);
        }

        // GET: Admin/BangSize/Create
        public ActionResult Create()
        {
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSanPham");
            return View();
        }

        // POST: Admin/BangSize/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaSize,MaSP,s38,s39,s40,s41,s42,s42_5,s43,s44,s45,s46,s47,s48")] BANGSIZE bANGSIZE)
        {
            if (ModelState.IsValid)
            {
                db.BANGSIZEs.Add(bANGSIZE);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSanPham", bANGSIZE.MaSP);
            return View(bANGSIZE);
        }

        // GET: Admin/BangSize/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANGSIZE bANGSIZE = await db.BANGSIZEs.FindAsync(id);
            if (bANGSIZE == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSanPham", bANGSIZE.MaSP);
            return View(bANGSIZE);
        }

        // POST: Admin/BangSize/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaSize,MaSP,s38,s39,s40,s41,s42,s42_5,s43,s44,s45,s46,s47,s48")] BANGSIZE bANGSIZE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bANGSIZE).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSanPham", bANGSIZE.MaSP);
            return View(bANGSIZE);
        }

        // GET: Admin/BangSize/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BANGSIZE bANGSIZE = await db.BANGSIZEs.FindAsync(id);
            if (bANGSIZE == null)
            {
                return HttpNotFound();
            }
            return View(bANGSIZE);
        }

        // POST: Admin/BangSize/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BANGSIZE bANGSIZE = await db.BANGSIZEs.FindAsync(id);
            db.BANGSIZEs.Remove(bANGSIZE);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
