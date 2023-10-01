using PagedList;
using ShoesShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShoesShop.Controllers
{
    public class SanPhamController : Controller
    {
        private DBContextModel db = new DBContextModel();
        // GET: SanPham

        public ActionResult Product(int? id, int? page, string sort = "")
        {
            if (page == null) page = 1;

            if (id == null)
            {
                var all_product = db.SANPHAMs.Where(m => m.SoLuongTon > 0).OrderBy(m => m.MaSP);
                var count = all_product.Count();
                ViewBag.count = count;
                int pageSize = 20;
                int pageNum = page ?? 1;

                switch (sort)
                {
                    case "popular":
                        all_product = all_product.OrderByDescending(x => x.SoLuongMua);
                        break;
                    case "name":
                        all_product = all_product.OrderBy(x => x.TenSanPham);
                        break;
                    case "price":
                        all_product = all_product.OrderBy(x => x.GiaBan);
                        break;
                    default:
                        all_product = all_product.OrderByDescending(x => x.Moi);
                        break;
                }

                return View(all_product.ToPagedList(pageNum, pageSize));
            }
            else
            {
                var all_product = db.SANPHAMs.Where(n => n.MaHang == id && n.SoLuongTon > 0).OrderBy(m => m.MaSP);
                var TenHang = db.HANGGIAYs.SingleOrDefault(m => m.MaHang == id);
                var count = all_product.Count();
                ViewBag.tenhang = TenHang.TenHang;
                ViewBag.count = count;
                int pageSize = 20;
                int pageNum = page ?? 1;
                switch (sort)
                {
                    case "popular":
                        all_product = all_product.OrderByDescending(x => x.SoLuongMua);
                        break;
                    case "name":
                        all_product = all_product.OrderBy(x => x.TenSanPham);
                        break;
                    case "price":
                        all_product = all_product.OrderBy(x => x.GiaBan);
                        break;
                    default:
                        all_product = all_product.OrderByDescending(x => x.Moi);
                        break;
                }
                return View(all_product.ToPagedList(pageNum, pageSize));
            }

        }
        public ActionResult SoldOut()
        {
            var soldout = db.SANPHAMs.Where(m => m.SoLuongTon <= 0).OrderByDescending(m => m.SoLuongMua);
            return View(soldout);
        }

        public ActionResult New_Product(int? page)
        {
            if (page == null) page = 1;
            var new_product = db.SANPHAMs.Where(n => n.Moi == true && n.SoLuongTon > 0).OrderBy(m => m.MaSP);
            var count = new_product.Count();
            ViewBag.count = count;
            int pageSize = 20;
            int pageNum = page ?? 1;
            return View(new_product.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Details(int id)
        {
            var D_sp = db.SANPHAMs.Where(n => n.MaSP == id).FirstOrDefault();
            return View(D_sp);
        }

        public ActionResult Sale_Product(int? id, int? page)
        {
            if (page == null) page = 1;
            if (id == null)
            {
                var sale_sanpham = db.CHITIETKHUYENMAIs.Where(n => n.KHUYENMAI.NgayBatDau <= DateTime.Now && n.KHUYENMAI.NgayKetThuc >= DateTime.Now && n.SANPHAM.SoLuongTon > 0).OrderBy(m => m.KHUYENMAI.TenKhuyenMai);
                var count = sale_sanpham.Count();
                ViewBag.count = count;
                int pageSize = 20;
                int pageNum = page ?? 1;
                return View(sale_sanpham.ToPagedList(pageNum, pageSize));
            }
            else
            {
                var sale_sanpham = db.CHITIETKHUYENMAIs.Where(n => n.MaKhuyenMai == id && n.KHUYENMAI.NgayBatDau <= DateTime.Now && n.KHUYENMAI.NgayKetThuc >= DateTime.Now && n.SANPHAM.SoLuongTon > 0).OrderBy(m => m.MaSP);
                var TenSale = db.KHUYENMAIs.SingleOrDefault(n => n.MaKhuyenMai == id);
                ViewBag.TenSale = TenSale.TenKhuyenMai;
                var count = sale_sanpham.Count();
                ViewBag.count = count;
                int pageSize = 20;
                int pageNum = page ?? 1;
                return View(sale_sanpham.ToPagedList(pageNum, pageSize));
            }
        }

        public ActionResult SizeGiay(int id)
        {
            var sp = db.BANGSIZEs.Where(n => n.MaSP == id).First();
            return View(sp);
        }

        public ActionResult HangDetail(int id)
        {
            var sp = db.HANGGIAYs.Where(n => n.MaHang == id).First();
            return View(sp);
        }
    }
}