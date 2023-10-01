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
using PagedList;

namespace ShoesShop.Areas.Admin.Controllers
{
    public class SanPhamController : BaseController
    {
        private DBContextModel db = new DBContextModel();

        // GET: Admin/SanPham
        /*public async Task<ActionResult> Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.DANHMUC).Include(s => s.HANGGIAY);
            return View(await sANPHAMs.ToListAsync());
        }*/

        public ActionResult Index(int? page)
        {
            // 1. Tham số int? dùng để thể hiện null và kiểu int( số nguyên)
            // page có thể có giá trị là null ( rỗng) và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn sql, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo Masp mới có thể phân trang.
            var sp = db.SANPHAMs.OrderBy(x => x.MaSP);

            // 4. Tạo kích thước trang (pageSize) hay là số sản phẩm hiển thị trên 1 trang
            int pageSize = 5;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các sản phẩm được phân trang theo kích thước và số trang.
            return View(sp.ToPagedList(pageNumber, pageSize));

        }

        // GET: Admin/SanPham/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = await db.SANPHAMs.FindAsync(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }
        public void getImage(SANPHAM sp)
        {
            string filename = Path.GetFileNameWithoutExtension(sp.HinhAnhFile.FileName);
            string extension = Path.GetExtension(sp.HinhAnhFile.FileName);
            string filienamepath = filename + DateTime.Now.ToString("yymmssfff") + extension;
            sp.HinhAnh = filienamepath;
            filienamepath = Path.Combine(Server.MapPath("~/Images/"), filienamepath);
            sp.HinhAnhFile.SaveAs(filienamepath);
        }

        // GET: Admin/SanPham/Create
        [HttpGet]
        public ActionResult Create()
        {
            SANPHAM sANPHAM = new SANPHAM();
            ViewBag.MaDanhMuc = new SelectList(db.DANHMUCs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaHang = new SelectList(db.HANGGIAYs, "MaHang", "TenHang");
            return View(sANPHAM);
        }

        // POST: Admin/SanPham/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SANPHAM sp)
        {
            try
            {
                var n = (NHANVIEN)Session["NV"];
                if (sp.HinhAnhFile != null)
                {
                    getImage(sp);
                }
                sp.SoLuongMua = 0;
                sp.SoLuongTon = 0;
                sp.NgayCapNhat = DateTime.Now;
                sp.UpdateBy = n.TenNhanVien;
                db.SANPHAMs.Add(sp);
                db.SaveChanges();
                return RedirectToAction("Index","SanPham");

            }
            catch(Exception)
            {
                ViewBag.MaDanhMuc = new SelectList(db.DANHMUCs, "MaDanhMuc", "TenDanhMuc", sp.MaDanhMuc);
                ViewBag.MaHang = new SelectList(db.HANGGIAYs, "MaHang", "TenHang", sp.MaHang);
                return View();
            }
        }

        // GET: Admin/SanPham/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = await db.SANPHAMs.FindAsync(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DANHMUCs, "MaDanhMuc", "TenDanhMuc", sANPHAM.MaDanhMuc);
            ViewBag.MaHang = new SelectList(db.HANGGIAYs, "MaHang", "TenHang", sANPHAM.MaHang);
            return View(sANPHAM);
        }

        // POST: Admin/SanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SANPHAM sp)
        {
            try
            {
                var n = (NHANVIEN)Session["NV"];
                if (sp.HinhAnhFile != null)
                {
                    getImage(sp);
                }
                sp.SoLuongTon = db.SANPHAMs.Where(x => x.MaSP == sp.MaSP).Select(s => s.SoLuongTon).FirstOrDefault();
                sp.SoLuongMua = db.SANPHAMs.Where(x => x.MaSP == sp.MaSP).Select(s => s.SoLuongMua).FirstOrDefault();
                sp.NgayCapNhat = DateTime.Now;
                sp.UpdateBy = n.TenNhanVien;
                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "SanPham");

            }
            catch (Exception)
            {
                ViewBag.MaDanhMuc = new SelectList(db.DANHMUCs, "MaDanhMuc", "TenDanhMuc", sp.MaDanhMuc);
                ViewBag.MaHang = new SelectList(db.HANGGIAYs, "MaHang", "TenHang", sp.MaHang);
                return View();
            }
        }

        // GET: Admin/SanPham/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = await db.SANPHAMs.FindAsync(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = await db.SANPHAMs.FindAsync(id);
            db.SANPHAMs.Remove(sANPHAM);
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
