using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoesShop.Models
{
    public class GioHang
    {
        DBContextModel db = new DBContextModel();
        public int masp { get; set; }
        [Display(Name = "Tên sản phẩm")]
        public string tensp { get; set; }
        [Display(Name = "Ảnh")]
        public string hinh { get; set; }
        [Display(Name = "Giá bán")]
        public decimal giaban { get; set; }
        [Display(Name = "Size")]
        public string size { get; set; }
        [Display(Name = "Số lượng")]
        public int iSoLuong { get; set; }
        [Display(Name = "Thành tiền")]
        public decimal dThanhTien
        {
            get { return iSoLuong * giaban; }
        }
        //Hàm tạo cho giỏ hàng
        public GioHang(int Masp)
        {
            masp = Masp;
            SANPHAM sp = db.SANPHAMs.Single(n => n.MaSP == masp);
            List<CHITIETKHUYENMAI> cs = db.CHITIETKHUYENMAIs.ToList();
            tensp = sp.TenSanPham;
            hinh = sp.HinhAnh;
            size = sp.size; ;
            foreach(var x in cs)
            {
                if (x.MaSP == masp)
                {
                    giaban = x.GiaKM;
                    break;
                }
                else
                {
                    giaban = sp.GiaBan;
                }
            }
            iSoLuong = 1;
        }
    }
}