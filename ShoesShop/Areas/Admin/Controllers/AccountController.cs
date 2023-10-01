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
using System.Security.Cryptography;
using System.Text;
using System.Net.Http;

namespace ShoesShop.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private DBContextModel db = new DBContextModel();

        // GET: Admin/Account
        public ActionResult Index()
        {
            BlockStaff();
            IEnumerable<ACCOUNT> listAccount;
            HttpResponseMessage responseMessage = GlobalVariables.WebApiClient.GetAsync("Account").Result;
            listAccount = responseMessage.Content.ReadAsAsync<IEnumerable<ACCOUNT>>().Result;
            return View(listAccount);
        }

        // GET: Admin/Account/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT aCCOUNT = await db.ACCOUNTs.FindAsync(id);
            if (aCCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNT);
        }

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

        // GET: Admin/Account/Create
        public ActionResult Create()
        {
            BlockStaff();
            ViewBag.MaQuyen = new SelectList(db.QUYENs, "MaQuyen", "TenQuyen");
            ViewBag.IdAccount = new SelectList(db.CHUCNANGs, "IdAccount", "IdAccount");
            return View();
        }

        // POST: Admin/Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAccount,UserName,Password,MaQuyen")] FormCollection collection, ACCOUNT a)
        {
            if (ModelState.IsValid)
            {
                var check = db.ACCOUNTs.FirstOrDefault(s => s.UserName == a.UserName);
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
                            a.Password = GetMD5(a.Password);
                            db.ACCOUNTs.Add(a);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    ViewData["TenNguoiDungDaTonTai"] = "Tên người dùng đã tồn tại";
                    ViewBag.MaQuyen = new SelectList(db.QUYENs, "MaQuyen", "TenQuyen");
                    ViewBag.IdAccount = new SelectList(db.CHUCNANGs, "IdAccount", "IdAccount");
                    return View();
                }
            }
            return this.View();
        }

        // GET: Admin/Account/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT aCCOUNT = await db.ACCOUNTs.FindAsync(id);
            if (aCCOUNT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaQuyen = new SelectList(db.QUYENs, "MaQuyen", "TenQuyen", aCCOUNT.MaQuyen);
            ViewBag.IdAccount = new SelectList(db.CHUCNANGs, "IdAccount", "IdAccount", aCCOUNT.IdAccount);
            return View(aCCOUNT);
        }

        // POST: Admin/Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdAccount,UserName,Password,MaQuyen")] ACCOUNT aCCOUNT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aCCOUNT).State = EntityState.Modified;
                aCCOUNT.Password = GetMD5(aCCOUNT.Password);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MaQuyen = new SelectList(db.QUYENs, "MaQuyen", "TenQuyen", aCCOUNT.MaQuyen);
            ViewBag.IdAccount = new SelectList(db.CHUCNANGs, "IdAccount", "IdAccount", aCCOUNT.IdAccount);
            return View(aCCOUNT);
        }

        // GET: Admin/Account/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            BlockStaff();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT aCCOUNT = await db.ACCOUNTs.FindAsync(id);
            if (aCCOUNT == null)
            {
                return HttpNotFound();
            }
            return View(aCCOUNT);
        }

        // POST: Admin/Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ACCOUNT aCCOUNT = await db.ACCOUNTs.FindAsync(id);
            db.ACCOUNTs.Remove(aCCOUNT);
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
