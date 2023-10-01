using ShoesShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShoesShop.Controllers
{
    public class GioHangController : Controller
    {
        private DBContextModel db = new DBContextModel();
        // GET: GioHang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int id, string strURL, FormCollection formCollection)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp này đã tồn tại trong session[giohang] chưa
            GioHang sanpham = lstGioHang.Find(n => n.masp == id);
            var x = formCollection["button2"];
            if (formCollection["button2"] == null || formCollection["button2"] == "")
            {
                ViewData["checksize"] = "Chưa chọn size";
            }
            else
            {
                if (sanpham == null)
                {
                    sanpham = new GioHang(id);
                    sanpham.size = x;
                    //Add sản phẩm mới thêm vào list
                    lstGioHang.Add(sanpham);
                }
                else
                {
                    if (sanpham.size == x)
                    {
                        sanpham.iSoLuong++;
                    }
                    else
                    {
                        sanpham = new GioHang(id);
                        sanpham.size = x;
                        //Add sản phẩm mới thêm vào list
                        lstGioHang.Add(sanpham);
                    }
                }
            }
            return Redirect(strURL);
        }
        //Cập nhật giỏ hàng 
        public ActionResult CapNhatGioHang(int id, string size, FormCollection f)
        {
            //Kiểm tra masp
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var x = f["size"];
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sp có tồn tại trong session["GioHang"]
            GioHang sanpham = lstGioHang.Where(n=>n.size == x).SingleOrDefault(n=>n.masp ==id);
            BANGSIZE bANGSIZE = db.BANGSIZEs.FirstOrDefault(X => X.MaSP == sanpham.masp);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null && (bANGSIZE.s38>= int.Parse(f["txtSoLg"]) || bANGSIZE.s39 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s39 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s40 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s41 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s42 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s42_5 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s43 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s44 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s45 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s46 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s47 >= int.Parse(f["txtSoLg"]) || bANGSIZE.s48 >= int.Parse(f["txtSoLg"])))
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLg"].ToString());

            }
            else
            {
                if(bANGSIZE.s38 < int.Parse(f["txtSoLg"]) || bANGSIZE.s39 < int.Parse(f["txtSoLg"]) || bANGSIZE.s39 < int.Parse(f["txtSoLg"]) || bANGSIZE.s40 < int.Parse(f["txtSoLg"]) || bANGSIZE.s41 < int.Parse(f["txtSoLg"]) || bANGSIZE.s42 < int.Parse(f["txtSoLg"]) || bANGSIZE.s42_5 < int.Parse(f["txtSoLg"]) || bANGSIZE.s43 < int.Parse(f["txtSoLg"]) || bANGSIZE.s44 < int.Parse(f["txtSoLg"]) || bANGSIZE.s45 < int.Parse(f["txtSoLg"]) || bANGSIZE.s46 < int.Parse(f["txtSoLg"]) || bANGSIZE.s47 < int.Parse(f["txtSoLg"]) || bANGSIZE.s48 < int.Parse(f["txtSoLg"]))
                {
                    ViewData["SoLuongTon"] = "Số lượng không đủ";
                }
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang(int id, string size)
        {
            //Kiểm tra masp
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            //Nếu get sai masp thì sẽ trả về trang lỗi 404
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var x = size;
            //Lấy giỏ hàng ra từ session
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.Where(n => n.size == x).FirstOrDefault(n => n.masp == id);
            //Nếu mà tồn tại thì chúng ta cho sửa số lượng
            if (sanpham != null)
            {
                lstGioHang.Remove(sanpham);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }
        //Xóa tất cả giỏ hàng
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> gioHangs = LayGioHang();
            gioHangs.Clear();
            return RedirectToAction("GioHang");
        }
        //Xây dựng trang giỏ hàng
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGioHang);
        }
        //Tính tổng số lượng và tổng tiền
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tổng số lượng sản phẩm
        private int TongSoLuongSanPham()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Count();
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private decimal TongTien()
        {
            decimal dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        //tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }
        //Xây dựng 1 view cho người dùng chỉnh sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }

        public ActionResult DatHangPartial()
        {
            DONDATHANG dhh = new DONDATHANG();
            return View(dhh);
        }


        [HttpGet]
        public ActionResult DatHang()
        {

            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGiohang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            if (Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            DONDATHANG dh = new DONDATHANG();
            ACCOUNT kh = (ACCOUNT)Session["user"];
            SANPHAM s = new SANPHAM();
            List<GioHang> gh = LayGioHang();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            var hoten = collection["Hoten"];
            var email = collection["Email"];
            var diachi = collection["DiaChi"];
            var ghichu = collection["GhiChu"];
            var sdt = collection["SDT"];
            if (hoten == null || hoten == "")
            {
                ViewData["hoten"] = "Chọn ngày giao";
            }
            if (diachi == null || diachi == "")
            {
                ViewData["diachi"] = "Chọn ngày giao";
            }
            if (ngaygiao == null || ngaygiao == "")
            {
                ViewData["ngaygiao"] = "Chọn ngày giao";
            }
            int result = DateTime.Compare(DateTime.Parse(ngaygiao), DateTime.Now);
            if (Session["user"] != null)
            {
                dh.IdAccount = kh.IdAccount;
            }
            dh.NgayDat = DateTime.Now;
            dh.NgayGiao = DateTime.Parse(ngaygiao);
            dh.MaTT = 1;
            dh.ThanhToan = false;
            dh.Email = email;
            dh.GhiChu = ghichu;
            dh.SDT = sdt;
            dh.Hoten = hoten;
            dh.DiaChi = diachi;
            db.DONDATHANGs.Add(dh);
            db.SaveChanges();
            foreach (var item in gh)
            {
                CHITIETDONHANG ctdh = new CHITIETDONHANG();
                ctdh.MaDDH = dh.MaDDH;
                ctdh.MaSP = item.masp;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.Size = item.size;
                ctdh.DonGia = item.giaban;
                ctdh.TongTien = item.dThanhTien;
                s = db.SANPHAMs.Single(n => n.MaSP == item.masp);
                s.SoLuongTon -= ctdh.SoLuong;
                db.SaveChanges();
                db.CHITIETDONHANGs.Add(ctdh);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
        
        public ActionResult XemDonHang()
        {
            ACCOUNT kh = (ACCOUNT)Session["user"];
            var dONDATHANGs = db.DONDATHANGs.Where(n=>n.IdAccount == kh.IdAccount ).ToList();
            return View( dONDATHANGs);
        }
        public ActionResult XemChiTietDonHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONDATHANG dONDATHANG = db.DONDATHANGs.Find(id);
            var chitiet = db.CHITIETDONHANGs.Where(d => d.MaDDH == id).ToList();
            if (dONDATHANG == null)
            {
                return HttpNotFound();
            }
            return View(chitiet);
        }

        public ActionResult HuyDonHang(int id)
        {
            DONDATHANG dONDATHANG = db.DONDATHANGs.Where(x => x.MaDDH == id).FirstOrDefault();
            try
            {
                if(dONDATHANG.MaTT == 5)
                {
                    ViewData["ThongBaoHuy"] = "Đơn hàng đã hủy rồi";
                }
                dONDATHANG.IdAccount = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.IdAccount).FirstOrDefault();
                dONDATHANG.GhiChu = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.GhiChu).FirstOrDefault();
                dONDATHANG.Hoten = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.Hoten).FirstOrDefault(); ;
                dONDATHANG.MaTT = 5;
                dONDATHANG.NgayDat = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.NgayDat).FirstOrDefault();
                dONDATHANG.NgayGiao = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.NgayGiao).FirstOrDefault();
                dONDATHANG.ThanhToan = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.ThanhToan).FirstOrDefault();
                dONDATHANG.TongTien = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.TongTien).FirstOrDefault();
                dONDATHANG.SDT = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.SDT).FirstOrDefault();
                dONDATHANG.Email = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.Email).FirstOrDefault();
                dONDATHANG.DiaChi = db.DONDATHANGs.Where(x => x.MaDDH == dONDATHANG.MaDDH).Select(s => s.DiaChi).FirstOrDefault();
                db.Entry(dONDATHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("XemDonHang");
            }
            catch
            {
                return RedirectToAction("XemDonHang");
            }
        }
    }
}