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
    public class PhanQuyenController : BaseController
    {
        private DBContextModel db = new DBContextModel();

        // GET: Admin/PhanQuyen
        public async Task<ActionResult> Index()
        {
            BlockStaff();
            return View(await db.QUYENs.ToListAsync());
        }

        // GET: Admin/PhanQuyen/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUYEN qUYEN = await db.QUYENs.FindAsync(id);
            if (qUYEN == null)
            {
                return HttpNotFound();
            }
            return View(qUYEN);
        }

        // GET: Admin/PhanQuyen/Create
        public ActionResult Create()
        {
            BlockStaff();
            return View();
        }

        // POST: Admin/PhanQuyen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaQuyen,TenQuyen")] QUYEN qUYEN)
        {
            if (ModelState.IsValid)
            {
                db.QUYENs.Add(qUYEN);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(qUYEN);
        }

        // GET: Admin/PhanQuyen/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUYEN qUYEN = await db.QUYENs.FindAsync(id);
            if (qUYEN == null)
            {
                return HttpNotFound();
            }
            return View(qUYEN);
        }

        // POST: Admin/PhanQuyen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "MaQuyen,TenQuyen")] QUYEN qUYEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qUYEN).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(qUYEN);
        }

        // GET: Admin/PhanQuyen/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QUYEN qUYEN = await db.QUYENs.FindAsync(id);
            if (qUYEN == null)
            {
                return HttpNotFound();
            }
            return View(qUYEN);
        }

        // POST: Admin/PhanQuyen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QUYEN qUYEN = await db.QUYENs.FindAsync(id);
            db.QUYENs.Remove(qUYEN);
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
