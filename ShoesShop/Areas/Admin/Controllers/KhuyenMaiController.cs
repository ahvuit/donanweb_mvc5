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
    public class KhuyenMaiController : BaseController
    {
        private DBContextModel db = new DBContextModel();

        // GET: Admin/KhuyenMai
        public async Task<ActionResult> Index()
        {
            BlockStaff();
            return View(await db.KHUYENMAIs.ToListAsync());
        }

        // GET: Admin/KhuyenMai/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUYENMAI kHUYENMAI = await db.KHUYENMAIs.FindAsync(id);
            if (kHUYENMAI == null)
            {
                return HttpNotFound();
            }
            return View(kHUYENMAI);
        }

        // GET: Admin/KhuyenMai/Create
        public ActionResult Create()
        {
            BlockStaff();
            return View();
        }

        // POST: Admin/KhuyenMai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaKhuyenMai,NgayBatDau,NgayKetThuc,TenKhuyenMai,NgayTao,NoiDungKM")] KHUYENMAI kHUYENMAI)
        {
            if (ModelState.IsValid)
            {
                db.KHUYENMAIs.Add(kHUYENMAI);
                kHUYENMAI.NgayTao = DateTime.Now;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(kHUYENMAI);
        }

        // GET: Admin/KhuyenMai/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUYENMAI kHUYENMAI = await db.KHUYENMAIs.FindAsync(id);
            if (kHUYENMAI == null)
            {
                return HttpNotFound();
            }
            return View(kHUYENMAI);
        }

        // POST: Admin/KhuyenMai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(KHUYENMAI kHUYENMAI)
        {
            try
            {
                kHUYENMAI.NgayTao = DateTime.Now;
                db.Entry(kHUYENMAI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "KhuyenMai");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Admin/KhuyenMai/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUYENMAI kHUYENMAI = await db.KHUYENMAIs.FindAsync(id);
            if (kHUYENMAI == null)
            {
                return HttpNotFound();
            }
            return View(kHUYENMAI);
        }

        // POST: Admin/KhuyenMai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            KHUYENMAI kHUYENMAI = await db.KHUYENMAIs.FindAsync(id);
            db.KHUYENMAIs.Remove(kHUYENMAI);
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
