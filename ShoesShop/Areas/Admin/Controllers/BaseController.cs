using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoesShop.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        // GET: Admin/Base
        public BaseController()
        {
            if (System.Web.HttpContext.Current.Session["Staff"] == null || System.Web.HttpContext.Current.Session["Staff"].Equals("") || System.Web.HttpContext.Current.Session["user"] == null || System.Web.HttpContext.Current.Session["user"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Home/DangNhap");
            }

        }
        public void BlockStaff()
        {
            if (System.Web.HttpContext.Current.Session["Admin"] == null || System.Web.HttpContext.Current.Session["Admin"].Equals("") || System.Web.HttpContext.Current.Session["user"] == null || System.Web.HttpContext.Current.Session["user"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/DonDatHang");
            }
        }
    }
}