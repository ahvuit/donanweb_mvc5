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
    public class SanPhamKhuyenMaiController : BaseController
    {
        private DBContextModel db = new DBContextModel();

        // GET: Admin/SanPhamKhuyenMai
        public async Task<ActionResult> Index()
        {
            var cHITIETKHUYENMAIs = db.CHITIETKHUYENMAIs.Include(c => c.KHUYENMAI).Include(c => c.SANPHAM);
            return View(await cHITIETKHUYENMAIs.ToListAsync());
        }

        // GET: Admin/SanPhamKhuyenMai/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETKHUYENMAI cHITIETKHUYENMAI = db.CHITIETKHUYENMAIs.SingleOrDefault(m => m.MaSP == id);
            if (cHITIETKHUYENMAI == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETKHUYENMAI);
        }

        // GET: Admin/SanPhamKhuyenMai/Create
        public ActionResult Create()
        {
            ViewBag.MaKhuyenMai = new SelectList(db.KHUYENMAIs, "MaKhuyenMai", "TenKhuyenMai");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSanPham");
            return View();
        }

        // POST: Admin/SanPhamKhuyenMai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaKhuyenMai,MaSP,GiaKM")] CHITIETKHUYENMAI cHITIETKHUYENMAI)
        {
            if (ModelState.IsValid)
            {
                var n = (NHANVIEN)Session["NV"];
                cHITIETKHUYENMAI.UpdateBy = n.TenNhanVien;
                db.CHITIETKHUYENMAIs.Add(cHITIETKHUYENMAI);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.MaKhuyenMai = new SelectList(db.KHUYENMAIs, "MaKhuyenMai", "TenKhuyenMai", cHITIETKHUYENMAI.MaKhuyenMai);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSanPham", cHITIETKHUYENMAI.MaSP);
            return View(cHITIETKHUYENMAI);
        }

        // GET: Admin/SanPhamKhuyenMai/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETKHUYENMAI cHITIETKHUYENMAI = db.CHITIETKHUYENMAIs.SingleOrDefault(m => m.MaSP == id);
            if (cHITIETKHUYENMAI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhuyenMai = new SelectList(db.KHUYENMAIs, "MaKhuyenMai", "TenKhuyenMai", cHITIETKHUYENMAI.MaKhuyenMai);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSanPham", cHITIETKHUYENMAI.MaSP);
            return View(cHITIETKHUYENMAI);
        }

        // POST: Admin/SanPhamKhuyenMai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaKhuyenMai,MaSP,GiaKM")] CHITIETKHUYENMAI cHITIETKHUYENMAI)
        {
            if (ModelState.IsValid)
            {
                var n = (NHANVIEN)Session["NV"];
                cHITIETKHUYENMAI.UpdateBy = n.TenNhanVien;
                db.Entry(cHITIETKHUYENMAI).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaKhuyenMai = new SelectList(db.KHUYENMAIs, "MaKhuyenMai", "TenKhuyenMai", cHITIETKHUYENMAI.MaKhuyenMai);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSanPham", cHITIETKHUYENMAI.MaSP);
            return View(cHITIETKHUYENMAI);
        }

        // GET: Admin/SanPhamKhuyenMai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETKHUYENMAI cHITIETKHUYENMAI = db.CHITIETKHUYENMAIs.SingleOrDefault(m => m.MaSP == id);
            if (cHITIETKHUYENMAI == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETKHUYENMAI);
        }

        // POST: Admin/SanPhamKhuyenMai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            CHITIETKHUYENMAI cHITIETKHUYENMAI = db.CHITIETKHUYENMAIs.SingleOrDefault(m => m.MaSP == id);
            db.CHITIETKHUYENMAIs.Remove(cHITIETKHUYENMAI);
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
