using PagedList;
using ShoesShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShoesShop.Controllers
{
    public class HomeController : Controller
    {
        private DBContextModel db = new DBContextModel();
        public bool x;
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var all_product = db.SANPHAMs.Where(m => m.SoLuongTon > 0).OrderBy(m => m.MaSP);
            int pageSize = 8;
            int pageNum = page ?? 1;
            return View(all_product.ToPagedList(pageNum, pageSize));
        }

        public ActionResult New(int? page)
        {
            if (page == null) page = 1;
            var all_product = db.SANPHAMs.Where(n => n.Moi == true && n.SoLuongTon > 0).OrderBy(m => m.MaSP);
            int pageSize = 8;
            int pageNum = page ?? 1;
            return View(all_product.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Sale(int? page)
        {
            if (page == null) page = 1;
            var all_product = db.CHITIETKHUYENMAIs.Where(n => n.KHUYENMAI.NgayBatDau <= DateTime.Now && n.KHUYENMAI.NgayKetThuc >= DateTime.Now && n.SANPHAM.SoLuongTon > 0).OrderBy(m => m.KHUYENMAI.TenKhuyenMai);
            int pageSize = 8;
            int pageNum = page ?? 1;
            return View(all_product.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult MenuPartial()
        {
            // truy vấn list sản phẩm
            var lstSanPham = db.SANPHAMs;
            return PartialView(lstSanPham);
        }
        public ActionResult GetTenKhuyenMai()
        {
            // truy vấn list sản phẩm
            var lstKM = db.KHUYENMAIs;
            return PartialView(lstKM);
        }
        public ActionResult GetTenHang()
        {
            // truy vấn list sản phẩm
            var lstTenHang = db.HANGGIAYs;
            return PartialView(lstTenHang);
        }
        public ActionResult DangKy()
        {

            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy(FormCollection collection, ACCOUNT a)
        {
            if (ModelState.IsValid)
            {
                var check = db.ACCOUNTs.FirstOrDefault(s => s.UserName == a.UserName);
                int maquyen = db.QUYENs.FirstOrDefault(s => s.TenQuyen == "Member").MaQuyen;
                if (check == null)
                {
                    var UserName = collection["UserName"];
                    var Password = collection["Password"];
                    var RePassword = collection["RePassword"];

                    if (RePassword == null || RePassword == "")
                    {
                        ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận";

                    }
                    else
                    {
                        if (!Password.Equals(RePassword))
                        {
                            ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";

                        }
                        else
                        {
                            a.UserName = UserName;
                            a.Password = Password;
                            a.MaQuyen = maquyen;
                            a.Password = GetMD5(a.Password);
                            db.ACCOUNTs.Add(a);
                            db.SaveChanges();
                            return RedirectToAction("DangNhap");
                        }
                    }
                }
                else
                {
                    ViewData["TenNguoiDungDaTonTai"] = "Tên người dùng đã tồn tại";
                    return View();
                }
            }
            return this.View();
        }
        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangNhap(FormCollection userlog)
        {
            string username = userlog["UserName"].ToString();
            string password = userlog["Password"].ToString();
            var f_password = GetMD5(password);
            var islogin = db.ACCOUNTs.FirstOrDefault(x => x.UserName.Equals(username) && x.Password.Equals(f_password));

            if (islogin != null)
            {
                var getNV = db.NHANVIENs.FirstOrDefault(n => n.IdAccount == islogin.IdAccount);

                Session["user"] = islogin;
                if (islogin.QUYEN.TenQuyen == "Admin " /*|| islogin.MaQuyen == 2*/)
                {

                    Session["Admin"] = islogin;
                    Session["Staff"] = islogin;
                    Session["NV"] = getNV;
                    return RedirectToAction("Index", "Home");
                }
                else
                if (islogin.QUYEN.TenQuyen == "Staff ")
                {
                    Session["Staff"] = islogin;
                    Session["NV"] = getNV;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var getKH = db.DONDATHANGs.FirstOrDefault(n => n.IdAccount == islogin.IdAccount);
                    Session["KH"] = getKH;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                if (username == null || username == "")
                {
                    ViewData["ChuaNhapUserName"] = "Tên đăng nhập không được bỏ trống";

                }
                else
                if (password == null || password == "")
                {
                    ViewData["ChuaNhapPassword"] = "Mật khẩu không được để trống";

                }
                else
                {
                    ViewData["ER"] = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }

            return View();
        }
        public ActionResult DoiMatKhau(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aCCOUNT = db.ACCOUNTs.First(m => m.IdAccount == id);
            if (aCCOUNT == null)
            {
                return HttpNotFound();
            }

            return View(aCCOUNT);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var E_Account = db.ACCOUNTs.First(m => m.IdAccount == id);

                if (E_Account != null)
                {
                    var OldPassword = collection["OldPassword"];
                    var NewPassword = collection["NewPassword"];
                    var ReNewPassword = collection["ReNewPassword"];
                    var f_Oldpassword = GetMD5(OldPassword);
                    E_Account.IdAccount = id;
                    if (OldPassword == null || OldPassword == "")
                    {
                        ViewData["NhapMKC"] = "Phải nhập mật khẩu cũ ";
                    }
                    else
                    if (!f_Oldpassword.Equals(E_Account.Password))
                    {
                        ViewData["NhapMKCSai"] = "Mật khẩu cũ không đúng";
                    }
                    else
                    if (NewPassword == null || NewPassword == "")
                    {
                        ViewData["NhapMKM"] = "Phải nhập mật khẩu mới ";
                    }
                    else
                    if (ReNewPassword == null || ReNewPassword == "")
                    {
                        ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận ";
                    }
                    else
                    {
                        if (!NewPassword.Equals(ReNewPassword))
                        {
                            ViewData["MatKhauGiongNhau"] = "Mật khẩu mới và mật khẩu xác nhận phải giống nhau";
                        }
                        else
                        {

                            E_Account.Password = NewPassword;
                            E_Account.Password = GetMD5(E_Account.Password);
                            UpdateModel(E_Account);
                            db.ACCOUNTs.AddOrUpdate(E_Account);
                            db.SaveChanges();
                            Logout();
                            return RedirectToAction("DangNhap");
                        }
                    }
                }
                else
                {
                    ViewData["TenNguoiDungDaTonTai"] = "Có lỗi xảy ra khi thực hiện thao tác";
                    return View("DoiMatKhau");
                }
            }
            return this.DoiMatKhau(id);
        }

        //Logout
        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["Admin"] = null;
            Session["NV"] = null;
            Session["KH"] = null;
            Session["Staff"] = null;
            Session["GioHang"] = null;
            return RedirectToAction("DangNhap");
        }
    }
}