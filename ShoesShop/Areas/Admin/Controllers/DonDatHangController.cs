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
using PagedList;

namespace ShoesShop.Areas.Admin.Controllers
{
    public class DonDatHangController : BaseController
    {
        private DBContextModel db = new DBContextModel();

        // GET: Admin/DonDatHang
        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var dONDATHANGs = db.DONDATHANGs.Include(d => d.ACCOUNT).Include(d => d.TINHTRANG).OrderBy(m => m.MaTT);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(dONDATHANGs.ToPagedList(pageNumber, pageSize));

        }
        // GET: Admin/DonDatHang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG dONDATHANG = db.DONDATHANGs.Find(id);
            var chitiet = db.CHITIETDONHANGs.Include(d=>d.SANPHAM).Where(d => d.MaDDH == id).ToList();
            if (dONDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
        }

        // GET: Admin/DonDatHang/Create
        public ActionResult Create()
        {
            ViewBag.IdAccount = new SelectList(db.ACCOUNTs, "IdAccount", "UserName");
            ViewBag.MaTT = new SelectList(db.TINHTRANGs, "MaTT", "TinhTrangGiao");
            return View();
        }

        // POST: Admin/DonDatHang/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MaDDH,NgayDat,MaTT,NgayGiao,Hoten,DiaChi,SDT,Email,GhiChu,TongTien,IdAccount")] DONDATHANG dONDATHANG)
        {
            if (ModelState.IsValid)
            {
                db.DONDATHANGs.Add(dONDATHANG);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IdAccount = new SelectList(db.ACCOUNTs, "IdAccount", "UserName", dONDATHANG.IdAccount);
            ViewBag.MaTT = new SelectList(db.TINHTRANGs, "MaTT", "TinhTrangGiao", dONDATHANG.MaTT);
            return View(dONDATHANG);
        }

        // GET: Admin/DonDatHang/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG dONDATHANG = await db.DONDATHANGs.FindAsync(id);
            if (dONDATHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdAccount = new SelectList(db.ACCOUNTs, "IdAccount", "UserName", dONDATHANG.IdAccount);
            ViewBag.MaTT = new SelectList(db.TINHTRANGs, "MaTT", "TinhTrangGiao", dONDATHANG.MaTT);
            return View(dONDATHANG);
        }

        // POST: Admin/DonDatHang/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DONDATHANG dONDATHANG, FormCollection form)
        {
            try
            {
                dONDATHANG.IdAccount = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.IdAccount).FirstOrDefault();
                dONDATHANG.GhiChu = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.GhiChu).FirstOrDefault();
                db.Entry(dONDATHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.IdAccount = new SelectList(db.ACCOUNTs, "IdAccount", "UserName", dONDATHANG.IdAccount);
                ViewBag.MaTT = new SelectList(db.TINHTRANGs, "MaTT", "TinhTrangGiao", dONDATHANG.MaTT);
                return View(dONDATHANG);
            }
        }

        // GET: Admin/DonDatHang/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG dONDATHANG = await db.DONDATHANGs.FindAsync(id);
            if (dONDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONDATHANG);
        }

        // POST: Admin/DonDatHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DONDATHANG dONDATHANG = await db.DONDATHANGs.FindAsync(id);
            db.DONDATHANGs.Remove(dONDATHANG);
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
