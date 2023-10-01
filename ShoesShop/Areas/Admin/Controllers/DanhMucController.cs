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
    public class DanhMucController : BaseController
    {
        private DBContextModel db = new DBContextModel();

        // GET: Admin/DanhMuc
        public async Task<ActionResult> Index()
        {
            BlockStaff();
            return View(await db.DANHMUCs.ToListAsync());
        }

        // GET: Admin/DanhMuc/Create
        public ActionResult Create()
        {
            BlockStaff();
            return View();
        }

        // POST: Admin/DanhMuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaDanhMuc,TenDanhMuc")] DANHMUC dANHMUC)
        {
            if (ModelState.IsValid)
            {
                db.DANHMUCs.Add(dANHMUC);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(dANHMUC);
        }

        // GET: Admin/DanhMuc/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUC dANHMUC = await db.DANHMUCs.FindAsync(id);
            if (dANHMUC == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUC);
        }

        // POST: Admin/DanhMuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaDanhMuc,TenDanhMuc")] DANHMUC dANHMUC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dANHMUC).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(dANHMUC);
        }

        // GET: Admin/DanhMuc/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUC dANHMUC = await db.DANHMUCs.FindAsync(id);
            if (dANHMUC == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUC);
        }

        // POST: Admin/DanhMuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DANHMUC dANHMUC = await db.DANHMUCs.FindAsync(id);
            db.DANHMUCs.Remove(dANHMUC);
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
