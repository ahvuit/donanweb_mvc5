using PagedList;
using ShoesShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesShop.Controllers
{
    public class TimKiemController : Controller
    {
        private DBContextModel db = new DBContextModel();
        [HttpGet]
        public ActionResult KQTimKiem(string sTuKhoa, int? page)
        {
            //Tim kiem theo ten sp
            if (page == null) page = 1;
            var lstSP = db.SANPHAMs.Where(n => n.TenSanPham.Contains(sTuKhoa));
            ViewBag.tukhoa = sTuKhoa;
            var count = lstSP.Count();
            ViewBag.count = count;
            int pageSize = 16;
            int pageNum = page ?? 1;
            return View(lstSP.OrderBy(m => m.TenSanPham).ToPagedList(pageNum, pageSize));

        }
        [HttpPost]
        public ActionResult LayTuKhoaTimKiem(string sTuKhoa)
        {
            //gọi về hàm get tìm kiếm

            return RedirectToAction("KQTimKiem", new { @sTuKhoa = sTuKhoa });

        }
    }
}