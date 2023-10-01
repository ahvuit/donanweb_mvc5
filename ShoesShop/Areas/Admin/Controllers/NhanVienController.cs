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
using System.IO;

namespace ShoesShop.Areas.Admin.Controllers
{
    public class NhanVienController : BaseController
    {
        private DBContextModel db = new DBContextModel();

        // GET: Admin/NhanVien
        public async Task<ActionResult> Index()
        {
            BlockStaff();
            var nHANVIENs = db.NHANVIENs.Include(n => n.ACCOUNT);
            return View(await nHANVIENs.ToListAsync());
        }

        // GET: Admin/NhanVien/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = await db.NHANVIENs.FindAsync(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }
        public void getImage(NHANVIEN nv)
        {
            BlockStaff();
            string filename = Path.GetFileNameWithoutExtension(nv.HinhAnhFile.FileName);
            string extension = Path.GetExtension(nv.HinhAnhFile.FileName);
            string filienamepath = filename + DateTime.Now.ToString("yymmssfff") + extension;
            nv.HinhAnh = filienamepath;
            filienamepath = Path.Combine(Server.MapPath("~/Images/"), filienamepath);
            nv.HinhAnhFile.SaveAs(filienamepath);
        }



        // GET: Admin/NhanVien/Create
        public ActionResult Create()
        {
            BlockStaff();
            NHANVIEN nHANVIEN = new NHANVIEN();
            ViewBag.IdAccount = new SelectList(db.ACCOUNTs, "IdAccount", "UserName");
            return View(nHANVIEN);
        }

        // POST: Admin/NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NHANVIEN nv)
        {
            try
            {
                if (nv.HinhAnhFile != null)
                {
                    getImage(nv);
                }
                db.NHANVIENs.Add(nv);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ViewBag.IdAccount = new SelectList(db.ACCOUNTs, "IdAccount", "UserName", nv.IdAccount);
                return View(nv);
            }
        }

        // GET: Admin/NhanVien/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = await db.NHANVIENs.FindAsync(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAccount = new SelectList(db.ACCOUNTs, "IdAccount", "UserName", nHANVIEN.IdAccount);
            return View(nHANVIEN);
        }

        // POST: Admin/NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaNV,TenNhanVien,NgaySinh,GioiTinh,CMND,DiaChi,SDT,IdAccount,HinhAnh")] NHANVIEN nHANVIEN)
        {
            BlockStaff();
            if (ModelState.IsValid)
            {
                db.Entry(nHANVIEN).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IdAccount = new SelectList(db.ACCOUNTs, "IdAccount", "UserName", nHANVIEN.IdAccount);
            return View(nHANVIEN);
        }

        // GET: Admin/NhanVien/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHANVIEN nHANVIEN = await db.NHANVIENs.FindAsync(id);
            if (nHANVIEN == null)
            {
                return HttpNotFound();
            }
            return View(nHANVIEN);
        }

        // POST: Admin/NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NHANVIEN nHANVIEN = await db.NHANVIENs.FindAsync(id);
            db.NHANVIENs.Remove(nHANVIEN);
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
