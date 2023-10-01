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
    public class HangGiayController : BaseController
    {
        private DBContextModel db = new DBContextModel();

        // GET: Admin/HangGiay
        public async Task<ActionResult> Index()
        {
            BlockStaff();
            return View(await db.HANGGIAYs.ToListAsync());
        }

        // GET: Admin/HangGiay/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANGGIAY hANGGIAY = await db.HANGGIAYs.FindAsync(id);
            if (hANGGIAY == null)
            {
                return HttpNotFound();
            }
            return View(hANGGIAY);
        }
        public void getImage(HANGGIAY hg)
        {
            BlockStaff();
            string filename = Path.GetFileNameWithoutExtension(hg.LogoFile.FileName);
            string extension = Path.GetExtension(hg.LogoFile.FileName);
            string filienamepath = filename + DateTime.Now.ToString("yymmssfff") + extension;
            hg.Logo = filienamepath;
            filienamepath = Path.Combine(Server.MapPath("~/Images/"), filienamepath);
            hg.LogoFile.SaveAs(filienamepath);
        }

        // GET: Admin/HangGiay/Create
        public ActionResult Create()
        {
            BlockStaff();
            HANGGIAY hANGGIAY = new HANGGIAY();
            return View(hANGGIAY);
        }

        // POST: Admin/HangGiay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HANGGIAY hg)
        {
            try
            {
                if (hg.LogoFile != null)
                {
                    getImage(hg);
                }
                db.HANGGIAYs.Add(hg);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Admin/HangGiay/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANGGIAY hANGGIAY = await db.HANGGIAYs.FindAsync(id);
            if (hANGGIAY == null)
            {
                return HttpNotFound();
            }
            return View(hANGGIAY);
        }

        // POST: Admin/HangGiay/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HANGGIAY hg)
        {
            try
            {
                if (hg.LogoFile != null)
                {
                    getImage(hg);
                }
                db.Entry(hg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Admin/HangGiay/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HANGGIAY hANGGIAY = await db.HANGGIAYs.FindAsync(id);
            if (hANGGIAY == null)
            {
                return HttpNotFound();
            }
            return View(hANGGIAY);
        }

        // POST: Admin/HangGiay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HANGGIAY hANGGIAY = await db.HANGGIAYs.FindAsync(id);
            db.HANGGIAYs.Remove(hANGGIAY);
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
